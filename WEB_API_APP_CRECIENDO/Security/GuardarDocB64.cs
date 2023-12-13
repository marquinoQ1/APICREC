using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using WEB_API_APP_CRECIENDO.Entidades;


namespace WEB_API_APP_CRECIENDO.Security
{
    public class GuardarDocB64
    {
        public int GuardarDoc(EFotos F)
        {
            string[] foto = new string[5];
            foto[0] = F.GRUPAL_BLOB;
            foto[1] = F.DIRECTIVA_BLOB;
            foto[2] = F.REF_BLOB;
            foto[3] = F.INI_REUNION_BLOB;
            foto[4] = F.FIN_REUNION_BLOB;

            string[] nombrefoto = new string[5];
            nombrefoto[0] = "FOTO_GRUPAL";
            nombrefoto[1] = "FOTO_DIRECTIVA";
            nombrefoto[2] = "FOTO_REF";
            nombrefoto[3] = "FOTO_INI_REUNION";
            nombrefoto[4] = "FOTO_FIN_REUNION";

            string[] rsFoto = new string[5];
            rsFoto[0] = string.Empty;
            rsFoto[1] = string.Empty;
            rsFoto[2] = string.Empty;
            rsFoto[3] = string.Empty;
            rsFoto[4] = string.Empty;
            int i = 0;

            //var.ROOT = strings.fsPromociones;
            //var.FILE_NAME = strings.FOTO_PROMOCION;
            DateTime fechaAhora = DateTime.Now;
            DateTime fechaCambioRuta = fechaAhora;
            var culture = CultureInfo.CreateSpecificCulture("es-MX");
            var styles = DateTimeStyles.None;
            string rutaPublicacion = strings.publicacionRuta;

            DateTime.TryParse(strings.defaultXML, culture, styles, out fechaCambioRuta);

            if (fechaAhora >= fechaCambioRuta)
                rutaPublicacion = strings.prodRoot;

            //var.msj = strings.rs5;
            Directory.CreateDirectory(rutaPublicacion + strings.fsConsolidacion + F.ID_PRECOMITE);
            //foreach (string s in foto)
            for (i = 0; i <= 4; i++)
            {
                if (foto[i] == string.Empty || foto[i] == "" || foto[i] == null || foto[i] == "null" || foto[i] == " " || foto[i].Length == 0)
                {
                    continue;
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(foto[i])))
                    {
                        Bitmap bm2 = new Bitmap(ms);
                        try
                        {
                            bm2.Save(rutaPublicacion +  F.ID_PRECOMITE + signs.underscore + nombrefoto[i] + formats.jpg);
                            //var.msj = strings.rs6;
                            rsFoto[i] = strings.rs7;
                            //var.codeRS = 1;
                        }
                        catch
                        {
                            //var.msj = strings.rs8;
                            rsFoto[i] = strings.rs9;
                            //var.codeRS = -1;
                        }
                    }
                }
            }

            return 0;
        }

        public int GeneraBase64(EFotos Fotos)
        {

            EFotos Listta64 = new EFotos();
            var Path = strings.prodRoot;
            
            byte[] imageArray = File.ReadAllBytes(Path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            Listta64.GRUPAL_BLOB = Fotos.GRUPAL_BLOB;


            Listta64.ID_PRECOMITE = 212331;

            Listta64.DIRECTIVA_BLOB = Fotos.DIRECTIVA_BLOB;
            Listta64.REF_BLOB = Fotos.REF_BLOB;
            Listta64.INI_REUNION_BLOB = Fotos.INI_REUNION_BLOB;
            Listta64.FIN_REUNION_BLOB = Fotos.FIN_REUNION_BLOB;

            var respuesta = GuardarDoc(Listta64);

            return 0;
        }
    }
}
