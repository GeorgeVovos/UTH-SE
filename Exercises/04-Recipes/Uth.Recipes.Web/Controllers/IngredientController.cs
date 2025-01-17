using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uth.Recipes.Domain.Ingredients;

namespace Uth.Recipes.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        [HttpGet]
        public async Task<List<Ingredient>> Get()
        {
            return await _ingredientRepository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<Ingredient> Get(int id)
        {
            return await _ingredientRepository.GetIngredientById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            await _ingredientRepository.Create(new Ingredient(value));
        }
    }
}
