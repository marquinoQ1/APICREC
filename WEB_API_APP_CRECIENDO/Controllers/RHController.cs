using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_API_APP_CRECIENDO.Entidades;
using WEB_API_APP_CRECIENDO.Models;

namespace WEB_API_APP_CRECIENDO.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/RH")]

    public class RHController : Controller
    {

        [HttpGet("PClimaLaboral")]
        //[AllowAnonymous]
        public string PClimaLaboral(string id_evaluacion, string id_usuario)
        {
            using (var Db = new Datasql())
            {
                return Db.GetPClimaLaboral(id_evaluacion, id_usuario);
            }
            
        }


        [HttpPost("Evidencias")]
        //[AllowAnonymous]
        public string SincroEvidencias(ESincroCapa N)
        {
            using (var Db = new Datasql())
            {
                return Db.SincroEvidencias(N);
            }

        }

        [HttpGet("GETUsuariosCapacitacion")]
        //[AllowAnonymous]
        public string GetUsuariosCapa(string id_usuario , string id_sucursal)
        {
            using (var Db = new Datasql())
            {
                return Db.GetUsuariosCapa(id_usuario, id_sucursal);
            }            
        }


        [HttpGet("GetIncidencias")]
        //[AllowAnonymous]
        public IEnumerable<EIncidencias> GetIncidencias(string id_usuario)
        {
            using (var Db = new Datasql())
            {


                return Db.GetIncidencias(id_usuario);
            }
        }

        [HttpGet("MisIncidencias")]
        //[AllowAnonymous]
        public IEnumerable<iIncidencias> MisIncidencias(string id_usuario)
        {

            using (var Db = new Datasql())
            {

                return Db.MisIncidencias(id_usuario);

            }
        }

        [HttpGet("HistorialIncidencias")]
        //[AllowAnonymous]
        public IEnumerable<EUsuariosIncidencias> HistorialIncidencias()
        {

            var instancia = new Datasql();
            int hora = int.Parse(DateTime.Now.ToString("HH"));
            int tipo = 0;
            if (hora < 12)
            {
                tipo = 1;
            }
            else if (hora > 12)
            {
                tipo = 2;
            }


            return instancia.HistorialIncidencias(tipo);

        }


        [HttpGet("Incidencia")]
        //[AllowAnonymous]
        public string Incidencia()
        {


            
            string enviar = "2848";
            int tipo_incidencia = 1;
            string fecha = "21-01-2019";
            string tipo_notificacion = "1";  //Solicitud de Incidencia
            string tincidencia = "";

            if (tipo_incidencia == 1)
            {
                tincidencia = "Retardo";
            }
            else if (tipo_incidencia == 2)
            {
                tincidencia = "Entrada ";
            }
            else if (tipo_incidencia == 3)
            {
                tincidencia = "Salida Anticipada";
            }
            else if (tipo_incidencia == 4)
            {
                tincidencia = "Falta";
            }


            string url_FireBase = "https://fcm.googleapis.com/fcm/send";
            string key_FireBase = "key=AAAAsJpRYcg:APA91bE1wNPKiQm3KTFKo7z9ywWWB8iA_RWg2HlK_ZprNVIOqAojEnp9IxQZdyiIuSpFLIDxp-bZ_sqUepv2YN9yL6CI04EP_4E4QYpmLPDYBsp_k1wIMZdESMdq8rIfXVKE6OS2TncW";
            var result = "-1";
            var webAddr = url_FireBase;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, key_FireBase); httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string strNJson = "{\"to\": \"/topics/" + enviar + "\"," +

                                          "\"data\": {" +
                                          "\"payload\": {" +
                                          "\"title\": \"Incidencias\"," +
                                          "\"alert\": \"Hola, tienes una incidencia, ¿Deseas solicitar una incidencia?\"," +
                                          "\"icon\": \"appicon\"," +
                                          "\"collapse_key\": \"new_high_score\"," +
                                          "\"message\": \"Hola tienes una incidencia!\"," +
                                          "\"vibrate\": \"true\"," +
                                          "\"priority\": \"true\"," +
                                          "\"category\": \"Incident\"," +
                                          "\"tiponotificacion\": \"" + tipo_notificacion + "\"," +
                                          "\"tipoincidencia\": \"" + tipo_incidencia + "\"," +
                                          "\"dincidencia\": \"" + tincidencia + "\"," +
                                          "\"fecha\": \"" + fecha + "\"," +

                       "}}" +
                    "}";


                streamWriter.Write(strNJson);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;


        }

        [HttpPost("Evidencia")]
        //[AllowAnonymous]
        public int Evidencia(EEvidencia Lista)
        {
            using (var BD = new Datasql())
            {
                return BD.Evidencia(Lista);
            }

        }

        [HttpPost("RespuestasClima")]
        //[AllowAnonymous]
        public int RespuestasClima(ERespuestasClima Lista)
        {
            using (var BD = new Datasql())
            {
                return BD.RespuestasClima(Lista);
            }
        }


        [HttpPost("RespuestaIncidencia")]
        //[AllowAnonymous]
        public int RespuestaIncidencia(EUPDATE_INCIDENCIAS Lista)
        {
            using (var BD = new Datasql())
            {
                return BD.RespuestaIncidencia(Lista);
            }

        }

        [HttpPost("GetIncidencia")]
        //[AllowAnonymous]
        public IEnumerable<ERolAutorizacion> GetIncidencia(EINSERTAR_INCIDENCIAS Lista)
        {
            using (var BD = new Datasql())
            {
               IEnumerable<ERolAutorizacion> lista  =  BD.GetIncidencia(Lista);
                return lista;
            }
        }
        [HttpGet("PartidosQuiniela")]
        //[AllowAnonymous]
        public IEnumerable<EPartidosQuiniela> PartidosQuiniela(int ID_USUARIO)
        {
            using (var Db = new Datasql())
            {
                return Db.GetPartidos(ID_USUARIO);
            }
        }

        [HttpPost("RespuestasQuiniela")]
        //[AllowAnonymous]
        public IEnumerable<EConfirmacionQuiniela> RespuestasQuiniela(ERespuestasQuiniela R)
        {
            using (var Db = new Datasql())
            {
                return Db.GetRespuestasQuiniela(R);
            }
        }

        [HttpGet("MisResultados")]
        //[AllowAnonymous]
        public IEnumerable<EResultadosQuiniela> MisResultados(int ID_USUARIO)
        {
            using (var Db = new Datasql())
            {
                return Db.GetResultadosQuiniela(ID_USUARIO);
            }
        }

        [HttpGet("Ranking")]
        //[AllowAnonymous]
        public IEnumerable<ERankingQuiniela> Ranking(int ID_USUARIO)
        {
            using (var Db = new Datasql())
            {
                return Db.GetRankingQuiniela(ID_USUARIO);
            }
        }

    }
}
