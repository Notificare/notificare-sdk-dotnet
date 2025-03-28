using NotificareSdk.Android.Internal;
using NotificareSdk.Assets.Android.Internal;
using NotificareSdk.Assets.Core.Internal;
using NotificareSdk.Assets.Core.Models;
using NativeNotificare = NotificareSdk.Assets.Android.Binding.NotificareAssetsCompat;

namespace NotificareSdk.Assets.Android;

public class NotificareAssetsPlatformAndroid : INotificareAssetsPlatform
{
    public void Initialize()
    {
    }

    public async Task<IList<NotificareAsset>> FetchAsync(string group)
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.Fetch(group, callback);

        var result = await callback.Task ?? throw new ArgumentException("Native operation cannot return null.");
        var assets = (System.Collections.IList)result;

        return assets
            .Cast<Binding.Models.NotificareAsset>()
            .Select(NativeConverter.FromNativeAsset)
            .ToList();
    }
}
