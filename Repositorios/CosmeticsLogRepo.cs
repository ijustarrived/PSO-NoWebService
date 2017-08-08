using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PSO.Repositorios
{
    public class CosmeticsLogRepo
    {
        public static Exception Create(CosmeticLog cosmetic)
        {
            using (SqlConnection conn = DB.GetLogConnection())
            {
                #region Sql command

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Cosmetics (LogoPath,
                                                                    TitleBackColor,
                                                                    LabelForeColor,
                                                                    LinkForeColor,
                                                                    ColorVersion,
                                                                    ConsultaCoorTitle,
                                                                    ConsultaProcTitle,
                                                                    ConsultaSolicitudTitle,
                                                                    ConsultaSuperTitle,
                                                                    ReportComparacionTitle,
                                                                    ReportProduccionTitle,
                                                                    SolicitudTitle,
                                                                    CompletadasMesDiasTitle,
                                                                    HistoryCompletadasTitle,
                                                                    HistoryRecibidasTitle ,
                                                                    IndicadoresProductividadTitle,
                                                                    SolicitudesStatusTitle,
                                                                    UpdateDate,
                                                                    WhoUpdated)  
                                                                    VALUES 
                                                                    (@LogoPath,
                                                                    @TitleBackColor,
                                                                    @LabelForeColor,
                                                                    @LinkForeColor,
                                                                    @ColorVersion,
                                                                    @ConsultaCoorTitle,
                                                                    @ConsultaProcTitle,
                                                                    @ConsultaSolicitudTitle,
                                                                    @ConsultaSuperTitle,
                                                                    @ReportComparacionTitle,
                                                                    @ReportProduccionTitle,
                                                                    @SolicitudTitle,
                                                                    @CompletadasMesDiasTitle,
                                                                    @HistoryCompletadasTitle,
                                                                    @HistoryRecibidasTitle,
                                                                    @IndicadoresProductividadTitle,
                                                                    @SolicitudesStatusTitle,
                                                                    @UpdateDate,
                                                                    @WhoUpdated);", conn);

                #endregion

                #region Command Parameteres

                cmd.Parameters.AddWithValue("@WhoUpdated", cosmetic.WhoUpdated);

                cmd.Parameters.AddWithValue("@UpdateDate", cosmetic.UpdateDate);

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
    }
}