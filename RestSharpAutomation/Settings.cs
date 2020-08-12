using Newtonsoft.Json;

namespace RestSharpAutomation
{
    public class Settings
    {
        [JsonProperty("protocol")]
        public static string Protocol { get; set; }

        [JsonProperty("url")]
        public static string Url { get; set; }
    }
}
