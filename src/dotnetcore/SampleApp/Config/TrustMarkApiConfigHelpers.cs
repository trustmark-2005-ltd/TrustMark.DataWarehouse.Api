using SampleApp.Constants;
using System.Collections.Generic;

namespace SampleApp.Config
{
    public static class TrustMarkApiConfigHelpers
    {
        public static string GetUrl(string environment, string endpoint)
        {
            var roots = new Dictionary<string, string>()
            {
                { ApiEnvironment.UAT, "https://api.uat.data-hub.org.uk" },
                { ApiEnvironment.Sandbox, "https://api.sandbox.data-hub.org.uk" },
                { ApiEnvironment.Live, "https://api.data-hub.org.uk" }
            };
            var root = roots[environment];
            var url = $"{root}/lodgement/members/{endpoint}";
            return url;
        }
    }
}
