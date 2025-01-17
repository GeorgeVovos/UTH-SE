using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uth.Recipes.Domain.Recipes;

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
        public async Task<Recipe> Get(int id)
        {
            return await _recipeRepository.GetRecipeWithStepsAsync(id);
        }

        public class RecipeResource
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

}
