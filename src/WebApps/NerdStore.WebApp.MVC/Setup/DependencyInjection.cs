using MediatR;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Modules.Catalogo.Domain.Events;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Catalogo.Domain.Services;
using NerdStore.Modules.Catalogo.Infrastructure.Context;
using NerdStore.Modules.Core.Bus;

namespace NerdStore.WebApp.MVC.Setup;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        // Domain Bus (MediatR)
        services.AddScoped<IMessageBus, MessageBus>();


        // Notifications
        // services.AddScoped <INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Catalogo
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IEstoqueService, EstoqueService>();

        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoAbaixoEstoqueEventHandler>();
        // services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, ProdutoEventHandler>();
        // services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoEvent>, ProdutoEventHandler>();

        services.AddScoped<CatalogoContext>();

        //Vendas
        // services.AddScoped<IPedidoRepository, PedidoRepository>();
        // services.AddScoped<IPedidoQueries, PedidoQueries>();

        // services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();           
        // services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, PedidoCommandHandler>();
            
        // services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
        // services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoEvent>, PedidoEventHandler>();            
        // services.AddScoped<INotificationHandler<PagamentoRealizadoEvent>, PedidoEventHandler>();
        // services.AddScoped<INotificationHandler<PagamentoRecusadoEvent>, PedidoEventHandler>();

        // Pagamento
        // services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        // services.AddScoped<IPagamentoService, PagamentoService>();
        // services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
        // services.AddScoped<IPayPalGateway, PayPalGateway>();
        // services.AddScoped<IConfigManager,ConfigManager>();
    }
}