using Microsoft.EntityFrameworkCore;

namespace Uth.Recipes.DataAccess.DatabaseProviders
{
    public abstract class BaseDbContextProvider : IRecipesDbContextProvider
    {
        protected readonly string ConnectionString;

        protected BaseDbContextProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public RecipesDbContext GetDbContext()
        {
            DbContextOptionsBuilder<RecipesDbContext> builder = new DbContextOptionsBuilder<RecipesDbContext>();
            Configure(builder);
            return new RecipesDbContext(builder.Options);
        }

        protected abstract void Configure(DbContextOptionsBuilder<RecipesDbContext> builder);
    }
}