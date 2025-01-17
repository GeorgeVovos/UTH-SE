using Moq;
using Testcontainers.MsSql;
using Uth.Recipes.DataAccess;
using Uth.Recipes.DataAccess.Repositories;
using Uth.Recipes.Domain.Categories;
using Uth.Recipes.Domain.Infrastructure;
using Uth.Recipes.Domain.Ingredients;

namespace Uth.Recipes.IntegrationTests
{
    public sealed class RecipeRepositoryTests : IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();

        private IRecipesDbContextProvider contextProvider;
        private ICategoryRepository categoryRepository;
        private IIngredientRepository ingredientRepository;
        private Mock<IAppSettingsProvider> mockAppSettingsProvider;

        [Fact]
        public async Task ReadFromMsSqlDatabase()
        {
            //Arrange
            await InitDependencies();

            RecipeRepository recipeRepository = new RecipeRepository(contextProvider, categoryRepository, ingredientRepository);

            //Act
            var recipes = await recipeRepository.GetAllRecipes();

            //Assert
            Assert.Equal(5, recipes.Count);
        }

        public Task InitializeAsync()
        {
            return _msSqlContainer.StartAsync();
        }

        public Task DisposeAsync() => _msSqlContainer.DisposeAsync().AsTask();

        private async Task InitDependencies()
        {
            mockAppSettingsProvider = new Mock<IAppSettingsProvider>();
            mockAppSettingsProvider.Setup(x => x.GetAppSettings()).Returns(new AppSettings()
            {
                DatabaseType = DatabaseType.SqlServer,
                ConnectionString = _msSqlContainer.GetConnectionString()
            });

            contextProvider = new RecipesDbContextProvider(mockAppSettingsProvider.Object);
            categoryRepository = new CategoryRepository(contextProvider);
            ingredientRepository = new IngredientRepository(contextProvider);
            await contextProvider.GetDbContext().Database.EnsureCreatedAsync();
        }
    }
}