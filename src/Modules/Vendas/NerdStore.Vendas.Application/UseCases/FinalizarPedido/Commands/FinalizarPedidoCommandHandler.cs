using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.FinalizarPedido.Events;
using NerdStore.Modules.Vendas.Domain.Repositories;

namespace NerdStore.Modules.Vendas.Application.UseCases.FinalizarPedido.Commands;

public class FinalizarPedidoCommandHandler : IRequestHandler<FinalizarPedidoCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMessageBus _messageBus;


    public FinalizarPedidoCommandHandler(IPedidoRepository pedidoRepository, IMessageBus messageBus)
    {
        _pedidoRepository = pedidoRepository;
        _messageBus = messageBus;
    }

    public async Task<bool> Handle(FinalizarPedidoCommand message, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.ObterPorId(message.PedidoId);

        if (pedido is null)
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("Pedido", "Pedido não encontrado."));
            return false;
        }

        pedido.FinalizarPedido();
        pedido.AdicionarEvento(new PedidoFinalizadoEvent(message.PedidoId)); // Enviar NFe e E-mail
        
        return await _pedidoRepository.UnitOfWork.Commit();
    }
}