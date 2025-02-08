using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Events;

public class PedidoItemAdicionadoEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string ProdutoNome { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public int Quantidade { get; private set; }

    public PedidoItemAdicionadoEvent(Guid clientId, Guid pedidoId, Guid produtoId, string produtoNome, decimal valorUnitario, int quantidade)
    {
        AggregateId = pedidoId;
        ClientId = clientId;
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        ValorUnitario = valorUnitario;
        Quantidade = quantidade;
    }
}