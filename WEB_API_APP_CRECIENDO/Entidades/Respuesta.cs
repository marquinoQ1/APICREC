using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WEB_API_APP_CRECIENDO.Entidades
{

    public class EPayCashcrearReferenciaPrestamo
    {
        public int ID_PRESTAMO { get; set; }
        public string REFERENCIA { get; set; }
    }

    public class EPayCashcrearReferencia
    {
        public string Amount { get; set; }
        public string ExpirationDate { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

    }

    public class EBitacoraSMScandidata
    {
        public string TOKEN { get; set; }
        public string NUM_CEL { get; set; }
        public string MENSAJE { get; set; }
        public string PROVEEDOR_SMS { get; set; }
    }

    public class EBitacoraSMSCF
    {
        public int? ID_PRESTAMO { get; set; }
        public string TOKEN { get; set; }
        public string NUM_CEL { get; set; }
        public string MENSAJE { get; set; }
        public string PROVEEDOR_SMS { get; set; }
    }

    public class EBitacoraSMSAS
    {
        public int? ID_PRESTAMO { get; set; }
        public string TOKEN { get; set; }
        public string NUM_CEL { get; set; }
        public string MENSAJE { get; set; }
        public string PROVEEDOR_SMS { get; set; }
    }

    public class EEnvioSMS
    {
        public int? ID_PRESTAMO { get; set; }
        public int? ID_PRECOMITE_CLIENTE { get; set; }
        public string TOKEN { get; set; }
        public string destination { get; set; }
        public string messageText { get; set; }
        public string PROVEEDOR_SMS { get; set; }
        public string TIPO { get; set; }

    }

    public class EiD
    {
        public string id { get; set; }
    }

    public class EresulPaycashreference
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Reference { get; set; }

    }





    public class ESmssinch
    {
        public string destination { get; set; }
        public string messageText { get; set; }
    }


    public class SaveDocJuntas
    {
        public int ID_TIPO { get; set; }
        public int ID_REUNION { get; set; }
        public string PHOTO { get; set; }
    }

    public class ESaveDoc
    {
        public string PHOTO { get; set; }
        public string PATH { get; set; } 
        public string NAMEDOC { get; set; }
    }

    public class ESaveDocDes
    {
        public string PHOTO { get; set; }
        public int ID_PRESTAMO { get; set; }
    }

    public class EListActConsol
    {
        public int ID_ACT_C { get; set;}
        public string DESCRIPCION { get; set;}
        public int ID_PADRE { get; set;}
    }

    public class ESavePhotosConsol
    {
        public int ID_PRECOMITE { get; set; }
        public string GRUPAL_BLOB { get; set; }
        public string DIRECTIVA_BLOB { get; set; }
        public string REF_BLOB { get; set; }
        public string INI_REUNION_BLOB { get; set; }
        public string FIN_REUNION_BLOB { get; set; }
    }

    public class ESincroCapa
    {

        public int ID_USUARIO { get; set; }
        public int ID_CAPACITACION { get; set; }
        public int ID_TEMA { get; set; }
        public int ID_SUBTEMA { get; set; }
        public string FOTO { get; set; }
        public int REALIZADO { get; set; }
        public string GPS_REF_LAT { get; set; }
        public string GPS_REF_LON { get; set; }
        public string ID_USUARIO_EVIDENCIA { get; set; }
        public string FECHA { get; set; }

    }


    public class EFCM
    {

        public string Token { get; set; }
        public int Respuesta { get; set; }
        public string Title { get; set; }
        public string body { get; set; }
    }



    public class EPestanas
    {
        public int ID_MENU { get; set; }
        public string DES_MENU { get; set; }
    }

    public class LId
    {
       public string eltoken { get; set; }    
    }

    public class ElistreunionBDPost
    {
        public int idreunion { get; set; }
        public int idprestamo { get; set; }
        public string eltoken { get; set; }
    }

    public class Respuesta
    {

        public int IntRespuesta { get; set; }

        public string StrRespuesta { get; set; }

        public string Observaciones { get; set; }

    }

    public class EInfoDivice
    {
        public int ID_USUARIO { get; set; }
        public string MODELO { get; set; }
        public string MARCA { get; set; }
        public string VERSION_ANDROID { get; set; }
        public string COMPANIA { get; set; }
    }


    public class Actividades
    {
        
        public int R { get; set; }
        
        public int ID_ACTIVIDAD { get; set; }
        
        public int ID_PRESTAMO { get; set; }
        
        public int ID_TIPO { get; set; }
        
        public int DIA_VISITA { get; set; }
        
        public int DIA_ACTUAL { get; set; }

    }

    public class ESeguros
    {
        
        public string PAGO_QUINCENAL { get; set; }
        
        public Nullable<decimal> PRECIO_FINAL { get; set; }
        
        public int ID_SEG_VIDA { get; set; }
        
        public string NOMBRE { get; set; }
        
        public string DESC_SEG_VIDA { get; set; }
        
        public Nullable<int> ID_ASEGURADORA { get; set; }
        
        public string STATUS { get; set; }
        
        public Nullable<int> BENEFICIARIOS { get; set; }
        
        public Nullable<int> FAMILIARES { get; set; }
        
        public Nullable<int> GRUPO { get; set; }
        
        public Nullable<int> ID_USUARIO_UPDATE { get; set; }
        
        public Nullable<decimal> IMPORTE { get; set; }
        
        public Nullable<decimal> IMPORTE_CONYUGE { get; set; }
        
        public Nullable<byte> IS_SEG_VIDA { get; set; }
        
        public Nullable<byte> TIPO_SEGURO { get; set; }
        
        public string EXCLUSIONES { get; set; }
        
        public string PRECIO { get; set; }
        
        public string ASEGURADO { get; set; }
        
        public Nullable<decimal> PRECIO_EXCEPCION { get; set; }
        
        public Nullable<byte> IS_EXCEPCION { get; set; }
        
        public Nullable<bool> IS_FINANCIADO { get; set; }

    }

    public class EAgenda
    {
        
        public int R { get; set; }
        
        public int ID { get; set; }
        
        public string GR { get; set; }
        
        public int ID_SUCURSAL { get; set; }
        
        public float LAT { get; set; }
        
        public float LON { get; set; }
    }

    
    public class EACT_CON_PRE
    {
        
        public int R { get; set; }
        
        public int ID_ACT_C { get; set; }
        
        public string DESCRIPCION { get; set; }
        
        public int ID_PADRE { get; set; }
    }


    
    public class EGRUPOS_DESEMBOLSO
    {
        
        public int R { get; set; }
        
        public int ID { get; set; }
        
        public string GR { get; set; }
        
        public int ID_SUCURSAL { get; set; }
        
        public float LAT { get; set; }
        
        public float LON { get; set; }
    }

    
    public class EListReunion
    {
        
        public int R { get; set; }
        
        public int ID_REUNION { get; set; }
        
        public string NOM_USUARIO { get; set; }
        
        public string TIPO_REUNION { get; set; }
        
        public string STS_REUNION { get; set; }
        
        public DateTime FECHA_ALTA { get; set; }
        
        public string NOM_CLIEN { get; set; }
        
        public decimal MONTO { get; set; }

    }
    
    public class EListPreValDom
    {
        
        public int R { get; set; }
        
        public int ID { get; set; }
        
        public string GR { get; set; }
        
        public int ID_SUCURSAL { get; set; }
        
        public int ID_PRECOMITE_CLIENTE { get; set; }
        
        public string NOMBRE_CLIENTE { get; set; }
        
        public string DIRECCION { get; set; }
        
        public int ID_MIEMBRO { get; set; }
        
        public string TELEFONO { get; set; }
        
        public string CELULAR { get; set; }
        
        public int STATUS { get; set; }
        
        public int FECHA_VALIDACION { get; set; }
        
        public int TIPO_VIS { get; set; }

    }

    
    public class ELisPromoPrestamos
    {
        
        public int R { get; set; }
        
        public int ID_ACTIVIDAD { get; set; }
        
        public int ID_TIPO { get; set; }
        
        public int DIA_ACTUAL { get; set; }
    }

    

    public class ElistreunionBD
    {
        
        public int ID_PRESTAMO { get; set; }
        
        public int ID_REUNION { get; set; }
        
        public string COMENTARIO { get; set; }
        
        public string FECHA_REUNION { get; set; }
        
        public int ID_TIPO { get; set; }
        
        public string DESCRIPCION { get; set; }
        
        public string NOMBRE { get; set; }
        
        public string MONTO { get; set; }
        
        public int ID_CLIENTE { get; set; }
        
        public int ID_MIEMBRO { get; set; }
    }


    
    public class EListPRESTAMOSv2
    {
        
        public int ID_PRESTAMO { get; set; }
        
        public string GRUPO { get; set; }
        
        public int R { get; set; }
        
        public int S { get; set; }
        
        public string S_LIQ { get; set; }
        
        public string H_VISITA { get; set; }
        
        public int DV { get; set; }
        
        public string MA { get; set; }
        
        public string NP { get; set; }
        
        public string C { get; set; }
        
        public int T { get; set; }
        
        public string M { get; set; }
        
        public string N { get; set; }
        
        public int DEPURADO { get; set; }
        public int IT { get; set; }
        public int IA { get; set; }

    }

    
    public class EInsertReunion
    {
        
        public int ID_REUNION { get; set; }
        
        public int ID_PRESTAMO { get; set; }

        
        public int ID_USUARIO { get; set; }
        
        public int ID_TIPO { get; set; }
        
        public float GPS_REF_LAT { get; set; }
        
        public float GPS_REF_LON { get; set; }
        
        public string REF_BLOB { get; set; }
        
        public string FECHA_REFERENCIACION { get; set; }
        
        public int ID_ACTIVIDAD { get; set; }
        
        public string ID_CLIENTE { get; set; }
        
        public string MONTO { get; set; }
        
        public string COMENTARIO { get; set; }
        
        public string FEC_INI_ACT { get; set; }
        
        public float GPS_INI_LAT { get; set; }
        
        public float GPS_INI_LON { get; set; }

        public int BANDERA { get; set; }
    }
  

    public class EIncidencias
    {
        public string id_incidencia_app { get; set; } = string.Empty;

        public string id_usuario { get; set; } = string.Empty;

        public string nombre_completo { get; set; } = string.Empty;

        public string id_rol { get; set; } = string.Empty;

        public string desc_rol { get; set; } = string.Empty;

        public string id_sucursal { get; set; } = string.Empty;

        public string nombre_sucursal { get; set; } = string.Empty;

        public string id_usuario_responsable { get; set; } = string.Empty;

        public string fecha_alta { get; set; } = string.Empty;

        public string TIPO_INCIDENCIA { get; set; }

        public string comentario { get; set; } = string.Empty;

        public string nombre_foto { get; set; } = string.Empty;

        public string status_ { get; set; } = string.Empty;

        public string fecha_incidencia { get; set; } = string.Empty;

        public string id_incidencia { get; set; } = string.Empty;

    }

    public class iIncidencias
    {

        public string id_usuario { get; set; }

        public string fec_incidencia { get; set; }

        public string hora { get; set; }

        public string incidencia { get; set; }
    }

    public class EUsuariosIncidencias
    {

        public int id_usuario { get; set; }

        public string hora { get; set; }

        public string incidencia { get; set; }

        public string j_retardo { get; set; }

        public string j_falta { get; set; }

        public string incapacidad { get; set; }

        public string vacaciones { get; set; }

        public string fecha { get; set; }

    }

    public class EStatusPreco
    {
        public int STATUS { get; set; }
    }

    public class ESucursales
    {
        public int id { get; set; }
        public string N { get; set; }
    }

    public class EGetZonas
    {
        public int ID_AREA_S { get; set; }
        public string DESCRIPCION { get; set; }
        public int ID_PLAZA { get; set; }
    }

    public class EInserPromocion
    {   
        public int ID_ACTIVIDAD { get; set; }
        public int ID_USUARIO { get; set; }     
        public int ID_TIPO { get; set; }
        public int ID_STATUS { get; set; }
        public string COMENTARIO { get; set; }
        public int APP { get; set; }
        public DateTime FECHA_REFERENCIACION { get; set; }        
        public int REF_IS_DATOS { get; set; }
        public float GPS_REF_LAT { get; set; }
        public float GPS_REF_LON { get; set; }        
    }

    public class InstSincroDesembolso 
    {
        public int ID_PRESTAMO { get; set; }
        public int ID_USUARIO { get; set; }
        public string HORA_REF { get; set; }
        public int REF_IS_DATOS { get; set; }
        public string GPS_REF_LAT { get; set; }
        public string GPS_REF_LON { get; set; }
        public string COMENTARIO { get; set; }
        public string GPS_REF_LAT_FIN { get; set; }
        public string GPS_REF_LON_FIN { get; set; }
        public string HORA_REF_FIN { get; set; }
    }

    public  class ESincroValDomiciliaria
    {
        public int ID_PRECOMITE { get; set; }
        public int ID_PRECOMITE_CLIENTE { get; set; }
        public int ID_USUARIO { get; set; }
        public float GPS_REF_LAT { get; set; }
        public float GPS_REF_LON { get; set; }
        public string COMENTARIO { get; set; }
        public string FECHA { get; set; }
        public int TIPO_VIS { get; set; }
        public string FOTO { get; set; }



    }

    public class ESincronizacion
    {
        public int ID_USUARIO { get; set; }
        public int ID_PRECOMITE { get; set; }
        public DateTime HORA_REF { get; set; }
        public byte REF_IS_DATOS { get; set; }
        public DateTime HORA_REUNION { get; set; }
        public byte REUNION_IS_DATOS { get; set; }
        public DateTime HORA_FIN { get; set; }
        public byte FIN_IS_DATOS { get; set; }
        public float GPS_REF_LAT { get; set; }
        public float GPS_REF_LON { get; set; }
        public float GPS_INI_REUNION_LAT { get; set; }
        public float GPS_INI_REUNION_LON { get; set; }
        public float GPS_FIN_REUNION_LAT { get; set; }
        public float GPS_FIN_REUNION_LON { get; set; }
        public string COMENTARIO { get; set; }
        public string FOTO_REF { get; set; }
        public string FOTO_INI_REUNION { get; set; }
        public string FOTO_GRUPAL { get; set; }
        public string FOTO_DIRECTIVA { get; set; }
        public string FOTO_FIN_REUNION { get; set; }
        public string ID_ACT_C { get; set; }
        public string FEC_ALTAS { get; set; }        
   }

    public class EFotos
    {
        //Fotos
        public int    ID_PRECOMITE { get; set; }
        public string GRUPAL_BLOB { get; set; }
        public string DIRECTIVA_BLOB { get; set; }
        public string REF_BLOB { get; set; }
        public string INI_REUNION_BLOB { get; set; }
        public string FIN_REUNION_BLOB { get; set; }
    }


   public class EEvidencia
    {
        public int ID_USUARIO { get; set; }
        public int ID_CAPACITACION { get; set; }
        public int ID_TEMA { get; set; }
        public int ID_SUBTEMA { get; set; }
        public string ATTACH { get; set; }
        public int REALIZADO { get; set; }
        public float GPS_REF_LAT { get; set; }
        public float GPS_REF_LON { get; set; }
        public int ID_USUARIO_EVIDENCIA { get; set; }
        public string FECHA { get; set; }
    }

    public class ERespuestasClima
    {
        public int ID_USUARIO { get; set; }
        public int ID_EVALUACION { get; set; }
        public int ID_PREGUNTA { get; set; }
        public int IS_ABIERTA { get; set; }
        public int ID_RESPUESTA { get; set; }
        public string RESPUESTA { get; set; }
        public string FECHA_APP { get; set; }
    }

    public class EUPDATE_INCIDENCIAS
    {
        public int ID_INCIDENCIA { get; set; }
        public int RESPUESTA { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_USUARIO_INCIDENCIA { get; set; }
        public string COMENTARIO { get; set; }
        public string FOTO { get; set; }
        public string TIPO_INCIDENCIA { get; set; }
        public string FECHA { get; set; }
        public int ID_ATTACH { get; set; }
        public string COMENTARIO_SOLICITANTE { get; set; }
        public string FECHA_INCIDENCIA { get; set; }
        public int IDC_INCIDENCIA { get; set; }


    }

    public class EINSERTAR_INCIDENCIAS
    {
        public int ID_USUARIO { get; set; }
        public string COMENTARIO { get; set; }
        public string FOTO { get; set; }
        public string TIPO_INCIDENCIA { get; set; }
        public string FECHA_INCIDENCIA { get; set; }
        public int ID_INCIDENCIA { get; set; }

    }



    public class ERolAutorizacion
    {
        public int USR1 { get; set; }
        public int USR2 { get; set; }
        public string TOKEN { get; set; }
    }

    public class EErrorFoto
    {
        public string error { get; set; }
        public int iderror { get; set; }
    }
    public class EPartidosQuiniela
    {
        public string MENSAJE { get; set; }
        public int VALIDACION { get; set; }
        public int VALIDACION_FECHA { get; set; }
        public int ID_PARTIDO { get; set; }
        public string GRUPO { get; set; }
        public string BANDERA1 { get; set; }
        public string PAIS1 { get; set; }
        public int MARCADOR1 { get; set; }
        public int MARCADOR2 { get; set; }
        public string PAIS2 { get; set; }
        public string BANDERA2 { get; set; }

    }

    public class ERespuestasQuiniela
    {
        public int ID_USUARIO { get; set; }
        public int ID_PARTIDO { get; set; }
        public int MARCADOR1 { get; set; }
        public int MARCADOR2 { get; set; }
    }
    public class EConfirmacionQuiniela
    {
        public int CONFIRMACION { get; set; }
    }
    public class EResultadosQuiniela
    {
        public string MENSAJE { get; set; }
        public int VALIDACION { get; set; }
        public int VALIDACION_FECHA { get; set; }
        public int ID_PARTIDO { get; set; }
        public string GRUPO { get; set; }
        public string BANDERA1 { get; set; }
        public string PAIS1 { get; set; }
        public int MARCADOR1 { get; set; }
        public int MARCADOR2 { get; set; }
        public string PAIS2 { get; set; }
        public string BANDERA2 { get; set; }
        public int PUNTUAJE { get; set; }

    }

    public class ERankingQuiniela
    {
        public string MENSAJE { get; set; }
        public int VALIDACION { get; set; }
        public string NOMBRE { get; set; }
        public int PUNTUAJE { get; set; }
        public int POS { get; set; }
        public int ID { get; set; }

    }

    public  class ENotification
    {
        public string token { get; set; }
        public int id_usuario { get; set; }
        public int respuesta { get; set; }
    }


    public class EToken_FireBase
    {
        public int ID_USUARIO { get; set; }
        public string TOKEN { get; set; }

    }

    public class EListNotificacionMasiva
    {
        public int ID_USUARIO { get; set; }
        //public string INCIDENCIA { get; set; }
        public string NOTIFICACION { get; set; }

    }

    public class EListDesem
    {
        public int ID_PRESTAMO { get; set; }
        public string GRUPO_SOLIDARIO { get; set; }
        public int ID_SUCURSAL { get; set; }
        public float LAT { get; set; }
        public float LON { get; set; }
    }

    public class EListConsol
    {
        public int ID_PRECOMITE { get; set; }
        public string GRUPO_SOLIDARIO { get; set; }
        public int ID_SUCURSAL { get; set; }
        public float LAT { get; set; }
        public float LON { get; set; }
    }


    public class ESincroDesembolso
    {
        public int ID_PRESTAMO { get; set; }
        public int ID_USUARIO { get; set; }
        public string HORA_REF { get; set; }
        public string REF_IS_DATOS { get; set; }
        public string GPS_REF_LAT { get; set; }
        public string GPS_REF_LON { get; set; }
        public string REF_BLOB { get; set; }
        public string COMENTARIO { get; set; }
        public string GPS_REF_LAT_FIN { get; set; }
        public string GPS_REF_LON_FIN { get; set; }
        public string HORA_REF_FIN { get; set; }
    }

    public class ESincronizaConsol
    {
        public int ID_PRECOMITE { get; set; }
        public int ID_USUARIO { get; set; }
        public string HORA_REF { get; set; }
        public string REF_IS_DATOS { get; set; }
        public string HORA_REUNION { get; set; }
        public string REUNION_IS_DATOS { get; set; }
        public string HORA_FIN { get; set; }
        public string FIN_IS_DATOS { get; set; }
        public string GPS_REF_LAT { get; set; }
        public string GPS_REF_LON { get; set; }
        public string GPS_INI_REUNION_LAT { get; set; }
        public string GPS_INI_REUNION_LON { get; set; }
        public string GPS_FIN_REUNION_LAT { get; set; }
        public string GPS_FIN_REUNION_LON { get; set; }
        public string REF_BLOB { get; set; }
        public string INI_REUNION_BLOB { get; set; }
        public string GRUPAL_BLOB { get; set; }
        public string DIRECTIVA_BLOB { get; set; }
        public string FIN_REUNION_BLOB { get; set; }
        public string COMENTARIO { get; set; }
        public string ID_ACT_C { get; set; }
        public string FEC_ALTAS { get; set; }

    }
   
}
