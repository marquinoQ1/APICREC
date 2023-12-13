





using NuGet.Common;
using System.Net;
using System.Text.Json;
using WEB_API_APP_CRECIENDO.Entidades;

namespace WEB_API_APP_CRECIENDO.Models
{
    public class PayCashReference : IDisposable
    {
 

        public EresulPaycashreference CrearReferencia (EPayCashcrearReferenciaPrestamo P)
        {

            var error = new EresulPaycashreference();


            string token = "eyJhbGciOiJBMjU2S1ciLCJlbmMiOiJBMjU2Q0JDLUhTNTEyIiwidHlwIjoiSldUIn0.0ucCQGuTbZDz0Vog8eOhtMUFSN2BQiy0Oon_p6VnNgCGX-cQPlHDs9QTr52_V0hrZV9hZksfd10EgT4LguhSHuY0s8B31E03.jLoDfBYyaBo0qQSfE6hi-w.LH-fK8aPHs8svPjyaxXN1OMDvA2tXiOGOpXxA85aT9Qg24JUI81JpKfTiuQ3lHb_p6u_WYU211GQ1SibQhzVJvYhK-Nyod-ZMQfjL9s46lTAsl1fqnYNe9mP06G8hppN2tzEMPcG-lrocj5cxOYdOlfCN8GypttHWXTS594jrS9p0B5CU0KC2I_uAchQ3Tx5JHpkENtRQZ_v2Rby4hUjO6QRuxnzIuowTWU1ik_EMcjDu-iBXkRLk6qxMedlENl9MVoSOaE11MR1rm3A-EFxOC7NmFoGG0zEqBOVtAvmimmhYaO1MiMoKmVUcLSzpAV-C8OZRefqF9U6lGF842AXbdViDyWM3JaDnbS5uSW1RWQi4wl_7QarGfGOVyUhhCaWgm_5qQ-0_WZrQBblTUebZ0tn_L-VtdYCBy7b6fDoLdg.cFzErNdB7zkgy_EdMFiC3JkPrd6qYK2LOmhUNBj8SOc";
            //string url = "https://sb-api-mexico-emisor.paycashglobal.com/v1/reference";
            string url = "https://api-mexico-emisor.paycashglobal.com/v1/reference";

            var N = new EPayCashcrearReferencia();

            N.Amount = "0";
            N.ExpirationDate = "2040-01-01";
            N.Value = P.REFERENCIA;
            N.Type = "false";


            var request = (HttpWebRequest)WebRequest.Create(url);
            var jsons = JsonSerializer.Serialize(N);
            string json = jsons.ToString();

            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", token);


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }


            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return error;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        var idsms = JsonSerializer.Deserialize<EresulPaycashreference>(responseBody);
                        return idsms;
                    }
                }
            }
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
