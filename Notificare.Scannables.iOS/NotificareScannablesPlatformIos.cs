using NotificareSdk.Scannables.Core.Events;
using NotificareSdk.Scannables.Core.Internal;
using NotificareSdk.Scannables.Core.Models;
using NotificareSdk.Scannables.iOS.Internal;

namespace NotificareSdk.Scannables.iOS;

public class NotificareScannablesPlatformIos : INotificareScannablesPlatform
{
    private InternalNotificareScannablesDelegate? _delegate;
    private Binding.NotificareScannablesNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalNotificareScannablesDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<NotificareScannableDetectedEventArgs>? ScannableDetected;
    public event EventHandler<NotificareScannableSessionFailedEventArgs>? ScannableSessionFailed;

    public bool CanStartNfcScannableSession => _native.CanStartNfcScannableSession;

    public void StartScannableSession(UIViewController controller)
    {
        _native.StartScannableSession(controller);
    }

    public void StartNfcScannableSession()
    {
        _native.StartNfcScannableSession();
    }

    public void StartQrCodeScannableSession(UIViewController controller, bool modal)
    {
        _native.StartQrCodeScannableSession(controller, modal);
    }

    public Task<NotificareScannable> FetchAsync(string tag)
    {
        TaskCompletionSource<NotificareScannable> completion = new();

        _native.Fetch(
            tag,
            scannable => completion.TrySetResult(NativeConverter.FromNativeScannable(scannable)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }


    private sealed class InternalNotificareScannablesDelegate : Binding.NotificareScannablesNativeBindingDelegate
    {
        private readonly NotificareScannablesPlatformIos _platform;

        internal InternalNotificareScannablesDelegate(NotificareScannablesPlatformIos platform)
        {
            _platform = platform;
        }

        public override void DidDetectScannable(
            Binding.NotificareScannablesNativeBinding notificareScannables,
            Binding.NotificareScannable scannable
        )
        {
            _platform.ScannableDetected?.Invoke(
                _platform,
                new NotificareScannableDetectedEventArgs(
                    NativeConverter.FromNativeScannable(scannable)
                )
            );
        }

        public override void DidInvalidateScannerSession(
            Binding.NotificareScannablesNativeBinding notificareScannables,
            NSError error
        )
        {
            _platform.ScannableSessionFailed?.Invoke(
                _platform,
                new NotificareScannableSessionFailedEventArgs(
                    error.ToString()
                )
            );
        }
    }
}
