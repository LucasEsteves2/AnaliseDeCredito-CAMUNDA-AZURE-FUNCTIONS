using Newtonsoft.Json;

namespace Credito.Modelos
{
    public class Variable
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }
    public class Value
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class JsonValue : Value
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        public JsonValue()
        {
            Type = "json";
        }
    }

}
