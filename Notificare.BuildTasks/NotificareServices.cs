using System.Runtime.Serialization;

namespace NotificareSDK.BuildTasks;

[DataContract]
[Serializable]
public class ProjectInfo
{
    [DataMember(Name = "application_id")] public required string ApplicationId { get; set; }
    [DataMember(Name = "application_key")] public required string ApplicationKey { get; set; }

    [DataMember(Name = "application_secret")]
    public required string ApplicationSecret { get; set; }
}

[DataContract]
[Serializable]
public class HostsInfo
{
    [DataMember(Name = "rest_api")] public required string RestApi { get; set; }
    [DataMember(Name = "app_links")] public required string AppLinks { get; set; }
    [DataMember(Name = "short_links")] public required string ShortLinks { get; set; }
}

[DataContract]
[Serializable]
public class NotificareServices
{
    [DataMember(Name = "project_info")] public required ProjectInfo ProjectInfo { get; set; }
    [DataMember(Name = "hosts_info")] public HostsInfo? HostsInfo { get; set; }
}
