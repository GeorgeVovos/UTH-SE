using System.Collections.Generic;
using Uth.Recipes.Domain.Recipes;

namespace Uth.Recipes.Domain.Steps
{
    public class Step
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

        /// <summary>
        /// Step duration in Minutes
        /// </summary>
        public int Duration { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public List<StepIngredient> Ingredients { get; private set; }
        public List<StepImage> Images { get; private set; }


        public Step()
        {
            Ingredients = new List<StepIngredient>();
            Images = new List<StepImage>();
        }

        public void AddIngredients(List<StepIngredient> stepIngredients)
        {
            if (stepIngredients == null)
                return;

            Ingredients.AddRange(stepIngredients);
        }

        public void AddImages(List<StepImage> images)
        {
            if (images == null)
                return;

            Images.AddRange(images);
        }
    }
}