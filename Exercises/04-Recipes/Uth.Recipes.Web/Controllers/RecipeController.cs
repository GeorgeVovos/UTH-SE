using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Web.ViewModels;

namespace Uth.Recipes.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        public async Task<List<RecipeResource>> Get()
        {
            return (await _recipeRepository.GetAllRecipes()).Select(x => new RecipeResource { Id = x.Id, Name = x.Name }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<RecipeViewModel> Get(int id)
        {
            var recipe = await _recipeRepository.GetRecipeWithStepsAsync(id);
            return RecipeViewModel.MapFromRecipe(recipe);
        }

        [HttpPost]
        public async Task<PostApiResponse> Post([FromBody] RecipeViewModel model)
        {
            if (model == null)
                throw new ArgumentException("Request body must not be null");

            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ArgumentException("A recipe must have a name");

            Recipe recipe = await model.MapToRecipe(null);
            
            return new PostApiResponse()
            {
                Id = (await _recipeRepository.AddRecipe(recipe))?.Id
            };
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] RecipeViewModel model)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than 0", nameof(id));

            model.Id = id;
            Recipe recipe = await model.MapToRecipe(null);

            await _recipeRepository.EditRecipe(recipe);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _recipeRepository.Delete(id);
        }

        public class RecipeResource
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

}
