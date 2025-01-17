using System;
using System.Configuration;

namespace Uth.Recipes.Domain.Infrastructure
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        public AppSettings GetAppSettings()
        {
            return new AppSettings()
            {
                DatabaseType = Enum.Parse<DatabaseType>(ConfigurationManager.AppSettings["DatabaseType"]),
                ConnectionString = ConfigurationManager.AppSettings["ConnectionString"]
            };

        }
    }
}
