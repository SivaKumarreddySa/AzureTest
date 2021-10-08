using System;
using System.Collections.Generic;
using System.Text;

namespace Partner.Comms.DTO
{
    [Serializable]
    public class ChannelMappingDTO
    {
        public string Comms { get; set; }
        public string CodeType { get; set; }
        public int CodeID { get; set; }
        public string CodeDescr { get; set; }
        public string ActiveFlag { get; set; }
        public string SMS { get; set; }
        public string Email { get; set; }

    }

}
