using ObjCRuntime;

namespace NotificareSdk.Geo.iOS.Binding
{
	[Native]
	public enum NotificareBeaconProximity : long
	{
		Unknown = 0,
		Immediate = 1,
		Near = 2,
		Far = 3
	}
}
