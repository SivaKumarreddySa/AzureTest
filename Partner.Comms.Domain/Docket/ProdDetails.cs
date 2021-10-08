using System;

namespace Partner.Comms.Domain.Docket
{
    [Serializable]
    public class ProdDetails
    {
        public string prod_nbr { get; set; }
        public string product_descr { get; set; }
        public string prod_type { get; set; }
        public string sup_prod_nbr { get; set; }
        public string brand { get; set; }
        public string wty_resale_productid { get; set; }
        public string deptid { get; set; }
        public string deptname { get; set; }
        public string classid { get; set; }
        public string classname { get; set; }
        public string subclassid { get; set; }
        public string subclassname { get; set; }
    }
}
