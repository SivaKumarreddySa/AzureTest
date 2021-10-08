using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Partner.Comms.Common.CustomTypes
{
    [Serializable]
    public class HttpResponseException : Exception
    {
        public int StatusCode { get; set; }
        public HttpResponseMessage Response { get; set; }
        public HttpResponseException(string message, HttpResponseMessage response, string payload = "") : base(message)
        {
            StatusCode = (int)response.StatusCode;
            Response = response;
            base.Data.Add("Response", response);
            base.Data.Add("Payload", payload);
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(GetBaseException());
        }
    }

}
