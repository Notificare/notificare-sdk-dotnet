using Newtonsoft.Json;

namespace NotificareSdk.Core.Models;

public class NotificareApplication
{
    [JsonProperty(propertyName: "id")]
    public string Id { get; }

    [JsonProperty(propertyName: "name")]
    public string Name { get; }

    [JsonProperty(propertyName: "category")]
    public string Category { get; }

    [JsonProperty(propertyName: "appStoreId")]
    public string? AppStoreId { get; }

    [JsonProperty(propertyName: "services")]
    public IDictionary<string, bool> Services { get; }

    [JsonProperty(propertyName: "inboxConfig")]
    public NotificareApplicationInboxConfig? InboxConfig { get; }

    [JsonProperty(propertyName: "regionConfig")]
    public NotificareApplicationRegionConfig? RegionConfig { get; }

    [JsonProperty(propertyName: "userDataFields")]
    public IList<NotificareApplicationUserDataField> UserDataFields { get; }

    [JsonProperty(propertyName: "actionCategories")]
    public IList<NotificareApplicationActionCategory> ActionCategories { get; }

    public NotificareApplication(string id, string name, string category, string? appStoreId, IDictionary<string, bool> services, NotificareApplicationInboxConfig? inboxConfig, NotificareApplicationRegionConfig? regionConfig, IList<NotificareApplicationUserDataField> userDataFields, IList<NotificareApplicationActionCategory> actionCategories)
    {
        Id = id;
        Name = name;
        Category = category;
        AppStoreId = appStoreId;
        Services = services;
        InboxConfig = inboxConfig;
        RegionConfig = regionConfig;
        UserDataFields = userDataFields;
        ActionCategories = actionCategories;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareApplication FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareApplication>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

public class NotificareApplicationInboxConfig
{
    [JsonProperty(propertyName: "useInbox")]
    public bool UseInbox { get; }

    [JsonProperty(propertyName: "useUserInbox")]
    public bool UseUserInbox { get; }

    [JsonProperty(propertyName: "autoBadge")]
    public bool AutoBadge { get; }

    public NotificareApplicationInboxConfig(bool useInbox, bool useUserInbox, bool autoBadge)
    {
        UseInbox = useInbox;
        UseUserInbox = useUserInbox;
        AutoBadge = autoBadge;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareApplicationInboxConfig FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareApplicationInboxConfig>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

public class NotificareApplicationRegionConfig
{
    [JsonProperty(propertyName: "proximityUUID")]
    public string? ProximityUUID { get; }

    public NotificareApplicationRegionConfig(string? proximityUUID)
    {
        ProximityUUID = proximityUUID;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareApplicationRegionConfig FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareApplicationRegionConfig>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}


public class NotificareApplicationUserDataField
{
    [JsonProperty(propertyName: "type")]
    public string Type { get; }

    [JsonProperty(propertyName: "key")]
    public string Key { get; }

    [JsonProperty(propertyName: "label")]
    public string Label { get; }

    public NotificareApplicationUserDataField(string type, string key, string label)
    {
        Type = type;
        Key = key;
        Label = label;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareApplicationUserDataField FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareApplicationUserDataField>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

public class NotificareApplicationActionCategory
{
    [JsonProperty(propertyName: "name")]
    public string Name { get; }

    [JsonProperty(propertyName: "description")]
    public string? Description { get; }

    [JsonProperty(propertyName: "type")]
    public string Type { get; }

    [JsonProperty(propertyName: "actions")]
    public IList<NotificareNotificationAction> Actions { get; }

    public NotificareApplicationActionCategory(string name, string? description, string type, IList<NotificareNotificationAction> actions)
    {
        Name = name;
        Description = description;
        Type = type;
        Actions = actions;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareApplicationActionCategory FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareApplicationActionCategory>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}
