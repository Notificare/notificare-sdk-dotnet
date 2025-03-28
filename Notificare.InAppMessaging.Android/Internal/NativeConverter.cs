using NotificareSdk.InAppMessaging.Core.Models;

namespace NotificareSdk.InAppMessaging.Android.Internal;

internal static class NativeConverter
{
    internal static NotificareInAppMessage FromNativeMessage(
        NotificareSdk.InAppMessaging.Android.Binding.Models.NotificareInAppMessage message)
    {
        return new NotificareInAppMessage(
            id: message.Id,
            name: message.Name,
            type: message.Type,
            context: message.Context,
            title: message.Title,
            message: message.Message,
            image: message.Image,
            landscapeImage: message.LandscapeImage,
            delaySeconds: message.DelaySeconds,
            primaryAction: message.PrimaryAction == null ? null : FromNativeMessageAction(message.PrimaryAction),
            secondaryAction: message.SecondaryAction == null ? null : FromNativeMessageAction(message.SecondaryAction)
        );
    }

    internal static NotificareInAppMessageAction FromNativeMessageAction(
        NotificareSdk.InAppMessaging.Android.Binding.Models.NotificareInAppMessage.Action action)
    {
        return new NotificareInAppMessageAction(
            label: action.Label,
            destructive: action.Destructive,
            url: action.Url
        );
    }
}
