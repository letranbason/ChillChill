using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChillChill.Services.Auth
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly IAuthSession _authSession;
        public AuthHandler(IAuthSession authSession)
        {
            _authSession = authSession;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_authSession.IsLoggedIn && !string.IsNullOrEmpty(_authSession.Token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authSession.Token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
