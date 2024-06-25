

using System.Text.Json.Serialization;

namespace Vestillo.IDFaceAPI.Entities
{
    public class NewUserIdentified
    {
        public long device_id { get; set; }
        public int identifier_id { get; set; }
        public int component_id { get; set; }

        [JsonPropertyName("event")]
        public int _event { get; set; }
        public int user_id { get; set; }
        
        public int duress { get; set; }
        public bool face_mask { get; set; }

        public int time { get; set; }

        public int portal_id { get; set; }

        public string uuid { get; set; }

        public string block_read_data { get; set; }

        public int block_read_error { get; set; }

        public int card_value { get; set; }

        public string qrcode_value { get; set; }

        public string uhf_tag { get; set; }


        public string pin_value { get; set; }

        public int user_has_image { get; set; }

        public string user_name { get; set; }

        public string password { get; set; }

        public int confidence { get; set; }

        public int log_type_id { get; set; }


    }
}
