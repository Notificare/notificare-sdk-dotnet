using NotificareSdk.Core.Models;

namespace NotificareSdk.iOS.Internal;

public static class NotificareNativeConverter
{
    #region Decoding from native

    /// <summary>
    /// Create a <see cref="NotificareApplication"/> from the binding object.
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    internal static NotificareApplication FromNativeApplication(Binding.NotificareApplication application)
    {
        return new NotificareApplication(
            id: application.ApplicationId,
            name: application.Name,
            category: application.Category,
            appStoreId: null,
            services: application.Services.ToDictionary<KeyValuePair<NSString, NSNumber>, string, bool>(
                item => item.Key.ToString(),
                item => item.Value.BoolValue
            ),
            inboxConfig: application.InboxConfig != null
                ? FromNativeApplicationInboxConfig(application.InboxConfig)
                : null,
            regionConfig: application.RegionConfig != null
                ? FromNativeApplicationRegionConfig(application.RegionConfig)
                : null,
            userDataFields: application.UserDataFields.Select(FromNativeApplicationUserDataField).ToList(),
            actionCategories: application.ActionCategories.Select(FromNativeApplicationActionCategory).ToList()
        );
    }

    private static NotificareApplicationInboxConfig FromNativeApplicationInboxConfig(
        Binding.NotificareApplicationInboxConfig inboxConfig)
    {
        return new NotificareApplicationInboxConfig(
            useInbox: inboxConfig.UseInbox,
            useUserInbox: inboxConfig.UseUserInbox,
            autoBadge: inboxConfig.AutoBadge
        );
    }

    private static NotificareApplicationRegionConfig FromNativeApplicationRegionConfig(
        Binding.NotificareApplicationRegionConfig regionConfig)
    {
        return new NotificareApplicationRegionConfig(
            proximityUUID: regionConfig.ProximityUUID
        );
    }

    private static NotificareApplicationUserDataField FromNativeApplicationUserDataField(
        Binding.NotificareApplicationUserDataField userDataField)
    {
        return new NotificareApplicationUserDataField(
            type: userDataField.Type,
            key: userDataField.Key,
            label: userDataField.Label
        );
    }

