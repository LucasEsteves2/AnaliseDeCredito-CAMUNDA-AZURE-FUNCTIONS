using Newtonsoft.Json;
using System.Collections.Generic;

namespace Credito.Modelos
{
    public class CompleteExternalTaskRequest
    {
        [JsonProperty("workerId")]
        public string WorkerId { get; set; }

        [JsonProperty("variables")]
        public Dictionary<string, object> Variables { get; set; }
    }
}