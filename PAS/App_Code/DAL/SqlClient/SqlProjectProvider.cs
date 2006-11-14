using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using PAS.BLL.ProjectPackage;
using System.Data;
using System.Data.SqlClient;

namespace PAS.DAL.SqlClient
{
    public class SqlProjectProvider : ProjectProvider
    {
        /* Methodes voor de klasse Project */

        public override Project GetProjectByID(int projectid)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Project_SelectByProjectID", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                if (projectid > 0)
                {
                    oCmd.Parameters.Add("@projectID", SqlDbType.Int).Value = projectid;
                }
                oConn.Open();
                return GetProjectFromReader(ExecuteReader(oCmd));
            }
        }

        public override Dictionary<int,Project> GetProjecten()
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Project_SelectAll", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oConn.Open();
                return GetProjectCollectionFromReader(ExecuteReader(oCmd)); //oproepen uit de basisklasse
            }
        }

        /* Methodes voor de klasse ProjectLuik */

        public override ProjectLuik GetProjectLuikByID(int projectluikid)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Projectluik_SelectByID", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                if (projectluikid > 0)
                {
                    oCmd.Parameters.Add("@luikid", SqlDbType.Int).Value = projectluikid;
                }
                oConn.Open();
                return GetProjectLuikFromReader(ExecuteReader(oCmd));
            }
        }

        public override Dictionary<int,ProjectLuik> GetProjectLuiken()
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_Projectluik_SelectAll", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oConn.Open();
                return GetProjectLuikCollectionFromReader(ExecuteReader(oCmd));
            }
        }

        /* Methodes voor de klasse ProjectOpgave */

        public override bool InsertProjectOpgave(ProjectOpgave projectopgave)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_ProjectOpgave_Insert", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@opgaveTitel", SqlDbType.NVarChar).Value = projectopgave.OpgaveTitel;
                oCmd.Parameters.Add("@korteOmschrijving", SqlDbType.NVarChar).Value = projectopgave.KorteOmschrijving;
                oCmd.Parameters.Add("@aantalStudentenPerGroep", SqlDbType.SmallInt).Value = projectopgave.AantalStudentenPerGroep;
                oCmd.Parameters.Add("@aantalGroepen", SqlDbType.SmallInt).Value = projectopgave.AantalGroepen;
                oCmd.Parameters.Add("@projectID", SqlDbType.Int).Value = projectopgave.Project.ProjectId;
                oConn.Open();
                int ret = ExecuteNonQuery(oCmd);                
                return (ret >= 1);
            }
        }

        public override bool UpdateProjectOpgave(ProjectOpgave projectopgave)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_ProjectOpgave_Update", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@opgaveID", SqlDbType.Int).Value = projectopgave.OpgaveId;
                oCmd.Parameters.Add("@opgaveTitel", SqlDbType.NVarChar).Value = projectopgave.OpgaveTitel;
                oCmd.Parameters.Add("@korteOmschrijving", SqlDbType.NVarChar).Value = projectopgave.KorteOmschrijving;
                oCmd.Parameters.Add("@aantalStudentenPerGroep", SqlDbType.SmallInt).Value = projectopgave.AantalStudentenPerGroep;
                oCmd.Parameters.Add("@aantalGroepen", SqlDbType.SmallInt).Value = projectopgave.AantalGroepen;
                oCmd.Parameters.Add("@projectID", SqlDbType.Int).Value = projectopgave.Project.ProjectId;
                oConn.Open();
                int ret = ExecuteNonQuery(oCmd);
                return (ret >= 1);
            }
        }

        public override bool DeleteProjectOpgave(int projectopgaveid)
        {
                using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand oCmd = new SqlCommand("usp_ProjectOpgave_Delete", oConn);
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.Parameters.Add("@opgaveID", SqlDbType.Int).Value = projectopgaveid;
                    oConn.Open();
                    int ret = ExecuteNonQuery(oCmd);
                    return (ret >= 1);
                }
            
        }
        public override bool HardDeleteProjectOpgave(int projectopgaveid)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_ProjectOpgave_DeleteAll", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@opgaveID", SqlDbType.Int).Value = projectopgaveid;
                oConn.Open();
                int ret = ExecuteNonQuery(oCmd);
                return (ret >= 1);
            }
        }


        public override ProjectOpgave GetProjectOpgaveByID(int projectopgaveid)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_ProjectOpgave_SelectByOpgaveID", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                if (projectopgaveid > 0)
                {
                    oCmd.Parameters.Add("@opgaveID", SqlDbType.Int).Value = projectopgaveid;
                    
                }
                oConn.Open();
                return GetProjectOpgaveFromReader(ExecuteReader(oCmd));
            }
        }

        public override Dictionary<int,ProjectOpgave> GetProjectOpgaven()
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_ProjectOpgave_SelectAll", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                oConn.Open();
                return GetProjectOpgaveCollectionFromReader(ExecuteReader(oCmd)); //oproepen uit de basisklasse
            }
        }

        public override Dictionary<int,ProjectOpgave> GetProjectOpgavenByProjectID(int projectID)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand oCmd = new SqlCommand("usp_ProjectOpgave_SelectByProjectID", oConn);
                oCmd.CommandType = CommandType.StoredProcedure;
                if (projectID > 0)
                {
                    oCmd.Parameters.Add("@projectID", SqlDbType.Int).Value = projectID;
                }
                oConn.Open();
                return GetProjectOpgaveCollectionFromReader(ExecuteReader(oCmd));
            }
        }
    }
}
