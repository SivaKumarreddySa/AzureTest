using System;

namespace Partner.Comms.Domain.Docket
{
    [Serializable]
    public class StoreDetails
    {
        public string descr { get; set; }
        public string long_descr { get; set; }
        public string email { get; set; }
        public string addr1 { get; set; }
        public string addr2 { get; set; }
        public string addr_city { get; set; }
        public string addr_state { get; set; }
        public string post_code { get; set; }
        public string phone_nbr { get; set; }
        public string fax_nbr { get; set; }
        public string locn_business_nbr { get; set; }
    }
}
