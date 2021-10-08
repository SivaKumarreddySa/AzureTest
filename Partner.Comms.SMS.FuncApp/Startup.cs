using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Polly;
using System.Net.Http;
using System.Net.Http.Headers;
using Partner.Comms.SMS.FuncApp;
using Partner.Comms.Common;
using static Partner.Comms.Common.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Azure.KeyVault;
using System.IO;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Partner.Comms.SMS.FuncApp
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
            var SMSAPIMKey = Environment.GetEnvironmentVariable("APIKeyValue");

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "local";
            var context = builder.GetContext();
            builder.Services.ConfigureServices();

            if (keyVaultEndpoint != null)
            {
               
                apimHeaderValue = SMSAPIMKey;
            }
            else
            {
                apimHeaderValue = Environment.GetEnvironmentVariable("APIM:Header:Value");
            }

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