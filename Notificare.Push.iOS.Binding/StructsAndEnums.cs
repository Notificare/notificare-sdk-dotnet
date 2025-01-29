using ObjCRuntime;

namespace NotificareSdk.Push.iOS.Binding
{
	[Native]
	public enum NotificareNotificationDeliveryMechanism : long
	{
		Standard = 0,
		Silent = 1,
		Unknown = 2
	}

	[Native]
	public enum NotificareTransport : long
	{
		Notificare = 0,
		Apns = 1,
		Unknown = 2
	}
}
