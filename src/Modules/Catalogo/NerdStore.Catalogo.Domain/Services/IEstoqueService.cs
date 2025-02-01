namespace NerdStore.Modules.Catalogo.Domain.Services;

public interface IEstoqueService : IDisposable
{
    Task<bool> Debitar(Guid produtoId, int quantidade);
    Task<bool> Repor(Guid produtoId, int quantidade);
}