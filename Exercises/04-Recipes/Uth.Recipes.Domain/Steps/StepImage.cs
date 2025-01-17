using Uth.Recipes.Domain.Images;

namespace Uth.Recipes.Domain.Steps
{
    public class StepImage
    {
        public int StepId { get; set; }
        public int ImageId { get; set; }
        public Step Step { get; set; } = null;
        public Image Image { get; set; } = null;
    }
}
