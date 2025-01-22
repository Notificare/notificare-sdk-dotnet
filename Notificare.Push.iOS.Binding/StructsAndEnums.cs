using ObjCRuntime;

namespace NotificarePushBinding.MaciOS.Binding {

}

namespace NotificareSdk.Push.iOS.Binding
{
    [Native]
    public enum NotificareNotificationDeliveryMechanism : long
    {
        Standard = 0,
        Silent = 1
    }

    [Native]
    public enum NotificareTransport : long
    {
        Notificare = 0,
        Apns = 1,
        Unknown = 2
    }
}
