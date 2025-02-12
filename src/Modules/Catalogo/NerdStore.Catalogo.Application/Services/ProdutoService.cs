using AutoMapper;
using NerdStore.Catalogo.Application.DTOs;
using NerdStore.Modules.Catalogo.Domain.Aggregates;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Catalogo.Domain.Services;
using NerdStore.Modules.Core.DomainObjects;

namespace NerdStore.Catalogo.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IEstoqueService _estoqueService;
    private readonly IMapper _mapper;

    public ProdutoService(
        IProdutoRepository produtoRepository,
        IMapper mapper,
        IEstoqueService estoqueService
    )
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
        _estoqueService = estoqueService;
    }

    public async Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo)
    {
        return _mapper.Map<IEnumerable<ProdutoDto>>(
            await _produtoRepository.ObterPorCategoria(codigo)
        );
    }

    public async Task<ProdutoDto> ObterPorId(Guid id)
    {
        return _mapper.Map<ProdutoDto>(
            await _produtoRepository.ObterPorId(id)
        );
    }

    public async Task<IEnumerable<ProdutoDto>> ObterTodos()
    {
        return _mapper.Map<IEnumerable<ProdutoDto>>(
            await _produtoRepository.ObterTodos()
        );
    }

    public async Task<IEnumerable<CategoriaDto>> ObterCategorias()
    {
        return _mapper.Map<IEnumerable<CategoriaDto>>(
            await _produtoRepository.ObterCategorias()
        );
    }

    public async Task AdicionarProduto(ProdutoDto produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);

        _produtoRepository.Adicionar(produto);
        await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task AtualizarProduto(ProdutoDto produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);

        _produtoRepository.Atualizar(produto);
        await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade)
    {
        if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
            throw new DomainException("Falha ao debitar estoque");        

        return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
    }

    public async Task<ProdutoDto> ReporEstoque(Guid id, int quantidade)
    {
        if (!_estoqueService.ReporEstoque(id, quantidade).Result)
            throw new DomainException("Falha ao repor o estoque");        

        return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
        _estoqueService?.Dispose();
    }
}