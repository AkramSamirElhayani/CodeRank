namespace CodeRank.App.Handlers.Models;

public class IdentityDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Add any identity-specific handling here if needed
        return await base.SendAsync(request, cancellationToken);
    }
}

 