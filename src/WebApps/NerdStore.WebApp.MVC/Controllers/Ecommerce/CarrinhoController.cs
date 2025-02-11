
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.Queries;
using NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.ViewModel;
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

    [Route("meu-carrinho")]
    public async Task<IActionResult> Index()
    {
        var query = new ObterCarrinhoClienteQuery { ClienteId = ClienteId };
        var viewModel = await _messageBus.EnviarQuery<ObterCarrinhoClienteQuery, CarrinhoViewModel>(query);
        
        return View(viewModel);
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

    // [HttpPost]
    // [Route("remover-item")]
    // public async Task<IActionResult> RemoverItem(Guid id)
    // {
    //     var produto = await produtoService.ObterPorId(id);
    //     if (produto == null) return BadRequest();

    //     var command = new RemoverItemPedidoCommand(ClienteId, id);
    //     await _mediatorHandler.EnviarComando(command);

    //     if (OperacaoValida()) return RedirectToAction("Index");


    //     return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
    // }

    // [HttpPost]
    // [Route("atualizar-item")]
    // public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
    // {
    //     var produto = await _produtoAppService.ObterPorId(id);
    //     if (produto == null) return BadRequest();

    //     var command = new AtualizarItemPedidoCommand(ClienteId, id, quantidade);
    //     await _mediatorHandler.EnviarComando(command);

    //     if (OperacaoValida())
    //     {
    //         return RedirectToAction("Index");
    //     }

    //     return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
    // }

    // [HttpPost]
    // [Route("aplicar-voucher")]
    // public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
    // {
    //     var command = new AplicarVoucherPedidoCommand(ClienteId, voucherCodigo);
    //     await _mediatorHandler.EnviarComando(command);

    //     if (OperacaoValida())
    //     {
    //         return RedirectToAction("Index");
    //     }

    //     return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
    // }

    // [Route("resumo-da-compra")]
    // public async Task<IActionResult> ResumoDaCompra()
    // {
    //     return View(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
    // }

    // [HttpPost]
    // [Route("iniciar-pedido")]
    // public async Task<IActionResult> IniciarPedido(CarrinhoViewModel carrinhoViewModel)
    // {
    //     var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);

    //     var command = new IniciarPedidoCommand(carrinho.PedidoId, ClienteId, carrinho.ValorTotal, carrinhoViewModel.Pagamento.NomeCartao,
    //         carrinhoViewModel.Pagamento.NumeroCartao, carrinhoViewModel.Pagamento.ExpiracaoCartao, carrinhoViewModel.Pagamento.CvvCartao);

    //     await _mediatorHandler.EnviarComando(command);

    //     if (OperacaoValida())
    //     {
    //         return RedirectToAction("Index", "Pedido");
    //     }

    //     return View("ResumoDaCompra", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
    // }
}