using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using PAS.BLL.ProjectPackage;
using System.Collections.Generic;

namespace PAS.BLL.DocentPackage
{
    public class DocentTeam
    {
        private ProjectOpgave _projectopgave;
        private int _docentTeamId;

        public DocentTeam(int docentteamid, ProjectOpgave po)
        {
            DocentTeamId = docentteamid;
            ProjectOpgave = po;
            DocentMetProjectLuikInDitDocentTeam = DomeinController.Instance.DocentBeheerder.GetAlleDocentenInEenDocentTeam_ByDocentTeamID(docentteamid);
        }

        public DocentTeam(int docentTeamID,int projectOpgaveID,int projectID):
            this(docentTeamID,
            DomeinController.Instance.ProjectBeheerder.GetOpgaveByProjectID_OpgaveID(projectID,projectOpgaveID))
        {}
        public DocentTeam()
        {
        }
        //int = key van projectluik, string=key van docent
        private Dictionary<int, string> _docentMetProjectLuikInDitDocentTeam;

        public Dictionary<int,string> DocentMetProjectLuikInDitDocentTeam
        {
            get { return _docentMetProjectLuikInDitDocentTeam; }
            set { _docentMetProjectLuikInDitDocentTeam = value; }
        }
	

        public int DocentTeamId
        {
            get { return _docentTeamId; }
            set { _docentTeamId = value; }
        }

        public ProjectOpgave ProjectOpgave
        {
            get
            {
                return _projectopgave;
            }
            set
            {
                _projectopgave = value;
            }
        }
    }
}
