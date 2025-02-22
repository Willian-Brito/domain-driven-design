using EventStore.Client;
using Microsoft.Extensions.Configuration;

namespace EventSourcing.Services;
public class EventStoreService : IEventStoreService
{
    private readonly EventStoreClient _connection;

    public EventStoreService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EventStoreConnection");
        var settings = EventStoreClientSettings.Create(connectionString);
        _connection = new EventStoreClient(settings);
    }

    public EventStoreClient GetConnection() => _connection;
}