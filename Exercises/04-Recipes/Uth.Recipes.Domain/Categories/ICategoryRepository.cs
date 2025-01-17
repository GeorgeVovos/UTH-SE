using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uth.Recipes.Domain.Categories
{
    public interface ICategoryRepository :  IRepository<Category>
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryByName(string categoryName);
        Task<List<Category>> GetAll();
        Task<Category> GetCategoryById(int id);
        Task Create(Category category);
    }
}
