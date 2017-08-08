using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static PSO.Entities._Solicitud;

namespace PSO.Entities
{
    public class SolicitudLog
    {
        public SolicitudLog()
        {
            WhoUpdated = 0;

            UpdateDate = DateTime.MaxValue;

            ID = 0;

            LockedById = 0;

            Status = Statuses.NO_TRAMITADO;

            NumeroSolicitud = string.Empty;

            LockedAt = DateTime.MaxValue;

            FechaDocIncompleto = DateTime.MaxValue;

            FechaTramitada = DateTime.MaxValue;

            CoordinadorID = string.Empty;

            FechaRevision = DateTime.MaxValue;

            ProcesadorId = string.Empty;

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

            Duration = string.Empty;
        }

        public DateTime LockedAt
        {
            get;
            set;
        }

        public int LockedById
        {
            get;

            set;
        }

        //Only used in production report cause I was lazy
        public string Duration
        {
            get;
            set;
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

        public string CoordinadorID
        {
            get;

            set;
        }

        public DateTime FechaRevision
        {
            get;

            set;
        }

        public string ProcesadorId
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