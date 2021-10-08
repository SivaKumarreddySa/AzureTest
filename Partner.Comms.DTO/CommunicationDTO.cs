using Partner.Comms.Domain.Docket;
using System;

namespace Partner.Comms.DTO
{
    [Serializable]
    public class CommunicationDTO
    {
        public POSDocketDTO pOSDocketDTO { get; set; }
        public PaylinkDTO paylinkDTO { get; set; }
    }
    public class PaylinkDTO
    {
        public string LongPayLink { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string ShortPayLink { get; set; }
    }
}
