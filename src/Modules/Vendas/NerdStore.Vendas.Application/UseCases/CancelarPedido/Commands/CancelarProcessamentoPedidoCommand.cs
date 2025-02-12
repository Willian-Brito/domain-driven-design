using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Vendas.Application.Base;

namespace NerdStore.Modules.Vendas.Application.UseCases.CancelarPedido.Commands;

public class CancelarProcessamentoPedidoCommand : PedidoCommand
{    
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }

    public CancelarProcessamentoPedidoCommand(
        IMessageBus messageBus, 
        Guid pedidoId, 
        Guid clienteId
    ) : base (messageBus)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
    }
}

