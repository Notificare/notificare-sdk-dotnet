using System;
using Foundation;
using NotificareNotificationServiceExtensionBinding;
using UserNotifications;

namespace NotificareSdk.NotificationServiceExtension.iOS.Binding
{
	// @interface NotificareNotificationServiceExtensionNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificareNotificationServiceExtensionNativeBinding
	{
		// +(void)handleNotificationRequest:(UNNotificationRequest * _Nonnull)request :(void (^ _Nonnull)(UNNotificationContent * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Static]
		[Export ("handleNotificationRequest:::")]
		void HandleNotificationRequest (UNNotificationRequest request, Action<UNNotificationContent> onSuccess, Action<NSError> onFailure);
	}
}
