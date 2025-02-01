using NerdStore.Modules.Catalogo.Domain.Aggregates;
using NerdStore.Modules.Catalogo.Domain.Entity;
using NerdStore.Modules.Core.Data;

namespace NerdStore.Modules.Catalogo.Domain.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> ObterTodos();
    Task<Produto> ObterPorId(Guid id);
    Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
    Task<IEnumerable<Categoria>> ObterCategorias();

    void Adicionar(Produto produto);
    void Atualizar(Produto produto);
    void Adicionar(Categoria categoria);
    void Atualizar(Categoria categoria);
}