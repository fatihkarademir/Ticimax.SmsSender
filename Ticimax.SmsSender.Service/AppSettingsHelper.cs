using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticimax.SmsSender.Models;

namespace Ticimax.SmsSender.Service
{
    public static class AppSettingsHelper
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public static AppSettingsModel GetAppSettingsModel()
        {
            var settings = GetConfig();

            return settings.GetSection("AppSettingsModel") as AppSettingsModel;
        }
    }
}
