using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Platforms.unowasm
{
    internal class UnoWasmWebUIFactory : IWebUIFactory
    {
        public IWebUI CreateAuthenticationDialog(CoreUIParent coreUIParent, RequestContext requestContext)
        {
            return new UnoWasmWebUI(coreUIParent)
            {
                RequestContext = requestContext
            };
        }
    }
}
