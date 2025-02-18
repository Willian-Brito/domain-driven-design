using MediatR;
using NerdStore.Modules.Core.Data.EventSourcing;
using NerdStore.Modules.Core.Messages;
using NerdStore.Modules.Core.Messages.CommonMessages.DomainEvents;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Modules.Core.Communication.Mediator;

public class MessageBus : IMessageBus
{
    private readonly IMediator _mediator;
    private readonly IEventSourcingRepository _eventSourcingRepository;

    public MessageBus(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
    {
        _mediator = mediator;
        _eventSourcingRepository = eventSourcingRepository;
    }

    public async Task<bool> EnviarComando<T>(T comando) where T : Command
    {
        return await _mediator.Send(comando);
    }

    public async Task<TResult> EnviarQuery<TQuery, TResult>(TQuery query) where TQuery : IRequest<TResult>
    {
        return await _mediator.Send(query);
    }

    public async Task PublicarEvento<T>(T evento) where T : Event
    {
        await _mediator.Publish(evento);
        await _eventSourcingRepository.SalvarEvento(evento);
    }

    public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
    {
        await _mediator.Publish(notificacao);
    }

    public async Task PublicarDomainEvent<T>(T evento) where T : DomainEvent
    {
        await _mediator.Publish(evento);
    }
}