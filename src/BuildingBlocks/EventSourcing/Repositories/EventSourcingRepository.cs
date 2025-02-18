using EventSourcing.Services;
using EventStore.ClientAPI;
using NerdStore.Modules.Core.Data.EventSourcing;
using NerdStore.Modules.Core.Messages;
using Newtonsoft.Json;
using System.Text;

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
        var eventos = await _eventStoreService.GetConnection().ReadStreamEventsForwardAsync(
            aggregateId.ToString(),
            0,
            500,
            false   
        );

        var listaEventos = new List<StoredEvent>();

        foreach (var resolvedEvent in eventos.Events)
        {
            var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
            var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

            var evento = new StoredEvent(
                resolvedEvent.Event.EventId,
                resolvedEvent.Event.EventType,
                jsonData.Timestamp,
                dataEncoded
            );

            listaEventos.Add(evento);
        }

        return listaEventos.OrderBy(e => e.DataOcorrencia);
    }

    public async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
    {
        await _eventStoreService.GetConnection().AppendToStreamAsync(
            evento.AggregateId.ToString(),
            ExpectedVersion.Any,
            FormatarEvento(evento)
        );
    }

    private static IEnumerable<EventData> FormatarEvento<TEvento>(TEvento evento) where TEvento : Event
    {
        yield return new EventData(
            Guid.NewGuid(), 
            evento.MessageType, 
            true, 
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)), 
            null
        );
    }
}

internal class BaseEvent
{
    public DateTime Timestamp { get; set; }
}