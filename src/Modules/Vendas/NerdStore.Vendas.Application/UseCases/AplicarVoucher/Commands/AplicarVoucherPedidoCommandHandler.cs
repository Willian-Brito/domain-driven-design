using MediatR;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.AplicarVoucher.Events;
using NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Events;
using NerdStore.Modules.Vendas.Domain.Repositories;

namespace NerdStore.Modules.Vendas.Application.UseCases.AplicarVoucher.Commands;

public class AplicarVoucherPedidoCommandHandler : IRequestHandler<AplicarVoucherPedidoCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMessageBus _messageBus;
    public AplicarVoucherPedidoCommandHandler(IPedidoRepository pedidoRepository, IMessageBus messageBus)
    {
        _pedidoRepository = pedidoRepository;
        _messageBus = messageBus;
    }

    public async Task<bool> Handle(AplicarVoucherPedidoCommand message, CancellationToken cancellationToken)
    {
        if (!message.ValidarComando()) return false;

        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

        if (pedido == null)
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
            return false;
        }

        var voucher = await _pedidoRepository.ObterVoucherPorCodigo(message.CodigoVoucher);

        if (voucher == null)
        {
            await _messageBus.PublicarNotificacao(new DomainNotification("pedido", "Voucher não encontrado!"));
            return false;
        }

        var voucherAplicacaoValidation = pedido.AplicarVoucher(voucher);
        if (!voucherAplicacaoValidation.IsValid)
        {
            foreach (var error in voucherAplicacaoValidation.Errors)
            {
                await _messageBus.PublicarNotificacao(new DomainNotification(error.ErrorCode, error.ErrorMessage));
            }

            return false;
        }

        pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
        pedido.AdicionarEvento(new VoucherAplicadoPedidoEvent(message.ClienteId, pedido.Id, voucher.Id));

        _pedidoRepository.Atualizar(pedido);

        return await _pedidoRepository.UnitOfWork.Commit();
    }
}