using NotificareSdk.Scannables.Core.Events;
using NotificareSdk.Scannables.Core.Internal;
using NotificareSdk.Scannables.Core.Models;

namespace NotificareSdk.Scannables;

public static class NotificareScannables
{
    private static readonly Lazy<INotificareScannablesPlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificareScannablesPlatform Platform
    {
        get
        {
            if (Implementation.Value == null)
            {
                throw MissingPlatformSpecificImplementationException();
            }

            return Implementation.Value;
        }
    }


    public static event EventHandler<NotificareScannableDetectedEventArgs> ScannableDetected
    {
        add => Platform.ScannableDetected += value;
        remove => Platform.ScannableDetected -= value;
    }

    public static event EventHandler<NotificareScannableSessionFailedEventArgs> ScannableSessionFailed
    {
        add => Platform.ScannableSessionFailed += value;
        remove => Platform.ScannableSessionFailed -= value;
    }


    public static bool CanStartNfcScannableSession => Platform.CanStartNfcScannableSession;

#if ANDROID
    public static void StartScannableSession(Activity activity) => 
        Platform.StartScannableSession(activity);

    public static void StartNfcScannableSession(Activity activity) => 
        Platform.StartNfcScannableSession(activity);

    public static void StartQrCodeScannableSession(Activity activity) => 
        Platform.StartQrCodeScannableSession(activity);
#elif IOS
    public static void StartScannableSession(UIViewController controller) => 
        Platform.StartScannableSession(controller);
    
    public static void StartNfcScannableSession() => 
        Platform.StartNfcScannableSession();
    
    public static void StartQrCodeScannableSession(UIViewController controller, bool modal) => 
        Platform.StartQrCodeScannableSession(controller, modal);
#endif
    
    public static Task<NotificareScannable> FetchAsync(string tag) => Platform.FetchAsync(tag);


    private static INotificareScannablesPlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificareScannablesPlatformAndroid();
#elif IOS
        return new iOS.NotificareScannablesPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}
