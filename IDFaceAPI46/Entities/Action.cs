using Newtonsoft.Json;
namespace IDFaceAPI46.Entities
{
    public class Action
    {
        [JsonProperty("action")]
        public string ActionName { get; set; }
        [JsonProperty("event")]
        public string Parameters { get; set; }
    }

}
