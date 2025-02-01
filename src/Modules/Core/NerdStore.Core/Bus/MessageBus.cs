using MediatR;
using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Core.Bus;

public class MessageBus : IMessageBus
{
    private readonly IMediator _mediator;

    public MessageBus(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task PublicarEvento<T>(T evento) where T : Event
    {
        _mediator.Publish(evento);
    }
}