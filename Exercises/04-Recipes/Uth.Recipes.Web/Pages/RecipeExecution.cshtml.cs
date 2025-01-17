using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Web.ViewModels;

namespace Uth.Recipes.Web.Pages
{
    public class RecipeExecutionModel : BaseRecipePageModel
    {
        private readonly IRecipeRepository _recipeRepository;

        [BindProperty]
        public string SelectedRecipeId { get; set; }

        public List<SelectListItem> RecipeList { get; set; }

        public RecipeViewModel SelectedRecipe { get; set; }

        public RecipeExecutionModel(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task OnGet(string recipeId = null)
        {
            var allRecipes = (await _recipeRepository.GetAllRecipes()).Select(RecipeViewModel.MapFromRecipe).ToList();
            foreach (var recipe in allRecipes)
                SetEmptyImagesIfNecessary(recipe);
            
            RecipeList = CreateRecipeListItems(allRecipes);

            if (!string.IsNullOrEmpty(recipeId))
            {
                SelectedRecipe = allRecipes.FirstOrDefault(r => r.Id.ToString() == recipeId);
                SelectedRecipeId = recipeId;
            }
        }

        private static List<SelectListItem> CreateRecipeListItems(List<RecipeViewModel> allRecipes)
        {
            return allRecipes
                .Select(recipe => new SelectListItem
                {
                    Value = recipe.Id.ToString(),
                    Text = recipe.Name + " - " + recipe.Description
                })
                .ToList();
        }
    }

}
