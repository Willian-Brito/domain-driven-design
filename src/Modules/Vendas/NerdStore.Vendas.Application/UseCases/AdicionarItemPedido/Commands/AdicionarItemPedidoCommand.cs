using FluentValidation;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages;
using NerdStore.Modules.Vendas.Application.Base;

namespace NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Commands;

public class AdicionarItemPedidoCommand : PedidoCommand
{
    private readonly IMessageBus _messageBus;
    
    public Guid ClienteId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string Nome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    public AdicionarItemPedidoCommand(
        IMessageBus _messageBus,
        Guid clienteId, 
        Guid produtoId, 
        string nome, 
        int quantidade, 
        decimal valorUnitario
    ) : base(_messageBus)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
        Nome = nome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public override bool EhValido()
    {
        ValidationResult = new AdicionarItemPedidoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
{
    public AdicionarItemPedidoValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("O nome do produto não foi informado");

        RuleFor(c => c.Quantidade)
            .GreaterThan(0)
            .WithMessage("A quantidade miníma de um item é 1");

        RuleFor(c => c.Quantidade)
            .LessThan(15)
            .WithMessage("A quantidade máxima de um item é 15");

        RuleFor(c => c.ValorUnitario)
            .GreaterThan(0)
            .WithMessage("O valor do item precisa ser maior que 0");
    }
}