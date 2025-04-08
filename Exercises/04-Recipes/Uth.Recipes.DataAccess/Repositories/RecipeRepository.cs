using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Uth.Recipes.Domain.Categories;
using Uth.Recipes.Domain.Images;
using Uth.Recipes.Domain.Ingredients;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Domain.Steps;

namespace Uth.Recipes.DataAccess.Repositories
{
    public class RecipeRepository : BaseRepository, IRecipeRepository
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeRepository(IRecipesDbContextProvider contextProvider,
            ICategoryRepository categoryRepository,
            IIngredientRepository ingredientRepository) : base(contextProvider)
        {
            _categoryRepository = categoryRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Recipe> GetRecipeWithStepsAsync(int recipeId)
        {
            return await FullRecipeQuery().FirstOrDefaultAsync(r => r.Id == recipeId);
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await FullRecipeQuery().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Recipe>> GetAllRecipesWithImages()
        {
            return await Context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Images)
                .ThenInclude(i => i.Image)
                .OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Recipe>> GetAllRecipesWithoutDependencies()
        {
            return await Context.Recipes
                .Include(r => r.Category).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task DeleteRecipe(int recipeId)
        {
            var recipe = await Context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
            if (recipe == null)
                return;

            Context.Remove(recipe);
            await Context.SaveChangesAsync();
        }

        public async Task<Recipe> AddRecipe(Recipe recipe)
        {
            if (recipe == null)
                return null;

            // All this logic is to help Entity Framework handle object tracking correctly and not add duplicate
            // Ingredient records in the Ingredients table (when we attach an object graph on the Context, everything is marked as a new object)
            // Categories as simple as we don't allow the user to create new categories on the UI

            recipe.Category = await _categoryRepository.GetCategoryByName(recipe.Category.Name);
            if (recipe.Steps != null)
                foreach (var step in recipe.Steps)
                {
                    if (step.Ingredients != null)
                        foreach (var stepIngredient in step.Ingredients)
                        {
                            var ingredientName = stepIngredient.Ingredient.Name;
                            stepIngredient.Ingredient = await _ingredientRepository.GetIngredientByName(ingredientName);
                            if (stepIngredient.Ingredient == null)
                            {
                                stepIngredient.Ingredient = new Ingredient(ingredientName);
                            }
                            else
                            {
                                Context.Entry(stepIngredient.Ingredient).State = EntityState.Unchanged;
                            }
                        }
                }

            Context.Recipes.Add(recipe);
            Context.Entry(recipe.Category).State = EntityState.Unchanged;

            await Context.SaveChangesAsync();
            return recipe;
        }

        public async Task EditRecipe(Recipe recipeModel)
        {
            var existingRecord = await GetRecipeWithStepsAsync(recipeModel.Id);
            if (existingRecord == null)
                throw new ArgumentException("Recipe:" + recipeModel.Id + " not found");

            existingRecord.Name = recipeModel.Name;
            existingRecord.Description = recipeModel.Description;
            existingRecord.CategoryId = (await _categoryRepository.GetCategoryByName(recipeModel.Category.Name)).Id;
            existingRecord.Difficulty = recipeModel.Difficulty;

            // All this logic is to help Entity Framework handle object tracking correctly and not add duplicate
            // Ingredient records in the Ingredients table (when we attach an object graph on the Context, everything is marked as a new object)
            // Something similar for images
            // Categories as simple as we don't allow the user to create new categories on the UI


            if (recipeModel.Images != null && recipeModel.Images.Any())
            {
                foreach (var image in existingRecord.Images)
                    Context.Entry(image).State = EntityState.Deleted;

                foreach (var newImage in recipeModel.Images)
                    existingRecord.Images.Add(newImage);
            }

            Dictionary<int, List<StepImage>> existingStepImages = new Dictionary<int, List<StepImage>>();
            if (existingRecord.Steps != null && existingRecord.Steps.Any())
            {
                foreach (var step in existingRecord.Steps)
                {
                    Context.Entry(step).State = EntityState.Deleted;
                    if (step.Images?.Any() ?? false)
                    {
                        existingStepImages.Add(step.Order, step.Images.ToList());
                    }
                }

                if (recipeModel.Steps != null && recipeModel.Steps.Any())

                    foreach (var newStep in recipeModel.Steps)
                    {
                        if (newStep.Ingredients != null)
                        {
                            foreach (var stepIngredient in newStep.Ingredients)
                            {
                                var ingredientName = stepIngredient.Ingredient.Name;
                                var existingIngredient =
                                    await _ingredientRepository.GetIngredientByName(ingredientName);
                                if (existingIngredient != null)
                                {
                                    stepIngredient.Ingredient = null;
                                    stepIngredient.IngredientId = existingIngredient.Id;
                                }
                            }
                        }

                        if ((recipeModel.Images == null || !newStep.Images.Any()))
                        {
                            if (existingStepImages.ContainsKey(newStep.Order))
                            {
                                var images = existingStepImages[newStep.Order];
                                newStep.AddImages(images.Select(img => new StepImage()
                                { Image = new Image() { Data = img.Image.Data, Name = img.Image.Name } }).ToList());
                            }

                        }

                        existingRecord.AddStep(newStep);
                    }
            }
            else
            {
                existingRecord.AddSteps(recipeModel.Steps);
            }

            await Context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var recipe = Context.Recipes.FirstOrDefault(x => x.Id == id);
            if (recipe != null)
            {
                Context.Recipes.Remove(recipe);
                return Context.SaveChangesAsync();
            }

            throw new Exception("Recipes does not exist");
        }

        private IIncludableQueryable<Recipe, Ingredient> FullRecipeQuery()
        {
            return Context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Images)
                .ThenInclude(r => r.Image)
                .Include(r => r.Steps)
                .ThenInclude(s => s.Images)
                .ThenInclude(s => s.Image)
                .Include(s => s.Steps)
                .ThenInclude(s => s.Ingredients)
                .ThenInclude(r => r.Ingredient);
        }

    }
}
