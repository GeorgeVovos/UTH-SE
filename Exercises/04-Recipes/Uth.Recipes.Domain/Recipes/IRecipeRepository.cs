using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uth.Recipes.Domain.Recipes
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<Recipe> GetRecipeWithStepsAsync(int recipeId);
        Task<List<Recipe>> GetAllRecipes();
        Task DeleteRecipe(int recipeId);
        Task AddRecipe(Recipe recipe);
        Task EditRecipe(Recipe recipe);
    }
}