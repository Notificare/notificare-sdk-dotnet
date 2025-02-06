using NotificareSdk.iOS.Binding;

using System;
using Foundation;
using ObjCRuntime;

namespace NotificareSdk.UserInbox.iOS.Binding
{
	// @interface NotificareUserInboxItem : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC26NotificareUserInboxBinding23NotificareUserInboxItem")]
	[DisableDefaultCtor]
	interface NotificareUserInboxItem
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

	// @interface NotificareUserInboxNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificareUserInboxNativeBinding
	{
		// -(void)parseResponseFromString:(NSString * _Nonnull)str :(void (^ _Nonnull)(NotificareUserInboxResponse * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("parseResponseFromString:::")]
		void ParseResponseFromString (string str, Action<NotificareUserInboxResponse> onSuccess, Action<NSError> onFailure);

		// -(void)open:(NotificareUserInboxItem * _Nonnull)item :(void (^ _Nonnull)(NotificareNotification * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("open:::")]
		void Open (NotificareUserInboxItem item, Action<NotificareNotification> onSuccess, Action<NSError> onFailure);

		// -(void)markAsRead:(NotificareUserInboxItem * _Nonnull)item :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("markAsRead:::")]
		void MarkAsRead (NotificareUserInboxItem item, Action onSuccess, Action<NSError> onFailure);

		// -(void)remove:(NotificareUserInboxItem * _Nonnull)item :(void (^ _Nonnull)(void))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("remove:::")]
		void Remove (NotificareUserInboxItem item, Action onSuccess, Action<NSError> onFailure);
	}

	// @interface NotificareUserInboxResponse : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC26NotificareUserInboxBinding27NotificareUserInboxResponse")]
	[DisableDefaultCtor]
	interface NotificareUserInboxResponse
	{
		// @property (readonly, nonatomic) NSInteger count;
		[Export ("count")]
		nint Count { get; }

		// @property (readonly, nonatomic) NSInteger unread;
		[Export ("unread")]
		nint Unread { get; }

		// @property (readonly, copy, nonatomic) NSArray<NotificareUserInboxItem *> * _Nonnull items;
		[Export ("items", ArgumentSemantic.Copy)]
		NotificareUserInboxItem[] Items { get; }

		// -(instancetype _Nonnull)initWithCount:(NSInteger)count unread:(NSInteger)unread items:(NSArray<NotificareUserInboxItem *> * _Nonnull)items __attribute__((objc_designated_initializer));
		[Export ("initWithCount:unread:items:")]
		[DesignatedInitializer]
		NativeHandle Constructor (nint count, nint unread, NotificareUserInboxItem[] items);
	}
}

