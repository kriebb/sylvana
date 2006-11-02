using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using PAS.BLL.DocentPackage;
using PAS.BLL.ProjectPackage;

namespace PAS.BLL.StudentPackage
{
    public class StudentGroep
    {
        private int _studentGroepId;

        public int StudentGroepId
        {
            get { return _studentGroepId; }
            set { _studentGroepId = value; }
        }
        private ProjectOpgave _projectOpgave;
        private DocentTeam _docentTeam;
        private DateTime _inschrijvingsDatum;

        public DateTime InschrijvingsDatum
        {
            get { return _inschrijvingsDatum; }
            set { _inschrijvingsDatum = value; }
        }

        public PAS.BLL.ProjectPackage.ProjectOpgave ProjectOpgave
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public PAS.BLL.DocentPackage.DocentTeam DocentTeam
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public StudentGroep(int studentgroepid, DocentTeam docentteam, ProjectOpgave projectopgave, DateTime inschrijvingsdatum)
        {
            throw new System.NotImplementedException();
        }

        public StudentGroep()
        {
            throw new System.NotImplementedException();
        }
    }
}
