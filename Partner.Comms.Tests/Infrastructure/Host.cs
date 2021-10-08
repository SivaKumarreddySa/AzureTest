using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Partner.Comms.PayLink.FuncApp;
using System;
using System.Collections.Generic;
using System.IO;

namespace Partner.Comms.Integration.Tests.Infrastructure
{
    public class Host
    {
        public IServiceProvider ServiceProvider { get; }
        public Host()
        {
            GetEnviornmentVariables();

            var startup = new Startup();
            var host = new HostBuilder()
                .ConfigureWebJobs(startup.Configure)
                .Build();

            ServiceProvider = host.Services;
        }

        public void GetEnviornmentVariables()
        {
            using (var file = File.OpenText($"{Directory.GetCurrentDirectory()}\\local.settings.json"))
            {
                var reader = new JsonTextReader(file);
                var jObject = JObject.Load(reader);
                var values = jObject.GetValue("Values").ToString();
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(values);

                foreach (var item in dictionary)
                {
                    Environment.SetEnvironmentVariable(item.Key, item.Value);
                }
            }
        }
    }
}