using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Credito.Modelos
{
    public class ExternalTask
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("workerId")]
        public string WorkerId { get; set; }

        [JsonProperty("topicName")]
        public string TopicName { get; set; }

        [JsonProperty("variables")]
        public Dictionary<string, Variable> Variables { get; set; }

        [JsonProperty("businessKey")]
        public string BusinessKey { get; set; }
    }
}
