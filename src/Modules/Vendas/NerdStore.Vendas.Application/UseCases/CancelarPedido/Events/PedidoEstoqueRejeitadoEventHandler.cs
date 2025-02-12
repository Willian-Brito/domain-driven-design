using MediatR;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;

namespace NerdStore.Modules.Vendas.Application.UseCases.CancelarPedido.Events;

public class PedidoEstoqueRejeitadoEventHandler : INotificationHandler<PedidoEstoqueRejeitadoEvent>    
{
    public PedidoEstoqueRejeitadoEventHandler() { }

    public Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
    {
        // cancelar processamento do pedido - retornar erro para o cliente.
        return Task.CompletedTask;
    }
}