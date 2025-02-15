using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Vendas.Application.UseCases.CancelarPedido.Commands;

public class CancelarProcessamentoPedidoCommand : Command
{    
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }

    public CancelarProcessamentoPedidoCommand(Guid pedidoId, Guid clienteId)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
    }
}

