using Microsoft.AspNetCore.Mvc;
using WEB_API_APP_CRECIENDO.Entidades;
using WEB_API_APP_CRECIENDO.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace WEB_API_APP_CRECIENDO.Controllers
{
    [ApiController]
    [Route("api/desembolso")]
    [Authorize]
    public class DesembolsoController : Controller
    {
        [HttpGet("GetDesembolso")]
        //[AllowAnonymous]
        public IEnumerable<EListDesem> GetDesembolso(int ID_USUARIO)
        {
            using (var BD = new Datasql())
            {
                return BD.GetDesembolso(ID_USUARIO);

            }                        
        }

        [HttpPost("SincroDesembolso")]
        //[AllowAnonymous]
        public Respuesta SincroDesembolso(ESincroDesembolso info)
        {

            using (var BD = new Datasql())
            {
                return BD.SincroDesembolso(info);
            }

        }

    }
}
