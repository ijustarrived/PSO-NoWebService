using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class ReferenciasDisponiblesRepo
    {
        public static string Create(LinkedList<ReferenciaDisponible> referencias)
        {
            string error = string.Empty;

            for (int i = 0; i < referencias.Count; i++)
            {
                using (SqlConnection conn = DB.GetLocalConnection())
                {
                    #region Sql command

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Referencias (Nombre,
                                                                            Direccion,
                                                                            Pueblo,
                                                                            CodigoPostal,
                                                                            NumeroSolicitud,
                                                                            Parentesco,
                                                                            Telefono)
                                                                            VALUES(
                                                                            @Nombre,
                                                                            @Direccion,
                                                                            @Pueblo,
                                                                            @CodigoPostal,
                                                                            @NumeroSolicitud,
                                                                            @Parentesco,
                                                                            @Telefono);", conn);

                    #endregion

                    #region Command Parameteres

                    cmd.Parameters.AddWithValue("@Telefono", referencias.ElementAt(i).Telefono);

                    cmd.Parameters.AddWithValue("@NumeroSolicitud", referencias.ElementAt(i).NumeroSolicitud);

                    cmd.Parameters.AddWithValue("@Parentesco", referencias.ElementAt(i).Parentesco);

                    cmd.Parameters.AddWithValue("@Nombre", referencias.ElementAt(i).Nombre);

                    cmd.Parameters.AddWithValue("@Direccion", referencias.ElementAt(i).Direccion);

                    cmd.Parameters.AddWithValue("@Pueblo", referencias.ElementAt(i).Pueblo);

                    cmd.Parameters.AddWithValue("@CodigoPostal", referencias.ElementAt(i).CodigoPostal);                    

                    conn.Open();

                    #endregion

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        cmd.Transaction = transaction;

                        try
                        {
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }

                        catch (SqlException ex)
                        {
                            transaction.Rollback();

                            error += string.Format("{0} ", ex.Message);
                        }
                    }
                }
            }

            return error;
        }

        public static string Update(LinkedList<ReferenciaDisponible> referencias)
        {
            string error = string.Empty;

            for (int i = 0; i < referencias.Count; i++)
            {
                using (SqlConnection conn = DB.GetLocalConnection())
                {
                    #region Sql command

                    SqlCommand cmd = new SqlCommand(@"UPDATE Referencias SET Nombre = @Nombre,
                                                                            Direccion = @Direccion,
                                                                            Pueblo = @Pueblo,
                                                                            CodigoPostal = @CodigoPostal,
                                                                            Parentesco = @Parentesco,
                                                                            Telefono = @Telefono
                                                                            WHERE ID = @ID;", conn);

                    #endregion

                    #region Command Parameteres

                    cmd.Parameters.AddWithValue("@Telefono", referencias.ElementAt(i).Telefono);

                    cmd.Parameters.AddWithValue("@ID", referencias.ElementAt(i).ID);

                    cmd.Parameters.AddWithValue("@Parentesco", referencias.ElementAt(i).Parentesco);

                    cmd.Parameters.AddWithValue("@Nombre", referencias.ElementAt(i).Nombre);

                    cmd.Parameters.AddWithValue("@Direccion", referencias.ElementAt(i).Direccion);

                    cmd.Parameters.AddWithValue("@Pueblo", referencias.ElementAt(i).Pueblo);

                    cmd.Parameters.AddWithValue("@CodigoPostal", referencias.ElementAt(i).CodigoPostal);

                    conn.Open();

                    #endregion

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        cmd.Transaction = transaction;

                        try
                        {
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }

                        catch (SqlException ex)
                        {
                            transaction.Rollback();

                            error += string.Format("{0} ", ex.Message);
                        }
                    }
                }
            }

            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numSolicitud">Pass ID not num solicitud</param>
        /// <returns></returns>
        public static LinkedList<ReferenciaDisponible> GetReferenciasByNumSolicitud(string numSolicitud)
        {
            LinkedList<ReferenciaDisponible> referencias = new LinkedList<ReferenciaDisponible>();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Referencias WHERE NumeroSolicitud = @NumeroSolicitud", conn);

                cmd.Parameters.AddWithValue("NumeroSolicitud", numSolicitud);

                conn.Open();

                cmd.ExecuteNonQuery();

                BuildReferencias(cmd, ref referencias);
            }

            return referencias;
        }

        private static void BuildReferencias(SqlCommand command, ref LinkedList<ReferenciaDisponible> referencias)
        {
            SqlDataReader reader = command.ExecuteReader();

            #region Read query

            while (reader.Read())
            {
                int col = 0;

                ReferenciaDisponible referencia = new ReferenciaDisponible()
                {
                    ID = reader.GetInt32(col++),

                    Parentesco = reader.GetString(col++),

                    Direccion = reader.GetString(col++),

                    Nombre = reader.GetString(col++),

                    Pueblo = Convert.ToInt32(reader.GetString(col++)),

                    CodigoPostal = reader.GetString(col++),

                    NumeroSolicitud = reader.GetString(col++),

                    Telefono = reader.GetString(col++)
                };                

                referencias.AddLast(referencia);
            }
            #endregion
        }
    }    
}