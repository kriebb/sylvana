using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using PAS.BLL.ProjectPackage;

namespace PAS.BLL.DocentPackage
{
    public class DocentTeam
    {
        private int _projectopgaveid;
        private int _docentTeamId;

        public DocentTeam(int docentteamid, int projectopgaveid)
        {
            DocentTeamId = docentteamid;
            ProjectOpgaveId = projectopgaveid;
        }

        public DocentTeam()
        {
        }

        public int DocentTeamId
        {
            get { return _docentTeamId; }
            set { _docentTeamId = value; }
        }

        public int ProjectOpgaveId
        {
            get { return _projectopgaveid; }
            set { _projectopgaveid = value; }
        }
	
    }
}
