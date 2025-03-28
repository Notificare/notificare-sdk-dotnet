using NotificareSdk.Android.Internal;
using NotificareSdk.Scannables.Android.Internal;
using NotificareSdk.Scannables.Core.Events;
using NotificareSdk.Scannables.Core.Internal;
using NotificareSdk.Scannables.Core.Models;
using NativeNotificare = NotificareSdk.Scannables.Android.Binding.NotificareScannablesCompat;

namespace NotificareSdk.Scannables.Android;

public class NotificareScannablesPlatformAndroid : INotificareScannablesPlatform
{
    private Binding.INotificareScannables.IScannableSessionListener? _scannableSessionListener;

    public void Initialize()
    {
        ObserveSession();
    }
    
    public event EventHandler<NotificareScannableDetectedEventArgs>? ScannableDetected;
    
    public event EventHandler<NotificareScannableSessionFailedEventArgs>? ScannableSessionFailed;

    public bool CanStartNfcScannableSession => NativeNotificare.CanStartNfcScannableSession;

    public void StartScannableSession(Activity activity)
    {
        NativeNotificare.StartScannableSession(activity);
    }

    public void StartNfcScannableSession(Activity activity)
    {
        NativeNotificare.StartNfcScannableSession(activity);
    }

    public void StartQrCodeScannableSession(Activity activity)
    {
        NativeNotificare.StartQrCodeScannableSession(activity);
    }

    public async Task<NotificareScannable> FetchAsync(string tag)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.Fetch(tag, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var scannable = (Binding.Models.NotificareScannable)result;

        return NativeConverter.FromNativeScannable(scannable);
    }


    private void ObserveSession()
    {
        if (_scannableSessionListener != null)
            NativeNotificare.RemoveListener(_scannableSessionListener);

        _scannableSessionListener = new ScannableSessionListener(this);
        NativeNotificare.AddListener(_scannableSessionListener);
    }


    private class ScannableSessionListener : Java.Lang.Object, Binding.INotificareScannables.IScannableSessionListener
    {
        private readonly NotificareScannablesPlatformAndroid _platform;

        internal ScannableSessionListener(NotificareScannablesPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnScannableDetected(Binding.Models.NotificareScannable scannable)
        {
            _platform.ScannableDetected?.Invoke(
                _platform,
                new NotificareScannableDetectedEventArgs(
                    NativeConverter.FromNativeScannable(scannable)
                )
            );
        }

        public void OnScannableSessionError(Java.Lang.Exception error)
        {
            _platform.ScannableSessionFailed?.Invoke(
                _platform,
                new NotificareScannableSessionFailedEventArgs(
                    error.Message
                )
            );
        }
    }
}
