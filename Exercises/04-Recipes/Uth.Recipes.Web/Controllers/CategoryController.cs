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
        public async Task Post([FromBody] string value)
        {
            await _categoryRepository.Create(new Category(value));
        }
    }
}
