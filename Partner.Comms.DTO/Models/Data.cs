using System.Collections.Generic;

namespace Partner.Comms.DTO.Models
{
    public class Data<T>
    {
        public IDictionary<KeyValuePair<string, string>, T> Value { get; set; }

        public Data()
        {
            Value = new Dictionary<KeyValuePair<string, string>, T >();
        }
    }
}