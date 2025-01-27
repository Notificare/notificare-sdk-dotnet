namespace NotificareSdk.Push.Core.Models;

public class NotificarePushSubscription
{
    public string? Token { get; }

    public NotificarePushSubscription(string? token)
    {
        Token = token;
    }
}
