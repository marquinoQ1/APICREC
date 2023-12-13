namespace WEB_API_APP_CRECIENDO.Entidades
{
    public class ValidacionUsuario
    {
        public int Respuesta { get; set; }
        public string ?Cve_sucursal { get; set; }=string.Empty;

        public string ?Cve_rol { get; set; }=string.Empty;

        public int ?id_usuario { get; set; } = 0;

    }
}
    