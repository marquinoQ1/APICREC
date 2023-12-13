using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WEB_API_APP_CRECIENDO.Entidades;

namespace WEB_API_APP_CRECIENDO.Models
{
    public class SaveDocs : IDisposable
    {
        public int SaveFoto(ESaveDoc n)
        {
            int rs = 0;

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(n.PHOTO)))
            {
                Bitmap bm2 = new Bitmap(ms);
                try
                {
                    bm2.Save(n.PATH + n.NAMEDOC);
                    rs = 1;
                }
                catch
                {
                     rs = -1;
                }
            }

            return rs;
        }

        public int SaveFotoJT(SaveDocJuntas n)
        {
            string ROOT = "";
            string FILE_NAME = "";

            if (n.ID_REUNION != 0)
            {
                if (n.ID_TIPO != 2)
                {
                    //ROOT = strings.fsDistribucion + n.ID_REUNION + "/";
                    ROOT = strings.fsDistribuciontest + n.ID_REUNION + "/";
                    FILE_NAME = n.ID_REUNION + "_" + strings.FOTO_DISTRIBUCION + ".jpg";
                }
                else
                {
                    ROOT = strings.fsRenovaciontest + n.ID_REUNION + "/";
                    //ROOT = strings.fsRenovacion + n.ID_REUNION + "/";
                    FILE_NAME = n.ID_REUNION + "_" + strings.FOTO_RENOVACION + ".jpg";
                }
            }

            if (Directory.Exists(ROOT))
            {

            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(ROOT);
            }


            int rs = 0;

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(n.PHOTO)))
            {
                Bitmap bm2 = new Bitmap(ms);
                try
                {
                    bm2.Save(ROOT + FILE_NAME);
                    rs = 1;
                }
                catch
                {
                    rs = -1;
                }
            }

            return rs;
        }

        public int SavePhotoDesembolso(ESaveDocDes n)
        {
            string ROOT = "";
            string FILE_NAME = "";
            string rutaPublicacion = strings.prodRoot_2 + strings.fsDesembolso;

            System.IO.Directory.CreateDirectory(rutaPublicacion + n.ID_PRESTAMO);

            ROOT = rutaPublicacion + n.ID_PRESTAMO + "/";
            FILE_NAME = n.ID_PRESTAMO + "_"  + strings.FOTO_REF  + ".jpg";

            int rs = 0;

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(n.PHOTO)))
            {
                Bitmap bm2 = new Bitmap(ms);
                try
                {
                    bm2.Save(ROOT + FILE_NAME);
                    rs = 1;
                }
                catch
                {
                    rs = -1;
                }
            }
            return rs;
        }

        public int SavePhotosConsolidacion (ESavePhotosConsol n)
        {
            string ROOT = "";
            string FILE_NAME = "";
            string rutaPublicacion = strings.prodRoot_2 + strings.fsConsolidacion;
            int rs = 0;


            string[] foto = new string[5];
            foto[0] = n.GRUPAL_BLOB;
            foto[1] = n.DIRECTIVA_BLOB;
            foto[2] = n.REF_BLOB;
            foto[3] = n.INI_REUNION_BLOB;
            foto[4] = n.FIN_REUNION_BLOB;

            string[] nombrefoto = new string[5];
            nombrefoto[0] = n.ID_PRECOMITE + signs.underscore + strings.FOTO_GRUPAL + formats.jpg;
            nombrefoto[1] = n.ID_PRECOMITE + signs.underscore + strings.FOTO_DIRECTIVA + formats.jpg;
            nombrefoto[2] = n.ID_PRECOMITE + signs.underscore + strings.FOTO_REF + formats.jpg;
            nombrefoto[3] = n.ID_PRECOMITE + signs.underscore + strings.FOTO_INI_REUNION + formats.jpg;
            nombrefoto[4] = n.ID_PRECOMITE + signs.underscore + strings.FOTO_FIN_REUNION + formats.jpg;

            System.IO.Directory.CreateDirectory(rutaPublicacion + n.ID_PRECOMITE);

            ROOT = rutaPublicacion + n.ID_PRECOMITE + "/";


            for (int i = 0; i <= 4; i++)
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(foto[i])))
                {
                    Bitmap bm2 = new Bitmap(ms);
                    try
                    {
                        bm2.Save(ROOT + nombrefoto[i]);
                        rs = rs + 1;
                    }
                    catch
                    {
                        rs = rs -1;
                    }
                }
            }

            
            return rs;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
