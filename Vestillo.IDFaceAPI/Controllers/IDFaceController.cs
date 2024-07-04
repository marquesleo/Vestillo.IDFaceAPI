using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Vestillo.IDFaceAPI.Entities;


namespace Vestillo.IDFaceAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class IDFaceController : ControllerBase
    {
        private readonly ILogger<IDFaceController> _logger;
      

        public IDFaceController(ILogger<IDFaceController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            Connection.ProviderFactory.StringConnection = configuration.GetConnectionString("db");
            Lib.Funcoes.SetIdEmpresaLogada = Convert.ToInt32(configuration.GetSection("parametros").GetSection("empresa").Value);
            Lib.Funcoes.UtilizaAPI = true;
        }


        [HttpPost]
        [Route("new_user_identified.fcgi")]
        public IActionResult NewUserIdentified([FromForm] int user_id, [FromForm] string user_name)
        {

            _logger.LogInformation($"new user identified");
        
          
            if (user_id > 0 )
            {
                _logger.LogInformation($"new_user_identified.fcgi ------=> user_id:{user_id}, user_name:{user_name}");

                var nomeDoUsuario = string.Empty;
                var vetUsuario = user_name.Split('|');
                if (vetUsuario != null && vetUsuario.Length > 0)
                {
                    nomeDoUsuario = vetUsuario[0];
                }
                else
                    nomeDoUsuario = user_name;
              
                string msg = "Procure a Secretaria!!";
                string jsonString = "";


                if (!RetornarUsuarioValido(user_id,user_name))
                {

                   
                    var root = new Root();
                    root.Result = new Result();
                    root.Result.Event = 6;
                    root.Result.user_id = user_id;
                    root.Result.user_name = nomeDoUsuario;
                    root.Result.message = msg;
                    root.Result.portal_id = 1;
                    root.Result.actions = new List<Entities.Action>();

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
                    root.Result.user_name = nomeDoUsuario;
                    root.Result.message = "Entrada Liberada";
                    root.Result.portal_id = 1;
                    root.Result.actions = new List<Entities.Action>();
                    root.Result.actions.Add(new Entities.Action()
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


        private bool RetornarUsuarioValido(int idUser, string userName)
        {
            try
            {
                string matricula = string.Empty;
                if (!string.IsNullOrEmpty(userName) && userName.Contains("|"))
                {
                    var vet = userName.Split('|');
                   if (vet != null && vet.Length == 3)
                    {
                        matricula = vet[2];
                    }
                }

                var colaboradorRepository = new Business.Repositories.ColaboradorRepository();
                _logger.LogInformation("Validacao de usuario valido.");
                return colaboradorRepository.VerificaLiberacaoFinanceiraClube(idUser, matricula);
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Erro ao Validar liberacao do clube erro: " + ex.Message);
                throw ex;
            }
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
