using MediatR;

namespace NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Events;

public class AtualizarItemPedidoEventHandler 
    : INotificationHandler<PedidoAtualizadoEvent>    
{
    public AtualizarItemPedidoEventHandler() { }

    public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}