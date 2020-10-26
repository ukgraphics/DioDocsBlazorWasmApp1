using DioDocsBlazorWasmApp1.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace DioDocsBlazorWasmApp1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var configuration = new ConfigurationBuilder()
                                        .AddJsonStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DioDocsBlazorWasmApp1.appsettings.json"))
                                        .Build();

            builder.Services.AddSingleton(_ => { return configuration.GetSection("LicenseStrings").Get<LicenseStrings>(); });
            builder.Services.AddSingleton(_ => { return configuration.GetSection("ConnectionStrings").Get<AzStorageStrings>(); });

            builder.Services.AddSingleton<DDExcelService>();
            builder.Services.AddSingleton<DDPdfService>();
            
           

            await builder.Build().RunAsync();
        }
    }
}
