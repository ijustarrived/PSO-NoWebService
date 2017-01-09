using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class DocsReqRepo
    {
        public static string Create(LinkedList<DocReq> docs)
        {
            string error = string.Empty;

            for (int i = 0; i < docs.Count; i++)
            {
                using (SqlConnection conn = DB.GetLocalConnection())
                {
                    #region Sql command

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Documentos (Nombre,
                                                                            EmailUsuario,
                                                                            PathDocumento,
                                                                            Status,
                                                                            NumeroSolicitud)
                                                                            VALUES(
                                                                            @Nombre,
                                                                            @EmailUsuario,
                                                                            @PathDocumento,
                                                                            @Status,
                                                                            @NumeroSolicitud);", conn);

                    #endregion

                    #region Command Parameteres

                    cmd.Parameters.AddWithValue("@NumeroSolicitud", docs.ElementAt(i).NumeroSolicitud);

                    cmd.Parameters.AddWithValue("@EmailUsuario", docs.ElementAt(i).EmailUser);

                    cmd.Parameters.AddWithValue("@Nombre", docs.ElementAt(i).Nombre);

                    cmd.Parameters.AddWithValue("@PathDocumento", docs.ElementAt(i).PathDoc);

                    cmd.Parameters.AddWithValue("@Status", docs.ElementAt(i).Status);

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

        public static string Update(LinkedList<DocReq> docs)
        {
            string error = string.Empty;

            for (int i = 0; i < docs.Count; i++)
            {
                using (SqlConnection conn = DB.GetLocalConnection())
                {
                    #region Sql command

                    SqlCommand cmd = new SqlCommand(@"UPDATE Documentos SET Nombre = @Nombre,
                                                                            EmailUsuario = @EmailUsuario,
                                                                            PathDocumento = @PathDocumento,
                                                                            Status = @Status,
                                                                            NumeroSolicitud = @NumeroSolicitud
                                                                            WHERE ID = @ID;", conn);

                    #endregion

                    #region Command Parameteres

                    cmd.Parameters.AddWithValue("@ID", docs.ElementAt(i).ID);

                    cmd.Parameters.AddWithValue("@NumeroSolicitud", docs.ElementAt(i).NumeroSolicitud);

                    cmd.Parameters.AddWithValue("@EmailUsuario", docs.ElementAt(i).EmailUser);

                    cmd.Parameters.AddWithValue("@Nombre", docs.ElementAt(i).Nombre);

                    cmd.Parameters.AddWithValue("@PathDocumento", docs.ElementAt(i).PathDoc);

                    cmd.Parameters.AddWithValue("@Status", docs.ElementAt(i).Status);

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
        /// Gets the doc names created from the docsReqConfig page
        /// </summary>
        /// <returns></returns>
        public static LinkedList<DocReq> GetDocsNames()
        {
            LinkedList<DocReq> documentos = new LinkedList<DocReq>();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT Nombre FROM TitulosDocumentos  
                                                         ORDER BY Nombre", conn))
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int col = 0;

                        documentos.AddLast(new DocReq()
                        {
                            Nombre = reader[col].ToString()
                        });
                    }
                }
            }

            return documentos;
        }

        public static LinkedList<DocReq> GetDocsByNumSolicitud(string numSolicitud)
        {
            LinkedList<DocReq> documentos = new LinkedList<DocReq>();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Documentos  
                                                         WHERE NumeroSolicitud = @NumeroSolicitud", conn))
                {
                    cmd.Parameters.AddWithValue("@NumeroSolicitud", numSolicitud);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int col = 0;

                        documentos.AddLast(new DocReq()
                        {
                            ID = reader.GetInt32(col++),

                            EmailUser = reader.GetString(col++),

                            PathDoc = reader.GetString(col++),

                            Nombre = reader.GetString(col++),

                            NumeroSolicitud = reader.GetString(col++),

                            Status = reader.GetInt32(col++)
                        });
                    }
                }
            }

            return documentos;
        }
    }
}