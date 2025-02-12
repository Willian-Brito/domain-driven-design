using NerdStore.Modules.Core.DomainObjects.DTO;

namespace NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;

public class PedidoEstoqueConfirmadoEvent : IntegrationEvent
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal Total { get; private set; }
    public ListaProdutosPedido ProdutosPedido { get; private set; }
    public string NomeCartao { get; private set; }
    public string NumeroCartao { get; private set; }
    public string ExpiracaoCartao { get; private set; }
    public string CvvCartao { get; private set; }

    public PedidoEstoqueConfirmadoEvent(
        Guid pedidoId, 
        Guid clienteId, 
        decimal total, 
        ListaProdutosPedido produtosPedido, 
        string nomeCartao, 
        string numeroCartao, 
        string expiracaoCartao, 
        string cvvCartao
    )
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
        Total = total;
        ProdutosPedido = produtosPedido;
        NomeCartao = nomeCartao;
        NumeroCartao = numeroCartao;
        ExpiracaoCartao = expiracaoCartao;
        CvvCartao = cvvCartao;
    }
}