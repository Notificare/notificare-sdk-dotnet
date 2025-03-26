using System.Runtime.Serialization;

namespace Notificare.Build.Tasks.ProcessNotificareServices;

[DataContract]
[Serializable]
internal class NotificareServices
{
    [DataMember(Name = "project_info")] public required NotificareServicesProjectInfo ProjectInfo { get; set; }
    [DataMember(Name = "hosts_info")] public NotificareServicesHostsInfo? HostsInfo { get; set; }
}

[DataContract]
[Serializable]
internal class NotificareServicesProjectInfo
{
    [DataMember(Name = "application_id")] public required string ApplicationId { get; set; }

    [DataMember(Name = "application_key")] public required string ApplicationKey { get; set; }

    [DataMember(Name = "application_secret")]
    public required string ApplicationSecret { get; set; }
}

[DataContract]
[Serializable]
internal class NotificareServicesHostsInfo
{
    [DataMember(Name = "rest_api")] public required string RestApi { get; set; }
    [DataMember(Name = "app_links")] public required string AppLinks { get; set; }
    [DataMember(Name = "short_links")] public required string ShortLinks { get; set; }
}
