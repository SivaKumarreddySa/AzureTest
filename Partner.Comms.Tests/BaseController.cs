using Microsoft.Extensions.Logging;
using Partner.Comms.Integration.Tests.Infrastructure;

namespace Partner.Comms.Integration.Tests
{
    public class BaseController<T>
    {
        private readonly ILogger _logger;
        private readonly Host _host;
        public Host Host => _host;
        public ILogger Logger => _logger;

        public BaseController()
        {
            _host = new Host();
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<T>();
        }
    }
}