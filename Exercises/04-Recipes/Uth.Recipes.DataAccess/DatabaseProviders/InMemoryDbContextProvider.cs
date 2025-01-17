using Microsoft.EntityFrameworkCore;

namespace Uth.Recipes.DataAccess.DatabaseProviders
{
    public class InMemoryDbContextProvider : IRecipesDbContextProvider
    {
        public RecipesDbContext GetDbContext()
        {
            DbContextOptionsBuilder<RecipesDbContext> builder = new DbContextOptionsBuilder<RecipesDbContext>();
            builder.UseInMemoryDatabase("InMemoryDbName");
            return new RecipesDbContext(builder.Options);
        }
    }
}