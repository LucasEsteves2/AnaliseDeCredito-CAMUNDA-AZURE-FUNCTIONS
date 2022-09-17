using Newtonsoft.Json;
using System.Collections.Generic;

namespace Credito.Modelos
{
    public class Process
    {

        [JsonProperty(PropertyName = "variables")]
        public IDictionary<string, object> Variables { get; set; }

        [JsonProperty("businessKey")]
        public string BusinessKey { get; set; }

        [JsonProperty("withVariablesInReturn")]
        public bool WithVariablesInReturn { get; set; }

    }
}
