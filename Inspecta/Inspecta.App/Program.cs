using Inspecta.App;
using Inspecta.App.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// URL da API (podes pôr em appsettings ou usar BaseAddress)
var apiBase = new Uri(builder.HostEnvironment.BaseAddress.Replace("Inspecta.App", "Inspecta.Server"));
// Para desenvolvimento local simples, define manualmente:
apiBase = new Uri("https://localhost:7071/"); // <- ajusta para o porto da tua API


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


// serviços da App
builder.Services.AddScoped<ChecklistApi>();
builder.Services.AddScoped<InspectionApi>();


await builder.Build().RunAsync();
