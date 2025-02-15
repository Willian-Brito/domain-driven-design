using MediatR;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Catalogo.Domain.Services;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;

namespace NerdStore.Modules.Catalogo.Domain.Events;

public class ProdutoSaidaEstoqueEventHandler : INotificationHandler<PedidoIniciadoEvent>
{    
    private readonly IEstoqueService _estoqueService;
    private readonly IMessageBus _messageBus;

    public ProdutoSaidaEstoqueEventHandler(IEstoqueService estoqueService,IMessageBus messageBus)
    {        
        _estoqueService = estoqueService;
        _messageBus = messageBus;
    }

    public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)
    {
        var temEstoque = await _estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);

         if (temEstoque)
        {
            await _messageBus.PublicarEvento(
                new PedidoEstoqueConfirmadoEvent(
                    message.PedidoId, 
                    message.ClienteId, 
                    message.Total, 
                    message.ProdutosPedido, 
                    message.NomeCartao, 
                    message.NumeroCartao, 
                    message.ExpiracaoCartao, 
                    message.CvvCartao
                )
            );
        }
        else
        {
            await _messageBus.PublicarEvento(
                new PedidoEstoqueRejeitadoEvent(
                    message.PedidoId, 
                    message.ClienteId
                )
            );
        }
    }
}