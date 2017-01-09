using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PSO.Entities
{
    public class _Solicitud
    {
        public enum Statuses
        {
            NO_TRAMITADO = 0,
            PEND_REVISAR,
            PEND_ASIGNAR,
            PEND_TRABAJAR,
            APROBADA,
            DENEGADA,
            PEND_DOCS,
            INACTIVO
        }

        public _Solicitud()
        {
            ID = 0;

            Status = Statuses.NO_TRAMITADO;

            NumeroSolicitud = string.Empty;

            FechaDocIncompleto = DateTime.MaxValue;

            FechaTramitada = DateTime.MaxValue;

            CoordinadorID = 0;

            FechaRevision = DateTime.MaxValue;

            ProcesadorId = 0;

            FechaAsigProcesador = DateTime.MaxValue;

            ComentarioProcesador = string.Empty;

            TrabajadorId = 0;

            FechaTrabajado = DateTime.MaxValue;

            ComentarioTrabajo = string.Empty;

            Nombre = string.Empty;

            SeguroSocial = string.Empty;

            Email = string.Empty;

            ApellidoPaterno = string.Empty;

            ApellidoMaterno = string.Empty;

            Telefono = string.Empty;

            LicenciaConducir = string.Empty;

            Celular = string.Empty;

            Dirrecion = string.Empty;

            Pueblo = 0;

            CodigoPostal = string.Empty;

            DirrecionPostal = string.Empty;

            PuebloPostal = 0;

            CodigoPostalPostal = string.Empty;

            NombreCo = string.Empty;

            SeguroSocialCo = string.Empty;

            EmailCo = string.Empty;

            ApellidoPaternoCo = string.Empty;

            ApellidoMaternoCo = string.Empty;

            TelefonoCo = string.Empty;

            LicenciaConducirCo = string.Empty;

            CelularCo = string.Empty;

            DirrecionCo = string.Empty;

            PuebloCo = 0;

            CodigoPostalCo = string.Empty;

            DirrecionPostalCo = string.Empty;

            PuebloPostalCo = 0;

            CodigoPostalPostalCo = string.Empty;

            FechaNacimiento = DateTime.MaxValue;

            FechaNacimientoCo = DateTime.MaxValue;
        }

        public int ID
        {
            get;
            set;
        }

        public Statuses Status
        {
            get;

            set;
        }

        public string NumeroSolicitud
        {
            get;

            set;
        }

        public DateTime FechaTramitada
        {
            get;

            set;
        }

        public DateTime FechaDocIncompleto
        {
            get;

            set;
        }

        public DateTime FechaNacimiento
        {
            get;

            set;
        }

        public DateTime FechaNacimientoCo
        {
            get;

            set;
        }

        public int CoordinadorID
        {
            get;

            set;
        }

        public DateTime FechaRevision
        {
            get;

            set;
        }

        public int ProcesadorId
        {
            get;

            set;
        }

        public DateTime FechaAsigProcesador
        {
            get;

            set;
        }

        public string ComentarioProcesador
        {
            get;

            set;
        }

        public int TrabajadorId
        {
            get;

            set;
        }

        public DateTime FechaTrabajado
        {
            get;

            set;
        }

        public string ComentarioTrabajo
        {
            get;

            set;
        }

        public string Nombre
        {
            get;

            set;
        }

        public string SeguroSocial
        {
            get;

            set;
        }

        public string Email
        {
            get;

            set;
        }

        public string ApellidoPaterno
        {
            get;

            set;
        }

        public string ApellidoMaterno
        {
            get;

            set;
        }

        public string Telefono
        {
            get;

            set;
        }

        public string LicenciaConducir
        {
            get;

            set;
        }

        public string Celular
        {
            get;

            set;
        }

        public string Dirrecion
        {
            get;

            set;
        }

        public int Pueblo
        {
            get;

            set;
        }

        public string CodigoPostal
        {
            get;

            set;
        }

        public string DirrecionPostal
        {
            get;

            set;
        }

        public int PuebloPostal
        {
            get;

            set;
        }

        public string CodigoPostalPostal
        {
            get;

            set;
        }

        public string NombreCo
        {
            get;

            set;
        }

        public string SeguroSocialCo
        {
            get;

            set;
        }

        public string EmailCo
        {
            get;

            set;
        }

        public string ApellidoPaternoCo
        {
            get;

            set;
        }

        public string ApellidoMaternoCo
        {
            get;

            set;
        }

        public string TelefonoCo
        {
            get;

            set;
        }

        public string LicenciaConducirCo
        {
            get;

            set;
        }

        public string CelularCo
        {
            get;

            set;
        }

        public string DirrecionCo
        {
            get;

            set;
        }

        public int PuebloCo
        {
            get;

            set;
        }

        public string CodigoPostalCo
        {
            get;

            set;
        }

        public string DirrecionPostalCo
        {
            get;

            set;
        }

        public int PuebloPostalCo
        {
            get;

            set;
        }

        public string CodigoPostalPostalCo
        {
            get;

            set;
        }

        public static Dictionary<string, Color> GetStatusMsgAndColor(Statuses status)
        {
            Dictionary<string, Color> txtAndColor = new Dictionary<string, Color>();

            switch (status)
            {
                case Statuses.PEND_ASIGNAR:

                    txtAndColor.Add("Pendiente de Asignar a un Procesador"
                        , ColorTranslator.FromHtml("#CFCF38"));

                    break;

                case Statuses.PEND_REVISAR:

                    txtAndColor.Add("Pendiente a Revisar por Coordinador de Servicios"
                        , ColorTranslator.FromHtml("#CFCF38"));

                    break;

                case Statuses.PEND_TRABAJAR:

                    txtAndColor.Add("Pendiente a Trabajarse por un Procesador de Servicios"
                        , ColorTranslator.FromHtml("#CFCF38"));

                    break;

                case Statuses.APROBADA:

                    txtAndColor.Add("Aprobada"
                        , ColorTranslator.FromHtml("#009900"));

                    break;

                case Statuses.DENEGADA:

                    txtAndColor.Add("Denegada"
                        , ColorTranslator.FromHtml("#CC0000"));

                    break;

                case Statuses.PEND_DOCS:

                    txtAndColor.Add("Pendiente por Documentos Incompletos"
                        , ColorTranslator.FromHtml("#CFCF38"));

                    break;

                case Statuses.INACTIVO:

                    txtAndColor.Add("Inactiva"
                        , ColorTranslator.FromHtml("#616161"));

                    break;
            }

            return txtAndColor;
        }

        public static Statuses GetStatus(int statusID)
        {
            Statuses status = Statuses.DENEGADA;

            switch (statusID)
            {
                case (int)Statuses.PEND_ASIGNAR:

                    status = Statuses.PEND_ASIGNAR;

                    break;

                case (int)Statuses.PEND_REVISAR:

                    status = Statuses.PEND_REVISAR;

                    break;

                case (int)Statuses.PEND_TRABAJAR:

                    status = Statuses.PEND_TRABAJAR;

                    break;

                case (int)Statuses.APROBADA:

                    status = Statuses.APROBADA;

                    break;

                case (int)Statuses.DENEGADA:

                    status = Statuses.DENEGADA;

                    break;

                case (int)Statuses.PEND_DOCS:

                    status = Statuses.PEND_DOCS;

                    break;

                case (int)Statuses.INACTIVO:

                    status = Statuses.INACTIVO;

                    break;
            }

            return status;
        }
    }
}