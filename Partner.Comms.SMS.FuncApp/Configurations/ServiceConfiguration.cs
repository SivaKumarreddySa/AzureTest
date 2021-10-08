using Microsoft.Extensions.DependencyInjection;
using Partner.Comms.Common;
using Partner.Comms.Repository;
using Partner.Comms.Service;

namespace Partner.Comms.SMS.FuncApp
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<ICommsService, CommsService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ICommsRepository, CommsRepository>();
        }
    }
}