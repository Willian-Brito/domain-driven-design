using NerdStore.Modules.Catalogo.Domain.Events;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.DomainObjects.DTO;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;

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

    #region Metodos

    #region Debitar Estoque
    public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
    {
        var sucesso = await DebitarItemEstoque(produtoId, quantidade);
        if (!sucesso) return false;

        return await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista)
    {
        foreach (var item in lista.Itens)
        {
            if (!await DebitarItemEstoque(item.Id, item.Quantidade)) return false;
        }

        return await _produtoRepository.UnitOfWork.Commit();
    }

    private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterPorId(produtoId);

        if (produto == null) return false;

        if (!produto.PossuiEstoque(quantidade))
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque"));
            return false;
        }

        produto.DebitarEstoque(quantidade);

        // TODO: 10 pode ser parametrizavel em arquivo de configuração
        if (produto.QuantidadeEstoque < 10)
        {
            await _messageBus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
        }

        _produtoRepository.Atualizar(produto);
        return true;
    }
    #endregion

    #region Repor Estoque
    
    public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
    {
        var sucesso = await ReporItemEstoque(produtoId, quantidade);
        if (!sucesso) return false;
        
        return await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista)
    {
        foreach (var item in lista.Itens)
        {
            await ReporItemEstoque(item.Id, item.Quantidade);
        }

        return await _produtoRepository.UnitOfWork.Commit();
    }

    private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return true;
        }
    #endregion

    public void Dispose()
    {
        _produtoRepository.Dispose();
    }
    #endregion    

}