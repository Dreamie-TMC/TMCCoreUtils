using System.Net.Http;

namespace TMC.Tools.CoreLib.Core.Http;

public interface IGithubClient
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
}

public class GithubClient(HttpClient httpClient) : IGithubClient
{
    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) =>
        await httpClient.SendAsync(request);
}
