namespace WEB_API_APP_CRECIENDO.Models
{
    public class SqlRepositorio
    {
        public static string ObtenerConec()
        {
            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = build.Build();
            string connstring = configuration.GetValue<string>("ConnectionStrings:MyConn");
            return connstring;
        }



    }
}
