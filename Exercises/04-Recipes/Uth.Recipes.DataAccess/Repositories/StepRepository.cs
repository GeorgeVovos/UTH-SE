using Uth.Recipes.Domain.Steps;

namespace Uth.Recipes.DataAccess.Repositories
{

    public class StepRepository : BaseRepository, IStepRepository
    {
        public StepRepository(IRecipesDbContextProvider contextProvider) : base(contextProvider) { }
    }
}
