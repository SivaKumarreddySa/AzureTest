using AutoMapper;
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

namespace Partner.Comms.Email.FuncApp.Controllers
{
    public class EmailController
    {
        private readonly IErrorService _errorService;
        private readonly ICommsService _commsService;

        public EmailController(
            IErrorService errorService,
            ICommsService commsService)
        {
            _errorService = errorService;
            _commsService = commsService;
        }

        [FunctionName("partner-comms-email-consumer")]
        public async Task PartnerCommsEmailConsumer(
          [ServiceBusTrigger("comms-email-sbt","comms-email-sub-sbs", Connection = "ServiceBus:ConnectionString")]
          string body,
          string messageId,
          ILogger log)
        {
            log.LogInformation(">>> RECEIVED Message[messageId:{messageId}, body: {body}] <<<", messageId, body);

            try
            {
                var PartnerEmailDTO = JsonConvert.DeserializeObject<PartnerEmailDTO>(body);
                await _commsService.RunAsyncEmailConsumer(PartnerEmailDTO, messageId);
            }
            catch (Exception ex)
            {
                await _errorService.Log(ex);
                throw;
            }
        }
    }
}