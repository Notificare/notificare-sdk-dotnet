using NotificareSdk.iOS.Binding;

using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace NotificareSdk.Scannables.iOS.Binding
{
	// @interface NotificareScannable : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC27NotificareScannablesBinding19NotificareScannable")]
	[DisableDefaultCtor]
	interface NotificareScannable
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull scannableId;
		[Export ("scannableId")]
		string ScannableId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull tag;
		[Export ("tag")]
		string Tag { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, nonatomic, strong) NotificareNotification * _Nullable notification;
		[NullAllowed, Export ("notification", ArgumentSemantic.Strong)]
		NotificareNotification Notification { get; }

		// -(instancetype _Nonnull)initWithScannableId:(NSString * _Nonnull)scannableId name:(NSString * _Nonnull)name tag:(NSString * _Nonnull)tag type:(NSString * _Nonnull)type notification:(NotificareNotification * _Nullable)notification __attribute__((objc_designated_initializer));
		[Export ("initWithScannableId:name:tag:type:notification:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string scannableId, string name, string tag, string type, [NullAllowed] NotificareNotification notification);
	}

	// @interface NotificareScannablesNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificareScannablesNativeBinding
	{
		// @property (readonly, nonatomic) BOOL canStartNfcScannableSession;
		[Export ("canStartNfcScannableSession")]
		bool CanStartNfcScannableSession { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		NotificareScannablesNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<NotificareScannablesNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)startScannableSession:(UIViewController * _Nonnull)controller;
		[Export ("startScannableSession:")]
		void StartScannableSession (UIViewController controller);

		// -(void)startNfcScannableSession;
		[Export ("startNfcScannableSession")]
		void StartNfcScannableSession ();

		// -(void)startQrCodeScannableSession:(UIViewController * _Nonnull)controller modal:(BOOL)modal;
		[Export ("startQrCodeScannableSession:modal:")]
		void StartQrCodeScannableSession (UIViewController controller, bool modal);

		// -(void)fetch:(NSString * _Nonnull)tag :(void (^ _Nonnull)(NotificareScannable * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetch:::")]
		void Fetch (string tag, Action<NotificareScannable> onSuccess, Action<NSError> onFailure);
	}

	// @protocol NotificareScannablesNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP27NotificareScannablesBinding41NotificareScannablesNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP27NotificareScannablesBinding41NotificareScannablesNativeBindingDelegate_")]
	interface NotificareScannablesNativeBindingDelegate
	{
		// @required -(void)notificare:(NotificareScannablesNativeBinding * _Nonnull)notificareScannables didInvalidateScannerSession:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("notificare:didInvalidateScannerSession:")]
		void DidInvalidateScannerSession (NotificareScannablesNativeBinding notificareScannables, NSError error);

		// @required -(void)notificare:(NotificareScannablesNativeBinding * _Nonnull)notificareScannables didDetectScannable:(NotificareScannable * _Nonnull)scannable;
		[Abstract]
		[Export ("notificare:didDetectScannable:")]
		void DidDetectScannable (NotificareScannablesNativeBinding notificareScannables, NotificareScannable scannable);
	}
}

