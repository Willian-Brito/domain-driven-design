using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Core.Bus;

public interface IMessageBus
{
    Task PublicarEvento<T>(T evento) where T : Event;
}