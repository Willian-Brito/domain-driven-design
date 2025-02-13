using NerdStore.Modules.Core.Data;
using NerdStore.Modules.Pagamentos.Business.Aggregates;
using NerdStore.Modules.Pagamentos.Business.Entities;

namespace NerdStore.Modules.Pagamentos.Business.Repositories;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    void Adicionar(Pagamento pagamento);

    void AdicionarTransacao(Transacao transacao);
}