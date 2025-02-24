using NotificareSdk.Push.UI;
using NotificareSdk.Scannables;
using NotificareSdk.Scannables.Core.Events;

#if IOS
using UIKit;
#endif

namespace Sample.Pages.Scannables;

public partial class ScannablesPage : ContentPage
{
    public ScannablesPage()
    {
        InitializeComponent();

        NfcButton.IsEnabled = NotificareScannables.CanStartNfcScannableSession;

        NotificareScannables.ScannableDetected += OnScannableDetected;
        NotificareScannables.ScannableSessionFailed += OnScannableSessionFailed;
    }

    private void StartQrCodeScannableSession(object sender, EventArgs e)
    {
#if ANDROID
        var activity = Platform.CurrentActivity;

        if (activity == null)
        {
            Console.WriteLine("Could not initiate QrCode scannable session. Activity is null.");
            return;
        }

        NotificareScannables.StartQrCodeScannableSession(activity);
#elif IOS
        var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

        if (viewController == null)
        {
            Console.WriteLine("Could not initiate QrCode scannable session. View controller is null.");
            return;
        }

        NotificareScannables.StartQrCodeScannableSession(viewController, true);
#endif
    }

    private void StartNfcScannableSession(object sender, EventArgs e)
    {
#if ANDROID
        var activity = Platform.CurrentActivity;

        if (activity == null)
        {
            Console.WriteLine("Could not initiate Nfc scannable session. Activity is null.");
            return;
        }

        NotificareScannables.StartNfcScannableSession(activity);
#elif IOS
        var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

        if (viewController == null)
        {
            Console.WriteLine("Could not initiate Nfc scannable session. View controller is null.");
            return;
        }

        NotificareScannables.StartNfcScannableSession();
#endif
    }

    private void OnScannableDetected(object? sender, NotificareScannableDetectedEventArgs e)
    {
        var notification = e.Scannable.Notification;

        if (notification == null)
        {
            Console.WriteLine("Scannable without notification detected.");
            return;
        }

#if ANDROID
        var activity = Platform.CurrentActivity;

        if (activity == null)
        {
            Console.WriteLine("Could not present scannable notification. Activity is null.");
            return;
        }
        
        NotificarePushUI.PresentNotification(notification, activity);

#elif IOS
        var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

        if (viewController == null)
        {
            Console.WriteLine("Could not present scannable notification. View controller is null.");
            return;
        }

        Console.WriteLine("Present Scannable notification here.");
        NotificarePushUI.PresentNotification(notification, viewController);

#endif
    }

    private void OnScannableSessionFailed(object? sender, NotificareScannableSessionFailedEventArgs e)
    {
        Console.WriteLine($"Scannable session failed: {e.Error}");
    }
}
