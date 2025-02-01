using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Application.AutoMapper;
using NerdStore.Modules.Catalogo.Infrastructure.Context;
using NerdStore.WebApp.MVC.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectionString));

builder.Services.AddDbContext<CatalogoContext>(options =>
        options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(CatalogoContext).Namespace)));

// builder.Services.AddDbContext<VendasContext>(options =>
//         options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(VendasContext).Namespace)));

// builder.Services.AddDbContext<PagamentoContext>(options =>
//         options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(PagamentoContext).Namespace)));

// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddDependencyInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vitrine}/{action=Index}/{id?}");

app.Run();
