using MediatR;
using NerdStore.Modules.Core.Messages;
using NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.ViewModel;
using NerdStore.Modules.Vendas.Domain.Repositories;

namespace NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.Queries;

public class ObterCarrinhoClienteQuery : IRequest<CarrinhoViewModel>
{
    public Guid ClienteId { get; set; }

    public class ObterCarrinhoClienteQueryHandler : IRequestHandler<ObterCarrinhoClienteQuery, CarrinhoViewModel>
    {
        private readonly IPedidoRepository _pedidoRepository;
        
        public ObterCarrinhoClienteQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }        
        
        public async Task<CarrinhoViewModel> Handle(ObterCarrinhoClienteQuery request, CancellationToken cancellationToken)
        {            
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            if (pedido is null) return null;

            var carrinho = new CarrinhoViewModel
            {
                ClienteId = pedido.ClienteId,
                ValorTotal = pedido.ValorTotal,
                PedidoId = pedido.Id,
                ValorDesconto = pedido.Desconto,
                SubTotal = pedido.Desconto + pedido.ValorTotal
            };

            if (pedido.VoucherId != null)
                carrinho.VoucherCodigo = pedido.Voucher.Codigo;   

            foreach (var item in pedido.PedidoItems)    
            {
                carrinho.Items.Add(new CarrinhoItemViewModel
                {
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.ProdutoNome,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorUnitario * item.Quantidade
                });
            }

            return carrinho;
        }        
    }
}