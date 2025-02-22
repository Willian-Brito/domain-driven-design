using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NerdStore.Modules.Catalogo.Domain.Aggregates;
using NerdStore.Modules.Catalogo.Domain.Entity;
using NerdStore.Modules.Core.Data;
using NerdStore.Modules.Core.Messages;

namespace NerdStore.Catalogo.Infrastructure;

public class CatalogoContext : DbContext, IUnitOfWork
{
    public CatalogoContext(){ }
    public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var property in builder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        builder.Entity<Produto>()
            .Property(p => p.Valor)
            .HasColumnType("decimal(18,2)");

        builder.Ignore<Event>();

        builder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=localhost; Database=NerdStoreDB; User Id=sa; Password=sql@2019; TrustServerCertificate=True;";

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);

        }
             
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
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

        return await base.SaveChangesAsync() > 0;
    }
}