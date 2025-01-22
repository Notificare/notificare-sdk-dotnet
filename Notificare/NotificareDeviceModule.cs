using NotificareSdk.Core.Internal;
using NotificareSdk.Core.Models;

namespace NotificareSdk;

public class NotificareDeviceModule
{
    private readonly INotificarePlatform _platform;

    internal NotificareDeviceModule(INotificarePlatform platform)
    {
        _platform = platform;
    }

    public NotificareDevice? CurrentDevice => _platform.CurrentDevice;

    public string? PreferredLanguage => _platform.PreferredLanguage;

    public Task UpdatePreferredLanguageAsync(string? language) => _platform.UpdatePreferredLanguageAsync(language);

    public Task UpdateUserAsync(string? userId, string? userName) => _platform.UpdateUserAsync(userId, userName);

    public Task<IList<string>> FetchTagsAsync() => _platform.FetchTagsAsync();

    public Task AddTagAsync(string tag) => _platform.AddTagAsync(tag);

    public Task AddTagsAsync(IList<string> tags) => _platform.AddTagsAsync(tags);

    public Task RemoveTagAsync(string tag) => _platform.RemoveTagAsync(tag);

    public Task RemoveTagsAsync(IList<string> tags) => _platform.RemoveTagsAsync(tags);

    public Task ClearTagsAsync() => _platform.ClearTagsAsync();

    public Task<NotificareDoNotDisturb?> FetchDoNotDisturbAsync() => _platform.FetchDoNotDisturbAsync();

    public Task UpdateDoNotDisturbAsync(NotificareDoNotDisturb dnd) => _platform.UpdateDoNotDisturbAsync(dnd);

    public Task ClearDoNotDisturbAsync() => _platform.ClearDoNotDisturbAsync();

    public Task<IDictionary<string, string>> FetchUserDataAsync() => _platform.FetchUserDataAsync();

    public Task UpdateUserDataAsync(IDictionary<string, string> userData) => _platform.UpdateUserDataAsync(userData);
}
