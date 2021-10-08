using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Partner.Comms.Common;
using Partner.Comms.DTO;
using Partner.Comms.Service;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Partner.Comms.SMS.FuncApp.Controllers
{
    public class SMSController
    {
        private readonly IErrorService _errorService;
        private readonly ICommsService _commsService;

        public SMSController(
            IErrorService errorService,
            ICommsService commsService)
        {
            _errorService = errorService;
            _commsService = commsService;
        }

        [FunctionName("partner-comms-sms-consumer")]
        public async Task PartnerCommsSmsConsumer(
          [ServiceBusTrigger("comms-sms-sbt","comms-sms-sub-sbs", Connection = "ServiceBus:ConnectionString")]
          string body,
          string messageId,
          ILogger log)
        {
            log.LogInformation(">>> RECEIVED Message[messageId:{messageId}, body: {body}] <<<", messageId, body);

            try
            {
                
                var smsTopicRequestDTO = JsonConvert.DeserializeObject<SMSTopicRequestDTO>(body);
                await _commsService.RunAsyncSmsConsumer(smsTopicRequestDTO, messageId);
            }
            catch (Exception ex)
            {
                await _errorService.Log(ex);
                throw;
            }
        }
    }
}