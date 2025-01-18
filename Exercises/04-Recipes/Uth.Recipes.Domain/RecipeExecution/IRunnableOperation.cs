namespace Uth.Recipes.Domain.RecipeExecution
{
    public interface IRunnableOperation
    {
        int Order { get; }

        int Duration { get; }
    }
}