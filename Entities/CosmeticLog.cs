using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static PSO.Entities.Cosmetic;

namespace PSO.Entities
{
    public class CosmeticLog
    {
        private const string LOGO_DIR = "~/PageImages";

        public CosmeticLog()
        {
            ID = 0;

            LogoPath = "~/PageImages/PSO_horizontal.png";

            TitleBackColor = "#616161";

            LabelForeColor = "#79256E";

            LinkForeColor = "#79256E";

            ColorVersion = Versions.VANILLA;

            ConsultaCoorTitle = "Solicitudes Pendientes de Revisarse por Coordinador";

            ConsultaProcTitle = "Solicitudes Pendientes de Trabajarse por un Procesador";

            ConsultaSolicitudTitle = "Solicitudes por Nombre, Seguro Social o Status";

            ConsultaSuperTitle = "Solicitudes Pendientes de Asignarse a un Procesador";

            ReportComparacionTitle = "Comparación entre Solicitudes Recibidas y Procesadas";

            ReportProduccionTitle = "Resultados de Producción por Rol";

            SolicitudTitle = "Solicitud de Servicio";

            CompletadasMesDiasTitle = "Tiempo Transcurrido en Completar Solicitudes";

            HistoryCompletadasTitle = "Historial de Solicitudes Completadas";

            HistoryRecibidasTitle = "Historial de Solicitudes Recibidas";

            IndicadoresProductividadTitle = "Indicadores de Productividad en Proceso de Manejo de Solicitudes";

            SolicitudesStatusTitle = "Solicitudes Recibidas por Status";

            WhoUpdated = 0;

            UpdateDate = DateTime.MaxValue;
        }

        public CosmeticLog(CosmeticLog cos)
        {
            ID = cos.ID;

            LogoPath = cos.LogoPath;

            TitleBackColor = cos.TitleBackColor;

            LabelForeColor = cos.LabelForeColor;

            LinkForeColor = cos.LinkForeColor;

            ColorVersion = cos.ColorVersion;

            ConsultaCoorTitle = cos.ConsultaCoorTitle;

            ConsultaProcTitle = cos.ConsultaProcTitle;

            ConsultaSolicitudTitle = cos.ConsultaSolicitudTitle;

            ConsultaSuperTitle = cos.ConsultaSuperTitle;

            ReportComparacionTitle = cos.ReportComparacionTitle;

            ReportProduccionTitle = cos.ReportProduccionTitle;

            SolicitudTitle = cos.SolicitudTitle;

            CompletadasMesDiasTitle = cos.CompletadasMesDiasTitle;

            HistoryCompletadasTitle = cos.HistoryCompletadasTitle;

            HistoryRecibidasTitle = cos.HistoryRecibidasTitle;

            IndicadoresProductividadTitle = cos.IndicadoresProductividadTitle;

            SolicitudesStatusTitle = cos.SolicitudesStatusTitle;

            WhoUpdated = cos.WhoUpdated;

            UpdateDate = cos.UpdateDate;
        }

        public static string GetLogoDir()
        {
            return LOGO_DIR;
        }

        public int ID
        {
            get;

            set;
        }

        public string LogoPath
        {
            get;

            set;
        }

        public string TitleBackColor
        {
            get;

            set;
        }

        public string LabelForeColor
        {
            get;

            set;
        }

        public string LinkForeColor
        {
            get;

            set;
        }

        public string SolicitudTitle
        {
            get;

            set;
        }

        public string ConsultaCoorTitle
        {
            get;

            set;
        }

        public string ConsultaProcTitle
        {
            get;

            set;
        }

        public string ConsultaSuperTitle
        {
            get;

            set;
        }

        public string ConsultaSolicitudTitle
        {
            get;

            set;
        }

        public string ReportComparacionTitle
        {
            get;

            set;
        }

        public string ReportProduccionTitle
        {
            get;

            set;
        }

        public string HistoryRecibidasTitle
        {
            get;

            set;
        }

        public string HistoryCompletadasTitle
        {
            get;

            set;
        }

        public string SolicitudesStatusTitle
        {
            get;

            set;
        }

        public string IndicadoresProductividadTitle
        {
            get;

            set;
        }

        public string CompletadasMesDiasTitle
        {
            get;

            set;
        }

        public Versions ColorVersion
        {
            get;

            set;
        }

        public DateTime UpdateDate
        {
            get;

            set;
        }

        public int WhoUpdated
        {
            get;

            set;
        }
    }
}