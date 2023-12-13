using FCM.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;
//using FirebaseAdmin.Messaging;
using WEB_API_APP_CRECIENDO.Entidades;

namespace WEB_API_APP_CRECIENDO.Models
{
    public class FireBase : IDisposable
    {

        public async Task<int> NotificacAsync(EFCM Notification)
        {
            int respuesta = Notification.Respuesta;
            string registrationId = Notification.Token;
            string tittle = Notification.Title;
            string body = Notification.body;

            using (var sender = new Sender(strings.firebaseservertoken))
            {
                var json = "{\"notification\":{\"title\":\"" + tittle + "\",\"body\":\"" + body + "\"}," +
                    "\"data\":{\"title\":\"" + tittle + "\",\"body\":\"" + body + "\"}," +
                    "\"to\":\"" + registrationId + "\"}";
                var result = await sender.SendAsync(json);
                respuesta = result.MessageResponse.Success;
            }
            
            return respuesta;
        }


        public async Task<string> GetNotificacionesMasiva()
        {
            var data = new Datasql();

            var json = data.GetNotificacionesMasiva();

            foreach (var item in json)
            {
                int respuesta = 0;
                using (var sender = new Sender(strings.firebaseservertoken))
                {
                    var result = await sender.SendAsync(item.NOTIFICACION);
                    respuesta = result.MessageResponse.Success;
                }
            }                      

            return string.Empty;
        }

         public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
