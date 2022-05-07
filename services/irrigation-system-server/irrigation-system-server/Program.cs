using IrrigationSystemServer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using IrrigationSystemServer.Areas.Identity;
using IrrigationSystemServer.BackgroundServices;
using IrrigationSystemServer.Data;
using IrrigationSystemServer.Models;
using IrrigationSystemServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services
    .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddScoped<DeviceTypeService>();
builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<MoistureSensorService>();
builder.Services.AddScoped<PlantProfileService>();
builder.Services.AddScoped<IrrigationProfileService>();

builder.Services.AddGrpc();
builder.Services.AddGrpcClient<BrokerGrpc.BrokerGrpcClient>(opt =>
{
    opt.Address = new Uri("https://localhost:7220");
});

builder.Services.AddHostedService<MeasurementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<ServerGrpcService>();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();