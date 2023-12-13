using Microsoft.AspNetCore.Mvc;
using WEB_API_APP_CRECIENDO.Entidades;
using WEB_API_APP_CRECIENDO.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace WEB_API_APP_CRECIENDO.Controllers
{
    [ApiController]
    [Route("api/Consolidacion")]
    [Authorize]

    public class ConsolidacionController : Controller
    {

        //[HttpGet("GetDesembolso")]
        [HttpGet("GetConsolidacion")]
        //[AllowAnonymous]
        public IEnumerable<EListConsol> GetConsolida(int ID_USUARIO)
        {
            using (var BD = new Datasql())
            {
                return BD.GetConsolida(ID_USUARIO);
            }
        }

        [HttpPost("SincroConsolidacion")]
        //[AllowAnonymous]
        public Respuesta SincroConsolidacion(ESincronizaConsol info)
        {

            using (var BD = new Datasql())
            {
                return BD.SincroConsolidacion(info);
            }
        }

        [HttpGet("GetActiConsol")]
        [AllowAnonymous]
        public IEnumerable<EListActConsol> GetActiConsol()
        {
            using (var BD = new Datasql())
            {
                return BD.GetActiConsol();
            }
        }

    }
}
