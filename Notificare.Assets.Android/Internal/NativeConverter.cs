using NotificareSdk.Android.Internal;
using NotificareSdk.Assets.Core.Models;

namespace NotificareSdk.Assets.Android.Internal;

internal static class NativeConverter
{
    internal static NotificareAsset FromNativeAsset(NotificareSdk.Assets.Android.Binding.Models.NotificareAsset asset)
    {
        return new NotificareAsset(
            title: asset.Title,
            description: asset.Description,
            key: asset.Key,
            url: asset.Url,
            button: asset.GetButton() == null ? null : FromNativeAssetButton(asset.GetButton()!),
            metaData: asset.GetMetaData() == null ? null : FromNativeAssetMetaData(asset.GetMetaData()!),
            extra: NotificareNativeConverter.FromNativeExtraDictionary(asset.Extra)
        );
    }

    private static NotificareAssetButton FromNativeAssetButton(
        NotificareSdk.Assets.Android.Binding.Models.NotificareAsset.Button button)
    {
        return new NotificareAssetButton(
            label: button.Label,
            action: button.Action
        );
    }

    private static NotificareAssetMetaData FromNativeAssetMetaData(
        NotificareSdk.Assets.Android.Binding.Models.NotificareAsset.MetaData metaData)
    {
        return new NotificareAssetMetaData(
            originalFileName: metaData.OriginalFileName,
            contentType: metaData.ContentType,
            contentLength: metaData.ContentLength
        );
    }
}
