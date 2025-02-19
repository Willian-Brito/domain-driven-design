using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.ObterPedidosCliente.Queries;
using NerdStore.Modules.Vendas.Application.UseCases.ObterPedidosCliente.ViewModel;
using Base = NerdStore.WebApp.Controllers.Base;

namespace NerdStore.WebApp.Controllers.Ecommerce;
public class PedidoController : Base.ControllerBase
{
    private readonly IMessageBus _messageBus;

    public PedidoController(
        INotificationHandler<DomainNotification> notifications,
        IMessageBus messageBus,
        IHttpContextAccessor httpContextAccessor
    ) : base(notifications, messageBus)
    {
        _messageBus = messageBus;
    }

    [HttpGet]
    [Route("meus-pedidos")]
    public async Task<IActionResult> Index()
    {
        var query = new ObterPedidosClienteQuery { ClienteId = ClienteId };
        var pedidos = await _messageBus.EnviarQuery<ObterPedidosClienteQuery, IEnumerable<PedidoViewModel>>(query);

        return View(pedidos);
    }
}
