
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Commands;
using Base = NerdStore.WebApp.Controllers.Base;

namespace NerdStore.WebApp.Controllers.Ecommerce;

public class CarrinhoController : Base.ControllerBase
{
    private readonly IProdutoService _produtoService;
    private readonly IMessageBus _messageBus;

    public CarrinhoController(
        INotificationHandler<DomainNotification> notifications,
        IProdutoService produtoService, 
        IMessageBus messageBus
    ) : base(notifications, messageBus)
    {
        _produtoService = produtoService;
        _messageBus = messageBus;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("meu-carrinho")]
    public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
    {
        var produto = await _produtoService.ObterPorId(id);
        if(produto is null) return BadRequest();

        if(produto.QuantidadeEstoque < quantidade)
        {
            TempData["Erro"] = "Produto com estoque insuficiente";
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
        await _messageBus.EnviarComando(command);

        if(OperacaoValida()) return RedirectToAction("Index");        
        
        TempData["Erros"] = ObterMensagensErro();
        return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
    }
}