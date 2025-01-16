using NotificareSdk.Core.Events;
using NotificareSdk.Core.Internal;
using NotificareSdk.Core.Models;
using NotificareSdk.iOS.Internal;

namespace NotificareSdk.iOS;

public class NotificarePlatformIos : INotificarePlatform
{
    private InternalNotificareDelegate? _delegate;
    private Binding.NotificareNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalNotificareDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<NotificareReadyEventArgs>? Ready;
    public event EventHandler<NotificareUnlaunchedEventArgs>? Unlaunched;
    public event EventHandler<NotificareDeviceRegisteredEventArgs>? DeviceRegistered;

    public bool IsConfigured => _native.IsConfigured;

    public bool IsReady => _native.IsReady;

    public NotificareApplication? Application
    {
        get
        {
            var application = _native.Application;
            return application == null ? null : NotificareNativeConverter.FromNativeApplication(application);
        }
    }

    public Task LaunchAsync()
    {
        TaskCompletionSource completion = new();

        _native.Launch(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task UnlaunchAsync()
    {
        TaskCompletionSource completion = new();

        _native.Unlaunch(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<NotificareApplication> FetchApplicationAsync()
    {
        TaskCompletionSource<NotificareApplication> completion = new();

        _native.FetchApplication
        (
            application => completion.TrySetResult(NotificareNativeConverter.FromNativeApplication(application)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<NotificareNotification> FetchNotificationAsync(string id)
    {
        TaskCompletionSource<NotificareNotification> completion = new();

        _native.FetchNotification(
            id,
            notification => completion.TrySetResult(NotificareNativeConverter.FromNativeNotification(notification)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<NotificareDynamicLink> FetchDynamicLinkAsync(string url)
    {
        TaskCompletionSource<NotificareDynamicLink> completion = new();

        _native.FetchDynamicLink(
            url,
            link => completion.TrySetResult(NotificareNativeConverter.FromNativeDynamicLink(link)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<bool> CanEvaluateDeferredLinkAsync()
    {
        TaskCompletionSource<bool> completion = new();

        completion.TrySetResult(_native.CanEvaluateDeferredLink);

        return completion.Task;
    }

    public Task<bool> EvaluateDeferredLinkAsync()
    {
        TaskCompletionSource<bool> completion = new();

        _native.EvaluateDeferredLink(
            evaluated => completion.TrySetResult(evaluated),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    #region Device Module

    public NotificareDevice? CurrentDevice
    {
        get
        {
            var device = _native.CurrentDevice;
            return device == null ? null : NotificareNativeConverter.FromNativeDevice(device);
        }
    }

    public string? PreferredLanguage => _native.PreferredLanguage?.ToString();

    public Task UpdatePreferredLanguageAsync(string? language)
    {
        TaskCompletionSource completion = new();

        _native.UpdatePreferredLanguage(
            language,
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task UpdateUserAsync(string? userId, string? userName)
    {
        TaskCompletionSource completion = new();

        _native.UpdateUserWithUserId(
            userId,
            userName,
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<IList<string>> FetchTagsAsync()
    {
        TaskCompletionSource<IList<string>> completion = new();

        _native.FetchTags(
            tags => completion.TrySetResult(tags.ToArray().Select(item => item.ToString()).ToList()),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task AddTagAsync(string tag)
    {
        TaskCompletionSource completion = new();

        _native.AddTag(
            tag,
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task AddTagsAsync(IList<string> tags)
    {
        TaskCompletionSource completion = new();

        _native.AddTags(
            tags.ToArray(),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task RemoveTagAsync(string tag)
    {
        TaskCompletionSource completion = new();

        _native.RemoveTag(
            tag,
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task RemoveTagsAsync(IList<string> tags)
    {
        TaskCompletionSource completion = new();

        _native.RemoveTags(
            tags.ToArray(),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task ClearTagsAsync()
    {
        TaskCompletionSource completion = new();

        _native.ClearTags(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<NotificareDoNotDisturb?> FetchDoNotDisturbAsync()
    {
        TaskCompletionSource<NotificareDoNotDisturb?> completion = new();

        _native.FetchDoNotDisturb(
            dnd => completion.TrySetResult(dnd == null ? null : NotificareNativeConverter.FromNativeDoNotDisturb(dnd)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task UpdateDoNotDisturbAsync(NotificareDoNotDisturb dnd)
    {
        TaskCompletionSource completion = new();

        _native.UpdateDoNotDisturb(
            NotificareNativeConverter.ToNativeDoNotDisturb(dnd),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task ClearDoNotDisturbAsync()
    {
        TaskCompletionSource completion = new();

        _native.ClearDoNotDisturb(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<IDictionary<string, string>> FetchUserDataAsync()
    {
        TaskCompletionSource<IDictionary<string, string>> completion = new();

        _native.FetchUserData(
            userData => completion.TrySetResult(
                userData.ToDictionary<KeyValuePair<NSString, NSString>, string, string>(
                    item => item.Key.ToString(),
                    item => item.Value.ToString()
                )
            ),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task UpdateUserDataAsync(IDictionary<string, string> userData)
    {
        TaskCompletionSource completion = new();

        _native.UpdateUserData(
            NSDictionary<NSString, NSString>.FromObjectsAndKeys(
                userData.Values.Select(value => new NSString(value)).ToArray(),
                userData.Keys.Select(key => new NSString(key)).ToArray(),
                userData.Count
            ),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    #endregion

    #region Events Module

    public Task LogCustomAsync(string eventName, IDictionary<string, object>? data = null)
    {
        TaskCompletionSource completion = new();

        _native.LogCustom(
            eventName,
            data == null ? null : NotificareNativeConverter.ToNativeExtraDictionary(data),
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    #endregion


    private sealed class InternalNotificareDelegate : Binding.NotificareNativeBindingDelegate
    {
        private readonly NotificarePlatformIos _plugin;

        internal InternalNotificareDelegate(NotificarePlatformIos plugin)
        {
            _plugin = plugin;
        }

        public override void Notificare(Binding.NotificareNativeBinding notificare,
            Binding.NotificareApplication application)
        {
            var args = new NotificareReadyEventArgs(NotificareNativeConverter.FromNativeApplication(application));
            _plugin.Ready?.Invoke(_plugin, args);
        }

        public override void NotificareDidUnlaunch(Binding.NotificareNativeBinding notificare)
        {
            var args = new NotificareUnlaunchedEventArgs();
            _plugin.Unlaunched?.Invoke(_plugin, args);
        }

        public override void Notificare(Binding.NotificareNativeBinding notificare, Binding.NotificareDevice device)
        {
            var args = new NotificareDeviceRegisteredEventArgs(NotificareNativeConverter.FromNativeDevice(device));
            _plugin.DeviceRegistered?.Invoke(_plugin, args);
        }
    }
}
