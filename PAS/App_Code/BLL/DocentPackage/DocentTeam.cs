using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using PAS.BLL.ProjectPackage;

namespace PAS.BLL.DocentPackage
{
    public class DocentTeam
    {
        private ProjectOpgave _projectopgave;
        private int _docentTeamId;

        public DocentTeam(int docentteamid, ProjectOpgave projectopgave)
        {
            DocentTeamId = docentteamid;
            ProjectOpgaveObj = projectopgave;
        }
        public DocentTeam(int docentteamid, int projectid, int projectopgaveid)
        {
            DocentTeamId = docentteamid;
            ProjectOpgaveObj = DomeinController.Instance.ProjectBeheerder.GetOpgaveByProjectID_OpgaveID(projectid,projectopgaveid);
        }
        public DocentTeam()
        {
        }

        public int DocentTeamId
        {
            get { return _docentTeamId; }
            set { _docentTeamId = value; }
        }

        public ProjectOpgave ProjectOpgaveObj
        {
            get { return _projectopgave; }
            set { _projectopgave = value; }
        }
	
    }
}
