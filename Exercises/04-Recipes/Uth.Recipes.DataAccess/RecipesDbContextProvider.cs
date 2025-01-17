using System;
using Uth.Recipes.DataAccess.DatabaseProviders;
using Uth.Recipes.Domain.Infrastructure;

namespace Uth.Recipes.DataAccess
{
    public class RecipesDbContextProvider : IRecipesDbContextProvider
    {
        private readonly IAppSettingsProvider _appSettingsProvider;
        public RecipesDbContextProvider(IAppSettingsProvider appSettingsProvider)
        {
            _appSettingsProvider = appSettingsProvider;
        }

        public RecipesDbContext GetDbContext()
        {
            IRecipesDbContextProvider provider;
            if (_appSettingsProvider.GetAppSettings().DatabaseType == DatabaseType.SqlServer)
                provider = new SqlServerDbContextProvider(_appSettingsProvider.GetAppSettings().ConnectionString);
            else if (_appSettingsProvider.GetAppSettings().DatabaseType == DatabaseType.MySql)
                provider = new MySqlDbContextProvider(_appSettingsProvider.GetAppSettings().ConnectionString);
            else if (_appSettingsProvider.GetAppSettings().DatabaseType == DatabaseType.InMemory)
                provider = new InMemoryDbContextProvider();
            else
                throw new ArgumentException("Unknown database type");

            return provider.GetDbContext();
        }
    }
}
