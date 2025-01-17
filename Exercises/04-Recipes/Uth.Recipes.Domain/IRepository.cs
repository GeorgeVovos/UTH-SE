namespace Uth.Recipes.Domain
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}