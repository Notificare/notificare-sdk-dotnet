using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace NotificareSdk.Loyalty.iOS.Binding
{
	// @interface NotificareLoyaltyNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificareLoyaltyNativeBinding
	{
		// -(void)fetchPassBySerial:(NSString * _Nonnull)serial :(void (^ _Nonnull)(NotificarePass * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetchPassBySerial:::")]
		void FetchPassBySerial (string serial, Action<NotificarePass> onSuccess, Action<NSError> onFailure);

		// -(void)fetchPassByBarcode:(NSString * _Nonnull)barcode :(void (^ _Nonnull)(NotificarePass * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetchPassByBarcode:::")]
		void FetchPassByBarcode (string barcode, Action<NotificarePass> onSuccess, Action<NSError> onFailure);

		// -(void)present:(NotificarePass * _Nonnull)pass in:(UIViewController * _Nonnull)controller;
		[Export ("present:in:")]
		void Present (NotificarePass pass, UIViewController controller);
	}

	// @interface NotificarePass : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC24NotificareLoyaltyBinding14NotificarePass")]
	[DisableDefaultCtor]
	interface NotificarePass
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull passId;
		[Export ("passId")]
		string PassId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable type;
		[NullAllowed, Export ("type")]
		string Type { get; }

		// @property (readonly, nonatomic) NSInteger version;
		[Export ("version")]
		nint Version { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable passbook;
		[NullAllowed, Export ("passbook")]
		string Passbook { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable passTemplate;
		[NullAllowed, Export ("passTemplate")]
		string PassTemplate { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull serial;
		[Export ("serial")]
		string Serial { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull barcode;
		[Export ("barcode")]
		string Barcode { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull redeem;
		[Export ("redeem")]
		string Redeem { get; }

		// @property (readonly, copy, nonatomic) NSArray<NotificarePassRedemption *> * _Nonnull redeemHistory;
		[Export ("redeemHistory", ArgumentSemantic.Copy)]
		NotificarePassRedemption[] RedeemHistory { get; }

		// @property (readonly, nonatomic) NSInteger limit;
		[Export ("limit")]
		nint Limit { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull token;
		[Export ("token")]
		string Token { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nonnull data;
		[Export ("data", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Data { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull date;
		[Export ("date", ArgumentSemantic.Copy)]
		NSDate Date { get; }

		// -(instancetype _Nonnull)initWithPassId:(NSString * _Nonnull)passId type:(NSString * _Nullable)type version:(NSInteger)version passbook:(NSString * _Nullable)passbook passTemplate:(NSString * _Nullable)passTemplate serial:(NSString * _Nonnull)serial barcode:(NSString * _Nonnull)barcode redeem:(NSString * _Nonnull)redeem redeemHistory:(NSArray<NotificarePassRedemption *> * _Nonnull)redeemHistory limit:(NSInteger)limit token:(NSString * _Nonnull)token data:(NSDictionary<NSString *,id> * _Nonnull)data date:(NSDate * _Nonnull)date __attribute__((objc_designated_initializer));
		[Export ("initWithPassId:type:version:passbook:passTemplate:serial:barcode:redeem:redeemHistory:limit:token:data:date:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string passId, [NullAllowed] string type, nint version, [NullAllowed] string passbook, [NullAllowed] string passTemplate, string serial, string barcode, string redeem, NotificarePassRedemption[] redeemHistory, nint limit, string token, NSDictionary<NSString, NSObject> data, NSDate date);
	}

	// @interface NotificarePassRedemption : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC24NotificareLoyaltyBinding24NotificarePassRedemption")]
	[DisableDefaultCtor]
	interface NotificarePassRedemption
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable comments;
		[NullAllowed, Export ("comments")]
		string Comments { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull date;
		[Export ("date", ArgumentSemantic.Copy)]
		NSDate Date { get; }

		// -(instancetype _Nonnull)initWithComments:(NSString * _Nullable)comments date:(NSDate * _Nonnull)date __attribute__((objc_designated_initializer));
		[Export ("initWithComments:date:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string comments, NSDate date);
	}
}
