using System;
using System.Collections.Generic;

namespace Partner.Comms.Domain.Docket
{
    [Serializable]
    public class MobileStandardProduct
    {
        public DateTime? orig_trans_datei_sub { get; set; }
        public int dkt_lne_nbr { get; set; }
        public string trans_type { get; set; }
        public decimal trans_amt { get; set; }
        public double trans_qty { get; set; }
        public decimal vat_amt { get; set; }
        public decimal total_line_amt { get; set; }
        public decimal total_line_vat { get; set; }
        public string apprvl_nbr { get; set; }
        public string wty_ref_nbr { get; set; }
        public int? wty_ref_lne_nbr { get; set; }
        public string wty_prod_nbr { get; set; }
        public string wty_ref_type { get; set; }
        public string extern_ref1 { get; set; }
        public string ge_exc_flag { get; set; }
        public ProdDetails prod_details { get; set; }
        public IList<ServiceAgreementHeader> service_agreement_header { get; set; }
    }
}