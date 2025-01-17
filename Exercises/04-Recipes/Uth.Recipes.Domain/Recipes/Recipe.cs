using System.Collections.Generic;
using System.Linq;
using Uth.Recipes.Domain.Categories;
using Uth.Recipes.Domain.Images;
using Uth.Recipes.Domain.Ingredients;
using Uth.Recipes.Domain.Steps;

namespace Uth.Recipes.Domain.Recipes
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Difficulty Difficulty { get; set; }
        public string Description { get; set; }
        public List<Step> Steps { get; private set; }
        public List<RecipeImage> Images { get; private set; }

        public Recipe()
        {
            Steps = new List<Step>();
            Images = new List<RecipeImage>();
        }

        public void AddStep(Step step)
        {
            if (step == null)
                return;

            Steps.Add(step);
        }

        public void AddSteps(List<Step> steps)
        {
            if (steps == null)
                return;

            Steps.AddRange(steps);
        }

        public void AddImages(List<RecipeImage> images)
        {
            if (images == null)
                return;

            Images.AddRange(images);
        }
    }
}
