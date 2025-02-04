using NerdStore.Catalogo.Application.DTOs;

namespace NerdStore.Catalogo.Application.Services;

public interface IProdutoService : IDisposable
{
    Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo);
    Task<ProdutoDto> ObterPorId(Guid id);
    Task<IEnumerable<ProdutoDto>> ObterTodos();
    Task<IEnumerable<CategoriaDto>> ObterCategorias();

    Task AdicionarProduto(ProdutoDto produtoDto);
    Task AtualizarProduto(ProdutoDto produtoDto);

    Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade);
    Task<ProdutoDto> ReporEstoque(Guid id, int quantidade);
}