using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Vendas.Application.UseCases.FinalizarPedido.Events;

public class PedidoFinalizadoEvent : Event
{
    public Guid PedidoId { get; private set; }

    public PedidoFinalizadoEvent(Guid pedidoId)
    {
        PedidoId = pedidoId;
        AggregateId = pedidoId;
    }
}