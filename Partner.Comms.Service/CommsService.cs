using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Partner.Comms.Common;
using Partner.Comms.Common.CustomTypes;
using Partner.Comms.DTO;
using Partner.Comms.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;

namespace Partner.Comms.Service
{
    public interface ICommsService
    {

        Task<Dictionary<string, int>> RunAsyncLongURL(POSDocketDTO posDocketDTO, string messageId);
        Task<Dictionary<string, int>> RunAsyncShortURL(CommunicationDTO communicationDTO, string messageId);
        Task<Dictionary<string, int>> RunAsyncSmsConsumer(SMSTopicRequestDTO smsTopicRequestDTO, string messageId);
        Task<Dictionary<string, int>> RunAsyncEmailConsumer(PartnerEmailDTO emailDTO, string messageId);
    }
    public class CommsService : ICommsService
    {
        private readonly ILogger _log;
        private readonly HttpProxy _httpProxy;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public static  IConfiguration _config;
        private Dictionary<string, int> _response { get; set; }

        public CommsService(
            ILogger<CommsService> log,
            IHttpClientFactory factory,
            ILogger<HttpProxy> logProxy,
            IMessageService messageService,
            IMapper mapper,
            IConfiguration config
            )
        {
            _log = log;
            _httpProxy = new HttpProxy(factory, logProxy);
            _messageService = messageService;
            _mapper = mapper;
            _response = new Dictionary<string, int>();
            _config = config;
        }

        public async Task<Dictionary<string, int>> RunAsyncLongURL(POSDocketDTO posDocketDTO, string messageid)
        {
            _log.LogInformation(">>> PROCESS RunAsyncLongURL <<<");

                if (CheckPayLinkRules(posDocketDTO))
                {
                        var communicationDTO = GenerateCommunicationDTO(posDocketDTO);
                        if (communicationDTO?.paylinkDTO?.PhoneNumber != null || communicationDTO?.paylinkDTO?.EmailAddress != null)
                        {
                            await _messageService.SendMessagesServiceBusAsync(
                                Environment.GetEnvironmentVariable("ServiceBus:LONGURL:Queue")
                                , communicationDTO
                                , messageid);

                            // 1 - if message is sent 
                            _response.Add(typeof(CommunicationDTO).Name, 1);

                        }              

                }

            _log.LogInformation(">>> COMPLETED RunAsync <<<");

            return _response;
        }
        public async Task<Dictionary<string, int>> RunAsyncShortURL(CommunicationDTO communicationDTO, string messageid)
        {
            _log.LogInformation(">>> PROCESS RunAsyncShortURL <<<");

            var shortURLRequest = GetShortURLRequestDTO(communicationDTO);
            //var shortURLResponse = await SendPartnerCommsShortLinkHttpRequestAsync(shortURLRequest);
            var shortURLResponse = new ShortURLResponseDTO
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ShortUrl = "https://gguys.com.au/OKVicHxH4",
                OriginalUrl = "https://www.thegoodguys.com.au/televisions",
                urlcode = "OKVicHxH4",
                _Id = "614bd3bb00b473535eec86b6",
                _V = "0"
            };

            if (communicationDTO?.paylinkDTO.EmailAddress!=null)
            {
                var email = GetEmailDTO(shortURLResponse, communicationDTO);
                await _messageService.SendMessagesServiceBusAsync(
                                Environment.GetEnvironmentVariable("ServiceBus:SHORTURL-EMAIL:Topic")
                                , email
                                , messageid);
            }
           
            if(communicationDTO?.paylinkDTO?.PhoneNumber!=null)
            {
                var sms = GetSMSDTO(shortURLResponse, communicationDTO);
                await _messageService.SendMessagesServiceBusAsync(Environment.GetEnvironmentVariable("ServiceBus:SHORTURL-SMS:Topic")
                    , sms, messageid);
            }
           


            _log.LogInformation(">>> COMPLETED RunAsyncShortURL <<<");

