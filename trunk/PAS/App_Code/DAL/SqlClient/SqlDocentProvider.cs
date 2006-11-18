using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using PAS.BLL.DocentPackage;

namespace PAS.DAL.SqlClient
{
    public class SqlDocentProvider : DocentProvider
    {
        /* Methodes voor de klasse Docent */

        public override Docent GetDocentByID(int docentid)
        {
            throw new System.NotImplementedException();
        }

        public override List<Docent> GetDocenten()
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Docent_SelectAll", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oConn.Open();
                return GetDocentCollectionFromReader(ExecuteReader(oCmd));
            }
        }


        /* Methodes voor de klasse DocentTeam */
        public override void InsertDocentTeam(DocentTeam dt)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Docentteam_Insert", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@opgaveid", SqlDbType.Int).Value = dt.ProjectOpgave;
                oConn.Open();
                ExecuteNonQuery(oCmd);
            }
        }

        public override bool UpdateDocentTeam(DocentTeam docentteam)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Docentteam_Update", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@docentteamid", SqlDbType.Int).Value = docentteam.DocentTeamId;
                oCmd.Parameters.Add("@opgaveid", SqlDbType.Int).Value = docentteam.ProjectOpgave;
                oConn.Open();
                ExecuteNonQuery(oCmd);
                int ret = ExecuteNonQuery(oCmd);
                return (ret == 1);
            }
        }

        public override bool DeleteDocentTeam(int docentteamid)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Docentteam_Delete", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@docentteamid", SqlDbType.Int).Value = docentteamid;
                oConn.Open();
                ExecuteNonQuery(oCmd);
                int ret = ExecuteNonQuery(oCmd);
                return (ret == 1);
            }
        }

        public override DocentTeam GetDocentTeamByID(int docentteamid)
        {
            throw new System.NotImplementedException();
        }

        public override List<DocentTeam> GetDocentTeamsByProjectOpgave(int projectopgaveid)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Docentteam_By_Opgave", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                if (projectopgaveid > 0) oCmd.Parameters.Add("@opgaveid", SqlDbType.Int).Value = projectopgaveid;
                oConn.Open();
                return GetDocentTeamCollectionFromReader(ExecuteReader(oCmd));
            }
        }

        public override List<DocentTeam> GetDocentTeams()
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Docentteam_SelectAll", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oConn.Open();
                return GetDocentTeamCollectionFromReader(ExecuteReader(oCmd));
            }
        }

        /* Methodes voor de klasse DocentInDocentTeam */

        public override void InsertDocentInDocentTeam(DocentInDocentTeam docentinteam)
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdateDocentInDocentTeam(DocentInDocentTeam docentinteam)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_DocentInDocentteam_Update", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@docentteamid", SqlDbType.Int).Value = docentinteam.DocentTeam.DocentTeamId;
                oCmd.Parameters.Add("@projectluikid", SqlDbType.Int).Value = docentinteam.ProjectLuik.LuikId;
                oCmd.Parameters.Add("@docentid", SqlDbType.NVarChar).Value = docentinteam.Docent.DocentId;
                oConn.Open();
                ExecuteNonQuery(oCmd);
                int ret = ExecuteNonQuery(oCmd);
                return (ret == 1);
            }

        }

        public override bool DeleteDocentInDocentTeam(int docentteamid)
        {
            throw new System.NotImplementedException();
        }

        public override List<DocentInDocentTeam> GetDocentInDocentTeamByDocentTeam(int docentteamid)
        {
            throw new System.NotImplementedException();
        }

        public override List<DocentInDocentTeam> GetLuikenByProjectAndTeam(int projectid, int teamid)
        {
        //    using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand oCmd = new SqlCommand("usp_ProjectLuik_SelectAll", oConn);
        //        oCmd.CommandType = CommandType.StoredProcedure;
        //        oCmd.Parameters.Add("@projectid", SqlDbType.Int).Value = projectid;
        //        oConn.Open();
        //        return GetLuikenFromReader(ExecuteReader(oCmd), teamid);
        //    }
            return null;
        }

        public override List<DocentInDocentTeam> GetDocentInDocentTeamsByDocent(int docentid)
        {
            throw new System.NotImplementedException();
        }

        public override List<string> GetDocentInDocentTeam(int luikid, int teamid)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_DocentInTeam_SelectByLuikAndTeam", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@luikid", SqlDbType.Int).Value = luikid;
                oCmd.Parameters.Add("@teamid", SqlDbType.Int).Value = teamid;
                oConn.Open();
                return GetDocentInDocentTeamFromReader(ExecuteReader(oCmd));
            }
        }

        public override bool controleerLogin(string email, string paswoord)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                string strSQL = "select * from Docent where email=@email and paswoord=@paswoord and Admin=1";
                SqlCommand oCmd = new SqlCommand(strSQL, oConn);
                oCmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@email"].Value = email;
                oCmd.Parameters.Add(new SqlParameter("@paswoord", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@paswoord"].Value = paswoord;
                oConn.Open();

                SqlDataReader oReader = oCmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    return true;
                }
                return false;
            }
        }
    }
}
