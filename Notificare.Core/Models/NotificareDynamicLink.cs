namespace NotificareSdk.Core.Models;

public class NotificareDynamicLink
{
    public string Target { get; }

    public NotificareDynamicLink(string target)
    {
        Target = target;
    }
}
