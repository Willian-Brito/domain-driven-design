using NerdStore.Modules.Catalogo.Domain.Aggregates;
using NerdStore.Modules.Core.DomainObjects;

namespace NerdStore.Modules.Catalogo.Domain.Entity;

public class Categoria : Core.DomainObjects.Entity
{  
    public string Nome { get; private set; }
    public int Codigo { get; private set; }
    public ICollection<Produto> Produtos { get; private set; }

    protected Categoria() {}

    public Categoria(string nome, int codigo)
    {
        Nome = nome;
        Codigo = codigo;

        Validar();
    }

    public override string ToString()
    {
        return $"{Nome} - {Codigo}";
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, "O campo Nome do categoria não pode estar vazio");
        Validacoes.ValidarSeIgual(Codigo, 0, "O campo Código não pode ser 0");
    }
}