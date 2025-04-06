using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Uth.Recipes.Domain.Categories;

namespace Uth.Recipes.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IRecipesDbContextProvider contextProvider) : base(contextProvider)
        {
        }

        public async Task<Category> Add(Category category)
        {
            Category existingCategory = await GetCategoryByName(category.Name);
            if (existingCategory != null)
                return existingCategory;

            return Context.Categories.Add(category).Entity;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await Context.Categories.AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await Context.Categories.FirstOrDefaultAsync((x => x.Name == categoryName));
        }

        public async Task<List<Category>> GetAll()
        {
            return await Context.Categories.AsNoTracking().OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await Context.Categories.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Category category)
        {
            await Add(category);
            await Context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var category = Context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                Context.Categories.Remove(category);
                return Context.SaveChangesAsync();
            }

            throw new Exception("Category does not exist");
        }

        public Task<int> Update(int id, string categoryName)
        {
            var category = Context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                category.Name = new Category(categoryName).Name;
                return Context.SaveChangesAsync();
            }

            throw new Exception("Category does not exist");
        }
    }
}
