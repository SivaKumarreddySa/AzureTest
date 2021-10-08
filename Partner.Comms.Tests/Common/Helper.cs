using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;

namespace Partner.Comms.Integration.Tests
{
    public class Helper
    {
        public static HttpRequest CreateHttpRequest(string body, string method)
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Body = !string.IsNullOrEmpty(body) ? new MemoryStream(Encoding.UTF8.GetBytes(body)) : null;
            request.Method = method;
            return request;
        }

        public static string GetJsonPayload(string path, string fileName)
        {
            var directory = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\netcoreapp3.1", ""); ;
         
            var file = $"{directory}\\{path}{fileName}";
            if (!File.Exists(file))
                throw new ArgumentException($"Could not find file at path: {file}");

            return File.ReadAllText(file);
        }
    }
}