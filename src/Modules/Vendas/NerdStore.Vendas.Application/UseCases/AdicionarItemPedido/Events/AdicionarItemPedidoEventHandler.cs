using MediatR;

namespace NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Events;

public class AdicionarItemPedidoEventHandler 
    : INotificationHandler<PedidoRascunhoIniciadoEvent>,
    INotificationHandler<PedidoItemAdicionadoEvent>
{
    public AdicionarItemPedidoEventHandler() { }

    public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
    {
       return Task.CompletedTask;
    }

    public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}