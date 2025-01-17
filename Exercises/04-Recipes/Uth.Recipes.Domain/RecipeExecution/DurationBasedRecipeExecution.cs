using System.Collections.Generic;
using System.Linq;
using Uth.Recipes.Domain.Recipes;

namespace Uth.Recipes.Domain.RecipeExecution
{
    public class DurationBasedRecipeExecution : BaseRecipeExecution
    {
        public DurationBasedRecipeExecution(List<RecipeStepExecutionData> steps) : base(steps)
        {
        }

        protected override double GetCurrentProgress() =>
            (Steps.Where(s => s.Order <= (CurrentStepIndex - 1)).Sum(s => s.Duration)
             / (double)Steps.Sum(s => s.Duration))
            * 100;

    }
}