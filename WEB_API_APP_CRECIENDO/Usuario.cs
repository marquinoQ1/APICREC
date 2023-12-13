namespace WEB_API_APP_CRECIENDO
{
    public class Usuario
    {
        public string UserName { get; set; } = string.Empty;

        public string ?cve_rol { get; set; } = string.Empty;

        public string ?cve_sucursal { get; set; } = string.Empty;

        public int? id_usuario { get; set; } = 0;
        public byte[] PassWordHash {get; set; }
        public byte[] PassWordSalt { get; set; }
        public int respuesta { get; set; }

    }

   
}
