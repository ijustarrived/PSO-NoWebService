using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSO.Entities
{
    public class UsuarioLog
    {
        public UsuarioLog()
        {
            WhoUpdated = 0;

            LogInDate = DateTime.MaxValue;

            UpdateDate = DateTime.MaxValue;

            LogOutDate = DateTime.MaxValue;

            ID = 0;

            Password = string.Empty;

            SeguroSocial = string.Empty;

            Nombre = string.Empty;

            ApellidoMaterno = string.Empty;

            ApellidoPaterno = string.Empty;

            LicenciaConducir = string.Empty;

            Celular = string.Empty;

            Tel = string.Empty;

            Direccion = string.Empty;

            Pueblo = 0;

            CodigoPostal = string.Empty;

            DireccionPost = string.Empty;

            PuebloPost = 0;

            CodigoPostalPost = string.Empty;

            FechaNacimiento = new DateTime();

            Email = string.Empty;

            Activo = true;

            Role = new Rol();

            IsLoggedIn = false;

            LastTimeActive = DateTime.MaxValue;
        }

        public UsuarioLog(Usuario user)
        {
            LogInDate = DateTime.MaxValue;

            LogOutDate = DateTime.MaxValue;

            WhoUpdated = user.ID;

            UpdateDate = DateTime.Now;

            ID = user.ID;

            Password = user.Password;

            SeguroSocial = user.SeguroSocial;

            Nombre = user.Nombre;

            ApellidoMaterno = user.ApellidoMaterno;

            ApellidoPaterno = user.ApellidoPaterno;

            LicenciaConducir = user.LicenciaConducir;

            Celular = user.Celular;

            Tel = user.Tel;

            Direccion = user.Direccion;

            Pueblo = user.Pueblo;

            CodigoPostal = user.CodigoPostal;

            DireccionPost = user.DireccionPost;

            PuebloPost = user.PuebloPost;

            CodigoPostalPost = user.CodigoPostalPost;

            FechaNacimiento = user.FechaNacimiento;

            Email = user.Email;

            Activo = user.Activo;

            Role = user.Role;

            IsLoggedIn = user.IsLoggedIn;

            LastTimeActive = user.LastTimeActive;
        }

        public int WhoUpdated
        {
            get;

            set;
        }

        public DateTime UpdateDate
        {
            get;

            set;
        }

        public DateTime LogOutDate
        {
            get;

            set;
        }

        public DateTime LogInDate
        {
            get;

            set;
        }

        public DateTime LastTimeActive
        {
            get;
            set;
        }

        public bool IsLoggedIn
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string SeguroSocial
        {
            get;
            set;
        }

        public string Nombre
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

        public string GetNombreCompleto()
        {
            return string.Format("{0} {1} {2}", Nombre, ApellidoPaterno, ApellidoMaterno);
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

        public string Tel
        {
            get;
            set;
        }

        public string Direccion
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

        public string DireccionPost
        {
            get;
            set;
        }

        public int PuebloPost
        {
            get;
            set;
        }

        public string CodigoPostalPost
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public DateTime FechaNacimiento
        {
            get;
            set;
        }

        public Rol Role
        {
            get;
            set;
        }

        public bool Activo
        {
            get;
            set;
        }
    }
}