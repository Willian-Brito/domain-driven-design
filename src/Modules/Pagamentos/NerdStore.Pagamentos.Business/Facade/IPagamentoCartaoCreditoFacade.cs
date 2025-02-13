using NerdStore.Modules.Pagamentos.Business.Aggregates;
using NerdStore.Modules.Pagamentos.Business.Entities;

namespace NerdStore.Modules.Pagamentos.Business.Facade;

public interface IPagamentoCartaoCreditoFacade
{
    Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento);
}