using Credito.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Credito.Servicos
{
    public interface ICamundaService
    {
        Task StartProcess(string processKey, Process process);

        Task<List<ExternalTask>> GetExternalTask();

        Task CompleteExternalTask(string id, Dictionary<string, object> variables = null);
    }
}
