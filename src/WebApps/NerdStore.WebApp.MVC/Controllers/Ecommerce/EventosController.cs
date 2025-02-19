using Microsoft.AspNetCore.Mvc;
using NerdStore.Modules.Core.Data.EventSourcing;

namespace NerdStore.WebApp.Controllers.Ecommerce;

public class EventosController : Controller
{
    private readonly IEventSourcingRepository _eventSourcingRepository;

    public EventosController(IEventSourcingRepository eventSourcingRepository)
    {
        _eventSourcingRepository = eventSourcingRepository;
    }

    [HttpGet("eventos/{id:guid}")]
    public async Task<IActionResult> Index(Guid id)
    {
        var eventos = await _eventSourcingRepository.ObterEventos(id);
        return View(eventos);
    }
}