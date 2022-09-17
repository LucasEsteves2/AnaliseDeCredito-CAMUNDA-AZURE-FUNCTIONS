using System;
using Credito.Modelos;
using Credito.Servicos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Credito.Functions
{
    public class LiberarCredito
    {
        private readonly ICamundaService _camundaService;

        public LiberarCredito(ICamundaService camundaService)
        {
            _camundaService = camundaService;
        }


        [FunctionName("LiberarCredito")]
        public void Run([ServiceBusTrigger("libera.credito.topic", Connection = "SBConnection")] ExternalTask task, ILogger log)
        {
            _camundaService.CompleteExternalTask(task.Id.ToString());

            log.LogInformation($"Task:{task.Id} Completada com sucesso");

        }
    }
}
