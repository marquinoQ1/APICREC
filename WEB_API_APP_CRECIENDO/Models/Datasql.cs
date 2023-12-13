using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Claims;
using Dapper;    
using WEB_API_APP_CRECIENDO.Entidades;

namespace WEB_API_APP_CRECIENDO.Models
{
    public class Datasql : IDisposable
    {

        public IEnumerable<EListNotificacionMasiva> GetNotificacionesMasiva()
        {
            EListNotificacionMasiva list  = new EListNotificacionMasiva();

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
               
                    conexion.Open();
                    var result = conexion.Query<EListNotificacionMasiva>("SELECT * FROM  dbo.FUNC_GET_APP_NOTIFICACION_INCIDENCIAS()", commandType: CommandType.Text);
                    return result;                         

            }

            
        }


        public string GetPClimaLaboral(string id_evaluacion, string id_usuario)
        {
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var result = conexion.Query("SELECT dbo.FUNC_APP_CLIMA_LABORAL_JSON(" + id_evaluacion + "," + id_usuario + ") as Rjson", commandType: CommandType.Text);
                var RES = result.ToList().FirstOrDefault();
                string json = RES.Rjson;
                return json;
            }

        }


        public string SincroEvidencias(ESincroCapa n)
        {
            var DDocs = new ESaveDoc();
            DDocs.PATH = strings.publicacionRutaEvidencias;
            //DDocs.PATH = strings.TestRuta;
            DDocs.PHOTO = n.FOTO;

            Random rnd1 = new Random();
            int random = 0;
            for (int ctr = 1; ctr <= 1; ctr++)
                random = rnd1.Next();

            DDocs.NAMEDOC = "Evidencia_" + n.ID_USUARIO + "_" + random + ".jpg";

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_USUARIO", n.ID_USUARIO);
                parametros.Add("ID_CAPACITACION", n.ID_CAPACITACION);
                parametros.Add("ID_TEMA", n.ID_TEMA);
                parametros.Add("ID_SUBTEMA", n.ID_SUBTEMA);
                parametros.Add("ATTACH", DDocs.NAMEDOC);
                parametros.Add("REALIZADO", n.REALIZADO);
                parametros.Add("GPS_REF_LAT", n.GPS_REF_LAT);
                parametros.Add("GPS_REF_LON", n.GPS_REF_LON);
                parametros.Add("ID_USUARIO_EVIDENCIA", n.ID_USUARIO_EVIDENCIA);
                parametros.Add("FECHA", n.FECHA);

                var result = conexion.Query("dbo.SP_APP_INSERTAR_EVIDENCIAS", param: parametros, commandType: CommandType.StoredProcedure);
                var TOKEN = result.ToList().FirstOrDefault();

                int varts = TOKEN.CONFIRMACION;
                int doc = 0;

                //var savedocs = new SaveDocs();
                //doc = savedocs.SaveFoto(DDocs);

                if (varts == 1)
                {
                    var savedocs = new SaveDocs();
                    doc = savedocs.SaveFoto(DDocs);
                }
                return "Ok";
            }
        }
        public int Evidencia(EEvidencia N)
        {
            var DDocs = new ESaveDoc();
            int doc = 0;
            var savedocs = new SaveDocs();

            IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());
            int rsf = 0;

            //DDocs.PATH = strings.TestRuta;
            DDocs.PATH = strings.publicacionRutaEvidencias;
            DDocs.PHOTO = N.ATTACH;

            Random rnd1 = new Random();
            int random = 0;
            for (int ctr = 1; ctr <= 1; ctr++)
                random = rnd1.Next();

            DDocs.NAMEDOC = "Evidencia_" + N.ID_USUARIO + "_" + random + ".jpg";

            int rs = 0;

            try
            {
                doc = savedocs.SaveFoto(DDocs);
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_USUARIO", N.ID_USUARIO);
                parametros.Add("ID_CAPACITACION", N.ID_CAPACITACION);
                parametros.Add("ID_TEMA", N.ID_TEMA);
                parametros.Add("ID_SUBTEMA", N.ID_SUBTEMA);
                parametros.Add("ATTACH", DDocs.NAMEDOC);
                parametros.Add("REALIZADO", N.REALIZADO);
                parametros.Add("GPS_REF_LAT", N.GPS_REF_LAT);
                parametros.Add("GPS_REF_LON", N.GPS_REF_LON);
                parametros.Add("ID_USUARIO_EVIDENCIA", N.ID_USUARIO_EVIDENCIA);
                parametros.Add("FECHA", N.FECHA);
                var result = conexion.Query("dbo.SP_APP_INSERTAR_EVIDENCIAS", param: parametros, commandType: CommandType.StoredProcedure);
                rs = Convert.ToInt32(result.ToString());
            }
            catch (Exception)
            {
                rs = -1;
                throw;
            }
            return rs;

        }


        public string GetUsuariosCapa(string id_usuario, string id_sucursal)
        {
            string cjson = "";

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();

                var parametros = new DynamicParameters();
                parametros.Add("ID_USUARIO", id_usuario);
                parametros.Add("ID_SUCURSAL", id_sucursal);

                var rusucapa = conexion.Query("dbo.SP_USUARIOS_CAP_JSON", param: parametros, commandType: CommandType.StoredProcedure);
                var varts = rusucapa.ToList().FirstOrDefault();
                cjson = varts.LISTA;
                return cjson;

            }

        }

        public Respuesta ObRespu(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor =  seguridad.GwtCambiarJson(token.eltoken);

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();

                var parametros = new DynamicParameters();
                parametros.Add("IDCLIENTE", id_usuario);

                var rcliente = conexion.QuerySingle<Respuesta>("dbo.WCF_PROB", param: parametros, commandType: CommandType.StoredProcedure);

                return rcliente;

            }

        }

        public IEnumerable<Actividades> ObtListActividades(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(token.eltoken); 
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {

                conexion.Open();

                var parametros = new DynamicParameters();
                parametros.Add("ID_PROMOTOR", id_usuario);
                var listActiv = conexion.Query<Actividades>("dbo.SP_APP_PRESTAMO_ACTIVIDADES", param: parametros, commandType: CommandType.StoredProcedure);
                return listActiv;

            }
        }

        public IEnumerable<ESeguros> obtListSeguros(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(token.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {

                conexion.Open();

                var listseguro = conexion.Query<ESeguros>("dbo.SP_OBTENER_SEGUROS_APP", commandType: CommandType.StoredProcedure);

                return listseguro;

            }

        }

        public IEnumerable<EGRUPOS_DESEMBOLSO> ListGrupDesem(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(token.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listgrupDesem = conexion.Query<EGRUPOS_DESEMBOLSO>("SELECT * FROM FUNC_APP_GRUPOS_DESEMBOLSO_VREST(" + id_usuario + ")", CommandType.Text);
                return listgrupDesem;
            }
        }

        public IEnumerable<EAgenda> ObtListAgenda(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(token.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {

                conexion.Open();

                var parametros = new DynamicParameters();
                parametros.Add("ID_USUARIO", id_usuario);
                var listAgenda = conexion.Query<EAgenda>("dbo.SP_APP_AGENDA_2", param: parametros, commandType: CommandType.StoredProcedure);
                return listAgenda;

            }

        }

        public IEnumerable<EACT_CON_PRE> ObtListActConsolPreco(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(token.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listActaConsolPreco = conexion.Query<EACT_CON_PRE>("SELECT * FROM [dbo].[FUN_OBTENER_ACT_CONSOLIDACION_PRECOMITE_APP]()", CommandType.Text);
                return listActaConsolPreco;
            }
        }

        public IEnumerable<EGRUPOS_DESEMBOLSO> obtListGrupDesem(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(token.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listgrupDesem = conexion.Query<EGRUPOS_DESEMBOLSO>("SELECT * FROM FUNC_APP_GRUPOS_DESEMBOLSO_VREST(" + id_usuario + ")", CommandType.Text);
                return listgrupDesem;
            }
        }

        public IEnumerable<EListReunion> ObtListReuniones(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(token.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listReuniones = conexion.Query<EListReunion>("SELECT* FROM FUNC_APP_GET_REUNIONES_VREST(" + id_usuario + ")", CommandType.Text);
                return listReuniones;
            }
        }


        public IEnumerable<EListPreValDom> ListValDom(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(token.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_USUARIO", id_usuario);
                var listValDom = conexion.Query<EListPreValDom>("dbo.SP_APP_PRECOMITE_VAL_DOM_VREST", param: parametros, commandType: CommandType.StoredProcedure);
                return listValDom;
            }
        }

        public IEnumerable<ELisPromoPrestamos> ListPromPrestamo(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(token.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listPromPrestamo = conexion.Query<ELisPromoPrestamos>("SELECT * FROM  dbo.FUNC_APP_PRESTAMO_PROMOCIONES_VREST(" + id_usuario + ")", commandType: CommandType.Text);
                return listPromPrestamo;
            }
        }

        public string ListReunionBD(string ID_PRESTAMO)
        {

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                //conexion.Open();
                //var parametros = new DynamicParameters();
                //parametros.Add("ID_USUARIO", id_usuario);
                //parametros.Add("ID_REUNION", idreunion);
                //parametros.Add("ID_PRESTAMO", ID_PRESTAMO);

                //var listReBD = conexion.Query<ElistreunionBD>("dbo.SP_APP_GET_REUNIONESBD_VREST", param: parametros, commandType: CommandType.StoredProcedure);
                //return listReBD;

                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_PRESTAMO", ID_PRESTAMO);
                var result = conexion.Query("dbo.SP_APP_GET_REUNIONESBD_VREST", param: parametros, commandType: CommandType.StoredProcedure);
                var RES = result.ToList().FirstOrDefault();
                string json = RES.Rjson;
                return json;
            }
        }

        public IEnumerable<EListPRESTAMOSv2> ListPrestamosV2(string id_usuario)
        {
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listPrestamosV2 = conexion.Query<EListPRESTAMOSv2>("SELECT* FROM dbo.FUNC_APP_PRESTAMOS_XML2_VREST(" + id_usuario + ")", commandType: CommandType.Text);
                return listPrestamosV2;
            }
        }

        public int InsertReunion(EInsertReunion reunion)
        {

            //var seguridad = new Seguridad(); 
            IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());
            try
            {

                conexion.Open();
                //var sqlComm = new SqlCommand("dbo.SP_APP_INSERTAR_REUNION2_PRUEBA", conexion);

                var parametros = new DynamicParameters();
                parametros.Add("ID_PRESTAMO", reunion.ID_PRESTAMO);
                parametros.Add("ID_USUARIO", reunion.ID_USUARIO);
                parametros.Add("ID_TIPO", reunion.ID_TIPO);
                parametros.Add("GPS_REF_LAT", reunion.GPS_REF_LAT);
                parametros.Add("GPS_REF_LON", reunion.GPS_REF_LON);
                parametros.Add("REF_BLOB", reunion.REF_BLOB);
                parametros.Add("FECHA_REFERENCIACION", reunion.FECHA_REFERENCIACION);
                parametros.Add("ID_ACTIVIDAD", reunion.ID_ACTIVIDAD);
                parametros.Add("ID_CLIENTE", reunion.ID_CLIENTE);
                parametros.Add("MONTO", reunion.MONTO);
                parametros.Add("COMENTARIO", reunion.COMENTARIO);
                parametros.Add("FEC_INI_ACT", reunion.FEC_INI_ACT);
                parametros.Add("GPS_INI_LAT", reunion.GPS_INI_LAT);
                parametros.Add("GPS_INI_LON", reunion.GPS_INI_LON);
                parametros.Add("BANDERA", reunion.BANDERA);                
                parametros.Add("ID_REUNION", reunion.ID_REUNION, DbType.Int32, ParameterDirection.Output);

                //sqlComm.ExecuteNonQuery();

                var result = conexion.ExecuteScalar("dbo.SP_APP_INSERTAR_REUNION2_PRUEBA", param: parametros, commandType: CommandType.StoredProcedure);
                var rs = parametros.Get<Int32>("ID_REUNION");
                reunion.ID_REUNION = rs;


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }


            var ListSaveDocJuntas = new SaveDocJuntas();
            ListSaveDocJuntas.ID_TIPO = reunion.ID_TIPO;
            ListSaveDocJuntas.ID_REUNION = reunion.ID_REUNION;
            ListSaveDocJuntas.PHOTO = reunion.REF_BLOB;
            
            var SaveDoc = new SaveDocs();

            try
            {
                int respuesta = SaveDoc.SaveFotoJT(ListSaveDocJuntas);
            }
            catch (Exception)
            {

                throw;
            }


            return reunion.ID_REUNION;

        }

        public IEnumerable<EIncidencias> GetIncidencias(string id_usuario)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(id.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ListIncidencias = conexion.Query<EIncidencias>("SELECT * FROM dbo.FUNC_APP_INCIDENCIAS_REST(" + id_usuario + ")", CommandType.Text);
                return ListIncidencias;
            }

        }

        public IEnumerable<iIncidencias> MisIncidencias(string id_usuario)
        {

            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(id.eltoken);

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ListIncidencias = conexion.Query<iIncidencias>("SELECT * FROM dbo.FUNC_APP_MIS_INCIDENCIAS_REST(" + id_usuario + ")", CommandType.Text);
                return ListIncidencias;
            }
        }

        public IEnumerable<EUsuariosIncidencias> HistorialIncidencias(int tipo)
        {
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ListIncidencias = conexion.Query<EUsuariosIncidencias>("SELECT * FROM dbo.FUNC_APP_NOTIFICACION_INCIDENCIAS(" + tipo + ")", CommandType.Text);
                return ListIncidencias;
            }
        }

        public IEnumerable<ValidacionUsuario> validacionUsuarios(DatosUsuario request) {

      
            var parametros = new DynamicParameters();
            parametros.Add("@Usuario", request.UserName);
            parametros.Add("@contrasena", request.Contrasena);
            parametros.Add("@VApk", request.VApk);
            parametros.Add("@MODELO", request.dDevice.MODELO);
            parametros.Add("@MARCA", request.dDevice.MARCA);
            parametros.Add("@VERSION_ANDROID", request.dDevice.VERSION_ANDROID);
            parametros.Add("@COMPANIA", request.dDevice.COMPANIA);

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ListIncidencias = conexion.Query<ValidacionUsuario>("[SP_Autentificacion_App_Api]", param: parametros, commandType: CommandType.StoredProcedure).ToList();



                return ListIncidencias.ToList();
            }



        }

        public int ObtConsultaStatus(int id_precomite)
        {
            int lt;

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ListIncidencias = conexion.ExecuteScalar("SELECT dbo.FUNC_GET_STATUS_API_MICRO(" + id_precomite + ")", CommandType.Text);
                lt = Convert.ToInt32(ListIncidencias.ToString());
                return lt;
            }

        }

        public IEnumerable<ESucursales> ObtGetSucursaless(string id_usuario)
        {
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ObtGetSucursaless = conexion.Query<ESucursales>("SELECT * FROM dbo.FUNC_APP_GET_SUCURSALES_API(" + id_usuario + ")", CommandType.Text);
                return ObtGetSucursaless;
            }

        }

        public IEnumerable<EGetZonas> ObtGetZonas(string id_sucursal)
        {
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ObtGetSucursaless = conexion.Query<EGetZonas>("SELECT * FROM dbo.FUNC_APP_GET_ZONAS(" + id_sucursal + ")", CommandType.Text);
                return ObtGetSucursaless;
            }
        }


        public int insertarPromocion(EInserPromocion N)
        {
            int rs = 0;
            IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());
            try
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_ACTIVIDAD", N.ID_ACTIVIDAD);
                parametros.Add("ID_USUARIO", N.ID_USUARIO);
                parametros.Add("ID_TIPO", N.ID_TIPO);
                parametros.Add("COMENTARIO", N.COMENTARIO);
                parametros.Add("APP", N.APP);
                parametros.Add("FECHA_REFERENCIACION", N.FECHA_REFERENCIACION);
                parametros.Add("REF_IS_DATOS", N.REF_IS_DATOS);
                parametros.Add("GPS_REF_LAT", N.GPS_REF_LAT);
                parametros.Add("GPS_REF_LON", N.GPS_REF_LON);

                var result = conexion.ExecuteScalar("dbo.SP_APP_INSERTAR_PROMOCION", param: parametros, commandType: CommandType.StoredProcedure);
                rs = Convert.ToInt32(result.ToString());
            }
            catch (Exception)
            {

                throw;
            }

            return rs;
        }

        public int SincroDesembolso(InstSincroDesembolso N)
        {
            int rs = 0;
            IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());
            try
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_PRESTAMO", N.ID_PRESTAMO);
                parametros.Add("ID_USUARIO", N.ID_USUARIO);
                parametros.Add("HORA_REF", N.HORA_REF);
                parametros.Add("REF_IS_DATOS", N.REF_IS_DATOS);
                parametros.Add("GPS_REF_LAT", N.GPS_REF_LAT);
                parametros.Add("GPS_REF_LON", N.GPS_REF_LON);
                parametros.Add("COMENTARIO", N.COMENTARIO);
                parametros.Add("HORA_REF_FIN", N.HORA_REF_FIN);
                parametros.Add("GPS_REF_LAT_FIN", N.GPS_REF_LAT_FIN);
                parametros.Add("GPS_REF_LON_FIN", N.GPS_REF_LON_FIN);
                parametros.Add("COMENTARIO", N.COMENTARIO);
                var result = conexion.ExecuteScalar("dbo.SP_APP_GENERAR_DESEMBOLSO", param: parametros, commandType: CommandType.StoredProcedure);
                rs = Convert.ToInt32(result.ToString());
            }
            catch (Exception)
            {

                throw;
            }

            return rs;
        }


        public int obtSincroValDomiciliaria(ESincroValDomiciliaria N , string id_datos)
        {

            
            string txt = "";
            var saveDcs = new SaveDocs();
            ESaveDoc EdocsSave = new ESaveDoc();
            EdocsSave.PATH = strings.publicacionRutaValDom + N.ID_PRECOMITE;
            EdocsSave.PHOTO = N.FOTO;

            if (N.TIPO_VIS == 1)
            {
                txt = "_FOTO_VD.jpg";
            }
            else
            {
                txt = "_FOTO_VN.jpg";
            }

            int rs = 0;

            if (Directory.Exists(EdocsSave.PATH))
            {

            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(EdocsSave.PATH);
            }

            EdocsSave.NAMEDOC = (signs.slash + N.ID_PRECOMITE_CLIENTE + txt);



            try
            {
                int res = saveDcs.SaveFoto(EdocsSave);
            }
            catch (Exception)
            {

                throw;
            }

            int id;
            var resultda = new Respuesta(); 

            IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());
            try
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_PRECOMITE", N.ID_PRECOMITE);
                parametros.Add("ID_PRECOMITE_CLIENTE", N.ID_PRECOMITE_CLIENTE);
                parametros.Add("ID_USUARIO", N.ID_USUARIO);
                parametros.Add("GPS_REF_LAT", N.GPS_REF_LAT);
                parametros.Add("GPS_REF_LON", N.GPS_REF_LON);
                parametros.Add("COMENTARIO", N.COMENTARIO);
                //parametros.Add("FECHA", N.FECHA); ERROR CON LA FECHA EN EL SERVIDOR
                parametros.Add("ID_DATO", id_datos);
                parametros.Add("TIPO_VIS", N.TIPO_VIS);                
                //var result = conexion.ExecuteScalar("dbo.SP_APP_SINC_VAL_DOMICILIARIA", param: parametros, commandType: CommandType.StoredProcedure);
                var rsresult = conexion.QuerySingle <Respuesta>( "dbo.SP_APP_SINC_VAL_DOMICILIARIA", param: parametros, commandType: CommandType.StoredProcedure);
                id = Convert.ToInt32(rsresult.IntRespuesta.ToString());
                resultda = rsresult;

                rs = 1;
                conexion.Close();

            }
            catch (Exception)
            {

                throw;
            }


             

            return rs;

      

        }


        public int ObtSincronizacion(ESincronizacion N)
        {
            int rs = 0;
            string nom;
            IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());

            try
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_USUARIO", N.ID_USUARIO);
                parametros.Add("ID_PRECOMITE", N.ID_PRECOMITE);
                parametros.Add("HORA_REF", N.HORA_REF);
                parametros.Add("REF_IS_DATOS", N.REF_IS_DATOS);
                parametros.Add("HORA_REUNION", N.HORA_REUNION);
                parametros.Add("REUNION_IS_DATOS", N.REUNION_IS_DATOS);
                parametros.Add("HORA_FIN", N.HORA_FIN);
                parametros.Add("FIN_IS_DATOS", N.FIN_IS_DATOS);
                parametros.Add("GPS_REF_LAT", N.GPS_REF_LAT);
                parametros.Add("GPS_REF_LON", N.GPS_REF_LON);
                parametros.Add("GPS_INI_REUNION_LAT", N.GPS_INI_REUNION_LAT);
                parametros.Add("GPS_INI_REUNION_LON", N.GPS_INI_REUNION_LON);
                parametros.Add("GPS_FIN_REUNION_LAT", N.GPS_FIN_REUNION_LAT);
                parametros.Add("GPS_FIN_REUNION_LON", N.GPS_FIN_REUNION_LON);
                parametros.Add("COMENTARIO", N.COMENTARIO);
                parametros.Add("FOTO_REF", N.FOTO_REF);
                parametros.Add("FOTO_INI_REUNION", N.FOTO_INI_REUNION);
                parametros.Add("FOTO_GRUPAL", N.FOTO_GRUPAL);
                parametros.Add("FOTO_DIRECTIVA", N.FOTO_DIRECTIVA);
                parametros.Add("FOTO_FIN_REUNION", N.FOTO_FIN_REUNION);
                parametros.Add("ID_ACT_C", N.ID_ACT_C);
                parametros.Add("FEC_ALTAS", N.FEC_ALTAS);
                var result = conexion.Query("dbo.SP_APP_SINC_VAL_DOMICILIARIA", param: parametros, commandType: CommandType.StoredProcedure);
                rs = Convert.ToInt32(result.ToString());

            }
            catch (Exception)
            {

                throw;
            }
            return rs;
        }

      
        public int RespuestasClima(ERespuestasClima N)
        {


            int rs = 0;
            IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());
            try
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_USUARIO", N.ID_USUARIO);
                parametros.Add("ID_EVALUACION", N.ID_EVALUACION);
                parametros.Add("ID_PREGUNTA", N.ID_PREGUNTA);
                parametros.Add("IS_ABIERTA", N.IS_ABIERTA);
                parametros.Add("ID_RESPUESTA", N.ID_RESPUESTA);
                parametros.Add("RESPUESTA", N.RESPUESTA);
                parametros.Add("FECHA_APP", N.FECHA_APP);
                var result = conexion.Query("dbo.SP_APP_RESPUESTAS_CLIMA", param: parametros, commandType: CommandType.StoredProcedure);
                //rs = Convert.ToInt32(result.ToString());
                var TOKEN = result.ToList().FirstOrDefault();
                rs = Convert.ToInt32(TOKEN.CONFIRMACION);
            }
            catch (Exception EX)
            {
                rs = -1;
                throw;
            }

            return rs;
        }

        public int RespuestaIncidencia(EUPDATE_INCIDENCIAS N)
        {
            var notification = new EFCM();
            notification.Title = "Incidencias";
            notification.Respuesta = N.RESPUESTA;
            var FCM = new FireBase();

            int rs = 0;
            IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());
            try
            {

                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_INCIDENCIA", N.ID_INCIDENCIA);
                parametros.Add("RESPUESTA", N.RESPUESTA);
                parametros.Add("ID_USUARIO", N.ID_USUARIO);
                parametros.Add("ID_USUARIO_INCIDENCIA", N.ID_USUARIO_INCIDENCIA);
                parametros.Add("COMENTARIO", N.COMENTARIO);
                parametros.Add("FOTO", N.FOTO);
                parametros.Add("TIPO_INCIDENCIA", N.TIPO_INCIDENCIA);
                parametros.Add("FECHA", N.FECHA);
                parametros.Add("ID_ATTACH", N.ID_ATTACH);
                parametros.Add("COMENTARIO_SOLICITANTE", N.COMENTARIO_SOLICITANTE);
                parametros.Add("FECHA_INCIDENCIA", N.FECHA_INCIDENCIA);
                parametros.Add("IDC_INCIDENCIA", N.IDC_INCIDENCIA);
                var result = conexion.Query("SP_APP_UPDATE_INCIDENCIAS", param: parametros, commandType: CommandType.StoredProcedure);
                var TOKEN = result.ToList().FirstOrDefault();
                notification.Token = TOKEN.TOKEN;
                rs = 1;
            }
            catch (Exception)
            {
                rs = -1;
                throw;
            }


            if (N.RESPUESTA == 1)
            {
                notification.body = "Incidencia Autorizada";
            }
            else
            {
                notification.body = "Incidencia Rechazada";
            }



            Task<int> resultfcm = FCM.NotificacAsync(notification);




            return rs;

        }


        public IEnumerable<ERolAutorizacion> GetIncidencia(EINSERTAR_INCIDENCIAS N)
        {

            string rsf = "";
            string NOMBRE_FOTO = "";
            string title = "Incidencia";
            string body = "Tienes una INCIDENCIA POR AUTORIZAR DEL USUARIO " + N.ID_USUARIO;

            int rs = 0;

            var err = new EErrorFoto();
            var notification = new EFCM();

            var FCM = new FireBase();
            notification.Title = title;
            notification.body = body;
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(N.FOTO)))
            {

                Bitmap bm2 = new(ms);
                try
                {
                    Random r = new Random();
                    int nAleatorio1 = r.Next(10000000);
                    NOMBRE_FOTO = N.ID_USUARIO + "_Incidencia_" + N.ID_INCIDENCIA + "_" + nAleatorio1 + "_" + DateTime.Today.ToString("dd-MM-yyyy") + ".jpg";
                    bm2.Save(strings.publicacionRutaIncidencias + NOMBRE_FOTO);
                    //  bm2.Save(strings.TestRuta + NOMBRE_FOTO);
                    rsf = "1";
                }
                catch (Exception e)
                {

                    rsf = e.Message;
                }
                err.error = rsf;


                IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());
                try
                {
                    conexion.Open();
                    var parametros = new DynamicParameters();
                    parametros.Add("ID_USUARIO", N.ID_USUARIO);
                    parametros.Add("COMENTARIO", N.COMENTARIO);
                    parametros.Add("FOTO", NOMBRE_FOTO);
                    parametros.Add("TIPO_INCIDENCIA", N.TIPO_INCIDENCIA);
                    parametros.Add("FECHA_INCIDENCIA", N.FECHA_INCIDENCIA);
                    parametros.Add("ID_INCIDENCIA", N.ID_INCIDENCIA);
                    var result = conexion.Query<ERolAutorizacion>("dbo.SP_APP_INSERTAR_INCIDENCIAS", param: parametros, commandType: CommandType.StoredProcedure);
                    var TOKEN = result.ToList().FirstOrDefault();
                    notification.Token = TOKEN.TOKEN;

                    Task<int> FCMR = FCM.NotificacAsync(notification);

                    return result;
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
        public IEnumerable<EPartidosQuiniela> GetPartidos(int ID_USUARIO)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(id.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ListPartidos = conexion.Query<EPartidosQuiniela>("SELECT * FROM dbo.FUNC_APP_QUINIELA(" + ID_USUARIO + ")", CommandType.Text);
                return ListPartidos;
            }

        }

        public IEnumerable<EConfirmacionQuiniela> GetRespuestasQuiniela(ERespuestasQuiniela R)
        {


            var parametros = new DynamicParameters();
            parametros.Add("ID_USUARIO", R.ID_USUARIO);
            parametros.Add("ID_PARTIDO", R.ID_PARTIDO);
            parametros.Add("MARCADOR1", R.MARCADOR1);
            parametros.Add("MARCADOR2", R.MARCADOR2);

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();

                var result = conexion.Query<EConfirmacionQuiniela>("dbo.SP_APP_RESPUESTAS_QUINIELA", param: parametros, commandType: CommandType.StoredProcedure).ToList();



                return result.ToList();
            }



        }

        public IEnumerable<EResultadosQuiniela> GetResultadosQuiniela(int ID_USUARIO)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(id.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ListResultados = conexion.Query<EResultadosQuiniela>("SELECT * FROM dbo.[FUNC_APP_RESULTADOS_QUINIELA](" + ID_USUARIO + ")", CommandType.Text);
                return ListResultados;
            }

        }
        public IEnumerable<ERankingQuiniela> GetRankingQuiniela(int ID_USUARIO)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor = seguridad.GwtCambiarJson(id.eltoken);
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var ListResultados = conexion.Query<ERankingQuiniela>("SELECT * FROM dbo.[FUNC_APP_RANKING_QUINIELA](" + ID_USUARIO + ")", CommandType.Text);
                return ListResultados;
            }

        }


        public IEnumerable<EPestanas> ObtPestanasApp(string ID_USUARIO)
        {
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listPestana = conexion.Query<EPestanas>("SELECT * FROM FUNC_GET_PESTANA_APP_MICRO (" + ID_USUARIO + ")", CommandType.Text);
                return listPestana;
            }
        }

        public string SaveTokenMovil(EToken_FireBase N)
        {
            IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec());
            try
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_USUARIO", N.ID_USUARIO);
                parametros.Add("TOKEN", N.TOKEN);
                var listPestana = conexion.Query<EPestanas>("SP_SET_TOKEN_APP", param: parametros, commandType: CommandType.StoredProcedure);
                return "Token Guardado";
            }
            catch (Exception e)
            {
                return "Error Al guardar Token" + e;
                throw;
            }
        }


        public Respuesta SaveInfoDivice(EInfoDivice info,  string id_datos)
        {
            //var seguridad = new Seguridad();
            //JwtDatos valor =  seguridad.GwtCambiarJson(token.eltoken);w

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();

                var parametros = new DynamicParameters();
                parametros.Add("ID_USUARIO", 1703);
                parametros.Add("MODELO", info.MODELO);
                parametros.Add("MARCA", info.MARCA);
                parametros.Add("VERSION_ANDROID", info.VERSION_ANDROID);
                parametros.Add("COMPANIA", info.COMPANIA);

                var rcliente = conexion.QuerySingle<Respuesta>("dbo.SP_INSERT_DATOS_TELEFONO_APP_CREC_VP", param: parametros, commandType: CommandType.StoredProcedure);

                return rcliente;

            }

        }


        public IEnumerable<EListDesem> GetDesembolso(int ID_USUARIO)
        {
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listPestana = conexion.Query<EListDesem>("SELECT * FROM FUNC_APP_GRUPOS_DESEMBOLSO_APP_NUEVA(" + ID_USUARIO + ")", CommandType.Text);
                return listPestana;
            }
        }

        public IEnumerable<EListConsol> GetConsolida(int ID_USUARIO)
        {
            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listPestana = conexion.Query<EListConsol>("SELECT * FROM FUNC_APP_AGENDA_APP_NUEVA(" + ID_USUARIO + ")", CommandType.Text);
                return listPestana;
            }
        }

        public Respuesta SincroDesembolso(ESincroDesembolso info)
        {
            var resBD = new Respuesta();

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();

                var parametros = new DynamicParameters();
                parametros.Add("ID_PRESTAMO", info.ID_PRESTAMO);
                parametros.Add("ID_USUARIO", info.ID_USUARIO);
                parametros.Add("HORA_REF", info.HORA_REF);
                parametros.Add("REF_IS_DATOS", info.REF_IS_DATOS);
                parametros.Add("GPS_REF_LAT", info.GPS_REF_LAT);
                parametros.Add("GPS_REF_LON", info.GPS_REF_LON);
                parametros.Add("COMENTARIO", info.COMENTARIO);
                parametros.Add("HORA_REF_FIN", info.HORA_REF_FIN);
                parametros.Add("GPS_REF_LAT_FIN", info.GPS_REF_LAT_FIN);
                parametros.Add("GPS_REF_LON_FIN", info.GPS_REF_LON_FIN);   
                var rcliente = conexion.QuerySingle<Respuesta>("dbo.SP_APP_GENERAR_DESEMBOLSO_ERESPUESTA", param: parametros, commandType: CommandType.StoredProcedure);
                resBD = rcliente;              
            }

            var saveDoc = new SaveDocs();
            var infoDoc = new ESaveDocDes();

            infoDoc.ID_PRESTAMO = info.ID_PRESTAMO;
            infoDoc.PHOTO = info.REF_BLOB;

            resBD.IntRespuesta = saveDoc.SavePhotoDesembolso(infoDoc);
            
            
            return resBD;
        }


        public Respuesta SincroConsolidacion(ESincronizaConsol info)
        {
            var result = new Respuesta();

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();

                var parametros = new DynamicParameters();
                parametros.Add("ID_PRECOMITE", info.ID_PRECOMITE);
                parametros.Add("ID_USUARIO", info.ID_USUARIO);
                parametros.Add("HORA_REF", info.HORA_REF);
                parametros.Add("REF_IS_DATOS", info.REF_IS_DATOS);
                parametros.Add("HORA_REUNION", info.HORA_REUNION);
                parametros.Add("REUNION_IS_DATOS", info.REUNION_IS_DATOS);
                parametros.Add("HORA_FIN", info.HORA_FIN);
                parametros.Add("FIN_IS_DATOS", info.FIN_IS_DATOS);
                parametros.Add("GPS_REF_LAT", info.GPS_REF_LAT);
                parametros.Add("GPS_REF_LON", info.GPS_REF_LON);
                parametros.Add("GPS_INI_REUNION_LAT", info.GPS_INI_REUNION_LAT);
                parametros.Add("GPS_INI_REUNION_LON", info.GPS_INI_REUNION_LON);
                parametros.Add("GPS_FIN_REUNION_LAT", info.GPS_FIN_REUNION_LAT);
                parametros.Add("GPS_FIN_REUNION_LON", info.GPS_FIN_REUNION_LON);
                parametros.Add("COMENTARIO", info.COMENTARIO);
                parametros.Add("FOTO_REF", info.ID_PRECOMITE + signs.underscore + strings.FOTO_REF);
                parametros.Add("FOTO_INI_REUNION", info.ID_PRECOMITE + signs.underscore + strings.FOTO_INI_REUNION);
                parametros.Add("FOTO_GRUPAL", info.ID_PRECOMITE + signs.underscore + strings.FOTO_GRUPAL);
                parametros.Add("FOTO_DIRECTIVA", info.ID_PRECOMITE + signs.underscore + strings.FOTO_DIRECTIVA);
                parametros.Add("FOTO_FIN_REUNION", info.ID_PRECOMITE + signs.underscore + strings.FOTO_FIN_REUNION);
                parametros.Add("ID_ACT_C", info.ID_ACT_C);
                parametros.Add("FEC_ALTAS", info.FEC_ALTAS);

                var rcliente = conexion.QuerySingle<Respuesta>("dbo.SP_APP_ACTUALIZAR_CONSOLIDACION2_RESPUESTA", param: parametros, commandType: CommandType.StoredProcedure);
                result = rcliente;

            }

            var saveDoc = new SaveDocs();
            var infoDoc = new ESavePhotosConsol();

            infoDoc.ID_PRECOMITE = info.ID_PRECOMITE;
            infoDoc.GRUPAL_BLOB = info.GRUPAL_BLOB;
            infoDoc.DIRECTIVA_BLOB = info.DIRECTIVA_BLOB;
            infoDoc.REF_BLOB = info.REF_BLOB;
            infoDoc.INI_REUNION_BLOB  = info.INI_REUNION_BLOB;
            infoDoc.FIN_REUNION_BLOB = info.FIN_REUNION_BLOB;
           
            result.IntRespuesta = saveDoc.SavePhotosConsolidacion(infoDoc);


            return result;
        }



        public IEnumerable<EListActConsol> GetActiConsol()
        {

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var listPestana = conexion.Query<EListActConsol>("SELECT * FROM DBO.FUNC_GET_LIS_ACTIVIDADES_CONSOLIDACION()", CommandType.Text);
                return listPestana;
            }
        }

        public Respuesta Bitacora_sms_ca (EBitacoraSMScandidata n) {

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();



                var parametros = new DynamicParameters();
                parametros.Add("TOKEN", n.TOKEN);
                parametros.Add("NUM_CEL", n.NUM_CEL);
                parametros.Add("MENSAJE ", n.MENSAJE);
                parametros.Add("PROVEEDOR_SMS", n.PROVEEDOR_SMS);
                var rcliente = conexion.QuerySingle<Respuesta>("dbo.SP_BITACORA_SMS_CANDIDATA", param: parametros, commandType: CommandType.StoredProcedure);
                return rcliente;

            }
        }


        public Respuesta Bitacora_sms_CF(EBitacoraSMSCF n)
        {

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_PRESTAMO", n.ID_PRESTAMO);
                parametros.Add("TOKEN", n.TOKEN);
                parametros.Add("NUM_CEL", n.NUM_CEL);
                parametros.Add("MENSAJE ", n.MENSAJE);
                parametros.Add("PROVEEDOR_SMS", n.PROVEEDOR_SMS);
                var rcliente = conexion.QuerySingle<Respuesta>("dbo.SP_BITACORA_ENVIOS_SMS_CFS", param: parametros, commandType: CommandType.StoredProcedure);
                return rcliente;

            }
        }


        public Respuesta Bitacora_sms_AS(EBitacoraSMSAS n)
        {

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_PRESTAMO", n.ID_PRESTAMO);
                parametros.Add("TOKEN", n.TOKEN);
                parametros.Add("NUM_CEL", n.NUM_CEL);
                parametros.Add("MENSAJE ", n.MENSAJE);
                parametros.Add("PROVEEDOR_SMS", n.PROVEEDOR_SMS);
                var rcliente = conexion.QuerySingle<Respuesta>("dbo.SP_INSERT_BITACORA_ENVIOS_SMS_ASOCIADAS", param: parametros, commandType: CommandType.StoredProcedure);
                return rcliente;

            }
        }

        public IEnumerable<EPayCashcrearReferenciaPrestamo> GetListPrestamosPG_RPAY()
        {
           
                using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
                {
                    conexion.Open();
                    var listPestana = conexion.Query<EPayCashcrearReferenciaPrestamo>("SELECT * FROM DBO.FUNC_GET_LIST_REFE_PAYCASH_PG()", CommandType.Text);
                    return listPestana;
                }

            //return EPayCashcrearReferenciaPrestamo;
        }

        public Respuesta InsertReferenciaPayCash(int ID_PRESTAMO, EresulPaycashreference N)
        {

            using (IDbConnection conexion = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                conexion.Open();
                var parametros = new DynamicParameters();
                parametros.Add("ID_PRESTAMO", ID_PRESTAMO);
                parametros.Add("Reference", N.Reference);
                parametros.Add("ErrorCode", N.ErrorCode);
                parametros.Add("ErrorMessage", N.ErrorMessage);
                var rcliente = conexion.QuerySingle<Respuesta>("dbo.SP_INSERT_REFERENCIA_PAYCASH", param: parametros, commandType: CommandType.StoredProcedure);
                return rcliente;

            }
        }

        public string getreferenciamc(int ID_PRESTAMO)
        {
            using (IDbConnection connection = new SqlConnection(SqlRepositorio.ObtenerConec()))
            {
                connection.Open();
                var Lista = connection.ExecuteScalar("SELECT DBO.FUNC_GET_REFERENCIA(" + ID_PRESTAMO + " ) AS JSONS", CommandType.Text);
                return Lista.ToString();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        

        //EXEC SP_SET_TOKEN_APP @ID_Usuario , @Token

    }
}
