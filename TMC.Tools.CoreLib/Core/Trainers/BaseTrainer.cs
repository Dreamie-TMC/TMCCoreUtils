using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using TMC.Tools.CoreLib.Core.Http;
using TMC.Tools.CoreLib.Core.Interfaces;
using TMC.Tools.CoreLib.Core.Models;

namespace TMC.Tools.CoreLib.Core.Trainers;

/// <summary>
/// An implementation of <code>ITrainer</code> that provides a base for all trainers. This implements the CheckForUpdates
/// method given a repository URL.
/// </summary>
/// <param name="httpClient"></param>
public abstract class BaseTrainer(IGithubClient httpClient) : ITrainer
{
    protected abstract string RepoUrl { get; }
    protected abstract VersionIdentifier CurrentVersion { get; }
    protected internal IGithubClient HttpClient { get; } = httpClient;

    // Default impl for the puposes of registering services to service collection only
    protected BaseTrainer()
        : this(null!) { }

    public abstract void UpdateBefore();
    public abstract void UpdateAfter();
    public abstract void Restart();
    public abstract void TryAddDependenciesToCollection(IServiceCollection serviceCollection);
    public abstract string GetCheckboxName();

    public async Task<CheckForUpdateResponse> CheckForUpdates()
    {
        try
        {
            var repoPath = new Uri(RepoUrl.TrimEnd('/')).AbsolutePath;
            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"repos{repoPath}/releases/latest"
            );

            var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
                return new CheckForUpdateResponse { HasUpdateAvailable = false };

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var tagName = root.GetProperty("tag_name").GetString() ?? string.Empty;
            var latestVersion = VersionIdentifier.Parse(tagName);

            if (latestVersion.CompareTo(CurrentVersion) <= 0)
                return new CheckForUpdateResponse { HasUpdateAvailable = false };

            var htmlUrl = root.TryGetProperty("html_url", out var htmlProp)
                ? htmlProp.GetString()
                : null;

            string? downloadUrl = null;
            if (root.TryGetProperty("assets", out var assets) && assets.GetArrayLength() > 0)
                downloadUrl = assets[0].TryGetProperty("browser_download_url", out var dlProp)
                    ? dlProp.GetString()
                    : null;

            if (downloadUrl is null && root.TryGetProperty("zipball_url", out var zipProp))
                downloadUrl = zipProp.GetString();

            return new CheckForUpdateResponse
            {
                HasUpdateAvailable = true,
                DownloadUrl = downloadUrl,
                ReleasePageUrl = htmlUrl,
                LatestVersion = latestVersion,
            };
        }
        catch
        {
            return new CheckForUpdateResponse { HasUpdateAvailable = false };
        }
    }
}
