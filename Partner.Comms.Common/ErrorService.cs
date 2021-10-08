using Microsoft.Extensions.Logging;
using Partner.Comms.Common.CustomTypes;
using System;
using System.Threading.Tasks;

namespace Partner.Comms.Common
{
    public interface IErrorService
    {
        Task<bool> Log(Exception ex);
    }
    public class ErrorService : IErrorService
    {
        readonly ILogger _log;
        public ErrorService(ILogger<ErrorService> log)
        {
            _log = log;
        }

        public async Task<bool> Log(Exception ex)
        {
            return await Task.Run(() =>
            {
                var isLogLevelError = true;

                switch (ex.GetType().Name)
                {
                    case nameof(HttpResponseException):
                        isLogLevelError = Helper.IsLogErrorLevel((ex as HttpResponseException).StatusCode);
                        if (!isLogLevelError)
                            _log.LogWarning(ex, ">>> [StatusCode:{statusCode}, Message:{message}, Exception: {exception}] <<<", (ex as HttpResponseException).StatusCode, ex.Message, ex.GetBaseException().ToString());
                        else
                            _log.LogError(ex, ">>> [StatusCode:{statusCode}, Message:{message}, Exception: {exception}] <<<", (ex as HttpResponseException).StatusCode, ex.Message, ex.GetBaseException().ToString());
                        break;
                    default:
                        _log.LogError(ex, $">>> {ex.Message} <<<");
                        break;
                }

                return isLogLevelError;
            });
        }
    }
}
