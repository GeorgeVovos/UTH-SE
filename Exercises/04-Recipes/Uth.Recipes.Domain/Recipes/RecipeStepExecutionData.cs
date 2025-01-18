using Uth.Recipes.Domain.RecipeExecution;

namespace Uth.Recipes.Domain.Recipes
{
    public class RecipeStepExecutionData : IRunnableOperation
    {
        public int Order { get; }

        public int Duration { get; }
        public RecipeStepExecutionData(int order, int duration)
        {
            Order = order;
            Duration = duration;
        }
    }
}