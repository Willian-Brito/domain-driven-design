using EventSourcing.Repositories;
using EventSourcing.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Infrastructure;
using NerdStore.Modules.Catalogo.Domain.Events;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Catalogo.Domain.Services;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Data.EventSourcing;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Pagamentos.AntiCorruption.Config;
using NerdStore.Modules.Pagamentos.AntiCorruption.Facade;
using NerdStore.Modules.Pagamentos.AntiCorruption.Gateways;
using NerdStore.Modules.Pagamentos.Business.Events;
using NerdStore.Modules.Pagamentos.Business.Facade;
using NerdStore.Modules.Pagamentos.Business.Repositories;
using NerdStore.Modules.Pagamentos.Business.Services;
using NerdStore.Modules.Pagamentos.Data.Repositories;
using NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Events;
using NerdStore.Modules.Vendas.Application.UseCases.AplicarVoucher.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Events;
using NerdStore.Modules.Vendas.Application.UseCases.CancelarPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.CancelarPedido.Events;
using NerdStore.Modules.Vendas.Application.UseCases.CancelarPedidoEstornarEstoque.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.CancelarPedidoEstornarEstoque.Events;
using NerdStore.Modules.Vendas.Application.UseCases.FinalizarPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.FinalizarPedido.Events;
using NerdStore.Modules.Vendas.Application.UseCases.IniciarPedido.Commands;
using NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.Queries;
using NerdStore.Modules.Vendas.Application.UseCases.ObterCarrinhoCliente.ViewModel;
using NerdStore.Modules.Vendas.Application.UseCases.ObterEventos.Queries;
using NerdStore.Modules.Vendas.Application.UseCases.ObterEventos.ViewModel;
using NerdStore.Modules.Vendas.Application.UseCases.ObterPedidosCliente.Queries;
using NerdStore.Modules.Vendas.Application.UseCases.ObterPedidosCliente.ViewModel;
using NerdStore.Modules.Vendas.Application.UseCases.RemoverItemPedido.Commands;
using NerdStore.Modules.Vendas.Domain.Repositories;
using NerdStore.Modules.Vendas.Infrastructure.Repositories;
using NerdStore.Pagamentos.Data;
using NerdStore.Vendas.Infrastructure;
using NerdStore.WebApp.MVC.Data;

namespace NerdStore.WebApp.MVC.Setup;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        // Domain Bus (MediatR)
        services.AddScoped<IMessageBus, MessageBus>();

        // Notifications
        services.AddScoped <INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        
        #region Catalogo
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IEstoqueService, EstoqueService>();

        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoAbaixoEstoqueEventHandler>();
        services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, ProdutoSaidaEstoqueEventHandler>();
        services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoEvent>, ProdutoEntradaEstoqueEventHandler>();        
        #endregion

        #region Vendas
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IRequestHandler<ObterCarrinhoClienteQuery, CarrinhoViewModel>, ObterCarrinhoClienteQuery.ObterCarrinhoClienteQueryHandler>();
        services.AddScoped<IRequestHandler<ObterPedidosClienteQuery, IEnumerable<PedidoViewModel>>, ObterPedidosClienteQuery.ObterPedidosClienteQueryHandler>();

        services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, AdicionarItemPedidoCommandHandler>();           
        services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, AtualizarItemPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, RemoverItemPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, AplicarVoucherPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, IniciarPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, FinalizarPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, CancelarProcessamentoPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, CancelarProcessamentoPedidoEstornarEstoqueCommandHandler>();
            
        services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, AdicionarItemPedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, AdicionarItemPedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, AtualizarItemPedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoEvent>, PedidoEstoqueRejeitadoEventHandler>();            
        services.AddScoped<INotificationHandler<PagamentoRealizadoEvent>, PagamentoRealizadoEventHandler>();
        services.AddScoped<INotificationHandler<PagamentoRecusadoEvent>, PagamentoRecusadoEventHandler>();
        #endregion

        #region Pagamento
        services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        services.AddScoped<IPagamentoService, PagamentoService>();
        services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
        services.AddScoped<IPayPalGateway, PayPalGateway>();
        services.AddScoped<IConfigManager,ConfigManager>();

        services.AddScoped<INotificationHandler<PedidoEstoqueConfirmadoEvent>, PagamentoEventHandler>(); 
        #endregion

        #region EventSourcing        
        services.AddSingleton<IEventStoreService, EventStoreService>();
        services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
        services.AddScoped<IRequestHandler<ObterEventosQuery, IEnumerable<EventosViewModel>>, ObterEventosQuery.ObterEventosQueryHandler>();
        #endregion
    }

    public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        var eventStoreConnection = configuration.GetConnectionString("EventStoreConnection") ?? throw new InvalidOperationException("Connection string 'EventStoreConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<CatalogoContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(CatalogoContext).Namespace)));

        services.AddDbContext<VendasContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(VendasContext).Namespace)));

        services.AddDbContext<PagamentoContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(PagamentoContext).Namespace)));

        // services.AddEventStoreClient(eventStoreConnection);
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            void MigrateDb<T>(IServiceScope scope) where T : DbContext
            {
                var context = scope.ServiceProvider.GetService<T>();
                context?.Database.Migrate();
            }

            MigrateDb<ApplicationDbContext>(serviceScope);
            MigrateDb<CatalogoContext>(serviceScope);
            MigrateDb<VendasContext>(serviceScope);
            MigrateDb<PagamentoContext>(serviceScope);
        }
    }
}