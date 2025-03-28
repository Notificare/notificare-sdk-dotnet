using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using NotificareSdk.Android.Binding;
using NotificareSdk.Push;

namespace Sample;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[IntentFilter(
    ["re.notifica.intent.action.RemoteMessageOpened"],
    Categories = [Intent.CategoryDefault])]
[IntentFilter(
    new string[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "re.notifica.sample.app.dev")]   
[IntentFilter(
    new string[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "https",
    DataHost = "sample-app-dev.ntc.re",
    AutoVerify = true)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (Intent != null) HandleIntent(Intent);
    }

    protected override void OnNewIntent(Intent? intent)
    {
        base.OnNewIntent(intent);
        if (intent != null) HandleIntent(intent);
    }

    private void HandleIntent(Intent intent)
    {
        if (NotificarePush.HandleTrampolineIntent(intent)) return;
        if (Notificare.HandleTestDeviceIntent(intent)) return;
        if (Notificare.HandleDynamicLinkIntent(this, intent)) return;
        
        var action = intent.Action;
        var data = intent.Data?.ToString();

        if (action == Intent.ActionView && data is not null)
        {
            HandleDeepLink(data);
        }
    }

    private void HandleDeepLink(string url)
    {
        if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
            App.Current?.SendOnAppLinkRequestReceived(uri);
    }
}