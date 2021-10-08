using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Partner.Comms.Common.Enums;

namespace Partner.Comms.Common
{
    public class HttpProxy
    {
        readonly ILogger _log;
        protected virtual HttpClient _apimClient { get; set; }
        public HttpProxy(IHttpClientFactory httpClientFactory,
           ILogger<HttpProxy> log)
        {
           _log = log;
           _apimClient = httpClientFactory.CreateClient(Client.APIMClient.ToString());
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
           _log.LogInformation(">>> HttpProxy.GET [url: {url}] <<<", url);
           return await _apimClient.GetAsync(url);
        }
        public async Task<HttpResponseMessage> Delete(string url)
        {
           _log.LogInformation(">>> HttpProxy.DELETE [url: {url}] <<<", url);
           return await _apimClient.DeleteAsync(url);
        }

        public async Task<HttpResponseMessage> Post<T>(string url, T request)
        {
           var json = JsonConvert.SerializeObject(request, Formatting.None,
           new JsonSerializerSettings()
           {
               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
           });

           _log.LogInformation(">>> HttpProxy.POST [url: {url}, payload: {json}] <<<", url, json);
           return await _apimClient.PostAsync(url, new StringContent(json.ToString(), Encoding.UTF8, Enums.ContentType.JSON.Description()));
        }
        //public async Task<HttpResponseMessage> Send<T>(string url, T request)
        //{
        //    var json = JsonConvert.SerializeObject(request, Formatting.None,
        //    new JsonSerializerSettings()
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    });

        //    _log.LogInformation(">>> HttpProxy.POST [url: {url}, payload: {json}] <<<", url, json);
        //    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post,url))
        //    {
        //        request.Headers.Accept.Clear();
        //        request.Headers.Add("Api-Key", Environment.GetEnvironmentVariable("APIKeyValue"));

        //        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        //        _log.LogInformation($"{request}");
        //        using (HttpResponseMessage response = await _apimClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
        //        {
        //            _log.LogInformation($"{response}");
        //            // response.EnsureSuccessStatusCode();
        //            if (response.StatusCode == HttpStatusCode.OK)
        //            {
        //                var responseStream = await response.Content.ReadAsStringAsync();
        //                return responseStream;
        //            }
        //        }
        //        return "No Response";
        //    }
        //    return await _apimClient.SendAsync(url, new StringContent(json.ToString(), Encoding.UTF8, Enums.ContentType.JSON.Description()));
        //}
        public async Task<HttpResponseMessage> Put<T>(string url, T request)
        {
           var json = JsonConvert.SerializeObject(request, Formatting.None,
           new JsonSerializerSettings()
           {
               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
           });

           _log.LogInformation(">>> HttpProxy.PUT [url: {url}, payload: {json}] <<<", url, json);
           return await _apimClient.PutAsync(url, new StringContent(json.ToString(), Encoding.UTF8, Enums.ContentType.JSON.Description()));
        }
        public async Task<HttpResponseMessage> GetResponse(string json = "",string endpointURL = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpointURL);
            request.Headers.Accept.Clear();
            request.Headers.Add(Environment.GetEnvironmentVariable("APIM:Header:Key"), Environment.GetEnvironmentVariable("APIKeyValue"));
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _apimClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            return response;
        }
    }
}