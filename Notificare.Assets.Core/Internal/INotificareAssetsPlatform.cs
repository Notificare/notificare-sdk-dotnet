using NotificareSdk.Assets.Core.Models;

namespace NotificareSdk.Assets.Core.Internal;

public interface INotificareAssetsPlatform
{
    void Initialize();

    Task<IList<NotificareAsset>> FetchAsync(string group);
}
