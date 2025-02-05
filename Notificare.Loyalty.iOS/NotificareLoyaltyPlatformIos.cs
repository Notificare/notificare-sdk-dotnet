using NotificareSdk.Loyalty.Core.Internal;
using NotificareSdk.Loyalty.Core.Models;
using NotificareSdk.Loyalty.iOS.Internal;

namespace NotificareSdk.Loyalty.iOS;

public class NotificareLoyaltyPlatformIos : INotificareLoyaltyPlatform
{
    private Binding.NotificareLoyaltyNativeBinding _native = new();

    public void Initialize()
    {
    }

    public Task<NotificarePass> FetchPassBySerialAsync(string serial)
    {
        TaskCompletionSource<NotificarePass> completion = new();

        _native.FetchPassBySerial(
            serial,
            pass => completion.TrySetResult(NativeConverter.FromNativePass(pass)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task<NotificarePass> FetchPassByBarcodeAsync(string barcode)
    {
        TaskCompletionSource<NotificarePass> completion = new();

        _native.FetchPassByBarcode(
            barcode,
            pass => completion.TrySetResult(NativeConverter.FromNativePass(pass)),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public void Present(NotificarePass pass, UIViewController controller)
    {
        _native.Present(NativeConverter.ToNativePass(pass), controller);
    }
}
