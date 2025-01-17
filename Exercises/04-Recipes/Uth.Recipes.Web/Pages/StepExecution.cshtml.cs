using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uth.Recipes.Domain.RecipeExecution;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Web.ViewModels;

namespace Uth.Recipes.Web.Pages
{
    public class StepExecutionModel : BaseRecipePageModel
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeViewModel Recipe { get; set; }
        public StepBasedRecipeExecution StepBasedRecipeExecution { get; set; }
        public DurationBasedRecipeExecution DurationBasedRecipeExecution { get; set; }
        public int SelectedRecipeId { get; set; }

        public StepExecutionModel(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;

        }

        public async Task OnGet(int? recipeId, int? step)
        {
            if (recipeId != null)
            {
                this.Recipe = RecipeViewModel.MapFromRecipe(await _recipeRepository.GetRecipeWithStepsAsync(recipeId.Value));
                var stepExecutionData = this.Recipe?.Steps.Select(s => new RecipeStepExecutionData(s.Order, s.Duration)).ToList();
                StepBasedRecipeExecution = new StepBasedRecipeExecution(stepExecutionData);
                DurationBasedRecipeExecution = new DurationBasedRecipeExecution(stepExecutionData);
                SetEmptyImagesIfNecessary(this.Recipe);
            }


            if (recipeId.HasValue)
            {
                SelectedRecipeId = recipeId.Value;
                step = step ?? 1;
                StepBasedRecipeExecution.MoveToStep(step.Value);
                DurationBasedRecipeExecution.MoveToStep(step.Value);
            }
        }

        public StepViewModel CurrentStep
        {
            get
            {
                if ((Recipe?.Steps?.Count ?? 0) == 0)
                {
                    return new StepViewModel
                    {
                        Id = 0,
                        Title = "No steps found, please add steps to your recipe",
                        Description = "No more steps",
                        Duration = 0,
                        Images = new List<ImageViewModel>() { NoImageData },
                        Ingredients = new List<StepIngredientViewModel>()
                    };
                }

                if (StepBasedRecipeExecution.IsCompleted())
                {
                    return new StepViewModel
                    {
                        Id = 0,
                        Title = "Congratulations",
                        Description = "Enough you meal",
                        Duration = 0,
                        Images = new List<ImageViewModel>() { NoImageData },
                        Ingredients = new List<StepIngredientViewModel>()
                    };
                }

                if (Recipe?.Steps?.Count > 0 && StepBasedRecipeExecution.CurrentStepIndex > 0 && StepBasedRecipeExecution.CurrentStepIndex <= Recipe.Steps.Count)
                {
                    return Recipe.Steps[StepBasedRecipeExecution.CurrentStepIndex - 1];
                }

                return new StepViewModel
                {
                    Id = 0,
                    Title = "No steps",
                    Description = "No more steps",
                    Duration = 0,
                    Images = new List<ImageViewModel>() { NoImageData },
                    Ingredients = new List<StepIngredientViewModel>()
                };
            }
        }
    }
}
