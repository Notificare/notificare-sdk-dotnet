using System;
using Foundation;
using NotificareInboxBinding;
using ObjCRuntime;
using NotificareSdk.iOS.Binding;

namespace NotificareSdk.Inbox.iOS.Binding
{
	// @interface NotificareInboxItem : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC22NotificareInboxBinding19NotificareInboxItem")]
	[DisableDefaultCtor]
	interface NotificareInboxItem
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull inboxItemId;
		[Export ("inboxItemId")]
		string InboxItemId { get; }

		// @property (readonly, nonatomic, strong) NotificareNotification * _Nonnull notification;
		[Export ("notification", ArgumentSemantic.Strong)]
		NotificareNotification Notification { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull time;
		[Export ("time", ArgumentSemantic.Copy)]
		NSDate Time { get; }

		// @property (readonly, nonatomic) BOOL opened;
		[Export ("opened")]
		bool Opened { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nullable expires;
		[NullAllowed, Export ("expires", ArgumentSemantic.Copy)]
		NSDate Expires { get; }

		// -(instancetype _Nonnull)initWithInboxItemId:(NSString * _Nonnull)inboxItemId notification:(NotificareNotification * _Nonnull)notification time:(NSDate * _Nonnull)time opened:(BOOL)opened expires:(NSDate * _Nullable)expires __attribute__((objc_designated_initializer));
		[Export ("initWithInboxItemId:notification:time:opened:expires:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string inboxItemId, NotificareNotification notification, NSDate time, bool opened, [NullAllowed] NSDate expires);
	}

	// @interface NotificareInboxNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificareInboxNativeBinding
	{
		// @property (readonly, copy, nonatomic) NSArray<NotificareInboxItem *> * _Nonnull items;
		[Export ("items", ArgumentSemantic.Copy)]
		NotificareInboxItem[] Items { get; }

		// @property (readonly, nonatomic) NSInteger badge;
		[Export ("badge")]
		nint Badge { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		NotificareInboxNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<NotificareInboxNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)refresh;
		[Export ("refresh")]
		void Refresh ();

		// -(void)refreshBadge:(void (^ _Nonnull)(NSInteger))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("refreshBadge::")]
		void RefreshBadge (Action<nint> onSuccess, Action<NSError> onFailure);

		// -(void)open:(NotificareInboxItem * _Nonnull)item :(void (^ _Nonnull)(NotificareNotification * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("open:::")]
		void Open (NotificareInboxItem item, Action<NotificareNotification> onSuccess, Action<NSError> onFailure);

		// -(void)markAsRead:(NotificareInboxItem * _Nonnull)item :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("markAsRead:::")]
		void MarkAsRead (NotificareInboxItem item, Action onSuccess, Action<NSError> onFailure);

		// -(void)markAllAsRead:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("markAllAsRead::")]
		void MarkAllAsRead (Action onSuccess, Action<NSError> onFailure);

		// -(void)remove:(NotificareInboxItem * _Nonnull)item :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("remove:::")]
		void Remove (NotificareInboxItem item, Action onSuccess, Action<NSError> onFailure);

		// -(void)clear:(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("clear::")]
		void Clear (Action onSuccess, Action<NSError> onFailure);
	}

	// @protocol NotificareInboxNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP22NotificareInboxBinding36NotificareInboxNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP22NotificareInboxBinding36NotificareInboxNativeBindingDelegate_")]
	interface NotificareInboxNativeBindingDelegate
	{
		// @required -(void)notificare:(NotificareInboxNativeBinding * _Nonnull)notificareInbox didUpdateInbox:(NSArray<NotificareInboxItem *> * _Nonnull)items;
		[Abstract]
		[Export ("notificare:didUpdateInbox:")]
		void DidUpdateInbox (NotificareInboxNativeBinding notificareInbox, NotificareInboxItem[] items);

		// @required -(void)notificare:(NotificareInboxNativeBinding * _Nonnull)notificareInbox didUpdateBadge:(NSInteger)badge;
		[Abstract]
		[Export ("notificare:didUpdateBadge:")]
		void DidUpdateBadge (NotificareInboxNativeBinding notificareInbox, nint badge);
	}
}
