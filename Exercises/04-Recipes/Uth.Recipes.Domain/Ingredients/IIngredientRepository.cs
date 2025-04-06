using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uth.Recipes.Domain.Ingredients
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<Ingredient> Add(Ingredient ingredient);
        Task<Ingredient> GetIngredientByName(string name);
        Task<List<Ingredient>> GetAll();
        Task<Ingredient> GetIngredientById(int id);
        Task Create(Ingredient ingredient);

        Task Delete(int id);
        Task<int> Update(int id, string ingredientName);
    }
}