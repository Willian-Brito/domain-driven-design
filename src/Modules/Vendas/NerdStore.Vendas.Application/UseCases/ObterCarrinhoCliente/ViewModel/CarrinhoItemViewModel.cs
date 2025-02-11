namespace NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.ViewModel;

public class CarrinhoItemViewModel
{
    public Guid ProdutoId { get; set; }
    public string ProdutoNome { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
}