using System.Collections.Generic;
using System.Linq;

namespace Uth.Recipes.Domain.RecipeExecution
{
    public class DurationBasedRecipeExecution : BaseRecipeExecution
    {
        public DurationBasedRecipeExecution(List<IRunnableOperation> steps) : base(steps)
        {
        }

        protected override double GetCurrentProgress()
        {
            if (Steps == null)
                return 0;

            if (!Steps?.Any() ?? false)
                return 0;

            var totalDuration = (double)Steps.Sum(s => s.Duration);
            if (totalDuration == 0)
                return 100;

            return (Steps.Where(s => s.Order <= (CurrentStepIndex - 1)).Sum(s => s.Duration) / totalDuration) * 100;
        }
    }
}