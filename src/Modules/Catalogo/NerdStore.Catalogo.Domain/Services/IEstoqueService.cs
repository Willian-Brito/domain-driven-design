using NerdStore.Modules.Core.DomainObjects.DTO;

namespace NerdStore.Modules.Catalogo.Domain.Services;

public interface IEstoqueService : IDisposable
{
    Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
    Task<bool> ReporEstoque(Guid produtoId, int quantidade);
    Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista);
    Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista);
}