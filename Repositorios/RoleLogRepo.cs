using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class RoleLogRepo
    {
        public static Exception Create(RolLog role)
        {
            using (SqlConnection conn = DB.GetLogConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Roles (ViewConfigDocReq,
                                                                    ViewConfigRole,
                                                                    ViewConfigUser,
                                                                    ViewConsuCoor,
                                                                    ViewConsuProc,
                                                                    ViewConsuPendAsig,
                                                                    ViewRepAvisosStatus,
                                                                    ViewRepRecVsPen,
                                                                    ViewReportIndicadores,
                                                                    ViewConsuSolicitud,
                                                                    EditDocReq,
                                                                    EditRoles,
                                                                    EditUsers,
                                                                    ViewSolicitud,
                                                                    ViewRepProduction ,
                                                                    EditCustomizationPage,
                                                                    UpdateDate,
                                                                    WhoUpdated,
                                                                    RoleType)  
                                                                    VALUES 
                                                                    (@ViewConfigDocReq,
                                                                    @ViewConfigRole,
                                                                    @ViewConfigUser,
                                                                    @ViewConsuCoor,
                                                                    @ViewConsuProc,
                                                                    @ViewConsuPendAsig,
                                                                    @ViewRepAvisosStatus,
                                                                    @ViewRepRecVsPen,
                                                                    @ViewReportIndicadores,
                                                                    @ViewConsuSolicitud,
                                                                    @EditDocReq,
                                                                    @EditRoles,
                                                                    @EditUsers,
                                                                    @ViewSolicitud,
                                                                    @ViewRepProduction ,
                                                                    @EditCustomizationPage,
                                                                    @UpdateDate,
                                                                    @WhoUpdated,
                                                                    @RoleType);", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@WhoUpdated", role.WhoUpdated);

                cmd.Parameters.AddWithValue("@UpdateDate", role.UpdateDate);

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
    }
}