using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uth.Recipes.Domain.Categories;

namespace Uth.Recipes.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<List<Category>> Get()
        {
            return await _categoryRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        [HttpPost]
        public async Task<PostApiResponse> Post([FromBody] string value)
        {
            return new PostApiResponse()
            {
                Id = (await _categoryRepository.Create(new Category(value)))?.Id
            };
        }

        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] string value)
        {
            return await _categoryRepository.Update(id, value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _categoryRepository.Delete(id);
        }
    }
}
