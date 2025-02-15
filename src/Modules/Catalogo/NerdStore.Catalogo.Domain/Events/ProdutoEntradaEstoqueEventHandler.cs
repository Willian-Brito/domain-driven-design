using MediatR;
using NerdStore.Modules.Catalogo.Domain.Services;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;

namespace NerdStore.Modules.Catalogo.Domain.Events;

public class ProdutoEntradaEstoqueEventHandler : INotificationHandler<PedidoProcessamentoCanceladoEvent>
{    
    private readonly IEstoqueService _estoqueService;    

    public ProdutoEntradaEstoqueEventHandler(IEstoqueService estoqueService)
    {
        _estoqueService = estoqueService;
    }

    public async Task Handle(PedidoProcessamentoCanceladoEvent message, CancellationToken cancellationToken)
    {
        await _estoqueService.ReporListaProdutosPedido(message.ProdutosPedido);
    }
}