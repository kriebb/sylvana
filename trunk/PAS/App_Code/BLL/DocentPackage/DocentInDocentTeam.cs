using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using PAS.BLL.ProjectPackage;

namespace PAS.BLL.DocentPackage
{
    public class DocentInDocentTeam
    {
        private Docent _docent;
        private ProjectLuik _projectLuik;
        private DocentTeam _docentTeam;


        public DocentInDocentTeam(DocentTeam dt, ProjectLuik pl)
        {
            
            ProjectLuik = pl;
            DocentTeam = dt;
            
        }

        public DocentInDocentTeam()
        {
        }

        public Docent Docent
        {
            get
            {
                return _docent;
            }
            set
            {
                _docent = value;
            }
        }

        public ProjectLuik ProjectLuik
        {
            get
            {
                return _projectLuik;
            }
            set
            {
                _projectLuik = value;
            }
        }

        public DocentTeam DocentTeam
        {
            get
            {
                return _docentTeam;
            }
            set
            {
                _docentTeam = value;
            }
        }
    }
}
