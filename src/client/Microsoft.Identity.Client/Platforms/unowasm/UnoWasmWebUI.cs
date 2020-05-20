using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.UI;
using Uno.Foundation;

namespace Microsoft.Identity.Client.Platforms.unowasm
{
    internal class UnoWasmWebUI : IWebUI
    {
        private readonly CoreUIParent _parent;

        public UnoWasmWebUI(CoreUIParent parent)
        {
            _parent = parent;
        }

        public RequestContext RequestContext { get; set; }

        public async Task<AuthorizationResult> AcquireAuthorizationAsync(
            Uri authorizationUri,
            Uri redirectUri,
            RequestContext requestContext,
            CancellationToken cancellationToken)
        {
            var urlNavigate = WebAssemblyRuntime.EscapeJs(authorizationUri.OriginalString);
            var urlRedirect = WebAssemblyRuntime.EscapeJs(redirectUri.OriginalString);

            var js = $@"MSAL.WebUI.authenticate(""{urlNavigate}"", ""{urlRedirect}"", ""Sign in"", 483, 600);";

            try
            {
                var uri = await WebAssemblyRuntime.InvokeAsync(js).ConfigureAwait(false);
                return AuthorizationResult.FromUri(uri);
            }
            catch (Exception ex)
            {
                return AuthorizationResult.FromStatus(AuthorizationStatus.UserCancel, ex.Message, ex.Message);
            }
        }

        public Uri UpdateRedirectUri(Uri redirectUri)
        {
            RedirectUriHelper.Validate(redirectUri, usesSystemBrowser: true);
            return redirectUri;
        }
    }
}
