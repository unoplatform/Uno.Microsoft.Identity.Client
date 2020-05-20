using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client.Platforms.unowasm
{
    internal class UnoWasmFeatureFlags : IFeatureFlags
    {
        public bool IsFociEnabled => false;
    }
}
