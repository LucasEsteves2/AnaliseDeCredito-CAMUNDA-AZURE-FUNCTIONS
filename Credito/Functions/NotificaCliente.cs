using System;
using Credito.Modelos;
using Credito.Servicos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Credito.Functions
{
    public class NotificaCliente
    {
        private readonly ICamundaService _camundaService;

        public NotificaCliente(ICamundaService camundaService)
        {
            _camundaService = camundaService;
        }

        [FunctionName("NotificaCliente")]
        public void Run([ServiceBusTrigger("notifica.cliente.topic", Connection = "SBConnection")] ExternalTask task, ILogger log)
        {
            _camundaService.CompleteExternalTask(task.Id.ToString());
            log.LogInformation($"Task:{task.Id} Completada com sucesso");
        }
    }
}
