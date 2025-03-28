using NotificareSdk.Core.Events;
using NotificareSdk.Core.Internal;
using NotificareSdk.Core.Models;

namespace NotificareSdk;

public static class Notificare
{
    private static readonly Lazy<INotificarePlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificarePlatform Platform
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
    /// Called when the Notificare SDK is fully ready and the application metadata is available.
    ///  
    /// This method is invoked after the SDK has been successfully launched and is available for use.
    /// </summary>
    public static event EventHandler<NotificareReadyEventArgs> Ready
    {
        add => Platform.Ready += value;
        remove => Platform.Ready -= value;
    }

    /// <summary>
    /// Called when the Notificare SDK has been unlaunched.
    ///
    /// This method is invoked after the SDK has been shut down (unlaunched) and is no longer in use.
    /// </summary>
    public static event EventHandler<NotificareUnlaunchedEventArgs> Unlaunched
    {
        add => Platform.Unlaunched += value;
        remove => Platform.Unlaunched -= value;
    }

    /// <summary>
    /// Called when the device has been successfully registered with the Notificare platform.
    ///
    /// This method is triggered after the device is initially created, which happens the first time <c>launch()</c>
    /// is called.
    /// Once created, the method will not trigger again unless the device is deleted by calling <c>unlaunch()</c>
    /// and created again on a new <c>launch()</c>.
    /// </summary>
    public static event EventHandler<NotificareDeviceRegisteredEventArgs> DeviceRegistered
    {
        add => Platform.DeviceRegistered += value;
        remove => Platform.DeviceRegistered -= value;
    }

    // event EventHandler<...>? UrlOpened


    public static NotificareDeviceModule Device { get; } = new(Platform);

    public static NotificareEventsModule Events { get; } = new(Platform);


    /// <summary>
    /// Indicates whether Notificare has been configured.
    ///
    /// This property returns <c>true</c> if Notificare is successfully configured, and <c>false</c> otherwise.
    /// </summary>
    public static bool IsConfigured => Platform.IsConfigured;

    /// <summary>
    /// Indicates whether Notificare is ready.
    ///
    /// This property returns <c>true</c> once the SDK has completed the initialization process and is ready for use.
    /// </summary>
    public static bool IsReady => Platform.IsReady;

    /// <summary>
    /// Provides the current application metadata, if available.
    ///
    /// This property returns the <see cref="NotificareApplication"/> object representing the configured application,
    /// or <c>null</c> if the application is not yet available.
    /// </summary>
    public static NotificareApplication? Application => Platform.Application;


#if ANDROID
    /// <summary>
    /// Configures Notificare with the application context using the services info in the provided configuration file.
    /// 
    /// This method configures the SDK using the given context and the services info in the provided
    /// <c>notificare-services.json</c> file, and prepares it for use.
    /// </summary>
    /// <param name="context">The <see cref="global::Android.Content.Context"/> to use for configuration.</param>
    public static void Configure(global::Android.Content.Context context) => Platform.Configure(context);

    /// <summary>
    /// Configures Notificare with a specific application key and secret.
    /// 
    /// This method configures the SDK using the given context along with the provided application key and
    /// application secret, and prepares it for use.
    /// </summary>
    /// <param name="context">The <see cref="global::Android.Content.Context"/> to use for configuration.</param>
    /// <param name="applicationKey">The key for the Notificare application.</param>
    /// <param name="applicationSecret">The secret for the Notificare application.</param>
    public static void Configure(
        global::Android.Content.Context context,
        string applicationKey,
        string applicationSecret
    ) => Platform.Configure(context, applicationKey, applicationSecret);

#elif IOS
    /// <summary>
    /// Configures Notificare with the application context using the services info in the provided configuration file.
    ///
    /// This method configures the SDK using the services info in the provided <c>NotificareServices.plist</c> file,
    /// and prepares it for use.
    /// </summary>
    public static void Configure() => Platform.Configure();

    /// <summary>
    /// Configures Notificare with a specific application key and secret.
    /// 
    /// This method configures the SDK using the provided application key and application secret, and prepares it for use.
    /// </summary>
    /// <param name="applicationKey">The key for the Notificare application.</param>
    /// <param name="applicationSecret">The secret for the Notificare application.</param>
    public static void Configure(
        string applicationKey,
        string applicationSecret
    ) => Platform.Configure(applicationKey, applicationSecret);

#endif

    /// <summary>
    /// Launches the Notificare SDK, and all the additional available modules, preparing them for use.
    /// </summary>
    /// <returns>
    /// A task that resolves when the Notificare SDK and its modules have been successfully
    /// launched and are ready for use.
    /// </returns>
    public static Task LaunchAsync() => Platform.LaunchAsync();

