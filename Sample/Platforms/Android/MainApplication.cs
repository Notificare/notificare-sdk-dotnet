using Android.App;
using Android.Runtime;
using NotificareSdk;

namespace Sample;

[Application(Theme = "@style/Maui.MainTheme")]
[MetaData("re.notifica.debug_logging_enabled", Value = "true")]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override void OnCreate()
    {
        base.OnCreate();

        Task.Run(async () =>
        {
            try
            {
                await Notificare.LaunchAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to launch Notificare: {e.Message}");
            }
        });
    }
}
