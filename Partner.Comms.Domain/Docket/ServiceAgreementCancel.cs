using System;

namespace Partner.Comms.Domain.Docket
{
    [Serializable]
    public class ServiceAgreementCancel
    {
        public string csa { get; set; }
        public string agreement_status { get; set; }
        public string insurance_flag { get; set; }
        public string service_provider { get; set; }
    }
}