using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSO.Entities
{
    [Serializable]
    public class Rol
    {
        public Rol()
        {
            RoleType = TiposRole.EXTERNO;

            Nombre = RoleType.ToString();

            ID = 0;

            ViewSolicitud = false;

            ViewConfigDocReq = false;

            ViewConfigRole = false;

            ViewConfigUser = false;

            ViewConsuCoor = false;

            ViewConsuProc = false;

            ViewConsuPendAsig = false;

            ViewRepAvisosStatus = false;

            ViewRepRecVsPen = false;

            ViewConsuSolicitud = false;

            EditDocReq = false;

            EditRoles = false;

            EditUsers = false;

            EditCustomizationPage = false;

            ViewRepProduc = false;
        }

        /// <summary>
        /// Only for role config page
        /// </summary>
        /// <param name="tipo"></param>
        public Rol(TiposRole tipo)
        {
            RoleType = tipo;

            Nombre = tipo.ToString();

            ID = (int)tipo;
        }

        public enum TiposRole
        {
            EXTERNO = 0,
            PROCESADOR,
            COORDINADOR,
            ADMINISTRADOR,
            SUPERVISOR
        }

        public int ID
        {
            get;

            set;
        }

        public string Nombre
        {
            get;

            set;
        }

        public TiposRole RoleType
        {
            get;
            set;
        }

        public static TiposRole GetRoleType(int roleId)
        {
            TiposRole role = TiposRole.EXTERNO;

            switch (roleId)
            {
                case (int)TiposRole.ADMINISTRADOR:

                    role = TiposRole.ADMINISTRADOR;

                    break;

                case (int)TiposRole.COORDINADOR:

                    role = TiposRole.COORDINADOR;

                    break;

                case (int)TiposRole.PROCESADOR:

                    role = TiposRole.PROCESADOR;

                    break;

                case (int)TiposRole.SUPERVISOR:

                    role = TiposRole.SUPERVISOR;

                    break;

                default:

                    role = TiposRole.EXTERNO;

                    break;
            }

            return role;
        }

        public bool ViewSolicitud
        {
            get;

            set;
        }

        public bool ViewConsuCoor
        {
            get;

            set;
        }

        public bool ViewConsuProc
        {
            get;

            set;
        }

        public bool ViewConsuSolicitud
        {
            get;

            set;
        }

        public bool ViewConsuPendAsig
        {
            get;

            set;
        }

        public bool ViewConfigRole
        {
            get;

            set;
        }

        public bool ViewConfigUser
        {
            get;

            set;
        }

        public bool ViewConfigDocReq
        {
            get;

            set;
        }

        public bool ViewRepAvisosStatus
        {
            get;

            set;
        }

        public bool ViewRepRecVsPen
        {
            get;

            set;
        }

        public bool ViewRepProduc
        {
            get;

            set;
        }

        public bool EditRoles
        {
            get;

            set;
        }

        public bool EditUsers
        {
            get;

            set;
        }

        public bool EditDocReq
        {
            get;

            set;
        }

        public bool EditCustomizationPage
        {
            get;

            set;
        }

    }
}