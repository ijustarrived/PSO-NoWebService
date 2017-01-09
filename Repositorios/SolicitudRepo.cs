﻿using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class SolicitudRepo
    {
        public static Exception Create(_Solicitud solicitud)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
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
                                                                            FechaDocIncompleto)
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
                                                                            @PuebloPostalCo, @CodigoPostalPostalCo, @FechaDocIncompleto);", conn);

                #endregion

                #region Command Parameteres

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

        public static Exception Update(_Solicitud solicitud)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"UPDATE Solicitudes SET  Status = @Status ,
                                                                             NumeroSolicitud = @NumeroSolicitud,
                                                                            FechaTramitada = @FechaTramitada,
                                                                             CoordinadorID = @CoordinadorID,
                                                                            FechaRevision = @FechaRevision,
                                                                            ProcesadorID = @ProcesadorID,
                                                                            FechaAsigProcesador = @FechaAsigProcesador,
                                                                            ComentarioProcesador = @ComentarioProcesador,
                                                                            TrabajadorID = @TrabajadorID,
                                                                            FechaTrabajado = @FechaTrabajado,
                                                                            ComentarioTrabajador = @ComentarioTrabajador,
                                                                            Nombre = @Nombre,
                                                                            SeguroSocial = @SeguroSocial,
                                                                            Email = @Email,
                                                                            ApellidoPaterno = @ApellidoPaterno,
                                                                            ApellidoMaterno = @ApellidoMaterno,
                                                                            FechaNacimiento = @FechaNacimiento,
                                                                            Telefono = @Telefono,
                                                                            LicenciaConducir = @LicenciaConducir,
                                                                            Celular = @Celular,
                                                                            Direccion = @Direccion,
                                                                            Pueblo = @Pueblo,
                                                                            CodigoPostal = @CodigoPostal,
                                                                            DireccionPostal = @DireccionPostal,
                                                                            PuebloPostal = @PuebloPostal,
                                                                            CodigoPostalPostal = @CodigoPostalPostal,
                                                                            NombreCo = @NombreCo,
                                                                            SeguroSocialCo = @SeguroSocialCo,
                                                                            EmailCo = @EmailCo,
                                                                            ApellidoPaternoCo = @ApellidoPaternoCo,
                                                                            ApellidoMaternoCo = @ApellidoMaternoCo,
                                                                            FechaNacimientoCo = @FechaNacimientoCo,
                                                                            TelefonoCo = @TelefonoCo,
                                                                            LicenciaConducirCo = @LicenciaConducirCo,
                                                                            CelularCo = @CelularCo,
                                                                            DireccionCo = @DireccionCo,
                                                                            PuebloCo = @PuebloCo,
                                                                            CodigoPostalCo = @CodigoPostalCo,
                                                                            DireccionPostalCo = @DireccionPostalCo,
                                                                            PuebloPostalCo = @PuebloPostalCo,
                                                                            CodigoPostalPostalCo = @CodigoPostalPostalCo,
                                                                            FechaDocIncompleto = @FechaDocIncompleto
                                                                            WHERE ID = @ID;", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@ID", solicitud.ID);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numSolicitud">Pass ID not num solicitud</param>
        /// <returns></returns>
        public static _Solicitud GetSolicitudByNumSolicitud(string numSolicitud)
        {
            _Solicitud solicitud = new _Solicitud();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes WHERE NumeroSolicitud = @NumeroSolicitud", conn))
                {
                    cmd.Parameters.AddWithValue("NumeroSolicitud", numSolicitud);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    solicitud = BuildSolicitud(cmd);
                }
            }

            return solicitud;
        }

        public static LinkedList<_Solicitud> GetSolicitudesByStatus(int statusId)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes WHERE Status = @Status", conn))
                {
                    cmd.Parameters.AddWithValue("Status", statusId);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    solicitudes = BuildSolicitudes(cmd);
                }
            }

            return solicitudes;
        }

        public static LinkedList<_Solicitud> GetSolicitudes()
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes", conn))
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();

                    solicitudes = BuildSolicitudes(cmd);
                }
            }

            return solicitudes;
        }

        public static LinkedList<_Solicitud> GetSolicitudesByRole(Rol.TiposRole role)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();
            
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes @WHERE", conn))
                {
                    string where = string.Empty;

                    switch(role)
                    {
                        case Rol.TiposRole.COORDINADOR:

                            where = "WHERE FechaRevision != Convert(datetime, '12/31/9999 23:59:59.997')";

                            break;

                        case Rol.TiposRole.PROCESADOR:

                            //Esto va pero para el demo no
                            //where = "WHERE FechaTrabajado != Convert(datetime, '12/31/9999 23:59:59.997')";

                            //Esto es solo para el demo
                            where = "WHERE Status = 4 OR Status = 5";

                            break;

                        case Rol.TiposRole.SUPERVISOR:

                            where = "WHERE FechaAsigProcesador != Convert(datetime, '12/31/9999 23:59:59.997')";

                            break;

                        case Rol.TiposRole.EXTERNO:

                            where = "WHERE FechaTramitada != Convert(datetime, '12/31/9999 23:59:59.997')";

                            break;
                    }

                    cmd.CommandText = cmd.CommandText.Replace("@WHERE", where);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    solicitudes = BuildSolicitudes(cmd);
                }
            }

            return solicitudes;
        }

        public static LinkedList<_Solicitud> GetSolicitudesByRoleNDateRange(Rol.TiposRole role, DateTime desde, DateTime hasta)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes WHERE FechaTramitada 
                                            BETWEEN @DESDE AND @HASTA @AND", conn))
                {
                    cmd.Parameters.AddWithValue("DESDE", desde);

                    cmd.Parameters.AddWithValue("HASTA", hasta);

                    string and = string.Empty;

                    switch (role)
                    {
                        case Rol.TiposRole.COORDINADOR:

                            and = "AND FechaRevision != Convert(datetime, '12/31/9999 23:59:59.997')";

                            break;

                        case Rol.TiposRole.PROCESADOR:

                            and = "AND FechaTrabajado != Convert(datetime, '12/31/9999 23:59:59.997')";

                            break;

                        case Rol.TiposRole.SUPERVISOR:

                            and = "AND FechaAsigProcesador != Convert(datetime, '12/31/9999 23:59:59.997')";

                            break;

                        case Rol.TiposRole.EXTERNO:

                            and = "AND FechaTramitada != Convert(datetime, '12/31/9999 23:59:59.997')";

                            break;
                    }

                    cmd.CommandText = cmd.CommandText.Replace("@AND", and);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    solicitudes = BuildSolicitudes(cmd);
                }
            }

            return solicitudes;
        }

        /// <summary>
        /// Only returns the ones where status = pend revisar.
        /// </summary>
        /// <returns>Solicitudes with ID as the only variable initiated</returns>
        public static LinkedList<_Solicitud> GetSolicitudesByDateRange(DateTime desde, DateTime hasta)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes WHERE FechaTramitada 
                                            BETWEEN @DESDE AND @HASTA", conn))
                {
                    cmd.Parameters.AddWithValue("DESDE", desde);

                    cmd.Parameters.AddWithValue("HASTA", hasta);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    solicitudes = BuildSolicitudes(cmd);
                }
            }

            return solicitudes;
        }

        public static LinkedList<_Solicitud> GetSolicitudesByDateRangeNStatus(DateTime desde, DateTime hasta, int statusId)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes WHERE FechaTramitada 
                                            BETWEEN @DESDE AND @HASTA AND Status = @Status", conn))
                {
                    cmd.Parameters.AddWithValue("DESDE", desde);

                    cmd.Parameters.AddWithValue("HASTA", hasta);

                    cmd.Parameters.AddWithValue("Status", statusId);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    solicitudes = BuildSolicitudes(cmd);
                }
            }

            return solicitudes;
        }

        public static _Solicitud BuildSolicitud(SqlCommand command)
        {
            _Solicitud solicitud = new _Solicitud();

            SqlDataReader reader = command.ExecuteReader();

            #region Read query

            while (reader.Read())
            {
                int col = 0;

                solicitud = new _Solicitud()
                {
                    ID = reader.GetInt32(col++),

                    Status = _Solicitud.GetStatus(reader.GetInt32(col++)),

                    NumeroSolicitud = reader.GetString(col++),

                    FechaTramitada = reader.GetDateTime(col++),

                    CoordinadorID = reader.GetInt32(col++),

                    FechaRevision = reader.GetDateTime(col++),

                    ProcesadorId = reader.GetInt32(col++),

                    FechaAsigProcesador = reader.GetDateTime(col++),

                    ComentarioProcesador = reader.GetString(col++),

                    TrabajadorId = reader.GetInt32(col++),

                    FechaTrabajado = reader.GetDateTime(col++),

                    ComentarioTrabajo = reader.GetString(col++),

                    Nombre = reader.GetString(col++),

                    SeguroSocial = reader.GetString(col++),

                    Email = reader.GetString(col++),

                    ApellidoPaterno = reader.GetString(col++),

                    ApellidoMaterno = reader.GetString(col++),

                    FechaNacimiento = reader.GetDateTime(col++),

                    Telefono = reader.GetString(col++),

                    LicenciaConducir = reader.GetString(col++),

                    Celular = reader.GetString(col++),

                    Dirrecion = reader.GetString(col++),

                    Pueblo = Convert.ToInt32(reader.GetString(col++)),

                    CodigoPostal = reader.GetString(col++),

                    DirrecionPostal = reader.GetString(col++),

                    PuebloPostal = Convert.ToInt32(reader.GetString(col++)),

                    CodigoPostalPostal = reader.GetString(col++),

                    NombreCo = reader.GetString(col++),

                    SeguroSocialCo = reader.GetString(col++),

                    EmailCo = reader.GetString(col++),

                    ApellidoPaternoCo = reader.GetString(col++),

                    ApellidoMaternoCo = reader.GetString(col++),

                    FechaNacimientoCo = reader.GetDateTime(col++),

                    TelefonoCo = reader.GetString(col++),

                    LicenciaConducirCo = reader.GetString(col++),

                    CelularCo = reader.GetString(col++),

                    DirrecionCo = reader.GetString(col++),

                    PuebloCo = Convert.ToInt32(reader.GetString(col++)),

                    CodigoPostalCo = reader.GetString(col++),

                    DirrecionPostalCo = reader.GetString(col++),

                    PuebloPostalCo = Convert.ToInt32(reader.GetString(col++)),

                    CodigoPostalPostalCo = reader.GetString(col++),

                    FechaDocIncompleto = reader.GetDateTime(col++)
                };
            }
            #endregion

            return solicitud;
        }

        public static LinkedList<_Solicitud> BuildSolicitudes(SqlCommand command)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            SqlDataReader reader = command.ExecuteReader();

            #region Read query

            while (reader.Read())
            {
                int col = 0;

                solicitudes.AddLast(new _Solicitud()
                {
                    ID = reader.GetInt32(col++),

                    Status = _Solicitud.GetStatus(reader.GetInt32(col++)),

                    NumeroSolicitud = reader.GetString(col++),

                    FechaTramitada = reader.GetDateTime(col++),

                    CoordinadorID = reader.GetInt32(col++),

                    FechaRevision = reader.GetDateTime(col++),

                    ProcesadorId = reader.GetInt32(col++),

                    FechaAsigProcesador = reader.GetDateTime(col++),

                    ComentarioProcesador = reader.GetString(col++),

                    TrabajadorId = reader.GetInt32(col++),

                    FechaTrabajado = reader.GetDateTime(col++),

                    ComentarioTrabajo = reader.GetString(col++),

                    Nombre = reader.GetString(col++),

                    SeguroSocial = reader.GetString(col++),

                    Email = reader.GetString(col++),

                    ApellidoPaterno = reader.GetString(col++),

                    ApellidoMaterno = reader.GetString(col++),

                    FechaNacimiento = reader.GetDateTime(col++),

                    Telefono = reader.GetString(col++),

                    LicenciaConducir = reader.GetString(col++),

                    Celular = reader.GetString(col++),

                    Dirrecion = reader.GetString(col++),

                    Pueblo = Convert.ToInt32(reader.GetString(col++)),

                    CodigoPostal = reader.GetString(col++),

                    DirrecionPostal = reader.GetString(col++),

                    PuebloPostal = Convert.ToInt32(reader.GetString(col++)),

                    CodigoPostalPostal = reader.GetString(col++),

                    NombreCo = reader.GetString(col++),

                    SeguroSocialCo = reader.GetString(col++),

                    EmailCo = reader.GetString(col++),

                    ApellidoPaternoCo = reader.GetString(col++),

                    ApellidoMaternoCo = reader.GetString(col++),

                    FechaNacimientoCo = reader.GetDateTime(col++),

                    TelefonoCo = reader.GetString(col++),

                    LicenciaConducirCo = reader.GetString(col++),

                    CelularCo = reader.GetString(col++),

                    DirrecionCo = reader.GetString(col++),

                    PuebloCo = Convert.ToInt32(reader.GetString(col++)),

                    CodigoPostalCo = reader.GetString(col++),

                    DirrecionPostalCo = reader.GetString(col++),

                    PuebloPostalCo = Convert.ToInt32(reader.GetString(col++)),

                    CodigoPostalPostalCo = reader.GetString(col++),

                    FechaDocIncompleto = reader.GetDateTime(col++)
                });
            }
            #endregion

            return solicitudes;
        }

        public static string GetLastNumSolicitudByEmail(string email)
        {
            string numSolicitud = string.Empty;

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 ID FROM Solicitudes WHERE Email = @Email ORDER BY ID DESC", conn);

                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();

                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                #region Read query

                while (reader.Read())
                {
                    int col = 0;

                    numSolicitud = reader.GetInt32(col++).ToString();                    
                }
                #endregion
            }

            return numSolicitud;
        }
    }
}