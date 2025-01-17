using Microsoft.EntityFrameworkCore;

namespace Uth.Recipes.DataAccess.DatabaseProviders
{
    public class SqlServerDbContextProvider : BaseDbContextProvider
    {
        public SqlServerDbContextProvider(string connectionString) : base(connectionString) { }

        protected override void Configure(DbContextOptionsBuilder<RecipesDbContext> builder)
        {
            builder.UseSqlServer(ConnectionString);
        }
    }
}