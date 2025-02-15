using FluentValidation;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Vendas.Application.Base;

namespace NerdStore.Modules.Vendas.Application.UseCases.AplicarVoucher.Commands;

public class AplicarVoucherPedidoCommand : PedidoCommand
{    
    public Guid ClienteId { get; private set; }
    public string CodigoVoucher { get; private set; }

    public AplicarVoucherPedidoCommand(
        IMessageBus messageBus, 
        Guid clienteId, 
        string codigoVoucher
    ) : base(messageBus)
    {
        ClienteId = clienteId;
        CodigoVoucher = codigoVoucher;
    }

    public override bool EhValido()
    {
        ValidationResult = new AplicarVoucherPedidoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AplicarVoucherPedidoValidation : AbstractValidator<AplicarVoucherPedidoCommand>
{
    public AplicarVoucherPedidoValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.CodigoVoucher)
            .NotEmpty()
            .WithMessage("O código do voucher não pode ser vazio");
    }
}