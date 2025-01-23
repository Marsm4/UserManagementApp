using Amazon.OpsWorks.Model;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UserManagementApp.Services;

var builder = WebApplication.CreateBuilder(args);

// ���������� �������� SSL (������ ��� ����������)
builder.Services.AddHttpClient("UnsafeHttpClient")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });

// ����������� UserService � �������������� HttpClient
builder.Services.AddScoped<UserService>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("UnsafeHttpClient");
    httpClient.BaseAddress = new Uri("https://localhost:7253"); // �������, ��� ����� ��������� � API
    return new UserService(httpClient);
});

// ���������� �������� Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// ��������� ��������� ��������
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