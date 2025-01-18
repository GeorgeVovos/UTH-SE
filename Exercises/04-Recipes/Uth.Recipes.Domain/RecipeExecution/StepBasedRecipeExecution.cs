using System.Collections.Generic;

namespace Uth.Recipes.Domain.RecipeExecution
{
    public class StepBasedRecipeExecution : BaseRecipeExecution
    {
        public StepBasedRecipeExecution(List<IRunnableOperation> steps) : base(steps)
        {
        }
        protected override double GetCurrentProgress() => (double)(CurrentStepIndex - 1) / Steps.Count * 100;
    }
}