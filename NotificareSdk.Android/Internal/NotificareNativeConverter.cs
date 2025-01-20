using Android.Runtime;
using NotificareSdk.Core.Models;

namespace NotificareSdk.Android.Internal;

public static class NotificareNativeConverter
{
    #region Decoding from native

    /// <summary>
    /// Create a <see cref="NotificareApplication"/> from the binding object.
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static NotificareApplication FromNativeApplication(
        Binding.Models.NotificareApplication application)
    {
        var inboxConfig = application.GetInboxConfig();
        var regionConfig = application.GetRegionConfig();

        return new NotificareApplication(
            id: application.Id,
            name: application.Name,
            category: application.Category,
            appStoreId: null,
            services: application.Services.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.BooleanValue()),
            inboxConfig: inboxConfig != null ? FromNativeApplicationInboxConfig(inboxConfig) : null,
            regionConfig: regionConfig != null ? FromNativeApplicationRegionConfig(regionConfig) : null,
            userDataFields: application.UserDataFields.Select(FromNativeApplicationUserDataField).ToList(),
            actionCategories: application.ActionCategories.Select(FromNativeApplicationActionCategory).ToList()
        );
    }

    private static NotificareApplicationInboxConfig FromNativeApplicationInboxConfig(
        Binding.Models.NotificareApplication.InboxConfig inboxConfig)
    {
        return new NotificareApplicationInboxConfig(
            useInbox: inboxConfig.UseInbox,
            useUserInbox: inboxConfig.UseUserInbox,
            autoBadge: inboxConfig.AutoBadge
        );
    }

    private static NotificareApplicationRegionConfig FromNativeApplicationRegionConfig(
        Binding.Models.NotificareApplication.RegionConfig regionConfig)
    {
        return new NotificareApplicationRegionConfig(
            proximityUUID: regionConfig.ProximityUUID
        );
    }

    private static NotificareApplicationUserDataField FromNativeApplicationUserDataField(
        Binding.Models.NotificareApplication.UserDataField userDataField)
    {
        return new NotificareApplicationUserDataField(
            type: userDataField.Type,
            key: userDataField.Key,
            label: userDataField.Label
        );
    }

    private static NotificareApplicationActionCategory FromNativeApplicationActionCategory(
        Binding.Models.NotificareApplication.ActionCategory actionCategory)
    {
        return new NotificareApplicationActionCategory(
            name: actionCategory.Name,
            description: actionCategory.Description,
            type: actionCategory.Type,
            actions: actionCategory.Actions.Select(FromNativeNotificationAction).ToList()
        );
    }

    /// <summary>
    /// Create a <see cref="NotificareNotification"/> from the binding object.
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    public static NotificareNotification FromNativeNotification(
        Binding.Models.NotificareNotification notification)
    {
        return new NotificareNotification(
            partial: notification.Partial,
            id: notification.Id,
            type: notification.Type,
            time: new DateTime(notification.Time.Time), // TODO: fix decoding
            title: notification.Title,
            subtitle: notification.Subtitle,
            message: notification.Message,
            content: notification.GetContent().Select(FromNativeNotificationContent).ToList(),
            actions: notification.Actions.Select(FromNativeNotificationAction).ToList(),
            attachments: notification.Attachments.Select(FromNativeNotificationAttachment).ToList(),
            extra: FromNativeExtraDictionary(notification.Extra),
            targetContentIdentifier: null
        );
    }

    private static NotificareNotificationContent FromNativeNotificationContent(
        Binding.Models.NotificareNotification.Content content)
    {
        return new NotificareNotificationContent(
            type: content.Type,
            data: content.Data switch
            {
                Java.Lang.String s => s.ToString(),
                Java.Util.IMap m => new JavaDictionary<Java.Lang.String, Java.Lang.Object>(m.Handle,
                    JniHandleOwnership.DoNotRegister).ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => FromNativeExtraPrimitive(kvp.Value)
                ),
                _ => new Dictionary<string, object>()
            }
        );
    }

    public static NotificareNotificationAction FromNativeNotificationAction(
        Binding.Models.NotificareNotification.Action action)
    {
        var icon = action.GetIcon();

        return new NotificareNotificationAction(
            type: action.Type,
            label: action.Label,
            target: action.Target,
            keyboard: action.Keyboard,
            camera: action.Camera,
            destructive: action.Destructive?.BooleanValue(),
            icon: icon != null ? FromNativeNotificationActionIcon(icon) : null
        );
    }

    private static NotificareNotificationActionIcon FromNativeNotificationActionIcon(
        Binding.Models.NotificareNotification.Action.Icon icon)
    {
        return new NotificareNotificationActionIcon(
            android: icon.Android,
            ios: icon.Ios,
            web: icon.Web
        );
    }

    private static NotificareNotificationAttachment FromNativeNotificationAttachment(
        Binding.Models.NotificareNotification.Attachment attachment)
    {
        return new NotificareNotificationAttachment(
            mimeType: attachment.MimeType,
            uri: attachment.Uri
        );
    }

    /// <summary>
    /// Create a <see cref="NotificareDynamicLink"/> from the binding object.
    /// </summary>
    /// <param name="dynamicLink"></param>
    /// <returns></returns>
    internal static NotificareDynamicLink FromNativeDynamicLink(
        Binding.Models.NotificareDynamicLink dynamicLink)
    {
        return new NotificareDynamicLink(
            target: dynamicLink.Target
        );
    }

    /// <summary>
    /// Create a <see cref="NotificareDevice"/> from the binding object.
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    internal static NotificareDevice FromNativeDevice(Binding.Models.NotificareDevice device)
    {
        return new NotificareDevice(
            id: device.Id,
            userId: device.UserId,
            userName: device.UserName,
            timeZoneOffset: device.TimeZoneOffset,
            dnd: device.Dnd == null ? null : FromNativeDoNotDisturb(device.Dnd),
            userData: device.UserData
        );
    }

    /// <summary>
    /// Create a <see cref="NotificareDoNotDisturb"/> from the binding object.
    /// </summary>
    /// <param name="dnd"></param>
    /// <returns></returns>
    internal static NotificareDoNotDisturb FromNativeDoNotDisturb(Binding.Models.NotificareDoNotDisturb dnd)
    {
        return new NotificareDoNotDisturb(
            start: new NotificareTime(
                hours: dnd.Start.Hours,
                minutes: dnd.Start.Minutes
            ),
            end: new NotificareTime(
                hours: dnd.End.Hours,
                minutes: dnd.End.Minutes
            )
        );
    }

    /// <summary>
    /// Create an "extra" dictionary from the binding representation.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    internal static IDictionary<string, object> FromNativeExtraDictionary(IDictionary<string, Java.Lang.Object> data)
    {
        return data.ToDictionary(
            kvp => kvp.Key,
            kvp => FromNativeExtraPrimitive(kvp.Value)
        );
    }

    private static object FromNativeExtraPrimitive(Java.Lang.Object value)
    {
        return value switch
        {
            Java.Lang.String s => s.ToString(),
            Java.Lang.Integer i => i.IntValue(),
            Java.Lang.Long l => l.LongValue(),
            Java.Lang.Double d => d.DoubleValue(),
            Java.Lang.Float f => f.FloatValue(),
            Java.Lang.Boolean b => b.BooleanValue(),
            JavaList l => l.Cast<object>()
                .Select(item => item is Java.Lang.Object o ? FromNativeExtraPrimitive(o) : item)
                .ToList(),
            JavaDictionary d => new JavaDictionary<string, Java.Lang.Object>(d.Handle, JniHandleOwnership.DoNotRegister)
                .ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => FromNativeExtraPrimitive(kvp.Value)
                ),
            Java.Util.IMap m => new JavaDictionary<string, Java.Lang.Object>(m.Handle, JniHandleOwnership.DoNotRegister)
                .ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => FromNativeExtraPrimitive(kvp.Value)
                ),
            _ => throw new ArgumentException($"Type '{value.GetType().Name}' cannot be decoded.")
        };
    }

    #endregion

    #region Enconding to native

    public static Binding.Models.NotificareNotification ToNativeNotification(NotificareNotification notification)
    {
        return new Binding.Models.NotificareNotification(
            partial: notification.Partial,
            id: notification.Id,
            type: notification.Type,
            time: new Java.Util.Date(new DateTimeOffset(notification.Time).ToUnixTimeMilliseconds()),
            title: notification.Title,
            subtitle: notification.Subtitle,
            message: notification.Message,
            content: notification.Content.Select(ToNativeNotificationContent).ToArray(),
            actions: notification.Actions.Select(ToNativeNotificationAction).ToArray(),
            attachments: notification.Attachments.Select(ToNativeNotificationAttachment).ToArray(),
            extra: ToNativeExtraDictionary(notification.Extra)
        );
    }
    
    private static Binding.Models.NotificareNotification.Content ToNativeNotificationContent(
        NotificareNotificationContent content)
    {
        return new Binding.Models.NotificareNotification.Content(
            type: content.Type,
            data: ToNativeExtraPrimitive(content.Data)
        );
    }
    
    public static Binding.Models.NotificareNotification.Action ToNativeNotificationAction(NotificareNotificationAction action)
    {
        return new Binding.Models.NotificareNotification.Action(
            type: action.Type,
            label: action.Label,
            target: action.Target,
            keyboard: action.Keyboard,
            camera: action.Camera,
            destructive: action.Destructive == null ? null : Java.Lang.Boolean.ValueOf((bool)action.Destructive),
            icon: action.Icon == null ? null : ToNativeNotificationActionIcon(action.Icon)
        );
    }

    private static Binding.Models.NotificareNotification.Action.Icon ToNativeNotificationActionIcon(
        NotificareNotificationActionIcon icon)
    {
        return new Binding.Models.NotificareNotification.Action.Icon(
            android: icon.Android,
            ios: icon.IOS,
            web: icon.Web
        );
    }

    private static Binding.Models.NotificareNotification.Attachment ToNativeNotificationAttachment(
        NotificareNotificationAttachment attachment)
    {
        return new Binding.Models.NotificareNotification.Attachment(
            mimeType: attachment.MimeType,
            uri: attachment.Uri
        );
    }
    
    /// <summary>
    /// Create a <see cref="Binding.Models.NotificareDoNotDisturb"/> binding object from the <see cref="NotificareDoNotDisturb"/> data model.
    /// </summary>
    /// <param name="dnd"></param>
    /// <returns></returns>
    internal static Binding.Models.NotificareDoNotDisturb ToNativeDoNotDisturb(NotificareDoNotDisturb dnd)
    {
        return new Binding.Models.NotificareDoNotDisturb(
            new Binding.Models.NotificareTime(dnd.Start.Hours, dnd.Start.Minutes),
            new Binding.Models.NotificareTime(dnd.End.Hours, dnd.End.Minutes)
        );
    }

    /// <summary>
    /// Create an "extra" dictionary consumable by the binding. 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    internal static IDictionary<string, Java.Lang.Object> ToNativeExtraDictionary(IDictionary<string, object> data)
    {
        return data.ToDictionary(
            kvp => kvp.Key,
            kvp => ToNativeExtraPrimitive(kvp.Value)
        );
    }

    private static Java.Lang.Object ToNativeExtraPrimitive(object value)
    {
        return value switch
        {
            string s => new Java.Lang.String(s),
            int i => Java.Lang.Integer.ValueOf(i),
            long l => Java.Lang.Long.ValueOf(l),
            float f => Java.Lang.Float.ValueOf(f),
            double d => Java.Lang.Double.ValueOf(d),
            bool b => Java.Lang.Boolean.ValueOf(b),
            IList<object> list => new Java.Util.ArrayList(
                list.Select(ToNativeExtraPrimitive).ToList()
            ),
            IDictionary<string, object> objects => new Java.Util.HashMap(
                objects.ToDictionary(
                    kvp => kvp.Key,
                    kvp => ToNativeExtraPrimitive(kvp.Value)
                )
            ),
            _ => throw new ArgumentException($"Type '{value.GetType().Name}' cannot be represented as JSON.")
        };
    }

    #endregion
}
