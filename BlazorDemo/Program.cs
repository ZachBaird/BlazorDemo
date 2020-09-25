using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorDemo.Logging;
using BlazorDemo.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton<IMyDependency, MyDependency>();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Configuration example using in-memory configuration for vehicle data.
            var vehicleData = new Dictionary<string, string>()
            {
                { "color", "blue" },
                { "type", "car" },
                { "wheels:count", "4" },
                { "wheels:brand", "Blazin" },
                { "wheels:brand:type", "rally" },
                { "wheels:year", "2008" }
            };

            // Reading from the local cars.json in wwwroot.
            var client = new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };

            builder.Services.AddScoped(sp => client);

            // Getting the data in the json file and adding it to the configuration via a Json
            //  stream.
            using var response = await client.GetAsync("cars.json");
            using var stream = await response.Content.ReadAsStreamAsync();
            builder.Configuration.AddJsonStream(stream);

            // Creating an in-memory configuration source with our vehicle data and adding it to
            //  the configuration.
            var inMemoryConfig = new MemoryConfigurationSource { InitialData = vehicleData };
            builder.Configuration.Add(inMemoryConfig);

            // Logging. We would write this if we wanted to plug in our own logging provider.
            //builder.Logging.SetMinimumLevel(LogLevel.Debug);
            //builder.Logging.AddProvider(new CustomLoggingProvider());

            await builder.Build().RunAsync();
        }
    }
}
