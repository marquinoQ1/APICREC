using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using WEB_API_APP_CRECIENDO.Entidades;

namespace WEB_API_APP_CRECIENDO.Security
{
    public class Seguridad
    {
        public JwtDatos GwtCambiarJson(string token)
        {

            var TokenInfo = new Dictionary<string, string>();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();

            foreach (var claim in claims)
            {
                TokenInfo.Add(claim.Type, claim.Value);
            }
            
            string json = JsonConvert.SerializeObject(TokenInfo);
            var detail = JObject.Parse(json);
            JwtDatos obj = JsonConvert.DeserializeObject<JwtDatos>(json);
            return obj;

        }

    }
}
