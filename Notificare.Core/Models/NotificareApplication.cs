namespace NotificareSdk.Core.Models;

public class NotificareApplication
{
    public string Id { get; }
    public string Name { get; }
    public string Category { get; }
    public string? AppStoreId { get; }
    public IDictionary<string, bool> Services { get; }
    public NotificareApplicationInboxConfig? InboxConfig { get; }
    public NotificareApplicationRegionConfig? RegionConfig { get; }
    public IList<NotificareApplicationUserDataField> UserDataFields { get; }
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
}

public class NotificareApplicationInboxConfig
{
    public bool UseInbox { get; }
    public bool UseUserInbox { get; }
    public bool AutoBadge { get; }

    public NotificareApplicationInboxConfig(bool useInbox, bool useUserInbox, bool autoBadge)
    {
        UseInbox = useInbox;
        UseUserInbox = useUserInbox;
        AutoBadge = autoBadge;
    }
}

public class NotificareApplicationRegionConfig
{
    public string? ProximityUUID { get; }

    public NotificareApplicationRegionConfig(string? proximityUUID)
    {
        ProximityUUID = proximityUUID;
    }
}


public class NotificareApplicationUserDataField
{
    public string Type { get; }
    public string Key { get; }
    public string Label { get; }

    public NotificareApplicationUserDataField(string type, string key, string label)
    {
        Type = type;
        Key = key;
        Label = label;
    }
}

public class NotificareApplicationActionCategory
{
    public string Name { get; }
    public string? Description { get; }
    public string Type { get; }
    public IList<NotificareNotificationAction> Actions { get; }

    public NotificareApplicationActionCategory(string name, string? description, string type, IList<NotificareNotificationAction> actions)
    {
        Name = name;
        Description = description;
        Type = type;
        Actions = actions;
    }
}
