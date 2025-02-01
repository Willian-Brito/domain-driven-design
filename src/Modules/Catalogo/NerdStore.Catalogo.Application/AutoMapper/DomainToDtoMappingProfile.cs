using AutoMapper;
using NerdStore.Catalogo.Application.DTOs;
using NerdStore.Modules.Catalogo.Domain.Aggregates;
using NerdStore.Modules.Catalogo.Domain.Entity;

namespace NerdStore.Catalogo.Application.AutoMapper;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Produto, ProdutoDto>()
                .ForMember(d => d.Largura, o => o.MapFrom(s => s.Dimensoes.Largura))
                .ForMember(d => d.Altura, o => o.MapFrom(s => s.Dimensoes.Altura))
                .ForMember(d => d.Profundidade, o => o.MapFrom(s => s.Dimensoes.Profundidade));

        CreateMap<Categoria, CategoriaDto>();
    }
}