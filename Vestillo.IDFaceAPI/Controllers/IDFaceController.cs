using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vestillo.IDFaceAPI.Entities;

namespace Vestillo.IDFaceAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class IDFaceController : ControllerBase
    {
        private readonly ILogger<IDFaceController> _logger;


        public IDFaceController(ILogger<IDFaceController> logger)
        {
            this._logger = logger;
        }


        public class Action
        {
            [JsonPropertyName("action")]
            public string ActionName { get; set; }
            [JsonPropertyName("parameters")]
            public string Parameters { get; set; }
        }

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

        public class Root
        {
            public Result Result { get; set; }
        }


        [HttpPost]
        [Route("new_user_identified.fcgi")]
        public IActionResult NewUserIdentified([FromForm] int user_id, [FromForm] string user_name)
        {

            _logger.LogInformation($"new user identified");
        
          
            if (user_id > 0 )
            {
                _logger.LogInformation($"new_user_identified.fcgi ------=> user_id:{user_id}, user_name:{user_name}");
                          

              
                string msg = "Procure a administração!!";
                string jsonString = "";


                if (!RetornarUsuarioValido(user_id))
                {
                    _logger.LogInformation($"vai bloquear");

                    var root = new Root();
                    root.Result = new Result();
                    root.Result.Event = 6;
                    root.Result.user_id = user_id;
                    root.Result.user_name = user_name;
                    root.Result.message = msg;
                    root.Result.portal_id = 1;
                    root.Result.actions = new List<Action>();

                     jsonString = JsonSerializer.Serialize(root);
                    _logger.LogInformation($"new_user_identified.fcgi ------=> objeto:{jsonString}");
                    _logger.LogInformation($"new_user_identified.fcgi ------=> entrada bloqueada");
                    return Ok(root);

                }
                else
                {



                    var root = new Root();
                    root.Result = new Result();
                    root.Result.Event = 7;
                    root.Result.user_id = user_id;
                    root.Result.user_name = user_name;
                    root.Result.message = "Entrada Liberada";
                    root.Result.portal_id = 1;
                    root.Result.actions = new List<Action>();
                    root.Result.actions.Add(new Action()
                    {
                        ActionName = "sec_box",
                        Parameters = "id=65793, reason=1"
                    });
                    jsonString = JsonSerializer.Serialize(root);
                    _logger.LogInformation($"new_user_identified.fcgi ------=> objeto:{jsonString}");
                    _logger.LogInformation($"new_user_identified.fcgi ------=> entrada liberada");



                    return Ok(root);
                }
            }
            else
            {
                _logger.LogInformation("new_user_identified.fcgi  ----- Usuário não encontrado!");
                return Ok();
            }
        }


        private bool RetornarUsuarioValido(int idUser)
        {
            return false;
        }

        [HttpPost]
        [Route("device_is_alive.fcgi")]
        public async Task<IActionResult> deviceisalive(int access_logs)
        {
            _logger.LogInformation("deviceisalive");
            return Ok();
        }

        [HttpGet]
        [Route("teste.fcgi")]
        public async Task<IActionResult> teste()
        {
            _logger.LogInformation("teste");
            return Ok("teste");
        }
    }
}
