using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
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
        NotificarePush.HandleTrampolineIntent(intent);
    }
}
