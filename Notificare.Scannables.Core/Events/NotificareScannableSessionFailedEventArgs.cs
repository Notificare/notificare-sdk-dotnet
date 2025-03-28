namespace NotificareSdk.Scannables.Core.Events;

public class NotificareScannableSessionFailedEventArgs(string? error) : EventArgs
{
    public string? Error { get; } = error;
}
