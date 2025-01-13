using Android.App;
using Android.Runtime;

namespace Sample;

[Application]
[MetaData("re.notifica.auto_configuration_enabled", Value = "false")]
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

		NotificareSdk.Android.Binding.Notificare.Configure(
			context: this,
			applicationKey: "",
			applicationSecret: ""
		);
	}
}
