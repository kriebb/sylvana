using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace PAS.BLL.ProjectPackage
{
    public class ProjectLuik
    {
        private int _luikId;

        public int LuikId
        {
            get { return _luikId; }
            set { _luikId = value; }
        }
        private string _luikTitel;

        public string LuikTitel
        {
            get { return _luikTitel; }
            set { _luikTitel = value; }
        }
        private int _percentage;

        public int Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
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

        public ProjectLuik(int luikid, string luiktitel, int percentage, Project project)
        {
            LuikId=luikid;
            LuikTitel=luiktitel;
            Percentage=percentage;
            Project=project;
        }

        public ProjectLuik()
        {
        }
    }
}
