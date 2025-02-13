
namespace NerdStore.Modules.Pagamentos.Business.Entities;

public class Pedido
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public List<Produto> Produtos { get; set; }
}