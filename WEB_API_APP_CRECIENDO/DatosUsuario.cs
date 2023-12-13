namespace WEB_API_APP_CRECIENDO
{
    public class DatosUsuario
    {
        public string UserName { get; set; }= string.Empty;
        public string Contrasena { get; set; }=string.Empty;
        public string VApk { get; set; } = string.Empty;
        public EInfoDivice dDevice { get; set; }
    }

    public class EInfoDivice
    {
        //public int ID_USUARIO { get; set; }
        public string MODELO { get; set; }
        public string MARCA { get; set; }
        public string VERSION_ANDROID { get; set; }
        public string COMPANIA { get; set; }
    }
}
