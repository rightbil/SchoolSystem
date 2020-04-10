using System.Web.Configuration;
using System.Web.Mvc;

namespace SchoolSystem.Customize
{
    public class SsPartialCacheAttribute :OutputCacheAttribute
    {
        public SsPartialCacheAttribute(string cacheProfileName)
        {
            var outputCacheSettingsSection= (OutputCacheSettingsSection)WebConfigurationManager.GetSection("system.web/caching/outputCacheSettings");
            var OutputCacheProfile = outputCacheSettingsSection.OutputCacheProfiles[cacheProfileName];
            Duration = OutputCacheProfile.Duration;
            VaryByParam = OutputCacheProfile.VaryByParam;

        }
    }
}
