using AutoMapper;
using NerdStore.Catalogo.Application.DTOs;
using NerdStore.Modules.Catalogo.Domain.Aggregates;
using NerdStore.Modules.Catalogo.Domain.Entity;

namespace NerdStore.Catalogo.Application.AutoMapper;

public class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile()
    {
        CreateMap<ProdutoDto, Produto>()
                .ConstructUsing(p =>
                    new Produto(p.Nome, p.Descricao, p.Ativo, p.Valor, p.CategoriaId, p.DataCadastro, 
                        p.Imagem, new Dimensoes(p.Altura, p.Largura, p.Profundidade)
                    )
                );

        CreateMap<CategoriaDto, Categoria>()
            .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
    }
}