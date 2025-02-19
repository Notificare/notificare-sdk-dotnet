using NotificareSdk.Push;
using NotificareSdk.Push.Core.Events;
using NotificareSdk.Push.UI;
using UIKit;

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
            var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            NotificarePushUI.PresentNotification(e.Notification, viewController!);
#endif
	}

	private void OnNotificationActionOpened(object? sender, NotificareNotificationActionOpenedEventArgs e)
	{
#if ANDROID
            var activity = Platform.CurrentActivity;
            NotificarePushUI.PresentAction(e.Notification, e.Action, activity!);

#elif IOS
            var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            NotificarePushUI.PresentAction(e.Notification, e.Action, viewController!);

#endif
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
	
	protected override async void OnAppLinkRequestReceived(Uri uri)
	{
		base.OnAppLinkRequestReceived(uri);

		await Dispatcher.DispatchAsync(async () =>
		{
			await Windows[0].Page!.DisplayAlert("Deep link received", uri.ToString(), "OK");
		});

		Console.WriteLine("Deep link received:  " + uri.ToString());
	}
}