using FluentValidation;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Vendas.Application.Base;

namespace NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Commands;

public class AtualizarItemPedidoCommand : PedidoCommand
{    
    public Guid ClienteId { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public int Quantidade { get; private set; }

    public AtualizarItemPedidoCommand(
        IMessageBus messageBus, 
        Guid clienteId, 
        Guid produtoId, 
        int quantidade
    ) : base(messageBus)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }

    public override bool EhValido()
    {
        ValidationResult = new AtualizarItemPedidoValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class AtualizarItemPedidoValidation : AbstractValidator<AtualizarItemPedidoCommand>
    {
        public AtualizarItemPedidoValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantidade)
                .LessThan(15)
                .WithMessage("A quantidade tem que ser menor que 15");
        }
    }
}