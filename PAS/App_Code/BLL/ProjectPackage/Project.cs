using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;


namespace PAS.BLL.ProjectPackage
{
    
    public class Project
    {
        private int _projectId;

        public int ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }
        private string _projectTitel;

        public string ProjectTitel
        {
            get { return _projectTitel; }
            set { _projectTitel = value; }
        }
        private int _studiejaar;

        public int Studiejaar
        {
            get { return _studiejaar; }
            set { _studiejaar = value; }
        }
        private DateTime _inschrijvingVan;

        public DateTime InschrijvingVan
        {
            get { return _inschrijvingVan; }
            set { _inschrijvingVan = value; }
        }
        private DateTime _inschrijvingTot;

        public DateTime InschrijvingTot
        {
            get { return _inschrijvingTot; }
            set { _inschrijvingTot = value; }
        }

        public Project(int projectid, string projecttitel, int studiejaar, DateTime inschrijvingvan, DateTime inschrijvingtot)
        {
            ProjectId = projectid;
            ProjectTitel = projecttitel;
            Studiejaar = studiejaar;
            InschrijvingVan = inschrijvingvan;
            InschrijvingTot = inschrijvingtot;
        }

        public Project()
        {
   
        }
        public Project(int projectid)
        {
            ProjectId = projectid;
        }
        public override string ToString()
        {
            return ProjectTitel;
        }
    }
}
