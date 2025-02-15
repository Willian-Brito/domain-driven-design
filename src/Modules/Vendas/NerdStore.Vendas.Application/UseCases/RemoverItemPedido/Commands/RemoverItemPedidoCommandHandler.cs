using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Events;
using NerdStore.Modules.Vendas.Application.UseCases.RemoverItemPedido.Events;
using NerdStore.Modules.Vendas.Domain.Repositories;

namespace NerdStore.Modules.Vendas.Application.UseCases.RemoverItemPedido.Commands;

public class RemoverItemPedidoCommandHandler : IRequestHandler<RemoverItemPedidoCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMessageBus _messageBus;


    public RemoverItemPedidoCommandHandler(IPedidoRepository pedidoRepository, IMessageBus messageBus)
    {
        _pedidoRepository = pedidoRepository;
        _messageBus = messageBus;
    }

    public async Task<bool> Handle(RemoverItemPedidoCommand message, CancellationToken cancellationToken)
    {
        if (!message.ValidarComando()) return false;

        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

        if (pedido == null)
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("Pedido", "Pedido não encontrado!"));
            return false;
        }

        var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, message.ProdutoId);

        if (pedidoItem != null && !pedido.PedidoItemExistente(pedidoItem))
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("Pedido", "Item do pedido não encontrado!"));
            return false;
        }

        pedido.RemoverItem(pedidoItem);
        pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
        pedido.AdicionarEvento(new PedidoProdutoRemovidoEvent(message.ClienteId, pedido.Id, message.ProdutoId));

        _pedidoRepository.RemoverItem(pedidoItem);
        _pedidoRepository.Atualizar(pedido);

        return await _pedidoRepository.UnitOfWork.Commit();
    }
}