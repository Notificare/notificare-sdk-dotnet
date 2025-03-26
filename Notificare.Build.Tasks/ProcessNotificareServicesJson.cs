using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Notificare.Build.Tasks.ProcessNotificareServices;
using Task = Microsoft.Build.Utilities.Task;

namespace Notificare.Build.Tasks;

public class ProcessNotificareServicesJson : Task
{
    [Required] public required ITaskItem ResStringsPath { get; set; }
    [Required] public required ITaskItem StampPath { get; set; }
    [Required] public required ITaskItem[] NotificareServicesJsonPaths { get; set; }
    [Required] public required ITaskItem ResPath { get; set; }

    [Output] public required ITaskItem[] ResPathAbsolute { get; set; }

    public override bool Execute()
    {
        var resPath = ResPath.GetMetadata("FullPath");
        var stampPath = StampPath.GetMetadata("FullPath");
        var resValuesPath = ResStringsPath.GetMetadata("FullPath");

        // Return the absolute path of our resources dir
        ResPathAbsolute = [new TaskItem(resPath)];

        if (NotificareServicesJsonPaths.Length == 0)
        {
            Log.LogWarning(
                "NotificareServicesJson not defined, make sure to manually configure Notificare or set the NotificareServicesJson."
            );

            DeleteFiles(resValuesPath);
            return true;
        }

        if (NotificareServicesJsonPaths.Length > 1)
            Log.LogWarning(
                "Multiple NotificareServicesJson files defined, continuing with the first one."
            );

        var notificareServicesJson = NotificareServicesJsonPaths.First();
        var notificareServicesJsonPath = CleanPath(notificareServicesJson.ItemSpec);

        NotificareServices notificareServices;

        try
        {
            using (var notificareServicesFileStream = File.OpenRead(notificareServicesJsonPath))
                notificareServices = NotificareServicesLoader.ProcessJson(notificareServicesFileStream);

            if (notificareServices == null)
                throw new NullReferenceException();
        }
        catch (Exception ex)
        {
            Log.LogError(
                $"Failed to Read or Deserialize NotificareServicesJson file: {notificareServicesJsonPath}{Environment.NewLine}{ex}"
            );

            DeleteFiles(resValuesPath);
            return false;
        }

        var resItems = new Dictionary<string, string>
        {
            { "notificare_services_application_id", notificareServices.ProjectInfo.ApplicationId },
            { "notificare_services_application_key", notificareServices.ProjectInfo.ApplicationKey },
            { "notificare_services_application_secret", notificareServices.ProjectInfo.ApplicationSecret }
        };

        if (resItems.Any(kvp => string.IsNullOrEmpty(kvp.Value)))
        {
            Log.LogWarning("Some of NotificareServicesJson required entries are missing or empty.");

            DeleteFiles(resValuesPath);
            return false;
        }

        if (notificareServices.HostsInfo != null)
        {
            resItems["notificare_services_hosts_rest_api"] = notificareServices.HostsInfo.RestApi;
            resItems["notificare_services_hosts_app_links"] = notificareServices.HostsInfo.AppLinks;
            resItems["notificare_services_hosts_short_links"] = notificareServices.HostsInfo.ShortLinks;
        }

        Log.LogMessage($"Writing NotificareServicesJson Resource: {resValuesPath}");
        WriteResourceDoc(resValuesPath, resItems);
        Log.LogMessage($"Success writing NotificareServicesJson Resource: {resValuesPath}");

        var stampTxt = string.Empty;

        if (File.Exists(resValuesPath))
            stampTxt += resValuesPath + Environment.NewLine;

        File.WriteAllText(stampPath, stampTxt);
        Log.LogMessage("NotificareServicesJson successfully added.");

        return true;
    }

    private static void WriteResourceDoc(string path, Dictionary<string, string> resourceValues)
    {
        var pathInfo = new FileInfo(path);

        if (pathInfo.Directory is { Exists: false })
            pathInfo.Directory.Create();

        var xws = new XmlWriterSettings
        {
            Indent = true
        };

        using var sw = File.Create(path);
        using var xw = XmlWriter.Create(sw, xws);
        xw.WriteStartDocument();
        xw.WriteStartElement("resources");

        foreach (var kvp in resourceValues.Where(kvp => !string.IsNullOrEmpty(kvp.Value)))
        {
            xw.WriteStartElement("string");
            xw.WriteAttributeString("name", kvp.Key);
            xw.WriteAttributeString("translatable", "false");
            xw.WriteString(kvp.Value);
            xw.WriteEndElement();
        }

        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
    }

    private void DeleteFiles(params string[] paths)
    {
        if (paths.Length == 0)
            return;

        foreach (var p in paths)
        {
            if (!File.Exists(p))
                continue;
            try
            {
                File.Delete(p);
            }
            catch (Exception ex)
            {
                Log.LogWarning($"Failed to delete file: {p}{Environment.NewLine}{ex}");
            }
        }
    }

    private static string CleanPath(params string[] paths)
    {
        var combined = Path.Combine(paths);
        return combined.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
    }
}
