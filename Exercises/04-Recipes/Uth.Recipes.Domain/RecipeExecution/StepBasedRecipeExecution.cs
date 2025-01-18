using System.Collections.Generic;

namespace Uth.Recipes.Domain.RecipeExecution
{
    public class StepBasedRecipeExecution : BaseRecipeExecution
    {
        StepBasedRecipeExecution() : base(steps)
        {
        }
        protected override double GetCurrentProgress() => (double)(CurrentStepIndex - 1) / Steps.Count * 100;
    }
}