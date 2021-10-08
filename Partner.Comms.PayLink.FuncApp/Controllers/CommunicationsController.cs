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

namespace Partner.Comms.PayLink.FuncApp.Controllers
{
    public class CommunicationsController
    {
        private readonly IErrorService _errorService;
        private readonly ICommsService _commsService;

        public CommunicationsController(
            IErrorService errorService,
            ICommsService commsService)
        {
            _errorService = errorService;
            _commsService = commsService;
        }


        #region Paylink Rules

        [FunctionName("partner-comms-create-longurl")]
        public async Task PartnerCommsCreateLongUrl(
          [ServiceBusTrigger("comms-main-sbt","comms-paylink-sub-sbs", Connection = "ServiceBus:ConnectionString")]
          string body,
          string messageId,
          ILogger log)
        {
            log.LogInformation(">>> RECEIVED Message[messageId:{messageId}, body: {body}] <<<", messageId, body);

            try
            {
                var posDocketDTO = JsonConvert.DeserializeObject<POSDocketDTO>(body);
                await _commsService.RunAsyncLongURL(posDocketDTO, messageId);
            }
            catch (Exception ex)
            {
                await _errorService.Log(ex);
                throw;
            }
        }


        [FunctionName("httptrigger-partner-comms-create-longurl")]
        public async Task<IActionResult> HttpTriggerCreateLongURL(
      [HttpTrigger(AuthorizationLevel.Function, "post", Route = "consumer/{messageId}")]
          HttpRequest req,
       string messageId,
       ILogger log)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation(">>> RECEIVED Message[messageId:{messageId}, body: {body}] <<<", messageId, body);

            try
            {
                var posDocketDTO = JsonConvert.DeserializeObject<POSDocketDTO>(body);
                var response = await _commsService.RunAsyncLongURL(posDocketDTO, messageId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                await _errorService.Log(ex);
                throw;
            }
        }

        #endregion


        #region ShortURL
        [FunctionName("partner-comms-Invoke-shorturl")]
        public async Task PartnerCommsInvokeShortUrl(
          [ServiceBusTrigger("comms-paylink-sbq", Connection = "ServiceBus:ConnectionString")]
          string body,
          string messageId,
          ILogger log)
        {
            log.LogInformation(">>> RECEIVED Message[messageId:{messageId}, body: {body}] <<<", messageId, body);

            try
            {
                var communicationDTO = JsonConvert.DeserializeObject<CommunicationDTO>(body);
                await _commsService.RunAsyncShortURL(communicationDTO, messageId);
            }
            catch (Exception ex)
            {
                await _errorService.Log(ex);
                throw;
            }
        }


        #endregion

    }
}