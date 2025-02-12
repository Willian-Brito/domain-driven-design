using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Modules.Vendas.Application.Base;

public class PedidoCommand : Command
{
    private readonly IMessageBus _messageBus;

    public PedidoCommand(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public bool ValidarComando()
    {
        if (this.EhValido()) return true;

        foreach (var error in this.ValidationResult.Errors)
        {
            _messageBus.PublicarNotificacao(new DomainNotification(this.MessageType, error.ErrorMessage));
        }

        return false;
    }
}