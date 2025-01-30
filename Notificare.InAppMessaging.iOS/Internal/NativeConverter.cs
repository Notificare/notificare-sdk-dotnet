using NotificareSdk.InAppMessaging.Core.Models;

namespace NotificareSdk.InAppMessaging.iOS.Internal;

internal static class NativeConverter
{
    internal static NotificareInAppMessage FromNativeMessage(
        NotificareSdk.InAppMessaging.iOS.Binding.NotificareInAppMessage message)
    {
        return new NotificareInAppMessage(
            id: message.InAppMessageId,
            name: message.InAppMessageId,
            type: message.Type,
            context: message.Context.ToList(),
            title: message.Title,
            message: message.Message,
            image: message.Image,
            landscapeImage: message.LandscapeImage,
            delaySeconds: message.DelaySeconds.ToInt32(),
            primaryAction: message.PrimaryAction == null ? null : FromNativeMessageAction(message.PrimaryAction),
            secondaryAction: message.SecondaryAction == null ? null : FromNativeMessageAction(message.SecondaryAction)
        );
    }

    internal static NotificareInAppMessageAction FromNativeMessageAction(
        NotificareSdk.InAppMessaging.iOS.Binding.NotificareInAppMessageAction action)
    {
        return new NotificareInAppMessageAction(
            label: action.Label,
            destructive: action.Destructive,
            url: action.Url
        );
    }
}
