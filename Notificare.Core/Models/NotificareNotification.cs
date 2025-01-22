using Newtonsoft.Json;

namespace NotificareSdk.Core.Models;

public class NotificareNotification
{
    [JsonProperty(propertyName: "partial")]
    public bool Partial { get; }

    [JsonProperty(propertyName: "id")]
    public string Id { get; }

    [JsonProperty(propertyName: "type")]
    public string Type { get; }

    [JsonProperty(propertyName: "time")]
    public DateTime Time { get; }

    [JsonProperty(propertyName: "title")]
    public string? Title { get; }

    [JsonProperty(propertyName: "subtitle")]
    public string? Subtitle { get; }

    [JsonProperty(propertyName: "message")]
    public string Message { get; }

    [JsonProperty(propertyName: "content")]
    public IList<NotificareNotificationContent> Content { get; }

    [JsonProperty(propertyName: "actions")]
    public IList<NotificareNotificationAction> Actions { get; }

    [JsonProperty(propertyName: "attachments")]
    public IList<NotificareNotificationAttachment> Attachments { get; }

    [JsonProperty(propertyName: "extra")]
    public IDictionary<string, object> Extra { get; }

    [JsonProperty(propertyName: "targetContentIdentifier")]
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

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareNotification FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareNotification>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

public class NotificareNotificationContent
{
    [JsonProperty(propertyName: "type")]
    public string Type { get; }

    [JsonProperty(propertyName: "data")]
    public object Data { get; }

    public NotificareNotificationContent(string type, object data)
    {
        Type = type;
        Data = data;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareNotificationContent FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareNotificationContent>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
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

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareNotificationAction FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareNotificationAction>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

public class NotificareNotificationActionIcon
{
    [JsonProperty(propertyName: "android")]
    public string? Android { get; }

    [JsonProperty(propertyName: "ios")]
    public string? IOS { get; }

    [JsonProperty(propertyName: "web")]
    public string? Web { get; }

    public NotificareNotificationActionIcon(string? android, string? ios, string? web)
    {
        Android = android;
        IOS = ios;
        Web = web;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareNotificationActionIcon FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareNotificationActionIcon>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

public class NotificareNotificationAttachment
{
    [JsonProperty(propertyName: "mimeType")]
    public string MimeType { get; }

    [JsonProperty(propertyName: "uri")]
    public string Uri { get; }

    public NotificareNotificationAttachment(string mimeType, string uri)
    {
        MimeType = mimeType;
        Uri = uri;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareNotificationAttachment FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareNotificationAttachment>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}
