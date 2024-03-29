using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using PAS.BLL.ProjectPackage;
using PAS.BLL.DocentPackage;

namespace PAS.DAL.SqlClient
{
    public class SqlDocentProvider : DocentProvider
    {
        /* Methodes voor de klasse Docent */

        public override Docent GetDocentByID(string docentid)
        {
            throw new System.NotImplementedException();
        }

        public override Dictionary<string,Docent> GetDocenten()
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
                int ret = ExecuteNonQuery(oCmd);
                return (ret == 1);
            }
        }

        public override DocentTeam GetDocentTeamByID(int docentteamid)
        {
            throw new System.NotImplementedException();
        }

        public override Dictionary<int,DocentTeam> GetDocentTeamsByProjectOpgave(int projectopgaveid)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Docentteam_By_Opgave", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                if (projectopgaveid > 0)
                {
                    oCmd.Parameters.Add("@opgaveid", SqlDbType.Int).Value = projectopgaveid;
                }
                oConn.Open();
                return GetDocentTeamCollectionFromReader(ExecuteReader(oCmd));
            }
        }

        public override Dictionary<int,DocentTeam> GetDocentTeams()
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

        

        public override bool UpdateDocentInDocentTeam(int docentteamid,int projectluikid, String docentid)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_DocentInDocentteam_Update", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@docentteamid", SqlDbType.Int).Value = docentteamid;
                oCmd.Parameters.Add("@projectluikid", SqlDbType.Int).Value = projectluikid;
                oCmd.Parameters.Add("@docentid", SqlDbType.NVarChar).Value = docentid;
                oConn.Open();               
                int ret = ExecuteNonQuery(oCmd);
                return (ret >= 1);
            }
        }

        public override Dictionary<int, string> GetDocentInDocentTeam_ByDocentTeamIDEnProjectID(int docentTeamID, int ProjectID, int OpgaveId)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_DocentInDocentteam_Select_By_DocentteamidEnProjectid", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                if (docentTeamID > 0)
                {
                    oCmd.Parameters.Add("@docentteamid", SqlDbType.Int).Value = docentTeamID;
                    oCmd.Parameters.Add("@projectid", SqlDbType.Int).Value = ProjectID;
                }
                oConn.Open();
                return GetDocentInDocentTeamCollectionFromReader(ExecuteReader(oCmd));
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
