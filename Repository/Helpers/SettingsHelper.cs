using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Repository.Helpers
{
    public class SettingsHelper
    {
        public readonly IConfiguration configuration;
        [ExcludeFromCodeCoverage]
        public SettingsHelper()
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }
        [ExcludeFromCodeCoverage]
        public string getDefaultRol()
        {
            string ROLIDDEFAULT = Environment.GetEnvironmentVariable("ROLIDDEFAULT") ?? "";

            if (string.IsNullOrEmpty(ROLIDDEFAULT))
            {
                return configuration.GetValue<string>("Settings:RolIdDefault");
            }
            else
            {
                return ROLIDDEFAULT;
            }
        }
        [ExcludeFromCodeCoverage]
        public string getFrontUrl()
        {
            string FRONTURL = Environment.GetEnvironmentVariable("FRONTURL") ?? "";

            if (string.IsNullOrEmpty(FRONTURL))
            {
                return configuration.GetValue<string>("Settings:FrontUrl");
            }
            else
            {
                return FRONTURL;
            }
        }
        [ExcludeFromCodeCoverage]
        public string getPageUrl()
        {
            string FRONTURL = Environment.GetEnvironmentVariable("PageUrl") ?? "";

            if (string.IsNullOrEmpty(FRONTURL))
            {
                return configuration.GetValue<string>("Settings:PageUrl");
            }
            else
            {
                return FRONTURL;
            }
        }
    }
}
