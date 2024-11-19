
using Codeflix.Catalogo.Domain.Entity;

namespace Codeflix.Catalogo.Domain.SeedWork;
public interface IGenericRepository<TAggregate> : IRepository
{
    public Task Insert(TAggregate aggregat, CancellationToken cancellationToken);
}
