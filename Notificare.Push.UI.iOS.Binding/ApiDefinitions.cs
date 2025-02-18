using NotificareSdk.iOS.Binding;

using Foundation;
using ObjCRuntime;
using UIKit;

namespace NotificareSdk.Push.UI.iOS.Binding
{
	// @interface NotificarePushUINativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificarePushUINativeBinding
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		NotificarePushUINativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<NotificarePushUINativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

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
		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI willPresentNotification:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:willPresentNotification:")]
		void WillPresentNotification (NotificarePushUINativeBinding notificarePushUI, NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI didPresentNotification:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didPresentNotification:")]
		void DidPresentNotification (NotificarePushUINativeBinding notificarePushUI, NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI didFinishPresentingNotification:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didFinishPresentingNotification:")]
		void DidFinishPresentingNotification (NotificarePushUINativeBinding notificarePushUI, NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI didFailToPresentNotification:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didFailToPresentNotification:")]
		void DidFailToPresentNotification (NotificarePushUINativeBinding notificarePushUI, NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI didClickURL:(NSURL * _Nonnull)url in:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didClickURL:in:")]
		void DidClickURL (NotificarePushUINativeBinding notificarePushUI, NSUrl url, NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI willExecuteAction:(NotificareNotificationAction * _Nonnull)action for:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:willExecuteAction:for:")]
		void WillExecuteAction (NotificarePushUINativeBinding notificarePushUI, NotificareNotificationAction action, NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI didExecuteAction:(NotificareNotificationAction * _Nonnull)action for:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didExecuteAction:for:")]
		void DidExecuteAction (NotificarePushUINativeBinding notificarePushUI, NotificareNotificationAction action, NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI didNotExecuteAction:(NotificareNotificationAction * _Nonnull)action for:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didNotExecuteAction:for:")]
		void DidNotExecuteAction (NotificarePushUINativeBinding notificarePushUI, NotificareNotificationAction action, NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI didFailToExecuteAction:(NotificareNotificationAction * _Nonnull)action for:(NotificareNotification * _Nonnull)notification error:(NSError * _Nullable)error;
		[Abstract]
		[Export ("notificare:didFailToExecuteAction:for:error:")]
		void DidFailToExecuteAction (NotificarePushUINativeBinding notificarePushUI, NotificareNotificationAction action, NotificareNotification notification, [NullAllowed] NSError error);

		// @required -(void)notificare:(NotificarePushUINativeBinding * _Nonnull)notificarePushUI didReceiveCustomAction:(NSURL * _Nonnull)url in:(NotificareNotificationAction * _Nonnull)action for:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didReceiveCustomAction:in:for:")]
		void DidReceiveCustomAction (NotificarePushUINativeBinding notificarePushUI, NSUrl url, NotificareNotificationAction action, NotificareNotification notification);
	}
}

