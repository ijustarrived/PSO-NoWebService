using PSO.Entities;
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
                                                                            FechaDocIncompleto,
                                                                            LockedById)
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
                                                                            @LockedById);", conn);

                #endregion

                #region Command Parameteres

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
                                                                            FechaDocIncompleto = @FechaDocIncompleto,
                                                                            LockedById = @LockedById
                                                                            WHERE ID = @ID;", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@LockedById", solicitud.LockedById);

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

        public static Exception UpdateLockedId(_Solicitud solicitud)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"UPDATE Solicitudes SET LockedById = @LockedById
                                                                            WHERE ID = @ID;", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@LockedById", solicitud.LockedById);

                cmd.Parameters.AddWithValue("@ID", solicitud.ID);

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
        /// Release all locked solicitudes that belong to a specific user
        /// </summary>
        /// <param name="lockedID">User ID</param>
        /// <returns></returns>
        public static Exception ReleaseAllLockedSolicitudes(int lockedID)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"UPDATE Solicitudes SET LockedById = 0
                                                  WHERE LockedById = @LockedById;", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@LockedById", lockedID);

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

        public static Exception ReleaseALockedSolicitud(string numSolicitud)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"UPDATE Solicitudes SET LockedById = 0
                                                  WHERE NumeroSolicitud = @NumeroSolicitud;", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@NumeroSolicitud", numSolicitud);

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

        public static LinkedList<_Solicitud> GetSolicitudesCompletadasByRole(Rol.TiposRole role)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT *, DATEDIFF(day, @FECHAASIG, @FECHA) AS Duration 
                                                            FROM Solicitudes @WHERE ORDER BY @ROLE NumeroSolicitud",
                                                            conn))
                {
                    string where = string.Empty,
                        rol = string.Empty,
                        fechaAsig = string.Empty,
                        fechaCompletada = string.Empty;

                    switch (role)
                    {
                        case Rol.TiposRole.COORDINADOR:

                            where = "WHERE FechaRevision != Convert(datetime, '12/31/9999 23:59:59.997')";

                            rol = "CoordinadorID,";

                            fechaCompletada = "FechaRevision";

                            fechaAsig = "FechaTramitada";

                            break;

                        case Rol.TiposRole.PROCESADOR:

                            //Esto va pero para el demo no
                            where = "WHERE FechaTrabajado != Convert(datetime, '12/31/9999 23:59:59.997')";

                            rol = "ProcesadorID,";

                            fechaCompletada = "FechaTrabajado";

                            fechaAsig = "FechaAsigProcesador";

                            //Esto es solo para el demo
                            //where = "WHERE Status = 4 OR Status = 5";

                            break;

                        case Rol.TiposRole.SUPERVISOR:

                            where = "WHERE FechaAsigProcesador != Convert(datetime, '12/31/9999 23:59:59.997')";

                            fechaCompletada = "FechaAsigProcesador";

                            fechaAsig = "FechaRevision";

                            break;

                        case Rol.TiposRole.EXTERNO:

                            where = "WHERE FechaTramitada != Convert(datetime, '12/31/9999 23:59:59.997')";

                            fechaCompletada = "GETDATE()";

                            break;
                    }

                    cmd.CommandText = cmd.CommandText.Replace("@WHERE", where).Replace("@FECHAASIG", fechaAsig).Replace(
                        "@FECHA", fechaCompletada).Replace("@ROLE", rol);

                    //cmd.CommandText = cmd.CommandText.Replace("@FECHA", fecha);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    #region Read query

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int col = 0;

                        solicitudes.AddLast(new _Solicitud()
                        {
                            ID = reader.GetInt32(col++),

                            Status = _Solicitud.GetStatus(reader.GetInt32(col++)),

                            NumeroSolicitud = reader.GetString(col++),

                            FechaTramitada = reader.GetDateTime(col++),

                            CoordinadorID = reader.GetString(col++),

                            FechaRevision = reader.GetDateTime(col++),

                            ProcesadorId = reader.GetString(col++),

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

                            FechaDocIncompleto = reader.GetDateTime(col++),

                            LockedById = reader.GetInt32(col++),

                            Duration = reader.GetInt32(col++).ToString()
                        });
                    }
                    #endregion
                }
            }

            return solicitudes;
        }

        public static LinkedList<_Solicitud> GetSolicitudesByRoleNDateRange(Rol.TiposRole role, DateTime desde, DateTime hasta)
        {
            hasta = hasta.AddHours(23);

            hasta = hasta.AddMinutes(59);

            hasta = hasta.AddSeconds(59);

            hasta = hasta.AddMilliseconds(997);

            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT *, DATEDIFF(day, @FECHAINICIAL, @date) AS Duration 
                                            FROM Solicitudes WHERE @FECHA >= @DESDE AND @FECHA < @HASTA @AND
                                            ORDER BY @ROLE NumeroSolicitud", conn))
                {
                    cmd.Parameters.AddWithValue("DESDE", desde);

                    cmd.Parameters.AddWithValue("HASTA", hasta);

                    string and = string.Empty,
                        fechaInicial = string.Empty,
                        fechaCompletada = string.Empty,
                         rol = string.Empty,
                        fechaCompletada2 = string.Empty;

                    switch (role)
                    {
                        case Rol.TiposRole.COORDINADOR:

                            fechaInicial = "FechaTramitada";

                            fechaCompletada = "FechaRevision";

                            rol = "CoordinadorID,";

                            fechaCompletada2 = fechaCompletada;

                            break;

                        case Rol.TiposRole.PROCESADOR:

                            fechaInicial = "FechaAsigProcesador";

                            fechaCompletada = "FechaTrabajado";

                            rol = "ProcesadorID,";

                            fechaCompletada2 = fechaCompletada;

                            break;

                        case Rol.TiposRole.SUPERVISOR:

                            fechaInicial = "FechaRevision";

                            fechaCompletada = "FechaAsigProcesador";

                            fechaCompletada2 = fechaCompletada;

                            break;

                        case Rol.TiposRole.EXTERNO:

                            fechaInicial = "FechaTramitada";

                            fechaCompletada = "FechaTramitada";

                            fechaCompletada2 = "GETDATE()";

                            break;
                    }

                    cmd.CommandText = cmd.CommandText.Replace("@AND", and).Replace("@FECHAINICIAL", fechaInicial).Replace(
                        "@FECHA", fechaCompletada).Replace("@ROLE", rol).Replace("@date", fechaCompletada2);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    #region Read query

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int col = 0;

                        solicitudes.AddLast(new _Solicitud()
                        {
                            ID = reader.GetInt32(col++),

                            Status = _Solicitud.GetStatus(reader.GetInt32(col++)),

                            NumeroSolicitud = reader.GetString(col++),

                            FechaTramitada = reader.GetDateTime(col++),

                            CoordinadorID = reader.GetString(col++),

                            FechaRevision = reader.GetDateTime(col++),

                            ProcesadorId = reader.GetString(col++),

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

                            FechaDocIncompleto = reader.GetDateTime(col++),

                            LockedById = reader.GetInt32(col++),

                            Duration = reader.GetInt32(col++).ToString()
                        });
                    }
                    #endregion
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
            hasta = hasta.AddHours(23);

            hasta = hasta.AddMinutes(59);

            hasta = hasta.AddSeconds(59);

            hasta = hasta.AddMilliseconds(997);

            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes WHERE FechaTramitada 
                                            >= @DESDE AND FechaTramitada < @HASTA", conn))
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

        public static LinkedList<_Solicitud> GetSolicitudesByYear(int year)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes WHERE YEAR(FechaTramitada)
                                            = @YEAR", conn))
                {
                    cmd.Parameters.AddWithValue("YEAR", year);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    solicitudes = BuildSolicitudes(cmd);
                }
            }

            return solicitudes;
        }

        public static LinkedList<_Solicitud> GetSolicitudesCompletadasByYearNRol(int year, Rol.TiposRole role)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes WHERE YEAR(@DATE)
                                            = @YEAR", conn))
                {
                    cmd.Parameters.AddWithValue("YEAR", year);

                    string date = string.Empty;

                    switch (role)
                    {
                        case Rol.TiposRole.COORDINADOR:

                            date = "FechaRevision";

                            break;

                        case Rol.TiposRole.PROCESADOR:

                            //Esto va pero para el demo no
                            date = "FechaTrabajado";

                            //Esto es solo para el demo
                            //where = "WHERE Status = 4 OR Status = 5";

                            break;

                        case Rol.TiposRole.SUPERVISOR:

                            date = "FechaAsigProcesador";

                            break;

                        case Rol.TiposRole.EXTERNO:

                            date = "FechaTramitada";

                            break;
                    }

                    cmd.CommandText = cmd.CommandText.Replace("@DATE", date);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    solicitudes = BuildSolicitudes(cmd);
                }
            }

            return solicitudes;
        }

        public static LinkedList<_Solicitud> GetSolicitudesCompletadasByMonthYearNRol(int year, int month, Rol.TiposRole role)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            //using (SqlConnection conn = sql.GetConnection())
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * FROM Solicitudes WHERE YEAR(@DATE)
                                            = @YEAR AND MONTH(@DATE) = @MONTH", conn))
                {
                    cmd.Parameters.AddWithValue("YEAR", year);

                    cmd.Parameters.AddWithValue("MONTH", month);

                    string date = string.Empty;

                    switch (role)
                    {
                        case Rol.TiposRole.COORDINADOR:

                            date = "FechaRevision";

                            break;

                        case Rol.TiposRole.PROCESADOR:

                            //Esto va pero para el demo no
                            date = "FechaTrabajado";

                            //Esto es solo para el demo
                            //where = "WHERE Status = 4 OR Status = 5";

                            break;

                        case Rol.TiposRole.SUPERVISOR:

                            date = "FechaAsigProcesador";

                            break;

                        case Rol.TiposRole.EXTERNO:

                            date = "FechaTramitada";

                            break;
                    }

                    cmd.CommandText = cmd.CommandText.Replace("@DATE", date);

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
                                            >= @DESDE AND FechaTramitada < @HASTA AND Status = @Status", conn))
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

                    CoordinadorID = reader.GetString(col++),

                    FechaRevision = reader.GetDateTime(col++),

                    ProcesadorId = reader.GetString(col++),

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

                    FechaDocIncompleto = reader.GetDateTime(col++),

                    LockedById = reader.GetInt32(col++)
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

                    CoordinadorID = reader.GetString(col++),

                    FechaRevision = reader.GetDateTime(col++),

                    ProcesadorId = reader.GetString(col++),

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

                    FechaDocIncompleto = reader.GetDateTime(col++),

                    LockedById = reader.GetInt32(col++)
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