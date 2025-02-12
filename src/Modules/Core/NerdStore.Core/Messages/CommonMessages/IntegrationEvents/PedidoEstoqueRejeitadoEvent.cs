namespace NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;

public class PedidoEstoqueRejeitadoEvent : IntegrationEvent
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }

    public PedidoEstoqueRejeitadoEvent(Guid pedidoId, Guid clienteId)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
    }
}