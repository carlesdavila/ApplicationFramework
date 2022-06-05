using Microsoft.AspNetCore.Http;

namespace ApplicationFramework.Infrastructure.HttpClient;

public class HttpClientNoCacheDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpClientNoCacheDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var nocacheHeader = _httpContextAccessor.HttpContext?.Request.Headers[CacheHeader.NoCache];

        if (!string.IsNullOrEmpty(nocacheHeader)) request.Headers.Add(CacheHeader.NoCache, new List<string> { nocacheHeader });

        return await base.SendAsync(request, cancellationToken);
    }
}