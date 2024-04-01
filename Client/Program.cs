using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Blazored.Toast;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredToast();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Logging.SetMinimumLevel(LogLevel.Information); 

var host = builder.Build(); // To add Logger, I need the `host` variable

var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

builder.Logging.SetMinimumLevel(LogLevel.Warning);
logger.LogWarning("{project}!{class} before host.RunAsync().", nameof(Client), nameof(Program));

await host.RunAsync();

