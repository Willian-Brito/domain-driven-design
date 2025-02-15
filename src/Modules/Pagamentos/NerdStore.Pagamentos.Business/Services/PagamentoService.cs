
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.DomainObjects.DTO;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Pagamentos.Business.Aggregates;
using NerdStore.Modules.Pagamentos.Business.Entities;
using NerdStore.Modules.Pagamentos.Business.Enum;
using NerdStore.Modules.Pagamentos.Business.Facade;
using NerdStore.Modules.Pagamentos.Business.Repositories;

namespace NerdStore.Modules.Pagamentos.Business.Services;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IMessageBus _messageBus;

    public PagamentoService(
        IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
        IPagamentoRepository pagamentoRepository, 
        IMessageBus messageBus
    )
    {
        _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
        _pagamentoRepository = pagamentoRepository;
        _messageBus = messageBus;
    }

    public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
    {
        var pedido = new Pedido
        {
            Id = pagamentoPedido.PedidoId,
            Valor = pagamentoPedido.Total
        };

        var pagamento = new Pagamento
        {
            Valor = pagamentoPedido.Total,
            NomeCartao = pagamentoPedido.NomeCartao,
            NumeroCartao = pagamentoPedido.NumeroCartao,
            ExpiracaoCartao = pagamentoPedido.ExpiracaoCartao,
            CvvCartao = pagamentoPedido.CvvCartao,
            PedidoId = pagamentoPedido.PedidoId
        };

        var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

        if (transacao.StatusTransacao == StatusTransacao.Pago)
        {
            pagamento.Status = "Pago";
            pagamento.AdicionarEvento(
                new PagamentoRealizadoEvent(
                    pedido.Id, 
                    pagamentoPedido.ClienteId, 
                    transacao.PagamentoId, 
                    transacao.Id, 
                    pedido.Valor
                )
            );

            _pagamentoRepository.Adicionar(pagamento);
            _pagamentoRepository.AdicionarTransacao(transacao);

            await _pagamentoRepository.UnitOfWork.Commit();
            return transacao;
        }

        await _messageBus.PublicarNotificacao(new DomainNotification("Pagamento","A operadora recusou o pagamento"));
        
        await _messageBus.PublicarEvento(
            new PagamentoRecusadoEvent(
                pedido.Id, 
                pagamentoPedido.ClienteId, 
                transacao.PagamentoId, 
                transacao.Id, 
                pedido.Valor
            )
        );

        return transacao;
    }
}