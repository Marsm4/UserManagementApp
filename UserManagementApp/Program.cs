using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем HTTP-клиент для обращения к API
builder.Services.AddScoped<HttpClient>(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/") });

// Регистрируем сервисы
builder.Services.AddScoped<AdminService>();

// Настроим Blazor WebAssembly или Blazor Server
builder.Services.AddRazorComponents(); // Это нужно только для Blazor Server, для Blazor WebAssembly это не требуется

builder.Services.AddAuthorizationCore(); // Если используете авторизацию

builder.Services.AddControllers(); // Для API контроллеров, если нужны

var app = builder.Build();

// Настройка приложения
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.MapBlazorHub(); // Для Blazor Server
app.MapFallbackToPage("/_Host");

app.Run();
