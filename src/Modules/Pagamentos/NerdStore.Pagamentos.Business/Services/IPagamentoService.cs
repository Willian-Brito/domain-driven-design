using NerdStore.Modules.Core.DomainObjects.DTO;
using NerdStore.Modules.Pagamentos.Business.Entities;

namespace NerdStore.Modules.Pagamentos.Business.Services;

public interface IPagamentoService
{
    Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
}