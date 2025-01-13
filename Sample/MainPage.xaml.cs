using NotificareSdk;
using NotificareSdk.Core.Events;
using NotificareSdk.Core.Models;

namespace Sample;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();

        Notificare.Ready += OnReady;
        Notificare.Unlaunched += OnUnlaunched;
        Notificare.DeviceRegistered += OnDeviceRegistered;
    }

    private void OnReady(object? sender, NotificareReadyEventArgs e)
    {
        Console.WriteLine("---> on ready");

        var application = e.Application;
        Console.WriteLine($"application = {application}");
    }

    private void OnUnlaunched(object? sender, NotificareUnlaunchedEventArgs e)
    {
        Console.WriteLine("---> on unlaunched");
    }

    private void OnDeviceRegistered(object? sender, NotificareDeviceRegisteredEventArgs e)
    {
        Console.WriteLine("---> on device registered");

        var device = e.Device;
        Console.WriteLine($"device = {device}");
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);


        try
        {
            // Console.WriteLine($"is configured = {Notificare.IsConfigured}");
            // Console.WriteLine($"is ready = {Notificare.IsReady}");

            Console.WriteLine("---> launching");
            await Notificare.LaunchAsync();

            // var application = Notificare.Application;
            // Console.WriteLine($"application = {application}");

            // Console.WriteLine("---> unlaunching");
            // await Notificare.UnlaunchAsync();

            // application = Notificare.Application;
            // Console.WriteLine($"application = {application}");

            // Console.WriteLine("---> launching");
            // await Notificare.LaunchAsync();

            // Console.WriteLine("---> fetching application");
            // application = await Notificare.FetchApplicationAsync();
            // Console.WriteLine($"application = {application}");

            // Console.WriteLine("---> fetching notification");
            // var notification = await Notificare.FetchNotificationAsync("67473511140041630971551e");
            // Console.WriteLine($"notification = {notification}");

            // Console.WriteLine("---> fetching notification with null extras");
            // notification = await Notificare.FetchNotificationAsync("677eb5fa44af7eece02c0214");
            // Console.WriteLine($"notification = {notification}");

            // Console.WriteLine("---> fetching notification with extras");
            // var notification = await Notificare.FetchNotificationAsync("677ff6537a78bdef506c6250");
            // Console.WriteLine($"notification = {notification}");
            
            Console.WriteLine("---> fetching notification with content");
            var notification = await Notificare.FetchNotificationAsync("678145c7f2aca4b2497c2b2c");
            Console.WriteLine($"notification = {notification}");
            
            // Console.WriteLine("---> fetching unknown notification");
            // notification = await Notificare.FetchNotificationAsync("67473511140041630971551a");
            // Console.WriteLine($"notification = {notification}");

            // Console.WriteLine("---> fetching dynamic link");
            // var link = await Notificare.FetchDynamicLinkAsync("https://sample-app-dev.ntc.re/a2BcQ5kJZv");
            // Console.WriteLine($"link = {link}");

            // var canEvaluate = await Notificare.CanEvaluateDeferredLinkAsync();
            // Console.WriteLine($"canEvaluate = {canEvaluate}");

            // Console.WriteLine("---> logging custom events");
            // await Notificare.Events.LogCustomAsync("example");
            // await Notificare.Events.LogCustomAsync("example_with_data", new Dictionary<string, object>
            // {
            //     { "string", "value1" },
            //     { "int", 123 },
            //     { "double", 10.5 },
            //     { "bool", true },
            //     { "list", new List<object> { "value1", "value2", 123 } },
            //     {
            //         "dictionary",
            //         new Dictionary<string, object>
            //         {
            //             { "string", "foo" },
            //             { "int", 100 },
            //         }
            //     }
            // });

            // Console.WriteLine("---> updating user");
            // await Notificare.Device.UpdateUserAsync(
            //     userId: "helder@notifica.re",
            //     userName: "Helder Pinhal"
            // );
            //
            // var device = Notificare.Device.CurrentDevice;
            // Console.WriteLine($"current device = {device}");

            // var language = Notificare.Device.PreferredLanguage;
            // Console.WriteLine($"preferred language = {language}");
            //
            // Console.WriteLine("---> updating preferred language");
            // await Notificare.Device.UpdatePreferredLanguageAsync("pt-PT");
            //
            // language = Notificare.Device.PreferredLanguage;
            // Console.WriteLine($"preferred language = {language}");
            //
            // Console.WriteLine("---> updating preferred language");
            // await Notificare.Device.UpdatePreferredLanguageAsync(null);
            //
            // language = Notificare.Device.PreferredLanguage;
            // Console.WriteLine($"preferred language = {language}");

            // Console.WriteLine("---> adding tags");
            // await Notificare.Device.AddTagAsync("foo");
            // await Notificare.Device.AddTagsAsync(new List<string> { "bar", "baz" });
            //
            // var tags = await Notificare.Device.FetchTagsAsync();
            // Console.WriteLine($"tags: {string.Join(", ", tags)}");
            //
            // Console.WriteLine("---> removing tags");
            // await Notificare.Device.RemoveTagAsync("foo");
            // await Notificare.Device.RemoveTagsAsync(new List<string> { "bar" });
            //
            // tags = await Notificare.Device.FetchTagsAsync();
            // Console.WriteLine($"tags: {string.Join(", ", tags)}");
            //
            // Console.WriteLine("---> clearing tags");
            // await Notificare.Device.ClearTagsAsync();
            //
            // tags = await Notificare.Device.FetchTagsAsync();
            // Console.WriteLine($"tags: {string.Join(", ", tags)}");

            // var dnd = await Notificare.Device.FetchDoNotDisturbAsync();
            // Console.WriteLine($"dnd = {dnd}");
            //
            // Console.WriteLine("---> updating dnd");
            // await Notificare.Device.UpdateDoNotDisturbAsync(
            //     new NotificareDoNotDisturb(
            //         start: new NotificareTime(hours: 8, minutes: 10),
            //         end: new NotificareTime(hours: 12, minutes: 15)
            //     )
            // );
            //
            // dnd = await Notificare.Device.FetchDoNotDisturbAsync();
            // Console.WriteLine($"dnd = {dnd}");
            //
            // Console.WriteLine("---> clearing dnd");
            // await Notificare.Device.ClearDoNotDisturbAsync();
            //
            // dnd = await Notificare.Device.FetchDoNotDisturbAsync();
            // Console.WriteLine($"dnd = {dnd}");

            // var userData = await Notificare.Device.FetchUserDataAsync();
            // Console.WriteLine($"userData = {userData}");
            //
            // Console.WriteLine("---> updating user data");
            // await Notificare.Device.UpdateUserDataAsync(
            //     new Dictionary<string, string>
            //     {
            //         { "firstName", "Helder" },
            //         { "lastName", "Pinhal" },
            //     }
            // );
            //
            // userData = await Notificare.Device.FetchUserDataAsync();
            // Console.WriteLine($"userData = {userData}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"ooooops: {exception}");
        }
    }
}
