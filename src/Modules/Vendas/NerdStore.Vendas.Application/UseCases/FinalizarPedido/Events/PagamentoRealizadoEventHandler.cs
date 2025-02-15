using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;
using NerdStore.Modules.Vendas.Application.UseCases.FinalizarPedido.Commands;

namespace NerdStore.Modules.Vendas.Application.UseCases.FinalizarPedido.Events;

public class PagamentoRealizadoEventHandler : INotificationHandler<PagamentoRealizadoEvent>
{
    private readonly IMessageBus _messageBus;

    public PagamentoRealizadoEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task Handle(PagamentoRealizadoEvent message, CancellationToken cancellationToken)
    {
        await _messageBus.EnviarComando(new FinalizarPedidoCommand(message.PedidoId, message.ClienteId));
    }
}