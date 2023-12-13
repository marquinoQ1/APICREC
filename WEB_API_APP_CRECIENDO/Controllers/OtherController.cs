using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_APP_CRECIENDO.Entidades;
using WEB_API_APP_CRECIENDO.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using NuGet.Protocol;
using System.Text.Json;
using RestSharp;
using static Dapper.SqlMapper;

namespace WEB_API_APP_CRECIENDO.Controllers
{
    [ApiController]
    [Route("api/OTHER")]
    [Authorize]
    public class OtherController : Controller
    {
        [HttpPost ("DDivice")]
        //[AllowAnonymous]
        public Respuesta SaveInfoDivice(EInfoDivice info)
        {
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;


            var token = IAuthorizeData.Equals;
            using (var BD = new Datasql())
            {
                return BD.SaveInfoDivice(info, identity.FindFirst("id_datos").Value );
            }
            
        }

        [HttpPost("PAYCASHREFERENCEPG")]
        [AllowAnonymous]
        public int getReferenciapg()
        {
               var BD = new Datasql();
               var list =  BD.GetListPrestamosPG_RPAY();

            using (var RF = new PayCashReference())
            {
                foreach (var item in list)
                {
                    var listaprestamos = new EPayCashcrearReferenciaPrestamo();
                    var result = new EresulPaycashreference();
                    int num = item.ID_PRESTAMO;
                    listaprestamos.ID_PRESTAMO = item.ID_PRESTAMO;
                    listaprestamos.REFERENCIA = item.REFERENCIA;
                    result =  RF.CrearReferencia(listaprestamos);
                    BD.InsertReferenciaPayCash(item.ID_PRESTAMO, result);
                }
            }
            return 1;
        }

        [HttpGet("PAYCASHREFERENCEMC")]
        [AllowAnonymous]
        public int getReferenciamc(int ID_PRESTAMO)
        {
            var BD = new Datasql();
            string refe = BD.getreferenciamc(ID_PRESTAMO);

            using (var RF = new PayCashReference())
            {
                    var data = new EPayCashcrearReferenciaPrestamo();
                    var result = new EresulPaycashreference();
                    data.ID_PRESTAMO = ID_PRESTAMO;
                    data.REFERENCIA = refe;
                    result = RF.CrearReferencia(data);
                    BD.InsertReferenciaPayCash(ID_PRESTAMO, result) ;               
            }
            return 1;
        }


        [HttpPost("SMS_INDIVIDUAL")]
        [AllowAnonymous]
        public Respuesta EnvioSMSSINCH (EEnvioSMS N)
        {
            var data = new ESmssinch();
            var res = new Respuesta();
            

            data.destination = N.destination;
            data.messageText = N.messageText;
            

            string token = "3ylmui2Ul8i0R1a8SOa0pox70bhCCctUJoHZ-noH";
            string SINCHNAME = "jmb@creciendo.com.mx";
            var url = $"https://api-messaging.wavy.global/v1/send-sms";


            var request = (HttpWebRequest)WebRequest.Create(url);
            var jsons = JsonSerializer.Serialize(data);
            string json = jsons.ToString();

            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("UserName", SINCHNAME);
            request.Headers.Add("authenticationToken", token);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return res;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            var BD = new Datasql();
                            string responseBody = objReader.ReadToEnd();
                            var idsms = JsonSerializer.Deserialize<EiD>(responseBody);
                            res.StrRespuesta = idsms.id.ToString();
                            res.IntRespuesta = 1;
                            res.Observaciones = "TODO COOL";

                            switch (N.TIPO)
                            {
                                case "CA":
                                    var bitacoraSMScandidata = new EBitacoraSMScandidata();
                                    bitacoraSMScandidata.TOKEN = N.TOKEN;
                                    bitacoraSMScandidata.NUM_CEL = N.destination;
                                    bitacoraSMScandidata.MENSAJE = N.messageText;
                                    bitacoraSMScandidata.PROVEEDOR_SMS = N.PROVEEDOR_SMS;
                                    res = BD.Bitacora_sms_ca(bitacoraSMScandidata);
                                    res.StrRespuesta = idsms.id.ToString();
                                    break;
                                case "CF":
                                    var bitacoraSMSCF = new EBitacoraSMSCF();
                                    bitacoraSMSCF.ID_PRESTAMO= N.ID_PRESTAMO;
                                    bitacoraSMSCF.TOKEN = N.TOKEN;
                                    bitacoraSMSCF.NUM_CEL = N.destination;
                                    bitacoraSMSCF.MENSAJE = N.messageText;
                                    bitacoraSMSCF.PROVEEDOR_SMS = N.PROVEEDOR_SMS;
                                    res = BD.Bitacora_sms_CF(bitacoraSMSCF);
                                    res.StrRespuesta = idsms.id.ToString();
                                    break;
                                case "AS":
                                    var bitacoraSMSAS = new EBitacoraSMSAS();
                                    bitacoraSMSAS.ID_PRESTAMO = N.ID_PRESTAMO;
                                    bitacoraSMSAS.TOKEN = N.TOKEN;
                                    bitacoraSMSAS.NUM_CEL = N.destination;
                                    bitacoraSMSAS.MENSAJE = N.messageText;
                                    bitacoraSMSAS.PROVEEDOR_SMS = N.PROVEEDOR_SMS;
                                    res = BD.Bitacora_sms_AS(bitacoraSMSAS);
                                    res.StrRespuesta = idsms.id.ToString();
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                string msg = ex.Message;
                res.IntRespuesta = -1;
                res.StrRespuesta = "n/a";
                res.Observaciones+= msg;

            }
            return res;
        }


    }
}
