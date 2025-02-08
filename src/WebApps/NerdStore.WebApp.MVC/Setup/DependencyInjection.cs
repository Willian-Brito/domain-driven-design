using MediatR;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Modules.Catalogo.Domain.Events;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Catalogo.Domain.Services;
using NerdStore.Modules.Catalogo.Infrastructure.Context;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Events;
using NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Events;
using NerdStore.Modules.Vendas.Domain.Repositories;
using NerdStore.Modules.Vendas.Infrastructure.Context;
using NerdStore.Modules.Vendas.Infrastructure.Repositories;

namespace NerdStore.WebApp.MVC.Setup;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        // Domain Bus (MediatR)
        services.AddScoped<IMessageBus, MessageBus>();

        // Notifications
        services.AddScoped <INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Catalogo
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IEstoqueService, EstoqueService>();

        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoAbaixoEstoqueEventHandler>();
        // services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, ProdutoEventHandler>();
        // services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoEvent>, ProdutoEventHandler>();

        services.AddScoped<CatalogoContext>();

        //Vendas
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        // services.AddScoped<IPedidoQueries, PedidoQueries>();

        services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, AdicionarItemPedidoCommandHandler>();           
        // services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, PedidoCommandHandler>();
        // services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, PedidoCommandHandler>();
            
        services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, AdicionarItemPedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, AdicionarItemPedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, AtualizarItemPedidoEventHandler>();
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

    public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // services.AddDbContext<ApplicationDbContext>(options =>
        //     options.UseSqlServer(connectionString));

        services.AddDbContext<CatalogoContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(CatalogoContext).Namespace)));

        services.AddDbContext<VendasContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(VendasContext).Namespace)));

        // services.AddDbContext<PagamentoContext>(options =>
        //         options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(PagamentoContext).Namespace)));

        // services.AddDatabaseDeveloperPageExceptionFilter();

        // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //     .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}