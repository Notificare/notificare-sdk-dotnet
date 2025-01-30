using NotificareSdk.Assets.Core.Internal;
using NotificareSdk.Assets.Core.Models;
using NotificareSdk.Assets.iOS.Internal;

namespace NotificareSdk.Assets.iOS;

public class NotificareAssetsPlatformIos : INotificareAssetsPlatform
{
    private Binding.NotificareAssetsNativeBinding _native = new();

    public void Initialize()
    {
    }

    public Task<IList<NotificareAsset>> FetchAsync(string group)
    {
        TaskCompletionSource<IList<NotificareAsset>> completion = new();

        _native.Fetch(
            group,
            assets => completion.TrySetResult(assets.ToArray().Select(NativeConverter.FromNativeAsset).ToList()),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }
}
