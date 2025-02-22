using MediatR;
using NerdStore.Modules.Core.Messages;
using NerdStore.Modules.Vendas.Application.UseCases.ObterPedidosCliente.ViewModel;
using NerdStore.Modules.Vendas.Domain.Enum;
using NerdStore.Modules.Vendas.Domain.Repositories;

namespace NerdStore.Modules.Vendas.Application.UseCases.ObterPedidosCliente.Queries;

public class ObterPedidosClienteQuery : IRequest<IEnumerable<PedidoViewModel>>
{
    public Guid ClienteId { get; set; }

    public class ObterPedidosClienteQueryHandler : IRequestHandler<ObterPedidosClienteQuery, IEnumerable<PedidoViewModel>>
    {
        private readonly IPedidoRepository _pedidoRepository;
        
        public ObterPedidosClienteQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        } 

        public async Task<IEnumerable<PedidoViewModel>> Handle(ObterPedidosClienteQuery request, CancellationToken cancellationToken)
        {
            var pedidos = await _pedidoRepository.ObterListaPorClienteId(request.ClienteId);

            pedidos = pedidos.Where(p => p.PedidoStatus == PedidoStatus.Pago || p.PedidoStatus == PedidoStatus.Cancelado)
                .OrderByDescending(p => p.Codigo).ToList();

            if (!pedidos.Any()) return null;

            var viewModel = new List<PedidoViewModel>();

            foreach (var pedido in pedidos)
            {
                viewModel.Add(new PedidoViewModel
                {
                    Id = pedido.Id,
                    ValorTotal = pedido.ValorTotal,
                    PedidoStatus = (int)pedido.PedidoStatus,
                    Codigo = pedido.Codigo,
                    DataCadastro = pedido.DataCadastro
                });
            }

            return viewModel;
        }
    }
}