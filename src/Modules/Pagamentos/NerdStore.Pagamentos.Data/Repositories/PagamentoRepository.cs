

using NerdStore.Modules.Core.Data;
using NerdStore.Modules.Pagamentos.Business.Aggregates;
using NerdStore.Modules.Pagamentos.Business.Entities;
using NerdStore.Modules.Pagamentos.Business.Repositories;
using NerdStore.Pagamentos.Data;

namespace NerdStore.Modules.Pagamentos.Data.Repositories;
 public class PagamentoRepository : IPagamentoRepository
{
    private readonly PagamentoContext _context;

    public PagamentoRepository(PagamentoContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;


    public void Adicionar(Pagamento pagamento)
    {
        _context.Pagamentos.Add(pagamento);
    }

    public void AdicionarTransacao(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}