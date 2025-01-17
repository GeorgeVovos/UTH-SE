using Uth.Recipes.Domain.Ingredients;

namespace Uth.Recipes.Domain.Steps
{
    public class StepIngredient
    {
        public string Quantity { get; set; } // It's string so we don't have to add units
        public int StepId { get; set; }
        public int IngredientId { get; set; }
        public Step Step { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
