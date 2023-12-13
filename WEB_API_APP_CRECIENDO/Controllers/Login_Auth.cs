using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WEB_API_APP_CRECIENDO.Entidades;
using WEB_API_APP_CRECIENDO.Models;

namespace WEB_API_APP_CRECIENDO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class Login_Auth : ControllerBase
    {

        public static Usuario UsuarioIngresado = new Usuario();
        private readonly IConfiguration _configuration;
        public Login_Auth(IConfiguration configuration)
        {
            _configuration = configuration;

        }


        [HttpPost("Registro")]

        public async Task<ActionResult<Usuario>> Ingresar(DatosUsuario request) {

            CrearContrasenaHash(request.Contrasena, out byte[] passwordHash, out byte[] passwordSalt);

            UsuarioIngresado.UserName = request.UserName;
            UsuarioIngresado.PassWordHash = passwordHash;
            UsuarioIngresado.PassWordSalt = passwordSalt;


            List<ValidacionUsuario> n = new List<ValidacionUsuario>();

            var instamce = new Datasql();

            n = instamce.validacionUsuarios(request).ToList();




            if ( n[0].Respuesta == -2 || n[0].Respuesta == -3)
            {
                if (n[0].Respuesta == -2)
                {
                    //var respuesta = "Contraseña Incorrecta";
                    return BadRequest("Contraseña Incorrecta");
                }
                else
                {
                    //var respuesta = "Usuario Dado de Baja";
                    return BadRequest("Usuario Dado de Baja");
                }

            }
            if (n[0].Respuesta == -4)
            {
                return BadRequest("La version de APK no esta disponible");

            }

            UsuarioIngresado.cve_rol = n[0].Cve_rol;
            UsuarioIngresado.cve_sucursal = n[0].Cve_sucursal;
            UsuarioIngresado.id_usuario = n[0].id_usuario;
            UsuarioIngresado.respuesta = n[0].Respuesta;
           


            string token = CrearTokenJws(UsuarioIngresado);


            return Ok(token);           
        }


      

        [HttpPost ("ValidarUser")]

        public ActionResult ValidarDatos (DatosUsuario request)
        {

            if (UsuarioIngresado.UserName != request.UserName)
            {

                return BadRequest("UsuarioErroneo");  
            }
           
            if(!ValidaContraseña(request.Contrasena,UsuarioIngresado.PassWordHash,UsuarioIngresado.PassWordSalt))
            {
                return BadRequest("Contraseña incorrecta");
            }

            string token = CrearTokenJws(UsuarioIngresado);

            return Ok(token);
        }

        private string CrearTokenJws(Usuario usuario)
        {

            List<Claim> claims = new List<Claim> { 


                new Claim("Usuario", usuario.UserName),
                new Claim("cve_rol", usuario.cve_rol),
                new Claim("cve_sucursal", usuario.cve_sucursal),
                new Claim("id_usuario", usuario.id_usuario.ToString()),
                new Claim("id_datos", usuario.respuesta.ToString())

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddYears(3),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        private void CrearContrasenaHash(string password,out byte[] passwordHash , out byte[] passwordSalt)
        {

            using (var hmac=new HMACSHA512())
            {

                passwordSalt = hmac.Key;

                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));    


          }


        }

        private bool ValidaContraseña(string contraseña, byte[] passwordHas, byte[] passwordSalt)
        {

            using (var hac = new HMACSHA512(passwordSalt))
            {


                var computedHas = hac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contraseña));

                return computedHas.SequenceEqual(passwordHas);

            }

        }

       


        //[HttpGet ("DD") ]
        //public string GwtCambijjarJson(string token)
        //{


        //    var lifeTime = new JwtSecurityTokenHandler().ReadToken(token).ValidTo;


        //    return "f";
        //    //var token = jwt;
        //    //var handler = new JwtSecurityTokenHandler();
        //    //var jwtSecurityToken = handler.ReadJwtToken(token);

        //    //JObject json = JObject.Parse(jwtSecurityToken.ToString());
        //    //return "hola";

        //}



    }
}
