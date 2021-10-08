
using Partner.Comms.Domain.Docket;
using System;

namespace Partner.Comms.DTO
{
    [Serializable]
    public class POSDocketDTO
    {
        public string dkt_state { get; set; }
        public string dkt_uid { get; set; }
        public DktDetails dkt_details { get; set; }
        
    }
}
