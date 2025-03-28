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


    /// <summary>
    /// Called when a scannable item is detected during a scannable session.
    ///
    /// This method is triggered when either an NFC tag or a QR code is successfully
    /// scanned, and the corresponding <see cref="NotificareScannable"/> is retrieved.
    /// </summary>
    public static event EventHandler<NotificareScannableDetectedEventArgs> ScannableDetected
    {
        add => Platform.ScannableDetected += value;
        remove => Platform.ScannableDetected -= value;
    }

    /// <summary>
    /// Called when an error occurs during a scannable session.
    ///
    /// This method is triggered if there's a failure while scanning or processing
    /// the scannable item, either due to NFC or QR code scanning issues, or if the
    /// scannable item cannot be retrieved.
    /// </summary>
    public static event EventHandler<NotificareScannableSessionFailedEventArgs> ScannableSessionFailed
    {
        add => Platform.ScannableSessionFailed += value;
        remove => Platform.ScannableSessionFailed -= value;
    }


    /// <summary>
    /// Indicates whether an NFC scannable session can be started on the current device.
    /// </summary>
    public static bool CanStartNfcScannableSession => Platform.CanStartNfcScannableSession;

#if ANDROID
    /// <summary>
    /// Starts a scannable session, automatically selecting the best scanning method available.
    /// </summary>
    /// <param name="activity">The <see cref="Activity"/> in which to start the scannable session.</param>
    public static void StartScannableSession(Activity activity) =>
        Platform.StartScannableSession(activity);

    /// <summary>
    /// Starts an NFC scannable session.
    ///
    /// Initiates an NFC-based scan, allowing the user to scan NFC tags.
    /// This will only function on devices that support NFC and have it enabled.
    /// </summary>
    /// <param name="activity">The <see cref="Activity"/> in which to start the scannable session.</param>
    public static void StartNfcScannableSession(Activity activity) =>
        Platform.StartNfcScannableSession(activity);

    /// <summary>
    /// Starts a QR code scannable session.
    ///
    /// Initiates a QR code-based scan using the device camera, allowing the user to scan QR codes.
    /// </summary>
    /// <param name="activity">The <see cref="Activity"/> in which to start the scannable session.</param>
    public static void StartQrCodeScannableSession(Activity activity) =>
        Platform.StartQrCodeScannableSession(activity);
#elif IOS
    /// <summary>
    /// Starts a scannable session, automatically selecting the best scanning method available.
    /// </summary>
    /// <param name="controller">The <see cref="UIViewController"/> in which to start the scannable session.</param>
    public static void StartScannableSession(UIViewController controller) =>
        Platform.StartScannableSession(controller);

    /// <summary>
    /// Starts an NFC scannable session.
    ///
    /// Initiates an NFC-based scan, allowing the user to scan NFC tags.
    /// This will only function on devices that support NFC and have it enabled.
    /// </summary>
    public static void StartNfcScannableSession() =>
        Platform.StartNfcScannableSession();

    /// <summary>
    /// Starts a QR code scannable session.
    ///
    /// Initiates a QR code-based scan using the device camera, allowing the user to scan QR codes.
    /// </summary>
    /// <param name="controller">The <see cref="UIViewController"/> in which to start the scannable session.</param>
    /// <param name="modal">A boolean indicating whether the scanner should be presented modally (<c>true</c>) or embedded in the existing navigation flow (<c>false</c>).</param>
    public static void StartQrCodeScannableSession(UIViewController controller, bool modal) =>
        Platform.StartQrCodeScannableSession(controller, modal);
#endif

    /// <summary>
    /// Fetches a scannable item by its tag.
    /// </summary>
    /// <param name="tag">The tag identifier for the scannable item to be fetched.</param>
    /// <returns>
    /// A task that resolves to the <see cref="NotificareScannable"/> object corresponding to the provided tag.
    /// </returns>
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
