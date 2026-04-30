namespace TMC.Tools.CoreLib.Core.Models;

public class CheckForUpdateResponse
{
    public bool HasUpdateAvailable { get; set; }
    public string? DownloadUrl { get; set; }
    public string? ReleasePageUrl { get; set; }
    public VersionIdentifier? LatestVersion { get; set; }
}
