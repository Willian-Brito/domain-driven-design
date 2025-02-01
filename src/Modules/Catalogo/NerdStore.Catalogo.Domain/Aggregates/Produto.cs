using NerdStore.Modules.Catalogo.Domain.Entity;
using NerdStore.Modules.Core.DomainObjects;

namespace NerdStore.Modules.Catalogo.Domain.Aggregates;

public class Produto : Core.DomainObjects.Entity, IAggregateRoot
{
    #region Propriedades da Classe

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public string Imagem { get; private set; }
    public int QuantidadeEstoque { get; private set; }

    public Guid CategoriaId { get; private set; }
    public Dimensoes Dimensoes { get; private set; }
    public Categoria Categoria { get; private set; }
    #endregion

    #region Construtor

    protected Produto() { }

    public Produto(
        string nome, 
        string descricao, 
        bool ativo, 
        decimal valor, 
        Guid categoriaId,
        DateTime dataCadastro, 
        string imagem,
        Dimensoes dimensoes     
    )
    {
        Nome = nome;
        Descricao = descricao;
        Ativo = ativo;
        Valor = valor;
        DataCadastro = dataCadastro;
        Imagem = imagem;        
        CategoriaId = categoriaId;
        Dimensoes = dimensoes;

        Validar();
    }
    #endregion

    #region Metodos

    public void Ativar() => Ativo = true;

    public void Desativar() => Ativo = false;

    public void AlterarCategoria(Categoria categoria)
    {
        Categoria = categoria;
        CategoriaId = categoria.Id;
    }

    public void AlterarDescricao(string descricao)
    {
        Validacoes.ValidarSeVazio(Descricao, "O campo Descrição do produto não pode estar vazio");
        Descricao = descricao;
    }

    public void DebitarEstoque(int quantidade)
    {
        if (quantidade < 0) quantidade *= -1;
        if (!PossuiEstoque(quantidade)) throw new DomainException("Estoque insuficiente");
        QuantidadeEstoque -= quantidade;
    }

    public void ReporEstoque(int quantidade)
    {        
        QuantidadeEstoque += quantidade;
    }

    public bool PossuiEstoque(int quantidade)
    {        
        return QuantidadeEstoque >= quantidade;
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, "O campo Nome do produto não pode estar vazio");
        Validacoes.ValidarSeVazio(Descricao, "O campo Descrição do produto não pode estar vazio");
        Validacoes.ValidarSeIgual(CategoriaId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
        Validacoes.ValidarSeMenorQue(Valor, 0, "O campo CategoriaId do produto não pode ser menor ou igual a zero");
        Validacoes.ValidarSeVazio(Imagem, "O campo Imagem do produto não pode estar vazio");
    }
    #endregion
}