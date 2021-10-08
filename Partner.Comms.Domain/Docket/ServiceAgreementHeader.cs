using System;
namespace Partner.Comms.Domain.Docket
{
    [Serializable]
    public class ServiceAgreementHeader
    {
        public string csa { get; set; }
        public DateTime plan_start_date { get; set; }
        public DateTime plan_end_date { get; set; }
        public string insurance_flag { get; set; }
        public string agreement_status { get; set; }
        public int manuf_wty_period { get; set; }
        public int acl_wty_period { get; set; }
        public int period_of_cover_to_purch { get; set; }
        public string original_csa { get; set; }
        public string previous_csa { get; set; }
        public string service_provider { get; set; }
        public string concierge_brand { get; set; }
        public string wty_cost { get; set; }
        public int cover_remaining { get; set; }
        public int total_period { get; set; }
        public decimal concierge_value { get; set; }
        public decimal? orig_prod_value { get; set; }
    }
}
