using NotificareSdk.Push;
using NotificareSdk.Push.Core.Events;
using NotificareSdk.Push.UI;

#if IOS
using UIKit;
#endif

namespace Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
		NotificarePush.NotificationOpened += OnNotificationOpened;
		NotificarePush.NotificationActionOpened += OnNotificationActionOpened;
	}
	
	private void OnNotificationOpened(object? sender, NotificareNotificationOpenedEventArgs e)
	{
#if ANDROID
            var activity = Platform.CurrentActivity;
            NotificarePushUI.PresentNotification(e.Notification, activity!);

#elif IOS
		var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

		if (rootViewController is null)
		{
			Console.WriteLine("Cannot present a notification with a null root view controller.");
			return;
		}

		if (e.Notification.RequiresViewController())
		{
			var navigationController = new UINavigationController();
			if (navigationController.View is not null)
				navigationController.View.BackgroundColor = UIColor.SystemBackground;

			rootViewController.PresentViewController(
				navigationController,
				true,
				() => NotificarePushUI.PresentNotification(e.Notification, navigationController)
			);
		}
		else
		{
			NotificarePushUI.PresentNotification(e.Notification, rootViewController);
		}
#endif
	}

	private void OnNotificationActionOpened(object? sender, NotificareNotificationActionOpenedEventArgs e)
	{
#if ANDROID
            var activity = Platform.CurrentActivity;
            NotificarePushUI.PresentAction(e.Notification, e.Action, activity!);

#elif IOS
            var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            if (rootViewController is null)
            {
	            Console.WriteLine("Cannot present a notification with a null root view controller.");
	            return;
            }

            NotificarePushUI.PresentAction(e.Notification, e.Action, rootViewController);

#endif
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
	
	protected override async void OnAppLinkRequestReceived(Uri uri)
	{
		base.OnAppLinkRequestReceived(uri);
		
		Console.WriteLine("Deep link received:  " + uri);

		await Dispatcher.DispatchAsync(async () =>
		{
			await Windows[0].Page!.DisplayAlert("Deep link received", uri.ToString(), "OK");
		});
	}
}
