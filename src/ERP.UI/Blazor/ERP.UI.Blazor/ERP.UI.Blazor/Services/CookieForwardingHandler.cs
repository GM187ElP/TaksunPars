
namespace Maui.Web.Services;

public class CookieForwardingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CookieForwardingHandler> _logger;

    public CookieForwardingHandler(
        IHttpContextAccessor httpContextAccessor,
        ILogger<CookieForwardingHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext != null)
        {
            var cookieHeader = httpContext.Request.Headers["Cookie"].ToString();

            if (!string.IsNullOrEmpty(cookieHeader))
            {
                request.Headers.Add("Cookie", cookieHeader);
                _logger.LogDebug("Forwarding cookies to API");
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}