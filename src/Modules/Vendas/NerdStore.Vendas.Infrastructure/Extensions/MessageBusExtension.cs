using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.DomainObjects;
using NerdStore.Modules.Vendas.Infrastructure.Context;

namespace NerdStore.Modules.Vendas.Infrastructure.Extension;

public static class MessageBusExtension
{
    public static async Task PublicarEventos(this IMessageBus messageBus, VendasContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Eventos != null && x.Entity.Eventos.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Eventos)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await messageBus.PublicarEvento(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}