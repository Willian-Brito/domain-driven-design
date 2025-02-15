using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;
using NerdStore.Modules.Vendas.Application.UseCases.CancelarPedido.Commands;

namespace NerdStore.Modules.Vendas.Application.UseCases.CancelarPedido.Events;

public class PedidoEstoqueRejeitadoEventHandler : INotificationHandler<PedidoEstoqueRejeitadoEvent>    
{
    private readonly IMessageBus _messageBus;
    public PedidoEstoqueRejeitadoEventHandler(IMessageBus messageBus) 
    { 
        _messageBus = messageBus;
    }

    public async Task Handle(PedidoEstoqueRejeitadoEvent message, CancellationToken cancellationToken)
    {        
        await _messageBus.EnviarComando(new CancelarProcessamentoPedidoCommand(message.PedidoId, message.ClienteId));;
    }
}