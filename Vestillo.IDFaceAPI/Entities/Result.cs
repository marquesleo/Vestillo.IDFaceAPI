using System.Text.Json.Serialization;

namespace Vestillo.IDFaceAPI.Entities
{
    public class Result
    {
        [JsonPropertyName("event")]
        public int Event { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public bool user_image { get; set; }
        public string message { get; set; }
        public int portal_id { get; set; }
        public List<Action> actions { get; set; }
    }
}
