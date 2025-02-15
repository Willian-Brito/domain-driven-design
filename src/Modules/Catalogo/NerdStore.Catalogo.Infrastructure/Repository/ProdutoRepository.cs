using Microsoft.EntityFrameworkCore;
using NerdStore.Modules.Catalogo.Domain.Aggregates;
using NerdStore.Modules.Catalogo.Domain.Entity;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Catalogo.Infrastructure.Context;
using NerdStore.Modules.Core.Data;

public class ProdutoRepository : IProdutoRepository
{
    private readonly CatalogoContext _context;       

    public ProdutoRepository(CatalogoContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Produto>> ObterTodos()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<Produto> ObterPorId(Guid id)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
    {
        return await _context.Produtos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .Where(c => c.Categoria.Codigo == codigo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Categoria>> ObterCategorias()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    public void Adicionar(Produto produto)
    {
        _context.Produtos.Add(produto);
    }

    public void Atualizar(Produto produto)
    {
        var trackedEntity = _context.Produtos.Local.FirstOrDefault(p => p.Id == produto.Id);
        if (trackedEntity != null)
        {
            _context.Entry(trackedEntity).State = EntityState.Detached;
        }

        _context.Produtos.Update(produto);
    }

    public void Adicionar(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
    }

    public void Atualizar(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
    }

    public void Dispose()
    {
        _context?.Dispose();
    } 
}