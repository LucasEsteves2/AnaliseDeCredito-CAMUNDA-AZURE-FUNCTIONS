using Credito.Modelos;
using Credito.Servicos;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Credito.Functions
{
    public class AnaliseCredito
    {
        private readonly ICamundaService _camundaService;

        public AnaliseCredito(ICamundaService camundaService)
        {
            _camundaService = camundaService;
        }

        [FunctionName("AnaliseCredito")]
        public void Run([ServiceBusTrigger("analise.topic", Connection = "SBConnection")] ExternalTask task, ILogger log)
        {
            var variables = new Dictionary<string, object>
            {
                {"creditoAprovado", new Variable {Type="boolean", Value="true"} },
              { "partnerBankTransactions", new Variable() { Value = JsonConvert.SerializeObject( new { ownRecurrence = true,}) } },

            };

            _camundaService.CompleteExternalTask(task.Id.ToString(), variables);

            log.LogInformation($"Task:{task.Id} Completada com sucesso");

        }
    }
}
