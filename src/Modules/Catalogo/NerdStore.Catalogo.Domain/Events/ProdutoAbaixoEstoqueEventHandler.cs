using MediatR;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Catalogo.Domain.Services;
using NerdStore.Modules.Core.Communication.Mediator;

namespace NerdStore.Modules.Catalogo.Domain.Events;

public class ProdutoAbaixoEstoqueEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>    
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoAbaixoEstoqueEventHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task Handle(ProdutoAbaixoEstoqueEvent message, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterPorId(message.AggregateId);

        // Enviar um email para comprar mais produtos        
    }
}