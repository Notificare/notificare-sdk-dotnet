using NotificareSdk.Core.Events;
using NotificareSdk.Core.Models;

namespace NotificareSdk.Core.Internal;

public interface INotificarePlatform
{
    void Initialize();

    event EventHandler<NotificareReadyEventArgs> Ready;
    event EventHandler<NotificareUnlaunchedEventArgs> Unlaunched;
    event EventHandler<NotificareDeviceRegisteredEventArgs> DeviceRegistered;


    bool IsConfigured { get; }

    bool IsReady { get; }

    NotificareApplication? Application { get; }

#if ANDROID
    void Configure(Android.Content.Context context);
    
    void Configure(Android.Content.Context context, string applicationKey, string applicationSecret);
#elif IOS
    void Configure();

    void Configure(string applicationKey, string applicationSecret);
#endif

    Task LaunchAsync();

    Task UnlaunchAsync();

    Task<NotificareApplication> FetchApplicationAsync();

    Task<NotificareNotification> FetchNotificationAsync(string id);

    Task<NotificareDynamicLink> FetchDynamicLinkAsync(string url);

    Task<bool> CanEvaluateDeferredLinkAsync();

    Task<bool> EvaluateDeferredLinkAsync();

    #region Device Module

    NotificareDevice? CurrentDevice { get; }

    string? PreferredLanguage { get; }

    Task UpdatePreferredLanguageAsync(string? language);

    Task UpdateUserAsync(string? userId, string? userName);

    Task<IList<string>> FetchTagsAsync();

    Task AddTagAsync(string tag);

    Task AddTagsAsync(IList<string> tags);

    Task RemoveTagAsync(string tag);

    Task RemoveTagsAsync(IList<string> tags);

    Task ClearTagsAsync();

    Task<NotificareDoNotDisturb?> FetchDoNotDisturbAsync();

    Task UpdateDoNotDisturbAsync(NotificareDoNotDisturb dnd);

    Task ClearDoNotDisturbAsync();

    Task<IDictionary<string, string>> FetchUserDataAsync();

    Task UpdateUserDataAsync(IDictionary<string, string> userData);

    #endregion

    #region Events Module

    Task LogCustomAsync(string eventName, IDictionary<string, object>? data = null);

    #endregion
}
