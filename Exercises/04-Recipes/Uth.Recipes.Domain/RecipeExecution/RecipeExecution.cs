using System.Collections.Generic;
using System.Linq;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Domain.Steps;

namespace Uth.Recipes.Domain.RecipeExecution
{
    public abstract class BaseRecipeExecution
    {
        protected readonly List<RecipeStepExecutionData> Steps;

        protected BaseRecipeExecution(List<RecipeStepExecutionData> steps)
        {
            Steps = steps;
        }

        public int CurrentStepIndex { get; private set; } = 1;

        protected abstract double GetCurrentProgress();

        public double CurrentProgress => (Steps?.Any() ?? false) ? GetCurrentProgress() : 100;

        public void MoveToStep(int index)
        {
            CurrentStepIndex = index;
        }

        public void MoveToNextStep()
        {
            if (Steps.Count >= CurrentStepIndex)
                CurrentStepIndex++;
        }

        public void MoveToPreviousStep()
        {
            if (CurrentStepIndex >= 2 && (Steps?.Any() ?? false))
                CurrentStepIndex--;
        }

        public bool IsCompleted()
        {
            return CurrentStepIndex > Steps.Count;
        }

    }
}