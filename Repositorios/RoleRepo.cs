using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class RoleRepo
    {
        public static Exception Update(Rol role)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"UPDATE Roles SET 
                                                                    ViewConfigDocReq = @ViewConfigDocReq,
                                                                    ViewConfigRole = @ViewConfigRole,
                                                                    ViewConfigUser = @ViewConfigUser,
                                                                    ViewConsuCoor = @ViewConsuCoor,
                                                                    ViewConsuProc = @ViewConsuProc,
                                                                    ViewConsuPendAsig = @ViewConsuPendAsig,
                                                                    ViewRepAvisosStatus = @ViewRepAvisosStatus,
                                                                    ViewRepRecVsPen = @ViewRepRecVsPen,
                                                                    ViewReportIndicadores = @ViewReportIndicadores,
                                                                    ViewConsuSolicitud = @ViewConsuSolicitud,
                                                                    EditDocReq = @EditDocReq,
                                                                    EditRoles = @EditRoles,
                                                                    EditUsers = @EditUsers,
                                                                    ViewSolicitud = @ViewSolicitud,
                                                                    ViewRepProduction = @ViewRepProduction,
                                                                    EditCustomizationPage = @EditCustomizationPage
                                                                    WHERE RoleType = @RoleType;", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@RoleType", (int)role.RoleType);

                cmd.Parameters.AddWithValue("@ViewReportIndicadores", role.ViewReportIndicadores);

                cmd.Parameters.AddWithValue("@ViewRepProduction", role.ViewRepProduc);

                cmd.Parameters.AddWithValue("@ViewSolicitud", role.ViewSolicitud);

                cmd.Parameters.AddWithValue("@ViewConfigDocReq", role.ViewConfigDocReq);

                cmd.Parameters.AddWithValue("@ViewConfigRole", role.ViewConfigRole);

                cmd.Parameters.AddWithValue("@ViewConfigUser", role.ViewConfigUser);

                cmd.Parameters.AddWithValue("@ViewConsuCoor", role.ViewConsuCoor);

                cmd.Parameters.AddWithValue("@ViewConsuProc", role.ViewConsuProc);

                cmd.Parameters.AddWithValue("@ViewConsuPendAsig", role.ViewConsuPendAsig);

                cmd.Parameters.AddWithValue("@ViewRepAvisosStatus", role.ViewRepAvisosStatus);

                cmd.Parameters.AddWithValue("@ViewRepRecVsPen", role.ViewRepRecVsPen);

                cmd.Parameters.AddWithValue("@ViewConsuSolicitud", role.ViewConsuSolicitud);

                cmd.Parameters.AddWithValue("@EditDocReq", role.EditDocReq);

                cmd.Parameters.AddWithValue("@EditRoles", role.EditRoles);

                cmd.Parameters.AddWithValue("@EditUsers", role.EditUsers);

                cmd.Parameters.AddWithValue("@EditCustomizationPage", role.EditCustomizationPage);

                conn.Open();

                #endregion

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    cmd.Transaction = transaction;

                    try
                    {
                        cmd.ExecuteNonQuery();

                        transaction.Commit();

                        return null;
                    }

                    catch (SqlException ex)
                    {
                        transaction.Rollback();

                        return ex;
                    }
                }
            }
        }

        public static Rol GetRoleByType(int roleId)
        {
            Rol role = new Rol();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 * FROM Roles WHERE RoleType = @RoleType", conn);

                cmd.Parameters.AddWithValue("RoleType", roleId);

                conn.Open();

                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int col = 0;

                    role = new Rol()
                    {
                        ID = reader.GetInt32(col++),

                        RoleType = Rol.GetRoleType(reader.GetInt32(col++)),

                        ViewConfigDocReq = reader.GetBoolean(col++),

                        ViewConfigRole = reader.GetBoolean(col++),

                        ViewConfigUser = reader.GetBoolean(col++),

                        ViewConsuCoor = reader.GetBoolean(col++),

                        ViewConsuProc = reader.GetBoolean(col++),

                        ViewConsuPendAsig = reader.GetBoolean(col++),

                        ViewRepAvisosStatus = reader.GetBoolean(col++),

                        ViewRepRecVsPen = reader.GetBoolean(col++),

                        ViewConsuSolicitud = reader.GetBoolean(col++),

                        EditDocReq = reader.GetBoolean(col++),

                        EditRoles = reader.GetBoolean(col++),

                        EditUsers = reader.GetBoolean(col++),

                        ViewSolicitud = reader.GetBoolean(col++),

                        ViewRepProduc = reader.GetBoolean(col++),

                        EditCustomizationPage = reader.GetBoolean(col++),

                        ViewReportIndicadores = reader.GetBoolean(col++)
                    };

                    role.Nombre = role.RoleType.ToString();
                }

                return role;
            }
        }
    }
}