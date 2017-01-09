using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSO.Entities
{
    public class DocReq
    {
        private const string PATH_DOCUMENTS = "~/Documents";

        public DocReq()
        {
            ID = 0;

            EmailUser = string.Empty;

            PathDoc = string.Empty;

            Nombre = string.Empty;

            NumeroSolicitud = string.Empty;

            Status = 0;
        }

        public int ID
        {
            get;

            set;
        }

        public string EmailUser
        {
            get;

            set;
        }

        public string PathDoc
        {
            get;

            set;
        }

        public string Nombre
        {
            get;

            set;
        }

        public string NumeroSolicitud
        {
            get;

            set;
        }

        public int Status
        {
            get;

            set;
        }

        public static string GetPathDocs()
        {
            return PATH_DOCUMENTS;
        }
    }
}