    private static NotificareApplicationActionCategory FromNativeApplicationActionCategory(
        Binding.NotificareApplicationActionCategory actionCategory)
    {
        return new NotificareApplicationActionCategory(
            name: actionCategory.Name,
            description: actionCategory.ActionCategoryDescription,
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
        Binding.NotificareNotification notification)
    {
        return new NotificareNotification(
            partial: notification.Partial,
            id: notification.NotificationId,
            type: notification.Type,
            time: DateTimeOffset.FromUnixTimeSeconds((long)notification.Time.SecondsSince1970).DateTime,
            title: notification.Title,
            subtitle: notification.Subtitle,
            message: notification.Message,
            content: notification.Content.Select(FromNativeNotificationContent).ToList(),
            actions: notification.Actions.Select(FromNativeNotificationAction).ToList(),
            attachments: notification.Attachments.Select(FromNativeNotificationAttachment).ToList(),
            extra: FromNativeExtraDictionary(notification.Extra),
            targetContentIdentifier: notification.TargetContentIdentifier
        );
    }

    private static NotificareNotificationContent FromNativeNotificationContent(
        Binding.NotificareNotificationContent content)
    {
        return new NotificareNotificationContent(
            type: content.Type,
            data: content.Data switch
            {
                NSString s => s.ToString(),
                NSDictionary d => FromNativeExtraDictionary(d),
                _ => new Dictionary<string, object>(),
            }
        );
    }

    public static NotificareNotificationAction FromNativeNotificationAction(
        Binding.NotificareNotificationAction action)
    {
        return new NotificareNotificationAction(
            type: action.Type,
            label: action.Label,
            target: action.Target,
            keyboard: action.Keyboard,
            camera: action.Camera,
            destructive: action.Destructive,
            icon: action.Icon != null ? FromNativeNotificationActionIcon(action.Icon) : null
        );
    }

    private static NotificareNotificationActionIcon FromNativeNotificationActionIcon(
        Binding.NotificareNotificationActionIcon icon)
    {
        return new NotificareNotificationActionIcon(
            android: icon.Android,
            ios: icon.Ios,
            web: icon.Web
        );
    }

    private static NotificareNotificationAttachment FromNativeNotificationAttachment(
        Binding.NotificareNotificationAttachment attachment)
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
    internal static NotificareDynamicLink FromNativeDynamicLink(Binding.NotificareDynamicLink dynamicLink)
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
    internal static NotificareDevice FromNativeDevice(Binding.NotificareDevice device)
    {
        return new NotificareDevice(
            id: device.DeviceId,
            userId: device.UserId,
            userName: device.UserName,
            timeZoneOffset: device.TimeZoneOffset,
            dnd: device.Dnd == null ? null : FromNativeDoNotDisturb(device.Dnd),
            userData: device.UserData.ToDictionary<KeyValuePair<NSString, NSString>, string, string>(
                item => item.Key.ToString(),
                item => item.Value.ToString()
            )
        );
    }

    /// <summary>
    /// Create a <see cref="NotificareDoNotDisturb"/> from the binding object.
    /// </summary>
    /// <param name="dnd"></param>
    /// <returns></returns>
    internal static NotificareDoNotDisturb FromNativeDoNotDisturb(Binding.NotificareDoNotDisturb dnd)
    {
        return new NotificareDoNotDisturb(
            start: new NotificareTime(
                hours: (int)dnd.Start.Hours,
                minutes: (int)dnd.Start.Minutes
            ),
            end: new NotificareTime(
                hours: (int)dnd.End.Hours,
                minutes: (int)dnd.End.Minutes
            )
        );
    }

    /// <summary>
    /// Create an "extra" dictionary from the binding representation.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static IDictionary<string, object> FromNativeExtraDictionary(NSDictionary data)
    {
        return data.ToDictionary(
            kvp => kvp.Key.ToString(),
            kvp => FromNativeExtraPrimitive(kvp.Value)
        );
    }

    private static object FromNativeExtraPrimitive(NSObject value)
    {
        return value switch
        {
            NSString s => s.ToString(),
            NSNumber { ObjCType: "i" } n => n.Int32Value,
            NSNumber { ObjCType: "q" } n => n.Int64Value,
            NSNumber { ObjCType: "d" } n => n.DoubleValue,
            NSNumber { ObjCType: "f" } n => n.FloatValue,
            NSNumber { ObjCType: "c" } n => n.BoolValue,
            NSArray a => a.Select(FromNativeExtraPrimitive).ToList(),
            NSDictionary d => FromNativeExtraDictionary(d),
            _ => throw new ArgumentException($"Type '{value.GetType().Name}' cannot be decoded.")
        };
    }

    #endregion

    #region Enconding to native

    public static Binding.NotificareNotification ToNativeNotification(NotificareNotification notification)
    {
        return new Binding.NotificareNotification(
            partial: notification.Partial,
            notificationId: notification.Id,
            type: notification.Type,
            time: NSDate.FromTimeIntervalSince1970(new DateTimeOffset(notification.Time).ToUnixTimeSeconds()),
            title: notification.Title,
            subtitle: notification.Subtitle,
            message: notification.Message,
            content: notification.Content.Select(ToNativeNotificationContent).ToArray(),
            actions: notification.Actions.Select(ToNativeNotificationAction).ToArray(),
            attachments: notification.Attachments.Select(ToNativeNotificationAttachment).ToArray(),
            extra: ToNativeExtraDictionary(notification.Extra),
            targetContentIdentifier: notification.TargetContentIdentifier
        );
    }

    private static Binding.NotificareNotificationContent ToNativeNotificationContent(
        NotificareNotificationContent content)
    {
        return new Binding.NotificareNotificationContent(
            type: content.Type,
            data: ToNativeExtraPrimitive(content.Data)
        );
    }

    public static Binding.NotificareNotificationAction ToNativeNotificationAction(NotificareNotificationAction action)
    {
        return new Binding.NotificareNotificationAction(
            type: action.Type,
            label: action.Label,
            target: action.Target,
            keyboard: action.Keyboard,
            camera: action.Camera,
            destructive: action.Destructive ?? false,
            icon: action.Icon == null ? null : ToNativeNotificationActionIcon(action.Icon)
        );
    }

    private static Binding.NotificareNotificationActionIcon ToNativeNotificationActionIcon(
        NotificareNotificationActionIcon icon)
    {
        return new Binding.NotificareNotificationActionIcon(
            android: icon.Android,
            ios: icon.IOS,
            web: icon.Web
        );
    }

    private static Binding.NotificareNotificationAttachment ToNativeNotificationAttachment(
        NotificareNotificationAttachment attachment)
    {
        return new Binding.NotificareNotificationAttachment(
            mimeType: attachment.MimeType,
            uri: attachment.Uri
        );
    }

    /// <summary>
    /// Create a <see cref="Binding.NotificareDoNotDisturb"/> binding object from the <see cref="NotificareDoNotDisturb"/> data model.
    /// </summary>
    /// <param name="dnd"></param>
    /// <returns></returns>
    internal static Binding.NotificareDoNotDisturb ToNativeDoNotDisturb(NotificareDoNotDisturb dnd)
    {
        return new Binding.NotificareDoNotDisturb(
            new Binding.NotificareTime(dnd.Start.Hours, dnd.Start.Minutes),
            new Binding.NotificareTime(dnd.End.Hours, dnd.End.Minutes)
        );
    }

    /// <summary>
    /// Create an "extra" dictionary consumable by the binding.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    internal static NSDictionary<NSString, NSObject> ToNativeExtraDictionary(IDictionary<string, object> data)
    {
        if (data.Count == 0) return new NSDictionary<NSString, NSObject>();
        
        return NSDictionary<NSString, NSObject>.FromObjectsAndKeys(
            data.Values.Select(ToNativeExtraPrimitive).ToArray(),
            data.Keys.Select(key => new NSString(key)).ToArray(),
            data.Count
        );
    }

    private static NSObject ToNativeExtraPrimitive(object value)
    {
        return value switch
        {
            string s => new NSString(s),
            int i => NSNumber.FromInt32(i),
            long l => NSNumber.FromInt64(l),
            double d => NSNumber.FromDouble(d),
            float f => NSNumber.FromFloat(f),
            bool b => NSNumber.FromBoolean(b),
            IList<object> list => NSArray<NSObject>.FromNSObjects(list.Select(ToNativeExtraPrimitive).ToArray()),
            IDictionary<string, object> objects => ToNativeExtraDictionary(objects),
            _ => throw new ArgumentException($"Type '{value.GetType().Name}' cannot be represented as JSON.")
        };
    }

    #endregion
}
