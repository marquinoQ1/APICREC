using Microsoft.AspNetCore.Mvc;

using WEB_API_APP_CRECIENDO.Entidades;
using WEB_API_APP_CRECIENDO.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WEB_API_APP_CRECIENDO.Controllers
{
    [ApiController]
    [Route("api/Micro")]
    [Authorize]
    public class MicroController : Controller
    {

        [HttpGet("Respuesta")]
        public Respuesta Respuesta(string id_usuario)
        {
            using (var BD = new Datasql())
            {
                return BD.ObRespu(id_usuario);
            }
        }

        [HttpGet("ListActividades")]
        public IEnumerable<Actividades> ListActividades(string id_usuario)
        {
            using (var BD = new Datasql())
            {
                return BD.ObtListActividades(id_usuario);
            }
        }

        [HttpGet("ListSeguros")]
        //[AllowAnonymous]
        public IEnumerable<ESeguros> ListSeguros(string id_usuario)
        {

            using (var BD = new Datasql())
            {
                return BD.obtListSeguros(id_usuario);
            }
        }

        [HttpGet("ListAgenda")]
        public IEnumerable<EAgenda> ListAgenda(string id_usuario)
        {
            using (var BD = new Datasql())
            {
                return BD.ObtListAgenda(id_usuario);
            }
        }


        [HttpGet("ListActConsolPreco")]
        public IEnumerable<EACT_CON_PRE> ListActConsolPreco(string id_usuario)
        {
            using (var BD = new Datasql())
            {
                return BD.ObtListActConsolPreco(id_usuario);
            }
        }

        [HttpGet("ListGrupDesem")]
        public IEnumerable<EGRUPOS_DESEMBOLSO> ListGrupDesem(string id_usuario)
        {
            var BD = new Datasql();
            return BD.obtListGrupDesem(id_usuario);
        }

        [HttpGet("ListReuniones")]
        public IEnumerable<EListReunion> ListReuniones(string id_usuario)
        {
            using (var instancia = new Datasql())
            {
                return instancia.ObtListReuniones(id_usuario);
            }
        }

        [HttpGet("ListValDom")]
        public IEnumerable<EListPreValDom> ListValDom(string id_usuario)
        {
            using (var instancia = new Datasql())
            {
                return instancia.ListValDom(id_usuario);
            }
        }

        [HttpGet("ListPromPrestamo")]
        public IEnumerable<ELisPromoPrestamos> ListPromPrestamo(string id_usuario)
        {
            using (var instancia = new Datasql())
            {
                return instancia.ListPromPrestamo(id_usuario);
            }
        }

        [HttpGet("ListReunionBD")]
        //[AllowAnonymous]
        public string ListReunionBD(string idprestamo)
        {
            string result;
            using (var BD = new Datasql())
            {
                result = BD.ListReunionBD(idprestamo);
                return result;
            }
        }
        [HttpGet("ListPrestamosV2")]
        //[AllowAnonymous]
        public IEnumerable<EListPRESTAMOSv2> ListPrestamosV2(string id_usuario)
        {
            using (var instancia = new Datasql())
            {
                return instancia.ListPrestamosV2(id_usuario);
            }
        }
        

        [HttpGet("ConsultaStatus")]        
        public int ConsultaStatus(int id_precomite)
        {
            using (var BD = new Datasql())
            {
                return BD.ObtConsultaStatus(id_precomite);
            }
        }

        [HttpGet("GetSucursales")]
        //[AllowAnonymous]
        public IEnumerable<ESucursales>GetSucursales(string id_usuario)
        {
            using (var BD = new Datasql())
            {
                return BD.ObtGetSucursaless(id_usuario);
            }

        }
        [HttpGet("GetZonas")]
        //[AllowAnonymous]
        public IEnumerable<EGetZonas> GetZonas (string id_sucursal)
        {
            using (var BD = new Datasql())
            {
                return BD.ObtGetZonas(id_sucursal);
            }
        }

        [HttpPost("Promociones_sinc")]
        //[AllowAnonymous]
        public int Promociones_sinc(EInserPromocion N)
        {
            using (var BD = new Datasql())
            {
                return BD.insertarPromocion(N);
            }
        }

        [HttpPost("SincroDesembolso")]
        //[AllowAnonymous]
        public int SincroDesembolso(InstSincroDesembolso lista)
        {
            using (var BD = new Datasql())
            {
                return BD.SincroDesembolso(lista);
            }
        }

        [HttpPost("SincroValDomiciliaria")]
        //[AllowAnonymous]
        public int SincroValDomiciliaria(ESincroValDomiciliaria N)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
           
            using (var BD = new Datasql())
            {
                return BD.obtSincroValDomiciliaria(N, identity.FindFirst("id_datos").Value);
            }
        }

        [HttpPost("InsertReunion")]
        //[AllowAnonymous]
        public int InsertReunion(EInsertReunion reunion)
        {
            using (var BD = new Datasql())
            {
                int result = BD.InsertReunion(reunion);

                return result;
            }

        }

        [HttpPut("Sincronizacion")]
        //[AllowAnonymous]
        public int Sincronizacion(ESincronizacion N)
        {
            using ( var BD = new Datasql())
            {


                return BD.ObtSincronizacion(N);
            }
        }

  

        [HttpGet ("GtePestanas")]
        //[AllowAnonymous]
        public IEnumerable<EPestanas> GtePestanas(string id_usuario)
        {
            using (var BD = new Datasql())
            {
                return BD.ObtPestanasApp(id_usuario);
            }            
        }

        [HttpPost("GuardarTokenMovil")]
        //[AllowAnonymous]
        public string SaveTokenMovil(EToken_FireBase N)
        {
            using (var BD = new Datasql())
            {
                return BD.SaveTokenMovil(N);
            }
        }

        [HttpPost("NotificaFireBase")]
        //[AllowAnonymous]
        public int Notifi(EFCM Notification)
        {

            using (var FCM = new FireBase())
            {
                int respuesta = 0;  
                Task<int> result = FCM.NotificacAsync(Notification);
                respuesta = result.Result;
                return respuesta;
            }
        }

        [HttpGet("NotifiMasivo")]
        [AllowAnonymous]
        public string GetNotificacionesMasiva()
        {
            string json;

            using (var FCM = new FireBase())
            {
                Task<string> result = FCM.GetNotificacionesMasiva();
                json = result.Result;
                
            }
            return json;
        }

        
    }
}
