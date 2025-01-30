namespace NotificareSdk.Assets.Core.Models;

public class NotificareAsset
{
    public string Title { get; }
    public string? Description { get; }
    public string? Key { get; }
    public string? Url { get; }
    public NotificareAssetButton? Button { get; }
    public NotificareAssetMetaData? MetaData { get; }
    public IDictionary<string, object> Extra { get; }

    public NotificareAsset(string title, string? description, string? key, string? url, NotificareAssetButton? button,
        NotificareAssetMetaData? metaData, IDictionary<string, object> extra)
    {
        Title = title;
        Description = description;
        Key = key;
        Url = url;
        Button = button;
        MetaData = metaData;
        Extra = extra;
    }
}

public class NotificareAssetButton
{
    public string? Label { get; }
    public string? Action { get; }

    public NotificareAssetButton(string? label, string? action)
    {
        Label = label;
        Action = action;
    }
}

public class NotificareAssetMetaData
{
    public string OriginalFileName { get; }
    public string ContentType { get; }
    public int ContentLength { get; }

    public NotificareAssetMetaData(string originalFileName, string contentType, int contentLength)
    {
        OriginalFileName = originalFileName;
        ContentType = contentType;
        ContentLength = contentLength;
    }
}
