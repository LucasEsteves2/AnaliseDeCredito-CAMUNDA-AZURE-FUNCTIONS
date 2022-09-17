using Credito.Modelos;
using Credito.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Credito.Functions
{
    public class AnaliseAPI
    {
        private readonly ICamundaService _camundaService;

        public AnaliseAPI(ICamundaService camundaService)
        {
            _camundaService = camundaService;
        }

        [FunctionName("StartAnalise")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            var request = await req.Content.ReadAsAsync<Pessoa>();

            ObjectResult result;

            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                result = new BadRequestObjectResult("Porfavor, informe o seu nome");
            }
            else
            {
                var processo = new Process
                {
                    BusinessKey = "email",
                    Variables = new Dictionary<string, object>
                {
                    {"nome", new Variable {Type="string", Value= request.Nome} },
                    {"email", new Variable {Type="string", Value=request.Email} },
                    {"valor", new Variable {Type="string", Value=request.Valor} },                   

              { "partnerBankTransactions", new JsonValue() { Value = JsonConvert.SerializeObject( new [] {Guid.NewGuid().ToString(),Guid.NewGuid().ToString(),Guid.NewGuid().ToString() }) } }

                }
                };

                await _camundaService.StartProcess("teste", processo);

                result = new OkObjectResult($"Ola, {request.Nome}, seu processo de analise foi criado com sucesso!!");
            }

            return result;

        }


    }
}
