using MediatR;
using NerdStore.Modules.Core.DomainObjects.DTO;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;
using NerdStore.Modules.Pagamentos.Business.Services;

namespace NerdStore.Modules.Pagamentos.Business.Events;
public class PagamentoEventHandler : INotificationHandler<PedidoEstoqueConfirmadoEvent>
{
    private readonly IPagamentoService _pagamentoService;

    public PagamentoEventHandler(IPagamentoService pagamentoService)
    {
        _pagamentoService = pagamentoService;
    }

    public async Task Handle(PedidoEstoqueConfirmadoEvent message, CancellationToken cancellationToken)
    {
        var pagamentoPedido = new PagamentoPedido
        {
            PedidoId = message.PedidoId,
            ClienteId = message.ClienteId,
            Total = message.Total,
            NomeCartao = message.NomeCartao,
            NumeroCartao = message.NumeroCartao,
            ExpiracaoCartao = message.ExpiracaoCartao,
            CvvCartao = message.CvvCartao
        };

        await _pagamentoService.RealizarPagamentoPedido(pagamentoPedido);
    }
}
