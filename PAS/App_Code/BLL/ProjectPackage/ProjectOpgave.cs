using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace PAS.BLL.ProjectPackage
{
    public class ProjectOpgave
    {
        private int _opgaveId;

        public int OpgaveId
        {
            get { return _opgaveId; }
            set { _opgaveId = value; }
        }
        private string _opgaveTitel;

        public string OpgaveTitel
        {
            get { return _opgaveTitel; }
            set { _opgaveTitel = value; }
        }
        private string _korteOmschrijving;

        public string KorteOmschrijving
        {
            get { return _korteOmschrijving; }
            set { _korteOmschrijving = value; }
        }
        private int _aantalStudentenPerGroep;

        public int AantalStudentenPerGroep
        {
            get { return _aantalStudentenPerGroep; }
            set { _aantalStudentenPerGroep = value; }
        }
        private int _aantalGroepen;

        public int AantalGroepen
        {
            get { return _aantalGroepen; }
            set { _aantalGroepen = value; }
        }
        private Project _project;

        public Project Project
        {
            get
            {
                return _project;
            }
            set
            {
                _project = value;
            }
        }

        public int ProjectID
        {
            get
            {
                return _project.ProjectId;
            }
            set
            {
                _project = DomeinController.Instance.ProjectBeheerder.GetProjectenDictionary()[value];
            }
        }

        public ProjectOpgave(
            int opgaveid, 
            string opgavetitel, 
            string korteomschrijving, 
            int aantalgroepen, 
            int aantalstudentenpergroep,
            int projectid):this(
        
          opgaveid,
          opgavetitel,
          korteomschrijving,
          aantalgroepen,
          aantalstudentenpergroep,
          DomeinController.Instance.ProjectBeheerder.GetProjectenDictionary()[projectid])
        {}        

        public ProjectOpgave(
         int opgaveid,
         string opgavetitel,
         string korteomschrijving,
         int aantalgroepen,
         int aantalstudentenpergroep,
         Project project)
        {
            OpgaveId = opgaveid;
            OpgaveTitel = opgavetitel;
            KorteOmschrijving = korteomschrijving;
            AantalGroepen = aantalgroepen;
            AantalStudentenPerGroep = aantalstudentenpergroep;
            Project = project;
        }

        public ProjectOpgave():this(
            int.MinValue,
            String.Empty,
            String.Empty,
            0,
            0,           
            new Project())
        {
        }
    }
}
