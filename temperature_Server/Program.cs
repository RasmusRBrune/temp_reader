using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using temperature_Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using temperature_Server.Services;
using temperature_Server.Repositories;
using temperature_Server.Data.Context;
using Microsoft.AspNetCore.Components.Authorization;
using temperature_Server.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient(typeof(IAccountRepository), typeof(AccountRepository));
builder.Services.AddTransient(typeof(ITemperatureReaderDeviceRepository), typeof(TemperatureReaderDeviceRepository));
builder.Services.AddTransient(typeof(ITemperatureReaderDeviceKeyRepository), typeof(TemperatureReaderDeviceKeyRepository));
builder.Services.AddTransient(typeof(IDeviceTimeLogRepository), typeof(DeviceTimeLogRepository));
builder.Services.AddTransient(typeof(ITemperatureReadingRepository), typeof(TemperatureReadingRepository));

builder.Services.AddTransient(typeof(IAccountService), typeof(AccountService));
builder.Services.AddTransient(typeof(ITemperatureReaderDeviceService), typeof(TemperatureReaderDeviceService));
builder.Services.AddTransient(typeof(IDeviceTimeLogService), typeof(DeviceTimeLogService));
builder.Services.AddTransient(typeof(ITemperatureReadingService), typeof(TemperatureReadingService));

var IdentityConnection = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'temperature_ServerContextConnection' not found.");

builder.Services.AddDbContext<temperature_ServerContext>(options => options.UseSqlServer(IdentityConnection));

var connectionString = builder.Configuration.GetConnectionString("TempConnection") ?? throw new InvalidOperationException("Connection string 'temperature_ServerContextConnection' not found.");

builder.Services.AddDbContext<TempReaderContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<temperature_ServerContext>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
