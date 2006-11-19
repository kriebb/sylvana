using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

using PAS.BLL.DocentPackage;
using PAS.BLL.ProjectPackage;
using System.Data;

namespace PAS.DAL
{
    public abstract class DocentProvider : DataAccess
    {
        private static DocentProvider _instance;

        public DocentProvider()
        {
            this.ConnectionString = WebConfigurationManager.ConnectionStrings["PAS"].ConnectionString;
        }

        public static DocentProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (DocentProvider)Activator.CreateInstance(Type.GetType(WebConfigurationManager.AppSettings["providerType"] + ".SqlDocentProvider"));
                return _instance;
            }
        }

        /* Methodes voor de klasse Docent */

        public abstract Docent GetDocentByID(string docentid);

        public abstract Dictionary<string,Docent> GetDocenten();

        public abstract bool controleerLogin(string email, string paswoord);

        /* Mappers voor de klasse Docent */

        protected virtual Docent GetDocentFromReader(IDataRecord oRecord)
        {
            return new Docent(oRecord["docentid"].ToString(), oRecord["naam"].ToString(), oRecord["voornaam"].ToString(), oRecord["email"].ToString(), oRecord["paswoord"].ToString(), (bool)oRecord["admin"]);
        }

        protected virtual Dictionary<string,Docent> GetDocentCollectionFromReader(IDataReader oReader)
        {
            Dictionary<string,Docent> docenten = new Dictionary<string,Docent>();
            while (oReader.Read())
            {
                Docent dc = GetDocentFromReader(oReader);
                docenten.Add(dc.DocentId,dc);
            }
            return docenten;
        }

        /* Methodes voor de klasse DocentTeam */

        public abstract void InsertDocentTeam(DocentTeam docentteam);

        public abstract bool UpdateDocentTeam(DocentTeam docentteam);

        public abstract bool DeleteDocentTeam(int docentteamid);

        public abstract DocentTeam GetDocentTeamByID(int docentteamid);

        public abstract Dictionary<int,DocentTeam> GetDocentTeamsByProjectOpgave(int projectopgaveid);

        public abstract Dictionary<int,DocentTeam> GetDocentTeams();

        /* Mappers voor de klasse DocentTeam */

        protected virtual DocentTeam GetDocentTeamFromReader(IDataRecord oRecord)
        {
            return new DocentTeam((int)oRecord["docentteamid"],(int)oRecord["opgaveid"],(int)oRecord["projectid"]);
        }

        protected virtual Dictionary<int,DocentTeam> GetDocentTeamCollectionFromReader(IDataReader oReader)
        {
            Dictionary<int,DocentTeam> docentteams = new Dictionary<int,DocentTeam>();
            while (oReader.Read())
            {
                DocentTeam dt = GetDocentTeamFromReader(oReader);
                docentteams.Add(dt.DocentTeamId,dt);
            }
            return docentteams;
        }

        /* Methodes voor de klasse DocentInDocentTeam */



        public abstract bool UpdateDocentInDocentTeam(int docentteamid, int projectluikid, string docentid);

        

        public abstract Dictionary<int,string> GetDocentInDocentTeam_ByDocentTeamID(int docentteamid);

        /* Mappers voor de klasse DocentInDocentTeam */

        protected virtual Dictionary<int,string> GetDocentInDocentTeamCollectionFromReader(IDataReader oReader)
        {
            Dictionary<int,string> docentInDocentTeam = new Dictionary<int, string>();
            while (oReader.Read())
            {
                docentInDocentTeam.Add(
                    (int)oReader["projectluikid"],
                    ((oReader["docentID"]== DBNull.Value)?(null):oReader["docentID"].ToString()));
            }
            return docentInDocentTeam;
        }

        
        


    }
}
