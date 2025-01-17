namespace Uth.Recipes.Domain.Recipes
{
    public class RecipeStepExecutionData
    {
        public readonly int Order;

        public readonly int Duration;
        public RecipeStepExecutionData(int order, int duration)
        {
            Order = order;
            Duration = duration;
        }
    }
}