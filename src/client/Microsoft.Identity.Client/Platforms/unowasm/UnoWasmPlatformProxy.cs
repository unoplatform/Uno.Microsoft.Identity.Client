using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.PlatformsCommon.Shared;
using Microsoft.Identity.Client.TelemetryCore.Internal;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Platforms.unowasm
{
   internal class UnoWasmPlatformProxy : AbstractPlatformProxy
    {
        public UnoWasmPlatformProxy(ICoreLogger logger) : base(logger)
        {
        }

        public override string GetEnvironmentVariable(string variable)
        {
            if (string.IsNullOrWhiteSpace(variable))
            {
                throw new ArgumentNullException(nameof(variable));
            }

            return Environment.GetEnvironmentVariable(variable);
        }

        public override Task<string> GetUserPrincipalNameAsync()
        {
            throw new PlatformNotSupportedException("UPN not available on Wasm.");
        }

        public override bool IsDomainJoined()
        {
            return false;
        }

        public override Task<bool> IsUserLocalAsync(RequestContext requestContext)
        {
            return Task.FromResult(false);
        }

        public override string GetBrokerOrRedirectUri(Uri redirectUri)
        {
            return redirectUri.OriginalString;
        }

        public override string GetDefaultRedirectUri(string clientId, bool useRecommendedRedirectUri = false)
        {
            return Constants.LocalHostRedirectUri;
        }

        public override ILegacyCachePersistence CreateLegacyCachePersistence()
        {
            return new InMemoryLegacyCachePersistance();
        }

        public override ITokenCacheAccessor CreateTokenCacheAccessor()
        {
            return new InMemoryTokenCacheAccessor(Logger);
        }

        protected override IWebUIFactory CreateWebUiFactory()
        {
            return new UnoWasmWebUIFactory();
        }

        protected override IFeatureFlags CreateFeatureFlags() => new UnoWasmFeatureFlags();

        protected override string InternalGetDeviceModel()
        {
            return null;
        }

        protected override string InternalGetOperatingSystem()
        {
            return System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        }

        protected override string InternalGetProcessorArchitecture()
        {
            return null;
        }

        protected override string InternalGetCallingApplicationName()
        {
            //return Package.Current?.DisplayName?.ToString();
            return null; // TODO
        }

        protected override string InternalGetCallingApplicationVersion()
        {
            return Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString();
        }

        protected override string InternalGetDeviceId()
        {
            // TODO --- use something better
            return Environment.MachineName;
        }

        protected override string InternalGetProductName()
        {
            return "MSAL.UnoWasm";
        }

        protected override ICryptographyManager InternalGetCryptographyManager() => new UnoWasmCryptographyManager();

        protected override IPlatformLogger InternalGetPlatformLogger() => new EventSourcePlatformLogger();

        public override string GetDevicePlatformTelemetryId()
        {
            return string.Empty;
        }

        public override string GetDeviceNetworkState()
        {
            return string.Empty;
        }

        public override int GetMatsOsPlatformCode()
        {
            return MatsConverter.AsInt(OsPlatform.Winrt);
        }

        public override string GetMatsOsPlatform()
        {
            return MatsConverter.AsString(OsPlatform.Winrt);
        }

        public override bool IsSystemWebViewAvailable => false;

        public override bool UseEmbeddedWebViewDefault => false;
    }
}
