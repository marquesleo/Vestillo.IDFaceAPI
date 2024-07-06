
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IDFaceAPI46.Entities
{
    public class Result
    {
        [JsonProperty("event")] 
        public int Event { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public bool user_image { get; set; }
        public string message { get; set; }
        public int portal_id { get; set; }
        public List<Action> actions { get; set; }
    }
}
