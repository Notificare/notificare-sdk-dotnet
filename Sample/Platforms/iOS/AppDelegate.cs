using CoreFoundation;
using Foundation;
using NotificareSdk;
using NotificareSdk.Push;
using UIKit;

namespace Sample;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        var logger = new CoreFoundation.OSLog(subsystem: "com.foo.maui", category: "category");
        logger.Log(OSLogLevel.Error, "FinishedLaunching");
        
        Task.Run(async () =>
        {
            try
            {
                NotificarePush.SetPresentationOptions(new List<string> { "banner", "badge", "sound" });
                await Notificare.LaunchAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to launch Notificare: {e.Message}");
            }
        });

        return base.FinishedLaunching(application, launchOptions);
    }
    
    public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
    {
        if (Notificare.HandleTestDeviceUrl(url))
        {
            return true;
        }
        
        if (Notificare.HandleDynamicLinkUrl(url))
        {
            return true;
        }

        HandleAppLink(url.AbsoluteString);
        return false;
    }

    public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity,
        UIApplicationRestorationHandler completionHandler)
    {
        var url = userActivity.WebPageUrl;
        if (url == null) return false;

        if (Notificare.HandleTestDeviceUrl(url))
        {
            return true;
        }

        return Notificare.HandleDynamicLinkUrl(url);
    }
    
    private static void HandleAppLink(string url)
    {
        if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
            App.Current?.SendOnAppLinkRequestReceived(uri);
    }
}
