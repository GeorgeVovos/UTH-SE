using System.Collections.Generic;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Domain.Steps;

namespace Uth.Recipes.Domain.RecipeExecution
{
    public class StepBasedRecipeExecution : BaseRecipeExecution
    {
        public StepBasedRecipeExecution(List<RecipeStepExecutionData> steps) : base(steps)
        {
        }
        protected override double GetCurrentProgress() => (double)(CurrentStepIndex - 1) / Steps.Count * 100;
    }
}