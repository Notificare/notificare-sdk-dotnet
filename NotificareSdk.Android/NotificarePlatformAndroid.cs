using Android.Content;
using Android.Runtime;
using NotificareSdk.Android.Internal;
using NotificareSdk.Core.Events;
using NotificareSdk.Core.Internal;
using NotificareSdk.Core.Models;
using NativeNotificare = NotificareSdk.Android.Binding.Notificare;

namespace NotificareSdk.Android;

public class NotificarePlatformAndroid : INotificarePlatform
{
    public void Initialize()
    {
        NotificareDotNetIntentReceiver.Platform = this;
        NativeNotificare.IntentReceiver = Java.Lang.Class.FromType(typeof(NotificareDotNetIntentReceiver));
    }

    public event EventHandler<NotificareReadyEventArgs>? Ready;
    public event EventHandler<NotificareUnlaunchedEventArgs>? Unlaunched;
    public event EventHandler<NotificareDeviceRegisteredEventArgs>? DeviceRegistered;

    public bool IsConfigured => NativeNotificare.IsConfigured;

    public bool IsReady => NativeNotificare.IsReady;

    public NotificareApplication? Application
    {
        get
        {
            var application = NativeNotificare.Application;
            return application == null ? null : NativeConverter.FromNativeApplication(application);
        }
    }

    public async Task LaunchAsync()
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.Launch(callback);

        await callback.Task;
    }

    public async Task UnlaunchAsync()
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.Unlaunch(callback);

        await callback.Task;
    }

    public async Task<NotificareApplication> FetchApplicationAsync()
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.FetchApplication(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var application = (Binding.Models.NotificareApplication)result;

        return NativeConverter.FromNativeApplication(application);
    }

    public async Task<NotificareNotification> FetchNotificationAsync(string id)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.FetchNotification(id, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var notification = (Binding.Models.NotificareNotification)result;

        return NativeConverter.FromNativeNotification(notification);
    }

    public async Task<NotificareDynamicLink> FetchDynamicLinkAsync(string url)
    {
        var uri = global::Android.Net.Uri.Parse(url);
        if (uri == null) throw new ArgumentException("Invalid url.");

        var callback = new NotificareAwaitableCallback();
        NativeNotificare.FetchDynamicLink(uri, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var link = (Binding.Models.NotificareDynamicLink)result;

        return NativeConverter.FromNativeDynamicLink(link);
    }

    public async Task<bool> CanEvaluateDeferredLinkAsync()
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.CanEvaluateDeferredLink(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var canEvaluateDeferredLink = (Java.Lang.Boolean)result;

        return canEvaluateDeferredLink.BooleanValue();
    }

    public async Task<bool> EvaluateDeferredLinkAsync()
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.EvaluateDeferredLink(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var evaluatedDeferredLink = (Java.Lang.Boolean)result;

        return evaluatedDeferredLink.BooleanValue();
    }

    #region Device Module

    public NotificareDevice? CurrentDevice
    {
        get
        {
            var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

            var device = module.CurrentDevice;
            return device == null ? null : NativeConverter.FromNativeDevice(device);
        }
    }

    public string? PreferredLanguage
    {
        get
        {
            var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

            return module.PreferredLanguage;
        }
    }

    public async Task UpdatePreferredLanguageAsync(string? language)
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.UpdatePreferredLanguage(language, callback);

        await callback.Task;
    }

    public async Task UpdateUserAsync(string? userId, string? userName)
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.UpdateUser(userId, userName, callback);

        await callback.Task;
    }

    public async Task<IList<string>> FetchTagsAsync()
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.FetchTags(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var tags = (System.Collections.IList)result;

        return tags
            .Cast<string>()
            .ToList();
    }

    public async Task AddTagAsync(string tag)
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.AddTag(tag, callback);

        await callback.Task;
    }

    public async Task AddTagsAsync(IList<string> tags)
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.AddTags(tags, callback);

        await callback.Task;
    }

    public async Task RemoveTagAsync(string tag)
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.RemoveTag(tag, callback);

        await callback.Task;
    }

    public async Task RemoveTagsAsync(IList<string> tags)
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.RemoveTags(tags, callback);

        await callback.Task;
    }

    public async Task ClearTagsAsync()
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.ClearTags(callback);

        await callback.Task;
    }

    public async Task<NotificareDoNotDisturb?> FetchDoNotDisturbAsync()
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.FetchDoNotDisturb(callback);

        var result = await callback.Task;
        var dnd = (Binding.Models.NotificareDoNotDisturb?)result;

        return dnd == null ? null : NativeConverter.FromNativeDoNotDisturb(dnd);
    }

    public async Task UpdateDoNotDisturbAsync(NotificareDoNotDisturb dnd)
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.UpdateDoNotDisturb(NativeConverter.ToNativeDoNotDisturb(dnd), callback);

        await callback.Task;
    }

    public async Task ClearDoNotDisturbAsync()
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.ClearDoNotDisturb(callback);

        await callback.Task;
    }

    public async Task<IDictionary<string, string>> FetchUserDataAsync()
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.FetchUserData(callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");

        if (result is Java.Util.IMap userData)
        {
            return userData.KeySet()
                .Cast<string>()
                .ToDictionary(
                    key => key.ToString(),
                    key => userData.Get(key)!.ToString()
                );
        }

        return new Dictionary<string, string>();
    }

    public async Task UpdateUserDataAsync(IDictionary<string, string> userData)
    {
        var module = Binding.Ktx.AugmentKt.Device(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.UpdateUserData(userData, callback);

        await callback.Task;
    }

    #endregion

    #region Events Module

    public async Task LogCustomAsync(string eventName, IDictionary<string, object>? data = null)
    {
        var module = Binding.Ktx.AugmentKt.Events(NativeNotificare.Instance);

        var callback = new NotificareAwaitableCallback();
        module.LogCustom(eventName, data == null ? null : NativeConverter.ToNativeExtraDictionary(data), callback);

        await callback.Task;
    }

    #endregion


    [BroadcastReceiver(Enabled = true, Exported = false)]
    private class NotificareDotNetIntentReceiver : Binding.NotificareIntentReceiver
    {
        internal static NotificarePlatformAndroid? Platform;

        protected override void OnReady(Context context, Binding.Models.NotificareApplication application)
        {
            Platform?.Ready?.Invoke(
                this,
                new NotificareReadyEventArgs(
                    NativeConverter.FromNativeApplication(application)
                )
            );
        }

        protected override void OnUnlaunched(Context context)
        {
            Platform?.Unlaunched?.Invoke(
                this,
                new NotificareUnlaunchedEventArgs()
            );
        }

        protected override void OnDeviceRegistered(Context context, Binding.Models.NotificareDevice device)
        {
            Platform?.DeviceRegistered?.Invoke(
                this,
                new NotificareDeviceRegisteredEventArgs(
                    NativeConverter.FromNativeDevice(device)
                )
            );
        }
    }
}
