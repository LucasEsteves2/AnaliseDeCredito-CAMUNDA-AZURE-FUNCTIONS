using Credito.Servicos;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Credito.Functions

{
    public class GetExternalTask
    {
        private readonly ICamundaService _camundaService;
        private readonly ServiceBusQueueService _serviceBusQueueService;

        public GetExternalTask(ICamundaService camundaService, ServiceBusQueueService serviceBusQueueService)
        {
            _camundaService = camundaService;
            _serviceBusQueueService = serviceBusQueueService;
        }

        [FunctionName("GetExternalTask")]
        public async Task Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            var response = await _camundaService.GetExternalTask();
            foreach (var task in response)
            {
                await _serviceBusQueueService.SendMessage(task.TopicName, JsonConvert.SerializeObject(task));
                log.LogInformation($"{response.Count} processo parado no topico {task.TopicName}");
            }
        }
    }
}
