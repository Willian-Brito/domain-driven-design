using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Core.Messages.CommonMessages.DomainEvents;

public class DomainEvent : Event
{
    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}