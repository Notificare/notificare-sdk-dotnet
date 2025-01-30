namespace NotificareSdk.InAppMessaging.Core.Models;

public class NotificareInAppMessage
{
    public string Id { get; }
    public string Name { get; }
    public string Type { get; }
    public IList<string> Context { get; }
    public string? Title { get; }
    public string? Message { get; }
    public string? Image { get; }
    public string? LandscapeImage { get; }
    public int DelaySeconds { get; }
    public NotificareInAppMessageAction? PrimaryAction { get; }
    public NotificareInAppMessageAction? SecondaryAction { get; }

    public NotificareInAppMessage(string id, string name, string type, IList<string> context, string? title,
        string? message, string? image, string? landscapeImage, int delaySeconds,
        NotificareInAppMessageAction? primaryAction, NotificareInAppMessageAction? secondaryAction)
    {
        Id = id;
        Name = name;
        Type = type;
        Context = context;
        Title = title;
        Message = message;
        Image = image;
        LandscapeImage = landscapeImage;
        DelaySeconds = delaySeconds;
        PrimaryAction = primaryAction;
        SecondaryAction = secondaryAction;
    }
}

public class NotificareInAppMessageAction
{
    public string? Label { get; }
    public bool Destructive { get; }
    public string? Url { get; }

    public NotificareInAppMessageAction(string? label, bool destructive, string? url)
    {
        Label = label;
        Destructive = destructive;
        Url = url;
    }
}
