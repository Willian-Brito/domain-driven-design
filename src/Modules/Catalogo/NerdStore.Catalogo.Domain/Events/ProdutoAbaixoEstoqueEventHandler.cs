using MediatR;
using NerdStore.Modules.Catalogo.Domain.Repositories;

namespace NerdStore.Modules.Catalogo.Domain.Events;

public class ProdutoAbaixoEstoqueEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoAbaixoEstoqueEventHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterPorId(mensagem.AggregateId);

        // Enviar um email para comprar mais produtos        
    }
}