using NotificareSdk.Assets.Core.Models;
using NotificareSdk.iOS.Internal;

namespace NotificareSdk.Assets.iOS.Internal;

internal static class NativeConverter
{
    internal static NotificareAsset FromNativeAsset(NotificareSdk.Assets.iOS.Binding.NotificareAsset asset)
    {
        return new NotificareAsset(
            title: asset.Title,
            description: asset.AssetDescription,
            key: asset.Key,
            url: asset.Url,
            button: asset.Button == null ? null : FromNativeAssetButton(asset.Button),
            metaData: asset.MetaData == null ? null : FromNativeAssetMetaData(asset.MetaData),
            extra: NotificareNativeConverter.FromNativeExtraDictionary(asset.Extra)
        );
    }

    private static NotificareAssetButton FromNativeAssetButton(
        NotificareSdk.Assets.iOS.Binding.NotificareAssetButton button)
    {
        return new NotificareAssetButton(
            label: button.Label,
            action: button.Action
        );
    }

    private static NotificareAssetMetaData FromNativeAssetMetaData(NotificareSdk.Assets.iOS.Binding.NotificareAssetMetaData metaData)
    {
        return new NotificareAssetMetaData(
            originalFileName: metaData.OriginalFileName,
            contentType: metaData.ContentType,
            contentLength: metaData.ContentLength.ToInt32()
        );
    }
}
