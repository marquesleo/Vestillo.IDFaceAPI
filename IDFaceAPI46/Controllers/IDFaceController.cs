using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;
using NLog;
using Newtonsoft.Json;
using IDFaceAPI46.Entities;
using System.IO;


namespace IDFaceAPI46.Controllers
{
    //[RoutePrefix("api")]
    public class IDFaceController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _connectionString;
        private readonly int _idEmpresaLogada;
        private readonly bool _utilizaAPI;
        public IDFaceController()
        {
            _connectionString =   ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            _idEmpresaLogada = Convert.ToInt32(ConfigurationManager.AppSettings["parametros:empresa"]);
            _utilizaAPI = true;

           Vestillo.Connection.ProviderFactory.StringConnection = _connectionString;
            Vestillo.Lib.Funcoes.SetIdEmpresaLogada = _idEmpresaLogada;
            Vestillo.Lib.Funcoes.UtilizaAPI = _utilizaAPI;
        }

        [HttpPost]
        [Route("new_user_identified.fcgi")]
        public IHttpActionResult NewUserIdentified([FromBody] NewUserRequest request)
        {
            int user_id = request.UserId;
            string user_name = request.UserName;
            _logger.Info($"new user identified");


            if (user_id > 0)
            {
                _logger.Info($"new_user_identified.fcgi ------=> user_id:{user_id}, user_name:{user_name}");

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


                if (!RetornarUsuarioValido(user_id, user_name))
                {


                    var root = new Root();
                    root.Result = new Result();
                    root.Result.Event = 6;
                    root.Result.user_id = user_id;
                    root.Result.user_name = nomeDoUsuario;
                    root.Result.message = msg;
                    root.Result.portal_id = 1;
                    root.Result.actions = new List<IDFaceAPI46.Entities.Action>();

                    JsonSerializer serializer = new JsonSerializer();

                    // Serializando para uma string
                 
                    using (StringWriter sw = new StringWriter())
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, root);
                        jsonString = sw.ToString();
                    }

                    _logger.Info($"new_user_identified.fcgi ------=> objeto:{jsonString}");
                    _logger.Info($"new_user_identified.fcgi ------=> entrada bloqueada");
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
                    root.Result.actions = new List<IDFaceAPI46.Entities.Action>();
                    root.Result.actions.Add(new IDFaceAPI46.Entities.Action()
                    {
                        ActionName = "sec_box",
                        Parameters = "id=65793, reason=1"
                    });
                    JsonSerializer serializer = new JsonSerializer();

                    // Serializando para uma string

                    using (StringWriter sw = new StringWriter())
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, root);
                        jsonString = sw.ToString();
                    }
                    _logger.Info($"new_user_identified.fcgi ------=> objeto:{jsonString}");
                    _logger.Info($"new_user_identified.fcgi ------=> entrada liberada");



                    return Ok(root);
                }
            }
            else
            {
                _logger.Info("new_user_identified.fcgi  ----- Usuário não encontrado!");
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


                var colaboradorRepository = new  Vestillo.Business.Repositories.ColaboradorRepository();
                _logger.Info("Validacao de usuario valido.");
                return colaboradorRepository.VerificaLiberacaoFinanceiraClube(idUser, matricula);

            }
            catch (Exception ex)
            {
                _logger.Info(ex, "Erro ao Validar liberacao do clube erro: " + ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Route("device_is_alive.fcgi")]
        public IHttpActionResult deviceisalive(int access_logs)
        {
            _logger.Info("deviceisalive");
            return Ok();
        }

        [HttpGet]
        [Route("teste.fcgi")]
        public IHttpActionResult teste()
        {
            _logger.Info("teste");
            return Ok("teste");
        }
    }

    public class NewUserRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
