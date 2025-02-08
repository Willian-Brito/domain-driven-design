using NerdStore.Modules.Core.Messages;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Modules.Core.Communication.Mediator;

public interface IMessageBus
{
    Task PublicarEvento<T>(T evento) where T : Event;
    Task<bool> EnviarComando<T>(T comando) where T : Command;
    Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
}