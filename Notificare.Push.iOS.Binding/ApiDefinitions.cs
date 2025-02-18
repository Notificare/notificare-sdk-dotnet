using NotificareSdk.iOS.Binding;

using System;
using Foundation;
using ObjCRuntime;
using UserNotifications;

namespace NotificareSdk.Push.iOS.Binding
{
	// @interface NotificarePushNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificarePushNativeBinding
	{
		// @property (nonatomic) UNAuthorizationOptions authorizationOptions;
		[Export ("authorizationOptions", ArgumentSemantic.Assign)]
		UNAuthorizationOptions AuthorizationOptions { get; set; }

		// @property (nonatomic) UNNotificationCategoryOptions categoryOptions;
		[Export ("categoryOptions", ArgumentSemantic.Assign)]
		UNNotificationCategoryOptions CategoryOptions { get; set; }

		// @property (nonatomic) UNNotificationPresentationOptions presentationOptions;
		[Export ("presentationOptions", ArgumentSemantic.Assign)]
		UNNotificationPresentationOptions PresentationOptions { get; set; }

		// @property (readonly, nonatomic) BOOL hasRemoteNotificationsEnabled;
		[Export ("hasRemoteNotificationsEnabled")]
		bool HasRemoteNotificationsEnabled { get; }

		// @property (readonly, nonatomic) enum NotificareTransport transport;
		[Export ("transport")]
		NotificareTransport Transport { get; }

		// @property (readonly, nonatomic, strong) NotificarePushSubscription * _Nullable subscription;
		[NullAllowed, Export ("subscription", ArgumentSemantic.Strong)]
		NotificarePushSubscription Subscription { get; }

		// @property (readonly, nonatomic) BOOL allowedUI;
		[Export ("allowedUI")]
		bool AllowedUI { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		NotificarePushNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<NotificarePushNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)enableRemoteNotifications:(void (^ _Nonnull)(BOOL))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("enableRemoteNotifications::")]
		void EnableRemoteNotifications (Action<bool> onSuccess, Action<NSError> onFailure);

		// -(void)disableRemoteNotifications:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("disableRemoteNotifications::")]
		void DisableRemoteNotifications (Action onSuccess, Action<NSError> onFailure);
	}

	// @protocol NotificarePushNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP21NotificarePushBinding35NotificarePushNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP21NotificarePushBinding35NotificarePushNativeBindingDelegate_")]
	interface NotificarePushNativeBindingDelegate
	{
		// @required -(void)notificare:(NotificarePushNativeBinding * _Nonnull)notificarePush didChangeSubscription:(NotificarePushSubscription * _Nullable)subscription;
		[Abstract]
		[Export ("notificare:didChangeSubscription:")]
		void DidChangeSubscription (NotificarePushNativeBinding notificarePush, [NullAllowed] NotificarePushSubscription subscription);

		// @required -(void)notificare:(NotificarePushNativeBinding * _Nonnull)notificarePush didChangeNotificationSettings:(BOOL)allowedUI;
		[Abstract]
		[Export ("notificare:didChangeNotificationSettings:")]
		void DidChangeNotificationSettings (NotificarePushNativeBinding notificarePush, bool allowedUI);

		// @required -(void)notificare:(NotificarePushNativeBinding * _Nonnull)notificarePush didReceiveNotification:(NotificareNotification * _Nonnull)notification deliveryMechanism:(enum NotificareNotificationDeliveryMechanism)deliveryMechanism;
		[Abstract]
		[Export ("notificare:didReceiveNotification:deliveryMechanism:")]
		void DidReceiveNotification (NotificarePushNativeBinding notificarePush, NotificareNotification notification, NotificareNotificationDeliveryMechanism deliveryMechanism);

		// @required -(void)notificare:(NotificarePushNativeBinding * _Nonnull)notificarePush didReceiveSystemNotification:(NotificareSystemNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didReceiveSystemNotification:")]
		void DidReceiveSystemNotification (NotificarePushNativeBinding notificarePush, NotificareSystemNotification notification);

		// @required -(void)notificare:(NotificarePushNativeBinding * _Nonnull)notificarePush shouldOpenSettings:(NotificareNotification * _Nullable)notification;
		[Abstract]
		[Export ("notificare:shouldOpenSettings:")]
		void ShouldOpenSettings (NotificarePushNativeBinding notificarePush, [NullAllowed] NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushNativeBinding * _Nonnull)notificarePush didOpenNotification:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didOpenNotification:")]
		void DidOpenNotification (NotificarePushNativeBinding notificarePush, NotificareNotification notification);

		// @required -(void)notificare:(NotificarePushNativeBinding * _Nonnull)notificarePush didOpenAction:(NotificareNotificationAction * _Nonnull)action for:(NotificareNotification * _Nonnull)notification;
		[Abstract]
		[Export ("notificare:didOpenAction:for:")]
		void DidOpenAction (NotificarePushNativeBinding notificarePush, NotificareNotificationAction action, NotificareNotification notification);
	}

	// @interface NotificarePushSubscription : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21NotificarePushBinding26NotificarePushSubscription")]
	[DisableDefaultCtor]
	interface NotificarePushSubscription
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull token;
		[Export ("token")]
		string Token { get; }

		// -(instancetype _Nonnull)initWithToken:(NSString * _Nonnull)token __attribute__((objc_designated_initializer));
		[Export ("initWithToken:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string token);
	}

	// @interface NotificareSystemNotification : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC21NotificarePushBinding28NotificareSystemNotification")]
	[DisableDefaultCtor]
	interface NotificareSystemNotification
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
		[Export ("id")]
		string Id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nonnull extra;
		[Export ("extra", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Extra { get; }

		// -(instancetype _Nonnull)initWithId:(NSString * _Nonnull)id type:(NSString * _Nonnull)type extra:(NSDictionary<NSString *,id> * _Nonnull)extra __attribute__((objc_designated_initializer));
		[Export ("initWithId:type:extra:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string id, string type, NSDictionary<NSString, NSObject> extra);
	}
}

