
using Microsoft.EntityFrameworkCore;
using NerdStore.Modules.Core.Data;
using NerdStore.Modules.Vendas.Domain.Aggregates;
using NerdStore.Modules.Vendas.Domain.Entities;
using NerdStore.Modules.Vendas.Domain.Enum;
using NerdStore.Modules.Vendas.Domain.Repositories;
using NerdStore.Vendas.Infrastructure;

namespace NerdStore.Modules.Vendas.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly VendasContext _context;

    public PedidoRepository(VendasContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Pedido> ObterPorId(Guid id)
    {
        return await _context.Pedidos.FindAsync(id);
    }

    public async Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId)
    {
        return await _context.Pedidos.AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();
    }

    public async Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
    {
        var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.ClienteId == clienteId && p.PedidoStatus == PedidoStatus.Rascunho);
        if (pedido == null) return null;

        await _context.Entry(pedido).Collection(i => i.PedidoItems).LoadAsync();

        if (pedido.VoucherId != null)
            await _context.Entry(pedido).Reference(i => i.Voucher).LoadAsync();        

        return pedido;
    }

    public void Adicionar(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
    }

    public void Atualizar(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
    }

    public async Task<PedidoItem> ObterItemPorId(Guid id)
    {
        return await _context.PedidoItems.FindAsync(id);
    }

    public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
    {
        var pedidoItem = await _context.PedidoItems
            .FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);
        
        return pedidoItem;
    }

    public void AdicionarItem(PedidoItem pedidoItem)
    {
        _context.PedidoItems.Add(pedidoItem);
    }

    public void AtualizarItem(PedidoItem pedidoItem)
    {
        _context.PedidoItems.Update(pedidoItem);
    }

    public void RemoverItem(PedidoItem pedidoItem)
    {
        _context.PedidoItems.Remove(pedidoItem);
    }

    public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
    {
        var voucher = await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        return voucher;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
