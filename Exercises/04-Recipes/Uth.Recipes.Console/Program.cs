using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Uth.Recipes.DataAccess;
using Uth.Recipes.Domain.Ingredients;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Domain.Steps;
using Uth.Recipes.IOC;

namespace Uth.Recipes.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var iocContainer = DependencyInjectionManager.ConfigureProvider(null);
            var dbContextProvider = iocContainer.GetRequiredService<IRecipesDbContextProvider>();

            var context = dbContextProvider.GetDbContext();
            await context.Database.EnsureCreatedAsync();

            var ingredientRepository = iocContainer.GetRequiredService<IIngredientRepository>();
            var recipeRepository = iocContainer.GetRequiredService<IRecipeRepository>();

            var category = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Main Course");


            Ingredient onion = new Ingredient("Onion");
            Ingredient tomato = new Ingredient("Tomato");
            await ingredientRepository.Add(onion);
            await ingredientRepository.Add(tomato);
            await ingredientRepository.UnitOfWork.SaveChangesAsync();


            var recipe = new Recipe
            {
                Name = "Spaghetti Carbonara",
                Description = "Classic Italian pasta dish",
                Difficulty = Difficulty.Medium,
                Category = category,
            };

            recipe.AddStep(new Step
            {
                Title = "Cook Pasta",
                Description = "Boil pasta in salted water",
                Order = 1,
                Duration = 10
            });
            recipe.AddStep(new Step
            {
                Title = "Prepare Sauce",
                Description = "Cook pancetta and mix with egg yolks",
                Order = 2,
                Duration = 15
            });
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            System.Console.WriteLine("Finished");

            System.Console.ReadLine();
        }
    }
}
