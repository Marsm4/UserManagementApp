using Amazon.OpsWorks.Model;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UserManagementApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Отключение проверки SSL (только для разработки)
builder.Services.AddHttpClient("UnsafeHttpClient")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });

// Регистрация UserService с использованием HttpClient
builder.Services.AddScoped<UserService>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("UnsafeHttpClient");
    httpClient.BaseAddress = new Uri("https://localhost:7253"); // Убедись, что адрес совпадает с API
    return new UserService(httpClient);
});

// Добавление сервисов Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Настройка конвейера запросов
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();