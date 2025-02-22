using NerdStore.Catalogo.Application.AutoMapper;
using NerdStore.WebApp.MVC.Setup;
using Microsoft.AspNetCore.Identity;
using NerdStore.WebApp.MVC.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddDependencyInjection();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.ApplyMigrations();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vitrine}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
