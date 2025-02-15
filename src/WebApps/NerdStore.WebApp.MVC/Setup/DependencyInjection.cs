using MediatR;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Modules.Catalogo.Domain.Events;
using NerdStore.Modules.Catalogo.Domain.Repositories;
using NerdStore.Modules.Catalogo.Domain.Services;
using NerdStore.Modules.Catalogo.Infrastructure.Context;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;
using NerdStore.Modules.Pagamentos.AntiCorruption.Config;
using NerdStore.Modules.Pagamentos.AntiCorruption.Facade;
using NerdStore.Modules.Pagamentos.AntiCorruption.Gateways;
using NerdStore.Modules.Pagamentos.Business.Events;
using NerdStore.Modules.Pagamentos.Business.Facade;
using NerdStore.Modules.Pagamentos.Business.Repositories;
using NerdStore.Modules.Pagamentos.Business.Services;
using NerdStore.Modules.Pagamentos.Data.Context;
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
using NerdStore.Modules.Vendas.Application.UseCases.ObterPedidosCliente.Queries;
using NerdStore.Modules.Vendas.Application.UseCases.ObterPedidosCliente.ViewModel;
using NerdStore.Modules.Vendas.Application.UseCases.RemoverItemPedido.Commands;
using NerdStore.Modules.Vendas.Domain.Repositories;
using NerdStore.Modules.Vendas.Infrastructure.Context;
using NerdStore.Modules.Vendas.Infrastructure.Repositories;
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

        // services.AddScoped<CatalogoContext>();
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

        // var handlers = AppDomain.CurrentDomain.Load("NerdStore.Vendas.Application");
        // services.AddMediatR(config => config.RegisterServicesFromAssemblies(handlers));
    }

    public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<CatalogoContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(CatalogoContext).Namespace)));

        services.AddDbContext<VendasContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(VendasContext).Namespace)));

        services.AddDbContext<PagamentoContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(PagamentoContext).Namespace)));

        // services.AddDatabaseDeveloperPageExceptionFilter();

        // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //     .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}