using Microsoft.Extensions.DependencyInjection;
using Uth.Recipes.DataAccess;
using Uth.Recipes.DataAccess.Repositories;
using Uth.Recipes.Domain.Categories;
using Uth.Recipes.Domain.Infrastructure;
using Uth.Recipes.Domain.Ingredients;
using Uth.Recipes.Domain.Recipes;

namespace Uth.Recipes.IOC
{
    public class DependencyInjectionManager
    {
        public static ServiceProvider ConfigureProvider(IServiceCollection serviceCollection)
        {
            var serviceProvider = (serviceCollection ?? new ServiceCollection())
                .AddScoped<IRecipesDbContextProvider, RecipesDbContextProvider>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IIngredientRepository, IngredientRepository>()
                .AddScoped<IRecipeRepository, RecipeRepository>()
                .AddScoped<IAppSettingsProvider, AppSettingsProvider>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
