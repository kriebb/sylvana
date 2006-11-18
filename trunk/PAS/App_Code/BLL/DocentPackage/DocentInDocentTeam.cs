using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using PAS.BLL.ProjectPackage;
using System.Collections.Generic;
using PAS.DAL;

namespace PAS.BLL.DocentPackage
{
    public class DocentInDocentTeam
    {
        private DocentTeam dt = null;
        public DocentInDocentTeam(int luikid, string luiknaam, int teamid)
        {
            DocentTeamId = teamid;
            LuikId = luikid;
            LuikNaam = luiknaam;

            List<string> lijst = DocentProvider.Instance.GetDocentInDocentTeam(luikid, teamid);
            if (lijst.Count != 0)
            {
                DocentId = lijst[0];
                DocentNaam = lijst[1];
            }
            else
            {
                DocentId = "-1";
                DocentNaam = "--Niet Ingevuld--";
            }
        }

        private int _docentteamid;

        public int DocentTeamId
        {
            get { return _docentteamid; }
            set { _docentteamid = value; }
        }

        private int _luikid;

        public int LuikId
        {
            get { return _luikid; }
            set { _luikid = value; }
        }
        private string _luiknaam;

        public string LuikNaam
        {
            get { return _luiknaam; }
            set { _luiknaam = value; }
        }
        private string _docentid;

        public string DocentId
        {
            get { return _docentid; }
            set { _docentid = value; }
        }

        private string _docentnaam;

	    public string DocentNaam
	    {
		    get { return _docentnaam;}
		    set { _docentnaam = value;}
	    }
	
    }
}
