
using Codeflix.Catalogo.Domain.Entity;

namespace Codeflix.Catalogo.Domain.SeedWork;
public interface IGenericRepository<TAggregate> : IRepository
{
    public Task Add(TAggregate aggregat, CancellationToken cancellationToken);
}
