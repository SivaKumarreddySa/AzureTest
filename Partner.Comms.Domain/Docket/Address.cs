using System;

namespace Partner.Comms.Domain.Docket
{
    [Serializable]
    public class Address
    {
        public string addr1 { get; set; }
        public string addr2 { get; set; }
        public string addr3 { get; set; }
        public string email_addr_name { get; set; }
        public string phone_nbr { get; set; }
        public string techone_fld2 { get; set; }
        public string post_code { get; set; }
        public string addr_state { get; set; }
        public string addr_city { get; set; }
    }
}