            return _response;
        }
        public async Task<Dictionary<string, int>> RunAsyncSmsConsumer(SMSTopicRequestDTO smsTopicRequestDTO, string messageid)
        {
            _log.LogInformation(">>> PROCESS RunAsyncSmsConsumer <<<");

           var Message= await SendPartnerCommsSMSHttpRequestAsync(smsTopicRequestDTO);

            _log.LogInformation(">>> COMPLETED RunAsyncSMSConsumer <<<");

            return _response;
        }
        public async Task<Dictionary<string, int>> RunAsyncEmailConsumer(PartnerEmailDTO emailDTO, string messageid)
        {
            _log.LogInformation(">>> PROCESS RunAsyncEmailConsumer <<<");

            var Message = await SendPartnerCommsEmailHttpRequestAsync(emailDTO);

            _log.LogInformation(">>> COMPLETED RunAsyncEmailConsumer <<<");

            return _response;
        }


        private string ValidatePhoneNumber(string phoneNumber)
        {
            if (Regex.IsMatch(phoneNumber, @"^04[0-9]{8}$"))
            {
                return phoneNumber;
            }
            return null;
        }
        private bool CheckPayLinkRules(POSDocketDTO posDocketDTO)
        {
            bool result = false;
            if(posDocketDTO.dkt_details?.dkt_status =="A" &&  posDocketDTO.dkt_details?.doc_bal_amt > 0)
            {
                result = true;
            }
            return result;
        }
        private CommunicationDTO GenerateCommunicationDTO(POSDocketDTO posDocketDTO)
        {
            _log.LogInformation(">>> PROCESS GenerateCommunicationDTO <<<");
            string PhoneNumber = null, EmailAddress = null, LongUrlDTO = null;
            List<ChannelMappingDTO> channel = _config.GetSection("CHANNEL_CONFIG").Get<List<ChannelMappingDTO>>();

            var mapping = channel?.FirstOrDefault(mapping => mapping.CodeID.ToString() == posDocketDTO.dkt_details.ChannelID);

            if (mapping != null && mapping.ActiveFlag == "Y")
            {
                if (mapping.SMS == "Primary" || string.IsNullOrEmpty(mapping.SMS))
                {
                    PhoneNumber = posDocketDTO.dkt_details?.customer?.address?.phone_nbr;
                }
                else if (mapping.SMS == "Delivery")
                {
                    PhoneNumber = posDocketDTO.dkt_details?.customer?.delivery_address?.phone_nbr;
                }
                if (mapping.Email == "Primary" || string.IsNullOrEmpty(mapping.Email))
                {
                    EmailAddress = posDocketDTO.dkt_details?.customer?.address?.email_addr_name;
                }
                else if (mapping.Email == "Delivery")
                {
                    EmailAddress = posDocketDTO.dkt_details?.customer?.delivery_address?.email_addr_name;
                }

                if (!string.IsNullOrEmpty(PhoneNumber))
                {
                    PhoneNumber = ValidatePhoneNumber(PhoneNumber);
                }

                LongUrlDTO = GenerateLongUrlDTO(posDocketDTO);
                PaylinkDTO paylinkDTO = new PaylinkDTO
                {
                    EmailAddress = EmailAddress,
                    PhoneNumber = PhoneNumber,
                    LongPayLink = LongUrlDTO,
                    CustomerName = posDocketDTO.dkt_details?.customer?.cust_disp_name
                }; 
                _log.LogInformation(">>> COMPLETED GenerateCommunicationDTO <<<");
                return new CommunicationDTO
                {
                    pOSDocketDTO = posDocketDTO,
                    paylinkDTO = paylinkDTO
                };
            }
            else
            {
                throw new Exception("Unable to load Paylink Mappings");
            }
            return new CommunicationDTO();
        }
        private string GenerateLongUrlDTO(POSDocketDTO posDocketDTO)
        {
            var hostname = Environment.GetEnvironmentVariable("Host");
            var hostvalue = Environment.GetEnvironmentVariable(hostname);
            var catalogId = Environment.GetEnvironmentVariable("catalogId");
            var storeId = Environment.GetEnvironmentVariable("storeId");
            var langId = Environment.GetEnvironmentVariable("langId");
            var dockethash = posDocketDTO.dkt_uid;
           // var longURL = Environment.GetEnvironmentVariable("")
            // Review: Try to set this up in local.settings.json the same way as below and see if values will come through -- Not getting
            var longURL = $"https://{hostvalue}/PayOnlineView?catalogId={catalogId}&storeId={storeId}&langId={langId}&hash={dockethash}";
            return longURL;
        }

