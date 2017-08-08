using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class SolicitudesLogRepo
    {
        public static Exception Create(SolicitudLog solicitud)
        {
            using (SqlConnection conn = DB.GetLogConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Solicitudes (Status,
                                                                             NumeroSolicitud,
                                                                            FechaTramitada,
                                                                             CoordinadorID,
                                                                            FechaRevision,
                                                                            ProcesadorID,
                                                                            FechaAsigProcesador,
                                                                            ComentarioProcesador,
                                                                            TrabajadorID,
                                                                            FechaTrabajado,
                                                                            ComentarioTrabajador,
                                                                            Nombre,
                                                                            SeguroSocial,
                                                                            Email,
                                                                            ApellidoPaterno,
                                                                            ApellidoMaterno,
                                                                            FechaNacimiento,
                                                                            Telefono,
                                                                            LicenciaConducir,
                                                                            Celular,
                                                                            Direccion,
                                                                            Pueblo,
                                                                            CodigoPostal,
                                                                            DireccionPostal,
                                                                            PuebloPostal,
                                                                            CodigoPostalPostal,
                                                                            NombreCo,
                                                                            SeguroSocialCo,
                                                                            EmailCo,
                                                                            ApellidoPaternoCo,
                                                                            ApellidoMaternoCo,
                                                                            FechaNacimientoCo,
                                                                            TelefonoCo,
                                                                            LicenciaConducirCo,
                                                                            CelularCo,
                                                                            DireccionCo,
                                                                            PuebloCo,
                                                                            CodigoPostalCo,
                                                                            DireccionPostalCo,
                                                                            PuebloPostalCo,
                                                                            CodigoPostalPostalCo,
                                                                            FechaDocIncompleto,
                                                                            LockedById,
                                                                            LockedAt,
                                                                            WhoUpdated,
                                                                            UpdateDate)
                                                                            VALUES(@Status, @NumeroSolicitud, @FechaTramitada, @CoordinadorID, @FechaRevision
                                                                            , @ProcesadorID, @FechaAsigProcesador, @ComentarioProcesador,
                                                                            @TrabajadorID, @FechaTrabajado, @ComentarioTrabajador,
                                                                            @Nombre, @SeguroSocial, @Email, @ApellidoPaterno,
                                                                            @ApellidoMaterno, @FechaNacimiento, @Telefono,
                                                                            @LicenciaConducir, @Celular, @Direccion,
                                                                            @Pueblo, @CodigoPostal, @DireccionPostal,
                                                                            @PuebloPostal, @CodigoPostalPostal,
                                                                            @NombreCo, @SeguroSocialCo, @EmailCo,
                                                                            @ApellidoPaternoCo, @ApellidoMaternoCo, @FechaNacimientoCo,
                                                                            @TelefonoCo, @LicenciaConducirCo, @CelularCo, @DireccionCo,
                                                                            @PuebloCo, @CodigoPostalCo, @DireccionPostalCo,
                                                                            @PuebloPostalCo, @CodigoPostalPostalCo, @FechaDocIncompleto,
                                                                            @LockedById, @LockedAt, @WhoUpdated, @UpdateDate);", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@UpdateDate", solicitud.UpdateDate);

                cmd.Parameters.AddWithValue("@WhoUpdated", solicitud.WhoUpdated);

                cmd.Parameters.AddWithValue("@LockedAt", solicitud.LockedAt);

                cmd.Parameters.AddWithValue("@LockedById", solicitud.LockedById);

                cmd.Parameters.AddWithValue("@Status", (int)solicitud.Status);

                cmd.Parameters.AddWithValue("@NumeroSolicitud", solicitud.NumeroSolicitud);

                cmd.Parameters.AddWithValue("@FechaTramitada", solicitud.FechaTramitada);

                cmd.Parameters.AddWithValue("@FechaDocIncompleto", solicitud.FechaDocIncompleto);

                cmd.Parameters.AddWithValue("@CoordinadorID", solicitud.CoordinadorID);

                cmd.Parameters.AddWithValue("@FechaRevision", solicitud.FechaRevision);

                cmd.Parameters.AddWithValue("@ProcesadorID", solicitud.ProcesadorId);

                cmd.Parameters.AddWithValue("@FechaAsigProcesador", solicitud.FechaAsigProcesador);

                cmd.Parameters.AddWithValue("@ComentarioProcesador", solicitud.ComentarioProcesador);

                cmd.Parameters.AddWithValue("@TrabajadorID", solicitud.TrabajadorId);

                cmd.Parameters.AddWithValue("@FechaTrabajado", solicitud.FechaTrabajado);

                cmd.Parameters.AddWithValue("@ComentarioTrabajador", solicitud.ComentarioTrabajo);

                cmd.Parameters.AddWithValue("@Nombre", solicitud.Nombre);

                cmd.Parameters.AddWithValue("@SeguroSocial", solicitud.SeguroSocial);

                cmd.Parameters.AddWithValue("@SeguroSocialCo", solicitud.SeguroSocialCo);

                cmd.Parameters.AddWithValue("@Email", solicitud.Email);

                cmd.Parameters.AddWithValue("@ApellidoPaterno", solicitud.ApellidoPaterno);

                cmd.Parameters.AddWithValue("@ApellidoMaterno", solicitud.ApellidoMaterno);

                cmd.Parameters.AddWithValue("@FechaNacimiento", solicitud.FechaNacimiento);

                cmd.Parameters.AddWithValue("@Telefono", solicitud.Telefono);

                cmd.Parameters.AddWithValue("@LicenciaConducir", solicitud.LicenciaConducir);

                cmd.Parameters.AddWithValue("@Celular", solicitud.Celular);

                cmd.Parameters.AddWithValue("@Direccion", solicitud.Dirrecion);

                cmd.Parameters.AddWithValue("@Pueblo", solicitud.Pueblo);

                cmd.Parameters.AddWithValue("@CodigoPostal", solicitud.CodigoPostal);

                cmd.Parameters.AddWithValue("@DireccionPostal", solicitud.DirrecionPostal);

                cmd.Parameters.AddWithValue("@PuebloPostal", solicitud.PuebloPostal);

                cmd.Parameters.AddWithValue("@CodigoPostalPostal", solicitud.CodigoPostalPostal);

                cmd.Parameters.AddWithValue("@NombreCo", solicitud.NombreCo);

                cmd.Parameters.AddWithValue("@EmailCo", solicitud.EmailCo);

                cmd.Parameters.AddWithValue("@ApellidoPaternoCo", solicitud.ApellidoPaternoCo);

                cmd.Parameters.AddWithValue("@ApellidoMaternoCo", solicitud.ApellidoMaternoCo);

                cmd.Parameters.AddWithValue("@FechaNacimientoCo", solicitud.FechaNacimientoCo);

                cmd.Parameters.AddWithValue("@TelefonoCo", solicitud.TelefonoCo);

                cmd.Parameters.AddWithValue("@LicenciaConducirCo", solicitud.LicenciaConducirCo);

                cmd.Parameters.AddWithValue("@CelularCo", solicitud.CelularCo);

                cmd.Parameters.AddWithValue("@DireccionCo", solicitud.DirrecionCo);

                cmd.Parameters.AddWithValue("@PuebloCo", solicitud.PuebloCo);

                cmd.Parameters.AddWithValue("@CodigoPostalCo", solicitud.CodigoPostalCo);

                cmd.Parameters.AddWithValue("@DireccionPostalCo", solicitud.DirrecionPostalCo);

                cmd.Parameters.AddWithValue("@PuebloPostalCo", solicitud.PuebloPostalCo);

                cmd.Parameters.AddWithValue("@CodigoPostalPostalCo", solicitud.CodigoPostalPostalCo);

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