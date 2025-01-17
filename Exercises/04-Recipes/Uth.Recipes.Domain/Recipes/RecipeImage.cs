using Uth.Recipes.Domain.Images;

namespace Uth.Recipes.Domain.Recipes
{
    public class RecipeImage
    {
        public int RecipeId { get; set; }
        public int ImageId { get; set; }
        public Recipe Recipe { get; set; } = null;
        public Image Image { get; set; } = null;
    }
}