    /// <summary>
    /// Unlaunches the Notificare SDK.
    ///
    /// This method shuts down the SDK, removing all data, both locally and remotely in the servers.
    /// It destroys all the device's data permanently.
    /// </summary>
    /// <returns>
    /// A task that resolves when the SDK has been successfully unlaunched and all data has been removed.
    /// </returns>
    public static Task UnlaunchAsync() => Platform.UnlaunchAsync();

    /// <summary>
    /// Fetches the application metadata.
    /// </summary>
    /// <returns>
    /// A task that resolves to a <see cref="NotificareApplication"/> object containing the application metadata.
    /// </returns>
    public static Task<NotificareApplication> FetchApplicationAsync() => Platform.FetchApplicationAsync();

    /// <summary>
    /// Fetches a <see cref="NotificareNotification"/> by its ID.
    /// </summary>
    /// <param name="id">The ID of the notification to fetch.</param>
    /// <returns>
    /// A task that resolves to a <see cref="NotificareNotification"/> object associated with the provided ID.
    /// </returns>
    public static Task<NotificareNotification> FetchNotificationAsync(string id) => Platform.FetchNotificationAsync(id);

    /// <summary>
    /// Fetches a <see cref="NotificareDynamicLink"/> from a URL.
    /// </summary>
    /// <param name="url">The URL to fetch the dynamic link from.</param>
    /// <returns>
    /// A task that resolves to a <see cref="NotificareDynamicLink"/> object.
    /// </returns>
    public static Task<NotificareDynamicLink> FetchDynamicLinkAsync(string url) => Platform.FetchDynamicLinkAsync(url);

    /// <summary>
    /// Checks if a deferred link exists and can be evaluated.
    /// </summary>
    /// <returns>
    /// A task that resolves to <c>true</c> if a deferred
    /// link can be evaluated, <c>false</c> otherwise.
    /// </returns>
    public static Task<bool> CanEvaluateDeferredLinkAsync() => Platform.CanEvaluateDeferredLinkAsync();

    /// <summary>
    /// Evaluates the deferred link. Once the deferred link is evaluated, Notificare
    /// will open the resolved deep link.
    /// </summary>
    /// <returns>
    /// A task that resolves to <c>true</c> if the
    /// deferred link was successfully evaluated, <c>false</c> otherwise.
    /// </returns>
    public static Task<bool> EvaluateDeferredLinkAsync() => Platform.EvaluateDeferredLinkAsync();

#if ANDROID
    /// <summary>
    /// Handles an intent to register the current device as a test device for Notificare services.
    ///
    /// This method processes the provided intent and attempts to register the device
    /// for testing purposes.
    /// </summary>
    /// <param name="intent">The intent containing the test device nonce.</param>
    /// <returns>
    /// <c>true</c> if the device registration process was initiated, or <c>false</c> if no valid nonce was found in
    /// the intent.
    /// </returns>
    public static bool HandleTestDeviceIntent(global::Android.Content.Intent intent) =>
        Platform.HandleTestDeviceIntent(intent);

    /// <summary>
    /// Handles an intent for dynamic links.
    /// 
    /// This method processes the provided intent in the context of an <see cref="Activity"/> for handling dynamic links.
    /// </summary>
    /// <param name="activity">The activity in which the intent is handled.</param>
    /// <param name="intent">The <see cref="global::Android.Content.Intent"/> to handle.</param>
    /// <returns>
    /// <c>true</c> if the intent was handled, <c>false</c> otherwise.
    /// </returns>
    public static bool HandleDynamicLinkIntent(
        global::Android.App.Activity activity,
        global::Android.Content.Intent intent
    ) => Platform.HandleDynamicLinkIntent(activity, intent);

#elif IOS
    /// <summary>
    /// Handles a URL to register the current device as a test device for Notificare services.
    ///
    /// This method processes the provided URL and attempts to register the device
    /// for testing purposes.
    /// </summary>
    /// <param name="url">The URL containing the test device nonce.</param>
    /// <returns>
    /// <c>true</c> if the device registration process was initiated, or <c>false</c> if no valid nonce was found in
    /// the intent.
    /// </returns>
    public static bool HandleTestDeviceUrl(global::Foundation.NSUrl url) => Platform.HandleTestDeviceUrl(url);
    
    /// <summary>
    /// Handles a URL for dynamic links.
    /// 
    /// This method processes the provided URL for handling dynamic links.
    /// </summary>
    /// <param name="url">The URL to handle.</param>
    /// <returns>
    /// <c>true</c> if the dynamic link was handled, <c>false</c> otherwise.
    /// </returns>
    public static bool HandleDynamicLinkUrl(global::Foundation.NSUrl url) => Platform.HandleDynamicLinkUrl(url);

#endif


    private static INotificarePlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificarePlatformAndroid();
#elif IOS
        return new iOS.NotificarePlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}
