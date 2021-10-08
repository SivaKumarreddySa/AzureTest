using System;
using System.Collections.Generic;
namespace Partner.Comms.Domain.Docket
{
    [Serializable]
    public class DktDetails
    {
        public string web_order_nbr { get; set; }
        public string sales_order { get; set; }
        public string dkt_nbr { get; set; }
        public string docket_hash { get; set; }
        public string long_pay_link { get; set; }
        public int pos_locn_nbr { get; set; }
        public string dkt_status { get; set; }
        public string trans_datei { get; set; }
        public int trans_timei { get; set; }
        public string orig_trans_datei { get; set; }
        public string sales_person { get; set; }
        public string ChannelID { get; set; }
        public string ChannelDescription { get; set; }
        public string order_type { get; set; }
        public string cust_nbr { get; set; }
        public string reqd_delv_datei { get; set; }
        public string expiry_datei { get; set; }
        public decimal doc_tot_amt { get; set; }
        public decimal doc_bal_amt { get; set; }
        public decimal doc_taken_amt { get; set; }
        public decimal goodsonorder { get; set; }
        public decimal securitydeposit { get; set; }
        public decimal total_amt { get; set; }
        public decimal total_tax_amt { get; set; }
        public decimal tender_amt { get; set; }
        public decimal change_amt { get; set; }
        public int trigger_correspondence { get; set; }
        public IList<Line> lines { get; set; }
        public Customer customer { get; set; }
        public StoreDetails store_details { get; set; }
    }
}
