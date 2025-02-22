using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Data;
using NerdStore.Modules.Core.Messages;
using NerdStore.Modules.Pagamentos.Business.Aggregates;
using NerdStore.Modules.Pagamentos.Business.Entities;
using NerdStore.Modules.Pagamentos.Data.Extensions;

namespace NerdStore.Pagamentos.Data;

public class PagamentoContext : DbContext, IUnitOfWork
{
    private readonly IMessageBus _messageBus;
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    public PagamentoContext() { }

    public PagamentoContext(DbContextOptions<PagamentoContext> options, IMessageBus messageBus) : base(options)
    {
        _messageBus = messageBus;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentoContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) 
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=localhost; Database=NerdStoreDB; User Id=sa; Password=sql@2019; TrustServerCertificate=True;";

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);

            // optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }        
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCadastro").IsModified = false;
            }
        }

        var sucesso = await base.SaveChangesAsync() > 0;
        if (sucesso) await _messageBus.PublicarEventos(this);

        return sucesso;
    }
}
