namespace Uth.Recipes.DataAccess
{
    public interface IRecipesDbContextProvider
    {
        RecipesDbContext GetDbContext();

    }
}