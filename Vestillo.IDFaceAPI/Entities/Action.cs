using System.Text.Json.Serialization;

namespace Vestillo.IDFaceAPI.Entities
{
    public class Action
    {
        [JsonPropertyName("action")]
        public string ActionName { get; set; }
        [JsonPropertyName("parameters")]
        public string Parameters { get; set; }
    }

}
