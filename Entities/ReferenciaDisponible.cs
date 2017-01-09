using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSO.Entities
{
    public class ReferenciaDisponible
    {
        public ReferenciaDisponible()
        {
            NumeroSolicitud = string.Empty;

            ID = 0;

            Parentesco = string.Empty;

            Direccion = string.Empty;

            Nombre = string.Empty;

            Pueblo = 0;

            CodigoPostal = string.Empty;

            Telefono = string.Empty;
        }

        public string NumeroSolicitud
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public string Parentesco
        {
            get;
            set;
        }

        public string Direccion
        {
            get;
            set;
        }

        public string Nombre
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

        public string Telefono
        {
            get;

            set;
        }
    }
}