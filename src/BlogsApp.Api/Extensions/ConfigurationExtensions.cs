using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlogsApp.Api.Extensions
{
    /// <summary>
    /// Configuration extensions
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Shorthand for GetSection("AppSettings")[name].
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="name">Parameter name</param>
        /// <returns></returns>
        public static string GetAppSettings(this IConfiguration configuration, string name)
        {
            if (configuration == null)
            { 
                return (string)null;
            }

            var section = configuration.GetSection("AppSettings");

            if (section == null)
            { 
                return (string)null;
            }

            return section[name];
        }

        public static JObject GetSettingsObject(this IConfiguration configuration, string name)
        {
            if (configuration == null)
            {
                return (JObject)null;
            }

            var section = configuration.GetSection("AppSettings");

            if (section == null)
            {
                return (JObject)null;
            }

            return JsonConvert.DeserializeObject<JObject>(section.Value);
        }
    }
}
