using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Partner.Comms.DTO;
using System;
using System.Collections.Generic;
using Polly;
using System.Net.Http;
using System.Net.Http.Headers;
using Partner.Comms.PayLink.FuncApp;
using Partner.Comms.Common;
using static Partner.Comms.Common.Enums;
using System.Configuration;
using Partner.Comms.Service;
using Microsoft.Azure.KeyVault;
using System.IO;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Azure.Services.AppAuthentication;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Partner.Comms.PayLink.FuncApp
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; set; }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string apimHeaderValue;
            var pollyCount = int.Parse(Environment.GetEnvironmentVariable("PollyCount"));
            var pollySpan = double.Parse(Environment.GetEnvironmentVariable("PollySpan"));
            var apimBaseUriClient = Environment.GetEnvironmentVariable("APIM:Base:Uri:Client");
            var apimHeaderKey = Environment.GetEnvironmentVariable("APIM:Header:Key");
            var keyVaultEndpoint = Environment.GetEnvironmentVariable("KVEndpointURL");
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "local";
            var ShortURLAPIKey = Environment.GetEnvironmentVariable("APIKeyValue");

            var context = builder.GetContext();
            builder.Services.ConfigureServices();
            var executioncontextoptions = builder.Services.BuildServiceProvider()
                    .GetService<IOptions<ExecutionContextOptions>>().Value;
            var currentDirectory = executioncontextoptions.AppDirectory;

            //if (!String.IsNullOrEmpty(currentDirectory))
            //{
                IConfiguration config = new ConfigurationBuilder()
                           .SetBasePath(currentDirectory)
                           .AddJsonFile("Paylink.json", optional: true, reloadOnChange: true)
                           .AddEnvironmentVariables()
                           .Build();

                builder.Services.AddSingleton<IConfiguration>(config);
           // }
            // Enable this to run unit tests from local
            //else
            //{
            //    IConfiguration config = new ConfigurationBuilder()
            //                  .SetBasePath(currentDirectory)
            //                  .AddJsonFile("Paylink.json", optional: true, reloadOnChange: true)
            //                  .AddEnvironmentVariables()
            //                  .Build();

            //    builder.Services.AddSingleton<IConfiguration>(config);

            //}

            apimHeaderValue = ShortURLAPIKey ?? Environment.GetEnvironmentVariable("APIM:Header:Value") ;


            builder.Services.AddAutoMapper(typeof(FuncApp.AutoMapperProfiles));

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            builder.Services.AddHttpClient(Client.APIMClient.ToString(), client =>
            {
                client.BaseAddress = new Uri(apimBaseUriClient);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Enums.ContentType.JSON.Description()));
                client.DefaultRequestHeaders.Add(apimHeaderKey, apimHeaderValue);
            }).AddTransientHttpErrorPolicy(p =>
                p.WaitAndRetryAsync(pollyCount, _ => TimeSpan.FromMilliseconds(pollySpan)))
               .ConfigurePrimaryHttpMessageHandler(() =>
               {
                   return new HttpClientHandler
                   {
                       ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyError) => true
                   };
               });



        }
    }
}