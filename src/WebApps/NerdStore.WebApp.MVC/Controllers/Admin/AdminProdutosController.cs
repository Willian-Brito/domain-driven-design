using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.DTOs;
using NerdStore.Catalogo.Application.Services;

namespace NerdStore.WebApp.Controllers.Admin;

public class AdminProdutosController : Controller
{
    private readonly IProdutoService _produtoService;

    public AdminProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    [Route("admin-produtos")]
    public async Task<IActionResult>Index()
    {
        var produtos = await _produtoService.ObterTodos();
        return View(produtos);
    }

     [Route("novo-produto")]
    public async Task<IActionResult> NovoProduto()
    {
        var produto = await PopularCategorias(new ProdutoDto());
        return View(produto);
    }

    [Route("novo-produto")]
    [HttpPost]
    public async Task<IActionResult> NovoProduto(ProdutoDto produtoDto)
    {
        if (!ModelState.IsValid) return View(await PopularCategorias(produtoDto));

        await _produtoService.AdicionarProduto(produtoDto);

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("editar-produto")]
    public async Task<IActionResult> AtualizarProduto(Guid id)
    {
        var produto = await _produtoService.ObterPorId(id);
        var produtoComCategorias = await PopularCategorias(produto);
        return View(produtoComCategorias);
    }

    [HttpPost]
    [Route("editar-produto")]
    public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoDto produtoDto)
    {
        var produto = await _produtoService.ObterPorId(id);
        produtoDto.QuantidadeEstoque = produto.QuantidadeEstoque;

        ModelState.Remove("QuantidadeEstoque");
        if (!ModelState.IsValid) return View(await PopularCategorias(produtoDto));

        await _produtoService.AtualizarProduto(produtoDto);

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("produtos-atualizar-estoque")]
    public async Task<IActionResult> AtualizarEstoque(Guid id)
    {
        return View("Estoque", await _produtoService.ObterPorId(id));
    }

    [HttpPost]
    [Route("produtos-atualizar-estoque")]
    public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
    {
        if (quantidade > 0)
        {
            await _produtoService.ReporEstoque(id, quantidade);
        }
        else
        {
            await _produtoService.DebitarEstoque(id, quantidade);
        }

        return View("Index", await _produtoService.ObterTodos());
    }

    private async Task<ProdutoDto> PopularCategorias(ProdutoDto produto)
    {
        produto.Categorias = await _produtoService.ObterCategorias();
        return produto;
    }
}