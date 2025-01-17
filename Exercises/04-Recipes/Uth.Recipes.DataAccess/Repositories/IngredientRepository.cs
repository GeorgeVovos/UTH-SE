using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
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
            return await Context.Ingredients.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Ingredient ingredient)
        {
            await Add(ingredient);
            await Context.SaveChangesAsync();
        }
    }
}
