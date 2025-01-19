using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Domain.Steps;

namespace Uth.Recipes.Web.Pages
{
    public class RecipeManagementModel : PageModel
    {
        private readonly IRecipeRepository _recipeRepository;

        public List<Recipe> Recipes { get; set; }
        public List<SelectListItem> CategoryOptions { get; set; }

        public RecipeManagementModel(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
            Recipes = new List<Recipe>();
            CategoryOptions = new List<SelectListItem>();
        }

        public List<Recipe> FilteredRecipes => Recipes
            .Where(r => (string.IsNullOrWhiteSpace(SearchTerm) || (r.Name?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ?? false) || (r.Description?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ?? false))
                        && (CategoryFilter == "all" || CategoryFilter == null || r.Category.Name.Equals(CategoryFilter, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        [BindProperty]
        public string SearchTerm { get; set; } = string.Empty;

        [BindProperty]
        public string CategoryFilter { get; set; } = "all";

        public async Task OnGet()
        {
            await RefreshData();
        }

        private async Task RefreshData()
        {
            Recipes = await _recipeRepository.GetAllRecipesWithoutDependencies();
            CategoryOptions = Recipes.
                Where(x => x.Category != null).DistinctBy(x => x.Category).
                Select(c => new SelectListItem { Value = c.Category.Name, Text = c.Category.Name })
                .ToList();
            CategoryOptions.Insert(0, new SelectListItem("All", string.Empty, true));
        }

        public Recipe RecipeToDelete { get; set; }
        public async Task<IActionResult> OnPost(string action, int? RecipeId, int? DeleteRecipeId)
        {
            await RefreshData();
            switch (action)
            {
                case "Execute":
                    return RedirectToPage("/StepExecution", new { recipeId = RecipeId });
                case "Edit":
                    return RedirectToPage("/RecipeEdit", new { recipeId = RecipeId });
                case "AttemptDelete":
                    RecipeToDelete = Recipes.FirstOrDefault(r => r.Id == RecipeId);
                    break;
                case "ConfirmDelete":
                    if (DeleteRecipeId.HasValue)
                        await _recipeRepository.DeleteRecipe(RecipeId ?? DeleteRecipeId.Value);

                    break;
                case "Cancel":
                    break;
            }


            await RefreshData();

            return Page();
        }
    }
}
