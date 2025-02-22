
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.AplicarVoucher.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.IniciarPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.Queries;
using NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.ViewModel;
using NerdStore.Modules.Vendas.Application.UseCases.RemoverItemPedido.Commands;
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
        var viewModel = await GetCarrinhoViewModel();        
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

        var command = new AdicionarItemPedidoCommand(
            _messageBus, 
            ClienteId, 
            produto.Id, 
            produto.Nome, 
            quantidade, 
            produto.Valor
        );
        
        await _messageBus.EnviarComando(command);

        if(OperacaoValida()) return RedirectToAction("Index");        
        
        TempData["Erros"] = ObterMensagensErro();
        return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
    }

    [HttpPost]
    [Route("remover-item")]
    public async Task<IActionResult> RemoverItem(Guid id)
    {
        var produto = await _produtoService.ObterPorId(id);
        if (produto == null) return BadRequest();

        var command = new RemoverItemPedidoCommand(_messageBus, ClienteId, id);
        await _messageBus.EnviarComando(command);

        if (OperacaoValida()) return RedirectToAction("Index");

        var carrinho = await GetCarrinhoViewModel();

        return View("Index", carrinho);
    }

    [HttpPost]
    [Route("atualizar-item")]
    public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
    {
        var produto = await _produtoService.ObterPorId(id);
        if (produto == null) return BadRequest();

        var command = new AtualizarItemPedidoCommand(_messageBus, ClienteId, id, quantidade);
        await _messageBus.EnviarComando(command);

        if (OperacaoValida()) return RedirectToAction("Index");        

        var carrinho = await GetCarrinhoViewModel();

        return View("Index", carrinho);
    }

    [HttpPost]
    [Route("aplicar-voucher")]
    public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
    {
        var command = new AplicarVoucherPedidoCommand(_messageBus, ClienteId, voucherCodigo);
        await _messageBus.EnviarComando(command);

        if (OperacaoValida()) return RedirectToAction("Index");

        var carrinho = await GetCarrinhoViewModel();

        return View("Index", carrinho);
    }

    [Route("resumo-da-compra")]
    public async Task<IActionResult> ResumoDaCompra()
    {        
        var viewModel = await GetCarrinhoViewModel();        
        return View(viewModel);        
    }

    [HttpPost]
    [Route("iniciar-pedido")]
    public async Task<IActionResult> IniciarPedido(CarrinhoViewModel carrinhoViewModel)
    {
        var carrinho = await GetCarrinhoViewModel();

        var command = new IniciarPedidoCommand(
            _messageBus,
            carrinho.PedidoId, 
            ClienteId, 
            carrinho.ValorTotal, 
            carrinhoViewModel.Pagamento.NomeCartao,
            carrinhoViewModel.Pagamento.NumeroCartao, 
            carrinhoViewModel.Pagamento.ExpiracaoCartao, 
            carrinhoViewModel.Pagamento.CvvCartao
        );

        await _messageBus.EnviarComando(command);

        if (OperacaoValida())
            return RedirectToAction("Index", "Pedido");
        
        return View("ResumoDaCompra", carrinho);
    }

    private async Task<CarrinhoViewModel> GetCarrinhoViewModel()
    {
        var query = new ObterCarrinhoClienteQuery { ClienteId = ClienteId };
        var viewModel = await _messageBus.EnviarQuery<ObterCarrinhoClienteQuery, CarrinhoViewModel>(query);
        return viewModel;
    }
}