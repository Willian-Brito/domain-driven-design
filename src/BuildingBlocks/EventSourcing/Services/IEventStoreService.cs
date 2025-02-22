using EventStore.Client;

namespace EventSourcing.Services;
public interface IEventStoreService
{
    EventStoreClient GetConnection();
}