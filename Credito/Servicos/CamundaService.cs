using Credito.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Credito.Servicos
{
    public class CamundaService : ICamundaService
    {

        private readonly UserAgent _userAgent;
        private readonly string _url;
        private readonly string _worker;

        public CamundaService(UserAgent userAgent)
        {
            _userAgent = userAgent;
            _url = Environment.GetEnvironmentVariable("CamundaUrl");
            _worker = Environment.GetEnvironmentVariable("WorkerId"); 
        }


        public async Task StartProcess(string processKey, Process process)
        {
            await _userAgent.PostAsJsonAsync($"{_url}/process-definition/key/{processKey}/start", process);
        }

        public async Task<List<ExternalTask>> GetExternalTask()
        {
            var request = new
            {
                workerId = _worker,
                maxTasks = 10,
                topics = new[]
                {
                    new
                    {
                        topicName = "analise.topic",
                        lockDuration = 6000
                    },
                    new
                    {
                        topicName = "libera.credito.topic",
                        lockDuration = 6000
                    },
                    new
                    {
                        topicName = "notifica.cliente.topic",
                        lockDuration = 6000
                    },
                }

            };
            var requestBody = JsonConvert.SerializeObject(request);
            return await _userAgent.PostAsJsonAsync<List<ExternalTask>>($"{_url}/external-task/fetchAndLock", requestBody);
        }

        public async Task CompleteExternalTask(string id, Dictionary<string, object> variables = null)
        {
            var request = new CompleteExternalTaskRequest() { WorkerId = _worker, Variables = variables };
            await _userAgent.PostAsJsonAsync<List<Dictionary<string, object>>>($"{_url}/external-task/{id}/complete", JsonConvert.SerializeObject(request));
        }
    }
}
