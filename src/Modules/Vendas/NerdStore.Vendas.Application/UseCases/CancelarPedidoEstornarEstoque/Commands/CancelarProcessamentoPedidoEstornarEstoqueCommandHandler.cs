using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.DomainObjects.DTO;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Core.Extensions;
using NerdStore.Modules.Vendas.Domain.Repositories;

namespace NerdStore.Modules.Vendas.Application.UseCases.CancelarPedidoEstornarEstoque.Commands;

public class CancelarProcessamentoPedidoEstornarEstoqueCommandHandler 
    : IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMessageBus _messageBus;


    public CancelarProcessamentoPedidoEstornarEstoqueCommandHandler(
        IPedidoRepository pedidoRepository, 
        IMessageBus messageBus
    )
    {
        _pedidoRepository = pedidoRepository;
        _messageBus = messageBus;
    }

    public async Task<bool> Handle(CancelarProcessamentoPedidoEstornarEstoqueCommand message, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.ObterPorId(message.PedidoId);

        if (pedido is null)
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("Pedido", "Pedido não encontrado."));
            return false;
        }

        var itensList = new List<Item>();
        pedido.PedidoItems.ForEach(x => itensList.Add(new Item { Id = x.ProdutoId, Quantidade = x.Quantidade }));
        var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itensList };

        // Catalogo: Estornar estoque
        pedido.AdicionarEvento(new PedidoProcessamentoCanceladoEvent(pedido.Id, pedido.ClienteId, listaProdutosPedido)); 
        pedido.TornarRascunho();

        return await _pedidoRepository.UnitOfWork.Commit();
    }
}