        private SMSTopicRequestDTO GetSMSDTO(ShortURLResponseDTO shortURLResponseDTO, CommunicationDTO commDocketDTO)
        {
            _log.LogInformation(">>> PROCESS GetSMSDTO <<<");
            var body = "Hi " + commDocketDTO?.paylinkDTO?.CustomerName + ",Thank you for shopping at The Good Guys. To finalise the Payment, click on ";
            var Message = body + "" + shortURLResponseDTO?.ShortUrl;
            return new SMSTopicRequestDTO
            {
                System = "POS",
                Function = "Paylink",
                ChannelID = commDocketDTO?.pOSDocketDTO?.dkt_details?.ChannelID,
                DocketNumber = commDocketDTO?.pOSDocketDTO?.dkt_details?.dkt_nbr,
                PhoneNumber = commDocketDTO?.paylinkDTO?.PhoneNumber,
                Message = Message
            };
        }
        private PartnerEmailDTO GetEmailDTO(ShortURLResponseDTO shortURLResponseDTO, CommunicationDTO commDocketDTO)
        {
            var mapped = _mapper.Map<PartnerEmailDTO>(commDocketDTO);
            var serialize = JsonConvert.SerializeObject(mapped);
            return mapped;
        }
        private ShortURLRequestDTO GetShortURLRequestDTO(CommunicationDTO commDocketDTO)
        {
            return new ShortURLRequestDTO {
                originalUrl = commDocketDTO?.paylinkDTO?.LongPayLink
            };
       
        }
        public async Task<string> GetResponse(string requestDTO, string endpointURL)
        {
            _log.LogInformation(">>> PROCESS GetResponse <<<");


            var responseMessage = await _httpProxy.GetResponse(
                           requestDTO,endpointURL);
            _log.LogInformation(responseMessage.ToString());

            if (!responseMessage.IsSuccessStatusCode)
                throw new HttpResponseException(responseMessage.ReasonPhrase, responseMessage, requestDTO);

            var response = responseMessage.Content.ReadAsStringAsync().Result;
            _log.LogInformation(">>> COMPLETED GetResponse <<<");

            return response;
        }
        public async Task<ShortURLResponseDTO> SendPartnerCommsShortLinkHttpRequestAsync(ShortURLRequestDTO shortURLRequestDTO)
        {
            _log.LogInformation(">>> PROCESS SendPartnerCommsShortLinkHttpRequestAsync <<<");

            var json = JsonConvert.SerializeObject(shortURLRequestDTO);
        
            var shortURLResponse = await GetResponse(json, Environment.GetEnvironmentVariable("ShortURLEndPoint"));

            _log.LogInformation(">>> COMPLETED SendPartnerCommsShortLinkHttpRequestAsync Response[result:{result}] <<<", shortURLResponse);

            return JsonConvert.DeserializeObject<ShortURLResponseDTO>(shortURLResponse);
        }
        public async Task<SMSEndAPIResponseDTO> SendPartnerCommsSMSHttpRequestAsync(SMSTopicRequestDTO smsTopicRequestDTO)
        {
            _log.LogInformation(">>> PROCESS SendPartnerCommsSMSHttpRequestAsync <<<");

            var smsEndAPIRequest = _mapper.Map<SMSEndAPIRequestDTO>(smsTopicRequestDTO);
            var json = JsonConvert.SerializeObject(smsEndAPIRequest);
            var smsResponse = await GetResponse(json, Environment.GetEnvironmentVariable("SMSEndPoint"));
            _log.LogInformation(">>> COMPLETED SendPartnerCommsSMSHttpRequestAsync Response[result:{result}] <<<", smsResponse);
            
            return JsonConvert.DeserializeObject<SMSEndAPIResponseDTO>(smsResponse);
        }
        public async Task<PartnerEmailDTO> SendPartnerCommsEmailHttpRequestAsync(PartnerEmailDTO emailDTO)
        {
            _log.LogInformation(">>> PROCESS SendPartnerCommsEmailHttpRequestAsync  <<<");

            var json = JsonConvert.SerializeObject(emailDTO);
            
            var emailResponse = await GetResponse(json, Environment.GetEnvironmentVariable("EmailEndPoint"));
             _log.LogInformation(">>> COMPLETED SendPartnerCommsEmailHttpRequestAsync Response[result:{result}] <<<", emailResponse);

            return JsonConvert.DeserializeObject<PartnerEmailDTO>(emailResponse);
        }

    }
}