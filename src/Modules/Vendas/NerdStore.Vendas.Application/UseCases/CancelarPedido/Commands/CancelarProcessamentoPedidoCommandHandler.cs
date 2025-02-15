using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Domain.Repositories;

namespace NerdStore.Modules.Vendas.Application.UseCases.CancelarPedido.Commands;

public class CancelarProcessamentoPedidoCommandHandler : IRequestHandler<CancelarProcessamentoPedidoCommand, bool>
{    
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMessageBus _messageBus;


    public CancelarProcessamentoPedidoCommandHandler(IPedidoRepository pedidoRepository, IMessageBus messageBus)
    {
        _pedidoRepository = pedidoRepository;
        _messageBus = messageBus;
    }

    public async Task<bool> Handle(CancelarProcessamentoPedidoCommand message, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.ObterPorId(message.PedidoId);

        if (pedido is null)
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("Pedido", "Pedido não encontrado."));
            return false;
        }

        pedido.TornarRascunho();

        return await _pedidoRepository.UnitOfWork.Commit();
    }
}

