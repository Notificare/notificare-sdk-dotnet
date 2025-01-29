using NotificareSdk.iOS.Binding;

using Foundation;
using UIKit;

namespace NotificareSdk.Push.UI.iOS.Binding
{
	// @interface NotificarePushUINativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificarePushUINativeBinding
	{
		// -(void)presentNotification:(NotificareNotification * _Nonnull)notification in:(UIViewController * _Nonnull)controller;
		[Export ("presentNotification:in:")]
		void PresentNotification (NotificareNotification notification, UIViewController controller);

		// -(void)presentAction:(NotificareNotificationAction * _Nonnull)action for:(NotificareNotification * _Nonnull)notification in:(UIViewController * _Nonnull)controller;
		[Export ("presentAction:for:in:")]
		void PresentAction (NotificareNotificationAction action, NotificareNotification notification, UIViewController controller);
	}

	// @protocol NotificarePushUINativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP23NotificarePushUIBinding37NotificarePushUINativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP23NotificarePushUIBinding37NotificarePushUINativeBindingDelegate_")]
	interface NotificarePushUINativeBindingDelegate
	{
	}
}

