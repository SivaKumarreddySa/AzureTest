using System;
using System.Collections.Generic;
using System.Text;
using Partner.Comms.Domain.Docket;

namespace Partner.Comms.DTO
{
    [Serializable]
    public class SMSTopicRequestDTO
    {
        public string System { get; set; }
        public string Function { get; set; }
        public string ChannelID { get; set; }
        public string DocketNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }

    [Serializable]
    public class SMSResponseDTO
    {
        public string status { get; set; }
        public DateTimeOffset datetime { get; set; }
    }
    [Serializable]
    public class SMSEndAPIRequestDTO
    {
        
        public string mobile { get; set; }
        public string message { get; set; }
    }

    [Serializable]
    public class SMSEndAPIResponseDTO
    {
        public string status { get; set; }
        
    }
}
