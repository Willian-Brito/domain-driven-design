using Microsoft.AspNetCore.Mvc;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Vendas.Application.UseCases.ObterEventos.Queries;
using NerdStore.Modules.Vendas.Application.UseCases.ObterEventos.ViewModel;

namespace NerdStore.WebApp.Controllers.Ecommerce;

public class EventoController : Controller
{
    private readonly IMessageBus _messageBus;

    public EventoController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    [HttpGet("eventos/{id:guid}")]
    public async Task<IActionResult> Index(Guid id)
    {
        var query = new ObterEventosQuery { AggregateId = id };
        var eventos = await _messageBus.EnviarQuery<ObterEventosQuery, IEnumerable<EventosViewModel>>(query);

        return View(eventos);
    }
}