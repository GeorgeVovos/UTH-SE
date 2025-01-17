using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Uth.Recipes.Domain.Categories;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Web.ViewModels;

namespace Uth.Recipes.Web.Pages
{
    public class RecipeRecipeEditModel : PageModel
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;

        [BindProperty]
        public RecipeViewModel ViewModel { get; set; }

        public RecipeRecipeEditModel(IRecipeRepository recipeRepository, ICategoryRepository categoryRepository)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task OnGetAsync(int? recipeId)
        {
            ViewModel = new RecipeViewModel();
            if (recipeId != null)
            {
                var recipe = await _recipeRepository.GetRecipeWithStepsAsync(recipeId.Value);
                ViewModel = RecipeViewModel.MapFromRecipe(recipe);
            }

            await LoadCategories();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return Page();
            }

            Recipe recipe = await ViewModel.MapToRecipe(Request);
            if (recipe.Id == 0)
                await _recipeRepository.AddRecipe(recipe);
            else
                await _recipeRepository.EditRecipe(recipe);

            return RedirectToPage("RecipeManagement");
        }

        private async Task LoadCategories()
        {
            ViewModel.Categories = (await this._categoryRepository.GetAllCategories()).Select(c => new SelectListItem { Value = c.Name, Text = c.Name }).ToList();
        }
    }
}
