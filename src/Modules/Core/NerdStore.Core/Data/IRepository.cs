
using NerdStore.Modules.Core.DomainObjects;

namespace NerdStore.Modules.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}