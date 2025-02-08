using NerdStore.Modules.Catalogo.Domain.Events;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Core.Communication.Mediator;

namespace NerdStore.Modules.Catalogo.Domain.Services;

public class EstoqueService : IEstoqueService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMessageBus _messageBus;

    public EstoqueService(IProdutoRepository produtoRepository, IMessageBus messageBus)
    {
        _produtoRepository = produtoRepository;
        _messageBus = messageBus;
    }

    public async Task<bool> Debitar(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if(produto is null) return false;
        if(!produto.PossuiEstoque(quantidade)) return false;

        produto.DebitarEstoque(quantidade);

        if(produto.QuantidadeEstoque < 10)
        {
            await _messageBus.PublicarEvento(
                new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque)
            );
        }

        _produtoRepository.Atualizar(produto);

        return await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Repor(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if(produto is null) return false;

        produto.ReporEstoque(quantidade);
        _produtoRepository.Atualizar(produto);
        
        return await _produtoRepository.UnitOfWork.Commit();
    }

    public void Dispose()
    {
        _produtoRepository.Dispose();
    }
}