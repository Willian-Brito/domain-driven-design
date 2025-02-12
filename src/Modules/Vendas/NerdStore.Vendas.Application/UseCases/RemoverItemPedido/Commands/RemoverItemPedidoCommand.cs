using FluentValidation;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Vendas.Application.Base;

namespace NerdStore.Modules.Vendas.Application.UseCases.RemoverItemPedido.Commands;

public class RemoverItemPedidoCommand : PedidoCommand
{    
    public Guid ClienteId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public RemoverItemPedidoCommand(
        IMessageBus messageBus, 
        Guid clienteId, 
        Guid produtoId
    ) : base(messageBus)
    {
        ClienteId = clienteId;
        ProdutoId = produtoId;
    }

    public override bool EhValido()
    {
        ValidationResult = new RemoverItemPedidoValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class RemoverItemPedidoValidation : AbstractValidator<RemoverItemPedidoCommand>
    {
        public RemoverItemPedidoValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");
        }
    }
}