using Microsoft.EntityFrameworkCore;

namespace Uth.Recipes.DataAccess.DatabaseProviders
{
    public class MySqlDbContextProvider : BaseDbContextProvider
    {
        public MySqlDbContextProvider(string connectionString) : base(connectionString) { }

        protected override void Configure(DbContextOptionsBuilder<RecipesDbContext> builder)
        {
            builder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }
    }
}