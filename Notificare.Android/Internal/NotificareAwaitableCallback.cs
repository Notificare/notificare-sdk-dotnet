using Java.Lang;
using NotificareSdk.Android.Binding;

namespace NotificareSdk.Android.Internal;

public class NotificareAwaitableCallback : Java.Lang.Object, INotificareCallback
{
    private readonly TaskCompletionSource<Java.Lang.Object?> _taskCompletionSource = new();
    public Task<Java.Lang.Object?> Task => _taskCompletionSource.Task;

    public void OnFailure(Java.Lang.Exception e)
    {
        _taskCompletionSource.TrySetException(Throwable.ToException(e));
    }

    public void OnSuccess(Java.Lang.Object? result)
    {
        _taskCompletionSource.TrySetResult(result);
    }
}
