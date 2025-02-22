using EventSourcing.Services;
using EventStore.Client;
using NerdStore.Modules.Core.Data.EventSourcing;
using NerdStore.Modules.Core.Messages;
using System.Text;
using System.Text.Json;

namespace EventSourcing.Repositories;

public class EventSourcingRepository : IEventSourcingRepository
{    
    private readonly IEventStoreService _eventStoreService;
    
    public EventSourcingRepository(IEventStoreService eventStoreService)
    {        
        _eventStoreService = eventStoreService;
    }

    public async Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId)
    {
        var eventStore = _eventStoreService.GetConnection();
        var stream = eventStore.ReadStreamAsync(
            Direction.Forwards,
            aggregateId.ToString(),
            StreamPosition.Start,
            maxCount: 500
        );

        var eventos = new List<StoredEvent>();

        if (await stream.ReadState == ReadState.StreamNotFound)
                return eventos;

        await foreach (var resolvedEvent in stream)
        {
            var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
            var json = JsonSerializer.Deserialize<BaseEvent>(dataEncoded);

            var storedEvent = new StoredEvent(
                resolvedEvent.Event.EventId.ToGuid(),
                resolvedEvent.Event.EventType,
                json.Timestamp,
                dataEncoded
            );

            eventos.Add(storedEvent);
        }

        return eventos.OrderBy(o => o.DataOcorrencia);
    }

    public async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
    {
        var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(evento);

        var eventData = new EventData( 
            Uuid.NewUuid(),
            evento.MessageType,
            utf8Bytes.AsMemory() 
        );

        var eventStore = _eventStoreService.GetConnection();
        await eventStore.AppendToStreamAsync( 
            evento.AggregateId.ToString(),
            StreamState.Any,
            new[ ] { eventData } 
        );    
    }
}

internal class BaseEvent
{
    public DateTime Timestamp { get; set; }
}