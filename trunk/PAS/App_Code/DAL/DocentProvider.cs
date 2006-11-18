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

        public abstract Docent GetDocentByID(int docentid);

        public abstract List<Docent> GetDocenten();

        public abstract bool controleerLogin(string email, string paswoord);

        /* Mappers voor de klasse Docent */

        protected virtual Docent GetDocentFromReader(IDataRecord oRecord)
        {
            return new Docent(oRecord["docentid"].ToString(), oRecord["naam"].ToString(), oRecord["voornaam"].ToString(), oRecord["email"].ToString(), oRecord["paswoord"].ToString(), (bool)oRecord["admin"]);
        }

        protected virtual List<Docent> GetDocentCollectionFromReader(IDataReader oReader)
        {
            List<Docent> docenten = new List<Docent>();
            while (oReader.Read())
            {
                docenten.Add(GetDocentFromReader(oReader));
            }
            return docenten;
        }

        /* Methodes voor de klasse DocentTeam */

        public abstract void InsertDocentTeam(DocentTeam docentteam);

        public abstract bool UpdateDocentTeam(DocentTeam docentteam);

        public abstract bool DeleteDocentTeam(int docentteamid);

        public abstract DocentTeam GetDocentTeamByID(int docentteamid);

        public abstract List<DocentTeam> GetDocentTeamsByProjectOpgave(int projectopgaveid);

        public abstract List<DocentTeam> GetDocentTeams();

        /* Mappers voor de klasse DocentTeam */

        protected virtual DocentTeam GetDocentTeamFromReader(IDataRecord oRecord)
        {
            ProjectOpgave po = new ProjectOpgave();
            po.OpgaveId=(int)oRecord["opgaveid"];
            return new DocentTeam((int)oRecord["docentteamid"],po );
        }

        protected virtual List<DocentTeam> GetDocentTeamCollectionFromReader(IDataReader oReader)
        {
            List<DocentTeam> docentteams = new List<DocentTeam>();
            while (oReader.Read())
            {
                docentteams.Add(GetDocentTeamFromReader(oReader));
            }
            return docentteams;
        }

        /* Methodes voor de klasse DocentInDocentTeam */

        public abstract void InsertDocentInDocentTeam(DocentInDocentTeam docentinteam);

        public abstract bool UpdateDocentInDocentTeam(DocentInDocentTeam docentinteam);

        public abstract bool DeleteDocentInDocentTeam(int docentteamid);

        public abstract List<DocentInDocentTeam> GetDocentInDocentTeamByDocentTeam(int docentteamid);

        public abstract List<DocentInDocentTeam> GetDocentInDocentTeamsByDocent(int docentid);

        public abstract List<string> GetDocentInDocentTeam(int luikid, int teamid);

        protected virtual List<string> GetDocentInDocentTeamFromReader(IDataReader oReader)
        {
            List<string> lijst = new List<string>();
            if (oReader.Read())
            {
                lijst.Add(oReader["docentid"].ToString());
                lijst.Add(oReader["naam"].ToString() + " " + oReader["voornaam"].ToString());
            }
            return lijst;
        }

        public abstract List<DocentInDocentTeam> GetLuikenByProjectAndTeam(int projectid, int teamid);

        /* Mappers voor de klasse DocentInDocentTeam */

        protected virtual List<DocentInDocentTeam> GetLuikenFromReader(IDataReader oReader, int teamid)
        {
            List<DocentInDocentTeam> lijst = new List<DocentInDocentTeam>();
            while (oReader.Read())
            {
                lijst.Add(GetLuikFromReader(oReader, teamid));
            }
            return lijst;
        }

        protected virtual DocentInDocentTeam GetLuikFromReader(IDataRecord oRecord, int teamid)
        {
            DocentTeam dt = new DocentTeam();
            ProjectLuik pl = new ProjectLuik();
            Docent d = new Docent();
            dt.DocentTeamId=teamid;
            pl.LuikId=(int)oRecord["luikid"];
            d.DocentId=oRecord["docentid"].ToString();
            return new DocentInDocentTeam(dt,pl,d );
        }

    }
}
