using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Events;
using NerdStore.Modules.Vendas.Domain.Repositories;

namespace NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Commands;

public class AtualizarItemPedidoCommandHandler : IRequestHandler<AtualizarItemPedidoCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMessageBus _messageBus;


    public AtualizarItemPedidoCommandHandler(IPedidoRepository pedidoRepository, IMessageBus messageBus)
    {
        _pedidoRepository = pedidoRepository;
        _messageBus = messageBus;
    }

    public async Task<bool> Handle(AtualizarItemPedidoCommand message, CancellationToken cancellationToken)
    {
        if (!message.ValidarComando()) return false;

        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

        if (pedido is null)
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("Pedido", "Pedido não encontrado!"));
            return false;
        }

        var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, message.ProdutoId);

        if (!pedido.PedidoItemExistente(pedidoItem))
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("Pedido", "Item do pedido não encontrado!"));
            return false;
        }

        pedido.AtualizarUnidades(pedidoItem, message.Quantidade);

        pedido.AdicionarEvento(
            new PedidoProdutoAtualizadoEvent(
                pedido.ClienteId, 
                pedido.Id, 
                message.ProdutoId, 
                message.Quantidade
            )
        );

        _pedidoRepository.AtualizarItem(pedidoItem);
        _pedidoRepository.Atualizar(pedido);

        return await _pedidoRepository.UnitOfWork.Commit();
    }
}