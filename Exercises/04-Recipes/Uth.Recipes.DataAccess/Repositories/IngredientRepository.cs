using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Uth.Recipes.Domain.Categories;
using Uth.Recipes.Domain.Ingredients;

namespace Uth.Recipes.DataAccess.Repositories
{
    public class IngredientRepository : BaseRepository, IIngredientRepository
    {
        public IngredientRepository(IRecipesDbContextProvider contextProvider) : base(contextProvider) { }

        public async Task<Ingredient> Add(Ingredient ingredient)
        {
            Ingredient existingIngredient = await GetIngredientByName(ingredient.Name);
            if (existingIngredient != null)
                return existingIngredient;

            return Context.Ingredients.Add(ingredient).Entity;

        }

        public async Task<Ingredient> GetIngredientByName(string ingredientName)
        {
            return await Context.Ingredients.Where(b => b.Name == ingredientName).FirstOrDefaultAsync();
        }

        public async Task<List<Ingredient>> GetAll()
        {
            return await Context.Ingredients.AsNoTracking().OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {
            var ingredient = await Context.Ingredients.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (ingredient == null)
            {
                throw new Exception("Ingredient does not exist");
            }

            return ingredient;
        }

        public async Task<Ingredient> Create(Ingredient ingredient)
        {
            var entity = await Add(ingredient);
            await Context.SaveChangesAsync();
            return entity;
        }

        public Task Delete(int id)
        {
            var ingredient = Context.Ingredients.FirstOrDefault(x => x.Id == id);
            if (ingredient != null)
            {
                Context.Ingredients.Remove(ingredient);
                return Context.SaveChangesAsync();
            }

            throw new Exception("Ingredient does not exist");
        }

        public Task<int> Update(int id, string ingredientName)
        {
            var ingredient = Context.Ingredients.FirstOrDefault(x => x.Id == id);
            if (ingredient != null)
            {
                ingredient.Name = new Ingredient(ingredientName).Name;
                return Context.SaveChangesAsync();
            }

            throw new Exception("Ingredient does not exist");
        }
    }
}
