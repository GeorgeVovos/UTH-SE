using System;
using System.Threading;
using System.Threading.Tasks;

namespace Uth.Recipes.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}