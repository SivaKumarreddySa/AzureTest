using System;

namespace Partner.Comms.Domain.Docket
{
    [Serializable]
    public class Customer
    {
        public string cust_disp_name { get; set; }
        public string cust_first_name { get; set; }
        public string cust_surname { get; set; }
        public string email_ind { get; set; }
        public string post_ind { get; set; }
        public string sms_ind { get; set; }
        public Address address { get; set; }
        public DeliveryAddress delivery_address { get; set; }
    }
}
