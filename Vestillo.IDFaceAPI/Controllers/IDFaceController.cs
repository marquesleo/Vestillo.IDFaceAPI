using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Route("new_user_identified.fcgi")]
        public async Task<IActionResult> NewUserIdentified([FromBody] NewUserIdentified contadorSenhaViewModel)
        {
            _logger.LogInformation("Passou aqui");
            return Ok();
        }

        [HttpGet]
        [Route("teste.fcgi")]
        public async Task<IActionResult> teste()
        {
            _logger.LogInformation("Passou aqui");
            return Ok("teste");
        }
    }
}
