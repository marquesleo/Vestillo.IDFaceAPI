using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vestillo.IDFaceAPI.Entities;

namespace Vestillo.IDFaceAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class IDFaceController : ControllerBase
    {


        [HttpPost]
        [Route("new_user_identified.fcgi")]
        public async Task<IActionResult> NewUserIdentified([FromBody] NewUserIdentified contadorSenhaViewModel)
        {

            return Ok();
        }
    }
}
