using MediatR;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Catalogo.Domain.Services;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;

namespace NerdStore.Modules.Catalogo.Domain.Events;

public class ProdutoAbaixoEstoqueEventHandler : 
    INotificationHandler<ProdutoAbaixoEstoqueEvent>,
    INotificationHandler<PedidoIniciadoEvent>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IEstoqueService _estoqueService;
    private readonly IMessageBus _messageBus;


    public ProdutoAbaixoEstoqueEventHandler(
        IProdutoRepository produtoRepository, 
        IEstoqueService estoqueService,
        IMessageBus messageBus
    )
    {
        _produtoRepository = produtoRepository;
        _estoqueService = estoqueService;
        _messageBus = messageBus;
    }

    public async Task Handle(ProdutoAbaixoEstoqueEvent message, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterPorId(message.AggregateId);

        // Enviar um email para comprar mais produtos        
    }

    public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)
    {
        var result = await _estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);

         if (result)
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