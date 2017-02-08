using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class CosmeticsRepo
    {
        public static Exception Update(Cosmetic cosmetic)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"UPDATE Cosmetics SET 
                                                                    LogoPath = @LogoPath,
                                                                    TitleBackColor = @TitleBackColor,
                                                                    LabelForeColor = @LabelForeColor,
                                                                    LinkForeColor = @LinkForeColor,
                                                                    ColorVersion = @ColorVersion,
                                                                    ConsultaCoorTitle = @ConsultaCoorTitle,
                                                                    ConsultaProcTitle = @ConsultaProcTitle,
                                                                    ConsultaSolicitudTitle = @ConsultaSolicitudTitle,
                                                                    ConsultaSuperTitle = @ConsultaSuperTitle,
                                                                    ReportComparacionTitle = @ReportComparacionTitle,
                                                                    ReportProduccionTitle = @ReportProduccionTitle,
                                                                    SolicitudTitle = @SolicitudTitle,
                                                                    CompletadasMesDiasTitle = @CompletadasMesDiasTitle,
                                                                    HistoryCompletadasTitle = @HistoryCompletadasTitle,
                                                                    HistoryRecibidasTitle = @HistoryRecibidasTitle,
                                                                    IndicadoresProductividadTitle 
                                                                    = @IndicadoresProductividadTitle,
                                                                    SolicitudesStatusTitle = @SolicitudesStatusTitle
                                                                    WHERE ID = 1;", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@SolicitudesStatusTitle", cosmetic.SolicitudesStatusTitle);

                cmd.Parameters.AddWithValue("@ConsultaCoorTitle", cosmetic.ConsultaCoorTitle);

                cmd.Parameters.AddWithValue("@ConsultaProcTitle", cosmetic.ConsultaProcTitle);

                cmd.Parameters.AddWithValue("@ConsultaSolicitudTitle", cosmetic.ConsultaSolicitudTitle);

                cmd.Parameters.AddWithValue("@ConsultaSuperTitle", cosmetic.ConsultaSuperTitle);

                cmd.Parameters.AddWithValue("@ReportComparacionTitle", cosmetic.ReportComparacionTitle);

                cmd.Parameters.AddWithValue("@ReportProduccionTitle", cosmetic.ReportProduccionTitle);

                cmd.Parameters.AddWithValue("@SolicitudTitle", cosmetic.SolicitudTitle);

                cmd.Parameters.AddWithValue("@CompletadasMesDiasTitle", cosmetic.CompletadasMesDiasTitle);

                cmd.Parameters.AddWithValue("@HistoryCompletadasTitle", cosmetic.HistoryCompletadasTitle);

                cmd.Parameters.AddWithValue("@HistoryRecibidasTitle", cosmetic.HistoryRecibidasTitle);

                cmd.Parameters.AddWithValue("@IndicadoresProductividadTitle", cosmetic.IndicadoresProductividadTitle);

                cmd.Parameters.AddWithValue("@LogoPath", cosmetic.LogoPath);

                cmd.Parameters.AddWithValue("@TitleBackColor", cosmetic.TitleBackColor);

                cmd.Parameters.AddWithValue("@LabelForeColor", cosmetic.LabelForeColor);

                cmd.Parameters.AddWithValue("@LinkForeColor", cosmetic.LinkForeColor);

                cmd.Parameters.AddWithValue("@ColorVersion", (int)cosmetic.ColorVersion);

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

        public static Cosmetic GetPageCosmetics()
        {
            Cosmetic cosmetic = new Cosmetic();

            using (SqlConnection conn = DB.GetLocalConnection())
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Cosmetics", conn);

                conn.Open();

                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int col = 0;

                    cosmetic = new Cosmetic()
                    {
                        LogoPath = reader.GetString(col++),

                        TitleBackColor = reader.GetString(col++),

                        LabelForeColor = reader.GetString(col++),

                        LinkForeColor = reader.GetString(col++),

                        ID = reader.GetInt32(col++),

                        ColorVersion = Cosmetic.GetVersion(reader.GetInt32(col++)),

                        ConsultaCoorTitle = reader.GetString(col++),

                        ConsultaProcTitle = reader.GetString(col++),

                        ConsultaSolicitudTitle = reader.GetString(col++),

                        ConsultaSuperTitle = reader.GetString(col++),

                        ReportComparacionTitle = reader.GetString(col++),

                        ReportProduccionTitle = reader.GetString(col++),

                        SolicitudTitle = reader.GetString(col++),

                        CompletadasMesDiasTitle = reader.GetString(col++),

                        HistoryCompletadasTitle = reader.GetString(col++),

                        HistoryRecibidasTitle = reader.GetString(col++),

                        IndicadoresProductividadTitle = reader.GetString(col++),

                        SolicitudesStatusTitle = reader.GetString(col++)
                    };
                }

                return cosmetic;
            }
        }
    }
}