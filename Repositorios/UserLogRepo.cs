using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class UserLogRepo
    {
        public static Exception Create(UsuarioLog user)
        {
            using (SqlConnection conn = DB.GetLogConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Usuarios (Nombre,
                                                                             SeguroSocial,
                                                                             ApellidoPaterno,
                                                                            ApellidoMaterno,
                                                                            Email,
                                                                            Password,
                                                                            Telefono,
                                                                            Direccion,
                                                                            Pueblo,
                                                                            CodigoPostal,
                                                                            DireccionPostal,
                                                                            PuebloPostal,
                                                                            CodigoPostalPostal,
                                                                            FechaNacimiento,
                                                                            LicenciaConducir,
                                                                            Celular,
                                                                            RoleID,
                                                                            Activo,
                                                                            IsLoggedIn,
                                                                            LastTimeActive,
                                                                            UpdateDate,
                                                                            WhoUpdated,
                                                                            LogInDate,
                                                                            LogOutDate)
                                                                            VALUES(@Nombre,
                                                                             @SeguroSocial,
                                                                             @ApellidoPaterno,
                                                                            @ApellidoMaterno,
                                                                            @Email,
                                                                            @Password,
                                                                            @Telefono,
                                                                            @Direccion,
                                                                            @Pueblo,
                                                                            @CodigoPostal,
                                                                            @DireccionPostal,
                                                                            @PuebloPostal,
                                                                            @CodigoPostalPostal,
                                                                            @FechaNacimiento,
                                                                            @LicenciaConducir,
                                                                            @Celular,
                                                                            @RoleID,
                                                                            @Activo,
                                                                            @IsLoggedIn,
                                                                            @LastTimeActive,
                                                                            @UpdateDate,
                                                                            @WhoUpdated,
                                                                            @LogInDate,
                                                                            @LogOutDate);", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@WhoUpdated", user.WhoUpdated);

                cmd.Parameters.AddWithValue("@UpdateDate", user.UpdateDate);

                cmd.Parameters.AddWithValue("@LogInDate", user.LogInDate);

                cmd.Parameters.AddWithValue("@LogOutDate", user.LogOutDate);

                cmd.Parameters.AddWithValue("@RoleID", (int)user.Role.RoleType);

                cmd.Parameters.AddWithValue("@LastTimeActive", user.LastTimeActive);

                cmd.Parameters.AddWithValue("@IsLoggedIn", user.IsLoggedIn);

                cmd.Parameters.AddWithValue("@Activo", user.Activo);

                cmd.Parameters.AddWithValue("@Nombre", user.Nombre);

                cmd.Parameters.AddWithValue("@SeguroSocial", user.SeguroSocial);

                cmd.Parameters.AddWithValue("@ApellidoPaterno", user.ApellidoPaterno);

                cmd.Parameters.AddWithValue("@ApellidoMaterno", user.ApellidoMaterno);

                cmd.Parameters.AddWithValue("@Email", user.Email);

                cmd.Parameters.AddWithValue("@Telefono", user.Tel);

                cmd.Parameters.AddWithValue("@Password", user.Password);

                cmd.Parameters.AddWithValue("@Direccion", user.Direccion);

                cmd.Parameters.AddWithValue("@Pueblo", user.Pueblo);

                cmd.Parameters.AddWithValue("@CodigoPostal", user.CodigoPostal);

                cmd.Parameters.AddWithValue("@DireccionPostal", user.DireccionPost);

                cmd.Parameters.AddWithValue("@PuebloPostal", user.PuebloPost);

                cmd.Parameters.AddWithValue("@CodigoPostalPostal", user.CodigoPostalPost);

                cmd.Parameters.AddWithValue("@FechaNacimiento", user.FechaNacimiento);

                cmd.Parameters.AddWithValue("@LicenciaConducir", user.LicenciaConducir);

                cmd.Parameters.AddWithValue("@Celular", user.Celular);

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