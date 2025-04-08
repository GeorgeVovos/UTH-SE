using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uth.Recipes.Domain.Recipes
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<Recipe> GetRecipeWithStepsAsync(int recipeId);
        Task<List<Recipe>> GetAllRecipes();
        Task<List<Recipe>> GetAllRecipesWithoutDependencies();
        Task<List<Recipe>> GetAllRecipesWithImages();

        Task DeleteRecipe(int recipeId);
        Task<Recipe> AddRecipe(Recipe recipe);
        Task EditRecipe(Recipe recipe);
        Task Delete(int id);
    }
}