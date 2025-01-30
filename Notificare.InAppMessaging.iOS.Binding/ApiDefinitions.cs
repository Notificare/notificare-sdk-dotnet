using System;
using Foundation;
using ObjCRuntime;

namespace NotificareSdk.InAppMessaging.iOS.Binding
{
	// @interface NotificareInAppMessage : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC31NotificareInAppMessagingBinding22NotificareInAppMessage")]
	[DisableDefaultCtor]
	interface NotificareInAppMessage
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull inAppMessageId;
		[Export ("inAppMessageId")]
		string InAppMessageId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nonnull context;
		[Export ("context", ArgumentSemantic.Copy)]
		string[] Context { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable title;
		[NullAllowed, Export ("title")]
		string Title { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable message;
		[NullAllowed, Export ("message")]
		string Message { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable image;
		[NullAllowed, Export ("image")]
		string Image { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable landscapeImage;
		[NullAllowed, Export ("landscapeImage")]
		string LandscapeImage { get; }

		// @property (readonly, nonatomic) NSInteger delaySeconds;
		[Export ("delaySeconds")]
		nint DelaySeconds { get; }

		// @property (readonly, nonatomic, strong) NotificareInAppMessageAction * _Nullable primaryAction;
		[NullAllowed, Export ("primaryAction", ArgumentSemantic.Strong)]
		NotificareInAppMessageAction PrimaryAction { get; }

		// @property (readonly, nonatomic, strong) NotificareInAppMessageAction * _Nullable secondaryAction;
		[NullAllowed, Export ("secondaryAction", ArgumentSemantic.Strong)]
		NotificareInAppMessageAction SecondaryAction { get; }

		// -(instancetype _Nonnull)initInAppMessageId:(NSString * _Nonnull)inAppMessageId name:(NSString * _Nonnull)name type:(NSString * _Nonnull)type context:(NSArray<NSString *> * _Nonnull)context title:(NSString * _Nullable)title message:(NSString * _Nullable)message image:(NSString * _Nullable)image landscapeImage:(NSString * _Nullable)landscapeImage delaySeconds:(NSInteger)delaySeconds primaryAction:(NotificareInAppMessageAction * _Nullable)primaryAction secondaryAction:(NotificareInAppMessageAction * _Nullable)secondaryAction __attribute__((objc_designated_initializer));
		[Export ("initInAppMessageId:name:type:context:title:message:image:landscapeImage:delaySeconds:primaryAction:secondaryAction:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string inAppMessageId, string name, string type, string[] context, [NullAllowed] string title, [NullAllowed] string message, [NullAllowed] string image, [NullAllowed] string landscapeImage, nint delaySeconds, [NullAllowed] NotificareInAppMessageAction primaryAction, [NullAllowed] NotificareInAppMessageAction secondaryAction);
	}

	// @interface NotificareInAppMessageAction : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC31NotificareInAppMessagingBinding28NotificareInAppMessageAction")]
	[DisableDefaultCtor]
	interface NotificareInAppMessageAction
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable label;
		[NullAllowed, Export ("label")]
		string Label { get; }

		// @property (readonly, nonatomic) BOOL destructive;
		[Export ("destructive")]
		bool Destructive { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable url;
		[NullAllowed, Export ("url")]
		string Url { get; }

		// -(instancetype _Nonnull)initWithLabel:(NSString * _Nullable)label destructive:(BOOL)destructive url:(NSString * _Nullable)url __attribute__((objc_designated_initializer));
		[Export ("initWithLabel:destructive:url:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string label, bool destructive, [NullAllowed] string url);
	}

	// @interface NotificareInAppMessagingNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificareInAppMessagingNativeBinding
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		NotificareInAppMessagingNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<NotificareInAppMessagingNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic) BOOL hasMessagesSuppressed;
		[Export ("hasMessagesSuppressed")]
		bool HasMessagesSuppressed { get; set; }

		// -(void)setMessagesSuppressed:(BOOL)suppressed evaluateContext:(BOOL)evaluateContext;
		[Export ("setMessagesSuppressed:evaluateContext:")]
		void SetMessagesSuppressed (bool suppressed, bool evaluateContext);
	}

	// @protocol NotificareInAppMessagingNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP31NotificareInAppMessagingBinding45NotificareInAppMessagingNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP31NotificareInAppMessagingBinding45NotificareInAppMessagingNativeBindingDelegate_")]
	interface NotificareInAppMessagingNativeBindingDelegate
	{
		// @required -(void)notificare:(NotificareInAppMessagingNativeBinding * _Nonnull)notificare didPresentMessage:(NotificareInAppMessage * _Nonnull)message;
		[Abstract]
		[Export ("notificare:didPresentMessage:")]
		void DidPresentMessage (NotificareInAppMessagingNativeBinding notificare, NotificareInAppMessage message);

		// @required -(void)notificare:(NotificareInAppMessagingNativeBinding * _Nonnull)notificare didFinishPresentingMessage:(NotificareInAppMessage * _Nonnull)message;
		[Abstract]
		[Export ("notificare:didFinishPresentingMessage:")]
		void DidFinishPresentingMessage (NotificareInAppMessagingNativeBinding notificare, NotificareInAppMessage message);

		// @required -(void)notificare:(NotificareInAppMessagingNativeBinding * _Nonnull)notificare didFailToPresentMessage:(NotificareInAppMessage * _Nonnull)message;
		[Abstract]
		[Export ("notificare:didFailToPresentMessage:")]
		void DidFailToPresentMessage (NotificareInAppMessagingNativeBinding notificare, NotificareInAppMessage message);

		// @required -(void)notificare:(NotificareInAppMessagingNativeBinding * _Nonnull)notificare didExecuteAction:(NotificareInAppMessageAction * _Nonnull)action for:(NotificareInAppMessage * _Nonnull)message;
		[Abstract]
		[Export ("notificare:didExecuteAction:for:")]
		void DidExecuteAction (NotificareInAppMessagingNativeBinding notificare, NotificareInAppMessageAction action, NotificareInAppMessage message);

		// @required -(void)notificare:(NotificareInAppMessagingNativeBinding * _Nonnull)notificare didFailToExecuteAction:(NotificareInAppMessageAction * _Nonnull)action for:(NotificareInAppMessage * _Nonnull)message error:(NSError * _Nullable)error;
		[Abstract]
		[Export ("notificare:didFailToExecuteAction:for:error:")]
		void DidFailToExecuteAction (NotificareInAppMessagingNativeBinding notificare, NotificareInAppMessageAction action, NotificareInAppMessage message, [NullAllowed] NSError error);
	}
}
