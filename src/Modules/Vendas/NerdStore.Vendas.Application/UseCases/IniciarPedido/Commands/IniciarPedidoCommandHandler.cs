using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.DomainObjects.DTO;
using NerdStore.Modules.Vendas.Domain.Repositories;
using NerdStore.Modules.Core.Extensions;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;

namespace NerdStore.Modules.Vendas.Application.UseCases.IniciarPedido.Commands;

public class IniciarPedidoCommandHandler : IRequestHandler<IniciarPedidoCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;    

    public IniciarPedidoCommandHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;        
    }

    public async Task<bool> Handle(IniciarPedidoCommand message, CancellationToken cancellationToken)
    {
        if (!message.ValidarComando()) return false;

        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);
        pedido.IniciarPedido();

        var itensList = new List<Item>();
        pedido.PedidoItems.ForEach(i => itensList.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));
        var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itensList };        

        pedido.AdicionarEvento(
            new PedidoIniciadoEvent(
                pedido.Id, 
                pedido.ClienteId, 
                listaProdutosPedido, 
                pedido.ValorTotal, 
                message.NomeCartao, 
                message.NumeroCartao, 
                message.ExpiracaoCartao, 
                message.CvvCartao
            )
        );        
        _pedidoRepository.Atualizar(pedido);

        return await _pedidoRepository.UnitOfWork.Commit();
    }
}