using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uth.Recipes.Domain.Categories;
using Uth.Recipes.Domain.Images;
using Uth.Recipes.Domain.Ingredients;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Domain.Steps;

namespace Uth.Recipes.Web.ViewModels
{
    public class RecipeViewModel
    {
        public RecipeViewModel() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public Difficulty Difficulty { get; set; }
        public string Description { get; set; }
        public List<StepViewModel> Steps { get; set; }
        public List<ImageViewModel> Images { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public int GetTotalDuration()
        {
            return Steps?.Any() ?? false ? Steps.Sum(s => s.Duration) : 0;
        }

        public List<string> GetAllIngredients()
        {
            return Steps?.Any() ?? false ?
                Steps.SelectMany(s => s.Ingredients.Select(i => i.Name)).Distinct().ToList()
                : new List<string>();
        }

        public static RecipeViewModel MapFromRecipe(Recipe recipe)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel()
            {
                Name = recipe.Name,
                CategoryName = recipe.Category.Name,
                Description = recipe.Description,
                Difficulty = recipe.Difficulty,
                Id = recipe.Id,
                Images = recipe.Images?.Select(i => new ImageViewModel() { Data = i.Image.Data, Name = i.Image.Name }).ToList(),
                Steps = recipe.Steps
                    ?.Select(s => new StepViewModel()
                    {
                        Description = s.Description,
                        Title = s.Title,
                        Duration = s.Duration,
                        Id = s.Id,
                        Order = s.Order,
                        Images = s.Images?.Select(i => new ImageViewModel() { Data = i.Image.Data, Name = i.Image.Name }).ToList(),
                        Ingredients = Enumerable.Select(s.Ingredients, i => new StepIngredientViewModel()
                        {
                            Name = i.Ingredient.Name,
                            Quantity = i.Quantity
                        }).ToList()
                    }).ToList()
            };

            return recipeViewModel;
        }

        public async Task<Recipe> MapToRecipe(HttpRequest request)
        {
            await LoadImageStreams(request);

            Recipe newRecipe = new Recipe()
            {
                Name = Name,
                Category = new Category(CategoryName),
                Description = Description,
                Difficulty = Difficulty,
                Id = Id
            };

            newRecipe.AddImages(Images?.Select(i => new RecipeImage() { Image = new Image() { Data = i.Data, Name = i.Name } }).ToList());
            newRecipe.AddSteps(Steps?.Select((s, i) =>
            {
                var newStep = new Step()
                {
                    Description = s.Description,
                    Duration = s.Duration,
                    Order = i + 1,
                    Title = s.Title,
                    Id = s.Id,
                };
                newStep.AddImages(s.Images?.Select(img => new StepImage() { Image = new Image { Data = img.Data, Name = img.Name } }).ToList());
                newStep.AddIngredients(s.Ingredients?.Select(si => new StepIngredient { Quantity = si.Quantity, Ingredient = new Ingredient(si.Name) }).ToList());
                return newStep;
            }).ToList());

            return newRecipe;
        }

        private async Task LoadImageStreams(HttpRequest request)
        {
            var recipeImages = request.Form.Files.Where(x => x.Name.Contains("ViewModel.Image")).Select(x => x).ToList();

            if (recipeImages?.Count() != 0)
            {
                foreach (var file in recipeImages)
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    var image = new ImageViewModel()
                    {
                        Name = file.FileName,
                        Data = memoryStream.ToArray()
                    };
                    Images.Add(image);
                }
            }


            if (Steps != null)
            {
                foreach (var step in Steps)
                {

                    step.Order = Steps.IndexOf(step);
                    var stepsImages = request.Form.Files.Where(x => x.Name.Contains("ViewModel.Steps")).Select(x => x);
                    if (stepsImages?.Count() > 0)
                    {
                        step.Images = new List<ImageViewModel>();

                        foreach (var file in request.Form.Files.Where(f => f.Name.Contains($"ViewModel.Steps[{step.Order}].Images")))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);
                                var image = new ImageViewModel
                                {
                                    Name = file.FileName,
                                    Data = memoryStream.ToArray()
                                };
                                step.Images.Add(image);
                            }
                        }
                    }
                }
            }
        }
    }
}
