using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push.Android.Internal;

internal static class NativeConverter
{
    internal static NotificareTransport? FromNativeTransport(
        NotificareSdk.Push.Android.Binding.Models.NotificareTransport transport)
    {
        return Enum.GetValues<NotificareTransport>()
            .First(t => t.ToRawValue() == transport.RawValue);
    }

    internal static NotificareNotificationDeliveryMechanism FromNativeDeliveryMechanism(
        NotificareSdk.Push.Android.Binding.Models.NotificareNotificationDeliveryMechanism deliveryMechanism)
    {
        return Enum.GetValues<NotificareNotificationDeliveryMechanism>()
            .First(dm => dm.ToRawValue() == deliveryMechanism.RawValue);
    }

    internal static NotificarePushSubscription FromNativeSubscription(
        NotificareSdk.Push.Android.Binding.Models.NotificarePushSubscription subscription)
    {
        return new NotificarePushSubscription(
            token: subscription.Token
        );
    }

    internal static NotificareSystemNotification FromNativeSystemNotification(
        NotificareSdk.Push.Android.Binding.Models.NotificareSystemNotification notification)
    {
        return new NotificareSystemNotification(
            id: notification.Id,
            type: notification.Type,
            extra: notification.Extra.ToDictionary(
                kvp => kvp.Key,
                object? (kvp) => kvp.Value
            )
        );
    }

    internal static IDictionary<string, object> FromNativeUnknownNotification(
        Binding.Models.NotificareUnknownNotification unknownNotification
    )
    {
        var result = new Dictionary<string, object>();

        if (unknownNotification.MessageId != null)
            result["messageId"] = unknownNotification.MessageId;

        if (unknownNotification.MessageType != null)
            result["messageType"] = unknownNotification.MessageType;

        if (unknownNotification.SenderId != null)
            result["senderId"] = unknownNotification.SenderId;

        if (unknownNotification.CollapseKey != null)
            result["collapseKey"] = unknownNotification.CollapseKey;

        if (unknownNotification.From != null)
            result["from"] = unknownNotification.From;

        if (unknownNotification.To != null)
            result["to"] = unknownNotification.To;

        result["sentTime"] = unknownNotification.SentTime;
        result["ttl"] = unknownNotification.Ttl;
        result["priority"] = unknownNotification.Priority;
        result["originalPriority"] = unknownNotification.OriginalPriority;

        var notification = unknownNotification.GetNotification();
        if (notification != null)
        {
            var notificationResult = new Dictionary<string, object>();

            if (notification.Title != null)
                notificationResult["title"] = notification.Title;

            if (notification.TitleLocalizationKey != null)
                notificationResult["titleLocalizationKey"] = notification.TitleLocalizationKey;

            if (notification.TitleLocalizationArgs != null)
                notificationResult["titleLocalizationArgs"] = notification.TitleLocalizationArgs;

            if (notification.Body != null)
                notificationResult["body"] = notification.Body;

            if (notification.BodyLocalizationKey != null)
                notificationResult["bodyLocalizationKey"] = notification.BodyLocalizationKey;

            if (notification.BodyLocalizationArgs != null)
                notificationResult["bodyLocalizationArgs"] = notification.BodyLocalizationArgs;

            if (notification.Icon != null)
                notificationResult["icon"] = notification.Icon;

            var imageUrl = notification.ImageUrl?.ToString();
            if (imageUrl != null)
                notificationResult["imageUrl"] = imageUrl;

            if (notification.Sound != null)
                notificationResult["sound"] = notification.Sound;

            if (notification.Tag != null)
                notificationResult["tag"] = notification.Tag;

            if (notification.Color != null)
                notificationResult["color"] = notification.Color;

            if (notification.ClickAction != null)
                notificationResult["clickAction"] = notification.ClickAction;

            if (notification.ChannelId != null)
                notificationResult["channelId"] = notification.ChannelId;

            var link = notification.Link?.ToString();
            if (link != null)
                notificationResult["link"] = link;

            if (notification.Ticker != null)
                notificationResult["ticker"] = notification.Ticker;

            notificationResult["sticky"] = notification.Sticky;
            notificationResult["localOnly"] = notification.LocalOnly;
            notificationResult["defaultSound"] = notification.DefaultSound;
            notificationResult["defaultVibrateSettings"] = notification.DefaultVibrateSettings;
            notificationResult["defaultLightSettings"] = notification.DefaultLightSettings;

            if (notification.NotificationPriority != null)
                notificationResult["notificationPriority"] = notification.NotificationPriority.IntValue();

            if (notification.Visibility != null)
                notificationResult["visibility"] = notification.Visibility.IntValue();

            if (notification.NotificationCount != null)
                notificationResult["notificationCount"] = notification.NotificationCount.IntValue();

            if (notification.EventTime != null)
                notificationResult["eventTime"] = notification.EventTime.LongValue();

            if (notification.LightSettings != null)
                notificationResult["lightSettings"] = notification.LightSettings.Select(el => el.IntValue()).ToList();

            if (notification.VibrateSettings != null)
                notificationResult["vibrateSettings"] =
                    notification.VibrateSettings.Select(el => el.LongValue()).ToList();

            result["notification"] = notificationResult;
        }

        result["data"] = unknownNotification.Data;

        return result;
    }
}
