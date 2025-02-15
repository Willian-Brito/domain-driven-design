using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Vendas.Application.UseCases.AplicarVoucher.Events;
public class VoucherAplicadoPedidoEvent : Event
{
    public Guid ClienteId { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid VoucherId { get; private set; }

    public VoucherAplicadoPedidoEvent(Guid clienteId, Guid pedidoId, Guid voucherId)
    {
        AggregateId = pedidoId;
        ClienteId = clienteId;
        PedidoId = pedidoId;
        VoucherId = voucherId;
    }
}