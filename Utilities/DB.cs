using System.Configuration;
using System.Data.SqlClient;

namespace PSO.Utilities
{
    public class DB
    {
        public static SqlConnection GetLocalConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
        }

        //public static SqlConnection GetServerConnection()
        //{
        //    //return new SqlConnection(ConfigurationManager.ConnectionStrings["local"].ConnectionString);
        //}

        public static SqlConnection GetLogConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["logLocal"].ConnectionString);
        }
    }
}