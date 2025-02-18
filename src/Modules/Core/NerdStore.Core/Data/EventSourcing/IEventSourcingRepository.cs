

using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Core.Data.EventSourcing;
public interface IEventSourcingRepository
{
    Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
    Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);
}