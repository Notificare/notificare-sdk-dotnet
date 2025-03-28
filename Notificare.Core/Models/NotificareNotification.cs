namespace NotificareSdk.Core.Models;

public class NotificareNotification
{
    public bool Partial { get; }
    public string Id { get; }
    public string Type { get; }
    public DateTime Time { get; }
    public string? Title { get; }
    public string? Subtitle { get; }
    public string Message { get; }
    public IList<NotificareNotificationContent> Content { get; }
    public IList<NotificareNotificationAction> Actions { get; }
    public IList<NotificareNotificationAttachment> Attachments { get; }
    public IDictionary<string, object> Extra { get; }
    public string? TargetContentIdentifier { get; }

    public NotificareNotification(bool partial, string id, string type, DateTime time, string? title, string? subtitle, string message, IList<NotificareNotificationContent> content, IList<NotificareNotificationAction> actions, IList<NotificareNotificationAttachment> attachments, IDictionary<string, object> extra, string? targetContentIdentifier)
    {
        Partial = partial;
        Id = id;
        Type = type;
        Time = time;
        Title = title;
        Subtitle = subtitle;
        Message = message;
        Content = content;
        Actions = actions;
        Attachments = attachments;
        Extra = extra;
        TargetContentIdentifier = targetContentIdentifier;
    }
}

public class NotificareNotificationContent
{
    public string Type { get; }
    public object Data { get; }

    public NotificareNotificationContent(string type, object data)
    {
        Type = type;
        Data = data;
    }
}

public class NotificareNotificationAction
{
    public string Type { get; }
    public string Label { get; }
    public string? Target { get; }
    public bool Keyboard { get; }
    public bool Camera { get; }
    public bool? Destructive { get; }
    public NotificareNotificationActionIcon? Icon { get; }

    public NotificareNotificationAction(string type, string label, string? target, bool keyboard, bool camera, bool? destructive, NotificareNotificationActionIcon? icon)
    {
        Type = type;
        Label = label;
        Target = target;
        Keyboard = keyboard;
        Camera = camera;
        Destructive = destructive;
        Icon = icon;
    }
}

public class NotificareNotificationActionIcon
{
    public string? Android { get; }
    public string? IOS { get; }
    public string? Web { get; }

    public NotificareNotificationActionIcon(string? android, string? ios, string? web)
    {
        Android = android;
        IOS = ios;
        Web = web;
    }
}

public class NotificareNotificationAttachment
{
    public string MimeType { get; }
    public string Uri { get; }

    public NotificareNotificationAttachment(string mimeType, string uri)
    {
        MimeType = mimeType;
        Uri = uri;
    }
}
