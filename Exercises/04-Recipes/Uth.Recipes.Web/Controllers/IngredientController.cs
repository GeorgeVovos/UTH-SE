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
        public async Task<PostApiResponse> Post([FromBody] string value)
        {
            return new PostApiResponse()
            {
                Id = (await _ingredientRepository.Create(new Ingredient(value)))?.Id
            };
        }

        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] string value)
        {
            return await _ingredientRepository.Update(id, value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _ingredientRepository.Delete(id);
        }
    }
}
