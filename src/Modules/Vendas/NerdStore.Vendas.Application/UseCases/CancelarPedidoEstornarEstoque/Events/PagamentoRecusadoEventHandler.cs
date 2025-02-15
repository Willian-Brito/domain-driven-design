using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;
using NerdStore.Modules.Vendas.Application.UseCases.CancelarPedidoEstornarEstoque.Commands;

namespace NerdStore.Modules.Vendas.Application.UseCases.CancelarPedidoEstornarEstoque.Events;

public class PagamentoRecusadoEventHandler : INotificationHandler<PagamentoRecusadoEvent>
{
    private readonly IMessageBus _messageBus;

    public PagamentoRecusadoEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task Handle(PagamentoRecusadoEvent message, CancellationToken cancellationToken)
    {
        await _messageBus.EnviarComando(
            new CancelarProcessamentoPedidoEstornarEstoqueCommand(                
                message.PedidoId, 
                message.ClienteId
            )
        );
    }
}