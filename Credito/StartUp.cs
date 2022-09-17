using Credito.Servicos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Credito.StartUp))]
namespace Credito
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ICamundaService, CamundaService>();
            builder.Services.AddScoped<ServiceBusQueueService, ServiceBusQueueService>();
            builder.Services.AddScoped<UserAgent, UserAgent>();

        }
    }
}
