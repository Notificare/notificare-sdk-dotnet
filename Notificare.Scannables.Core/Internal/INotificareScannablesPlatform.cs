using NotificareSdk.Scannables.Core.Events;
using NotificareSdk.Scannables.Core.Models;

namespace NotificareSdk.Scannables.Core.Internal;

public interface INotificareScannablesPlatform
{
    void Initialize();

    event EventHandler<NotificareScannableDetectedEventArgs> ScannableDetected;

    event EventHandler<NotificareScannableSessionFailedEventArgs> ScannableSessionFailed;

    bool CanStartNfcScannableSession { get; }
    
#if ANDROID
    void StartScannableSession(Activity activity);
    
    void StartNfcScannableSession(Activity activity);
    
    void StartQrCodeScannableSession(Activity activity);
#elif IOS
    void StartScannableSession(UIViewController controller);
    
    void StartNfcScannableSession();
    
    void StartQrCodeScannableSession(UIViewController controller, bool modal);
#endif

    Task<NotificareScannable> FetchAsync(string tag);
}
