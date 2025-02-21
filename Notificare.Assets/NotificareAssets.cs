using NotificareSdk.Assets.Core.Internal;
using NotificareSdk.Assets.Core.Models;

namespace NotificareSdk.Assets;

public static class NotificareAssets
{
    private static readonly Lazy<INotificareAssetsPlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificareAssetsPlatform Platform
    {
        get
        {
            if (Implementation.Value == null)
            {
                throw MissingPlatformSpecificImplementationException();
            }

            return Implementation.Value;
        }
    }


    /// <summary>
    /// Fetches a list of <see cref="NotificareAsset"/> for a specified group.
    /// </summary>
    /// <param name="group">The name of the group whose assets are to be fetched.</param>
    /// <returns>
    /// A task that resolves to a list of <see cref="NotificareAsset"/> belonging to the specified group.
    /// </returns>
    public static Task<IList<NotificareAsset>> FetchAsync(string group) => Platform.FetchAsync(group);


    private static INotificareAssetsPlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificareAssetsPlatformAndroid();
#elif IOS
        return new iOS.NotificareAssetsPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}
