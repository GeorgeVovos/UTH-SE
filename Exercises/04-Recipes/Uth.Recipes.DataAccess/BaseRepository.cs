using Uth.Recipes.Domain;

namespace Uth.Recipes.DataAccess
{
    public abstract class BaseRepository
    {
        protected readonly RecipesDbContext Context;
        public IUnitOfWork UnitOfWork => Context;

        protected BaseRepository(IRecipesDbContextProvider contextProvider)
        {
            Context = contextProvider.GetDbContext();
        }
    }
}
