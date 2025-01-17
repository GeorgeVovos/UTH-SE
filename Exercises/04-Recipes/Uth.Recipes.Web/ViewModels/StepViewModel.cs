using System.Collections.Generic;

namespace Uth.Recipes.Web.ViewModels
{
    public class StepViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int Duration { get; set; } // In minutes
        public List<StepIngredientViewModel> Ingredients { get; set; }
        public List<ImageViewModel> Images { get; set; }
    }
}