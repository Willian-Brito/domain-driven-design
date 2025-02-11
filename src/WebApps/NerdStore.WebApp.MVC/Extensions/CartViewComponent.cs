using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.Queries;
using NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.ViewModel;

namespace NerdStore.WebApp.MVC.Extensions;
public class CartViewComponent : ViewComponent
{
    private readonly IMessageBus _messageBus;

    // TODO: Obter cliente logado
    protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");


    public CartViewComponent(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var query = new ObterCarrinhoClienteQuery { ClienteId = ClienteId };
        var carrinho = await _messageBus.EnviarQuery<ObterCarrinhoClienteQuery, CarrinhoViewModel>(query);        
        var itens = carrinho?.Items.Count ?? 0;

        return View(itens);
    }
}