using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;

namespace NerdStore.WebApp.MVC.Controllers.Ecommerce;

public class VitrineController : Controller
{
    private readonly IProdutoService _produtoService;

    public VitrineController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    [Route("")]
    [Route("vitrine")]
    public async Task<IActionResult> Index()
    {
        return View(await _produtoService.ObterTodos());
    }

    [HttpGet]
    [Route("produto-detalhe/{id}")]
    public async Task<IActionResult> ProdutoDetalhe(Guid id)
    {
        return View(await _produtoService.ObterPorId(id));
    }
}
