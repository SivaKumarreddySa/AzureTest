using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Comms.Service
{
    public interface IMessageService
    {
        Task SendMessageServiceBusAsync(string queue, string body, string messageId);
        Task SendMessagesServiceBusAsync<T>(string queue, T record, string messageId);
     
    }

    public class MessageService : IMessageService
    {
        private readonly ILogger _log;
        public MessageService(
            ILogger<MessageService> log
            )
        {
            _log = log;
        }

        public async Task SendMessagesServiceBusAsync<T>(string queue, T dto, string messageId)
        {
                _log.LogInformation(">>> PROCESS {queue} | Message[messageId: {messageId}] <<", queue, messageId);

                await using var client = new ServiceBusClient(Environment.GetEnvironmentVariable("ServiceBus:ConnectionString"),
                        GetServiceBusClientOptions());

                ServiceBusSender sender = client.CreateSender(queue);

                IList<ServiceBusMessage> messages = new List<ServiceBusMessage>();
                
                var body = JsonConvert.SerializeObject(dto);
                messages.Add(new ServiceBusMessage(body));

                _log.LogInformation(">>> Added Message[body:{body}] <<<", body);
               

                await sender.SendMessagesAsync(messages);

                _log.LogInformation($">>> COMPLETED {queue} <<<");
            
        }
      
        public async Task SendMessageServiceBusAsync(string queue, string body, string messageId)
        {
            if (!string.IsNullOrWhiteSpace(body))
            {
                _log.LogInformation(">>> PROCESS {queue} | Message[messageId: {messageId}] <<", queue, messageId);

                await using var client = new ServiceBusClient(Environment.GetEnvironmentVariable("ServiceBus:ConnectionString"),
                         GetServiceBusClientOptions());

                var sender = client.CreateSender(queue);

                var message = new ServiceBusMessage(body);
                _log.LogInformation(">>> Added Message[body:{body}] <<<", body);

                await sender.SendMessageAsync(message);

                _log.LogInformation($">>> COMPLETED {queue} <<<");
            }
        }

        private ServiceBusClientOptions GetServiceBusClientOptions()
        {
            return new ServiceBusClientOptions()
            {
                RetryOptions = new ServiceBusRetryOptions()
                {
                    Mode = ServiceBusRetryMode.Fixed,
                    Delay = TimeSpan.FromSeconds(Convert.ToDouble(Environment.GetEnvironmentVariable("ServiceBus:Retry:Delay"))),
                    MaxDelay = TimeSpan.FromSeconds(Convert.ToDouble(Environment.GetEnvironmentVariable("ServiceBus:Retry:MaxDelay"))),
                    MaxRetries = Convert.ToInt32(Environment.GetEnvironmentVariable("ServiceBus:Retry:MaxRetries"))
                }
            };
        }
    }
}