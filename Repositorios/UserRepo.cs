using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class UserRepo
    {
        public static Exception Create(Usuario user)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
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
                                                                            LastTimeActive)
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
                                                                            @LastTimeActive);", conn);

                #endregion

                #region Command Parameteres

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

        public static Exception Update(Usuario user, string previousEmail)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"UPDATE Usuarios SET Nombre = @Nombre,
                                                                             SeguroSocial = @SeguroSocial,
                                                                             ApellidoPaterno = @ApellidoPaterno,
                                                                            ApellidoMaterno = @ApellidoMaterno,
                                                                            Email = @Email,
                                                                            Password = @Password,
                                                                            Telefono = @Telefono,
                                                                            Direccion = @Direccion,
                                                                            Pueblo = @Pueblo,
                                                                            CodigoPostal = @CodigoPostal,
                                                                            DireccionPostal = @DireccionPostal,
                                                                            PuebloPostal = @PuebloPostal,
                                                                            CodigoPostalPostal = @CodigoPostalPostal,
                                                                            FechaNacimiento = @FechaNacimiento,
                                                                            LicenciaConducir = @LicenciaConducir,
                                                                            Celular = @Celular,
                                                                            RoleID = @RoleID,
                                                                            Activo = @Activo
                                                                            WHERE Email = @PreviousEmail;", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@RoleID", (int)user.Role.RoleType);

                cmd.Parameters.AddWithValue("@Activo", user.Activo);

                cmd.Parameters.AddWithValue("@Nombre", user.Nombre);

                cmd.Parameters.AddWithValue("@SeguroSocial", user.SeguroSocial);

                cmd.Parameters.AddWithValue("@ApellidoPaterno", user.ApellidoPaterno);

                cmd.Parameters.AddWithValue("@ApellidoMaterno", user.ApellidoMaterno);

                cmd.Parameters.AddWithValue("@Email", user.Email);

                cmd.Parameters.AddWithValue("@PreviousEmail", previousEmail);

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

        public static Exception UpdateUserLoggedLock(int id, bool shouldBeLoggedLocked)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"UPDATE Usuarios SET 
                                                                    IsLoggedIn = @IsLoggedIn,
                                                                    LastTimeActive = @LastTimeActive
                                                                            WHERE ID = @ID;", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@IsLoggedIn", shouldBeLoggedLocked);

                cmd.Parameters.AddWithValue("@LastTimeActive", DateTime.Now);

                cmd.Parameters.AddWithValue("@ID", id);

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

        public static Usuario GetUserByEmail(string email)
        {
            Usuario user = new Usuario();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Usuarios WHERE Email = @Email", conn);

                cmd.Parameters.AddWithValue("Email", email);

                conn.Open();

                cmd.ExecuteNonQuery();

                BuildUser(cmd, ref user);
            }

            return user;
        }

        public static int GetInternalUserCount()
        {
            int count = 0;

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) FROM Usuarios WHERE RoleID != 0", conn);

                conn.Open();

                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int col = 0;

                    count = reader.GetInt32(col);
                }
            }

            return count;
        }

        public static Usuario GetUserByID(int id)
        {
            Usuario user = new Usuario();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Usuarios WHERE ID = @ID", conn);

                cmd.Parameters.AddWithValue("ID", id);

                conn.Open();

                cmd.ExecuteNonQuery();

                BuildUser(cmd, ref user);
            }

            return user;
        }

        public static LinkedList<Usuario> GetUsersByRole(int roleId, bool orderByName)
        {
            LinkedList<Usuario> users = new LinkedList<Usuario>();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                SqlCommand cmd = new SqlCommand();

                if (orderByName)
                    cmd = new SqlCommand(@"SELECT * FROM Usuarios WHERE RoleID = @ID ORDER BY Nombre", conn);

                else
                    cmd = new SqlCommand(@"SELECT * FROM Usuarios WHERE RoleID = @ID", conn);

                cmd.Parameters.AddWithValue("ID", roleId);

                conn.Open();

                cmd.ExecuteNonQuery();

                BuildUseres(cmd, ref users);
            }

            return users;
        }

        public static LinkedList<Usuario> GetAllEmployeeUsers()
        {
            LinkedList<Usuario> users = new LinkedList<Usuario>();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Usuarios WHERE RoleID <> 0 ORDER BY Nombre", conn);

                conn.Open();

                cmd.ExecuteNonQuery();

                BuildUseres(cmd, ref users);
            }

            return users;
        }

        private static void BuildUser(SqlCommand command, ref Usuario user)
        {
            SqlDataReader reader = command.ExecuteReader();

            #region Read query

            while (reader.Read())
            {
                int col = 0;

                user = new Usuario()
                {
                    ID = reader.GetInt32(col++),

                    Password = reader.GetString(col++),

                    SeguroSocial = reader.GetString(col++),

                    Nombre = reader.GetString(col++),

                    ApellidoPaterno = reader.GetString(col++),

                    ApellidoMaterno = reader.GetString(col++),

                    LicenciaConducir = reader.GetString(col++),

                    Celular = reader.GetString(col++),

                    Tel = reader.GetString(col++),

                    Direccion = reader.GetString(col++),

                    Pueblo = reader.GetInt32(col++),

                    CodigoPostal = reader.GetString(col++),

                    DireccionPost = reader.GetString(col++),

                    PuebloPost = reader.GetInt32(col++),

                    CodigoPostalPost = reader.GetString(col++),

                    FechaNacimiento = reader.GetDateTime(col++),

                    Role = new Rol()
                };

                user.Role.RoleType = Rol.GetRoleType(reader.GetInt32(col++));

                user.Role = RoleRepo.GetRoleByType((int)user.Role.RoleType);

                user.Email = reader.GetString(col++);

                user.Activo = reader.GetBoolean(col++);

                user.IsLoggedIn = reader.GetBoolean(col++);

                user.LastTimeActive = reader.GetDateTime(col++);
            }
            #endregion
        }

        private static void BuildUseres(SqlCommand command, ref LinkedList<Usuario> users)
        {
            SqlDataReader reader = command.ExecuteReader();

            #region Read query

            while (reader.Read())
            {
                int col = 0;

                Usuario user = new Usuario()
                {
                    ID = reader.GetInt32(col++),

                    Password = reader.GetString(col++),

                    SeguroSocial = reader.GetString(col++),

                    Nombre = reader.GetString(col++),

                    ApellidoPaterno = reader.GetString(col++),

                    ApellidoMaterno = reader.GetString(col++),

                    LicenciaConducir = reader.GetString(col++),

                    Celular = reader.GetString(col++),

                    Tel = reader.GetString(col++),

                    Direccion = reader.GetString(col++),

                    Pueblo = reader.GetInt32(col++),

                    CodigoPostal = reader.GetString(col++),

                    DireccionPost = reader.GetString(col++),

                    PuebloPost = reader.GetInt32(col++),

                    CodigoPostalPost = reader.GetString(col++),

                    FechaNacimiento = reader.GetDateTime(col++),

                    Role = new Rol()
                };

                //Set type cause I only store an int
                user.Role.RoleType = Rol.GetRoleType(reader.GetInt32(col++));

                //Get whole role obj afterwards
                user.Role = RoleRepo.GetRoleByType((int)user.Role.RoleType);

                user.Email = reader.GetString(col++);

                user.Activo = reader.GetBoolean(col++);

                user.IsLoggedIn = reader.GetBoolean(col++);

                user.LastTimeActive = reader.GetDateTime(col++);

                users.AddLast(user);
            }
            #endregion
        }
    }
}