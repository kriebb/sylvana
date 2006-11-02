using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using PAS.BLL.DocentPackage;
using PAS.BLL.StudentPackage;
using PAS.BLL.ProjectPackage;

namespace PAS.BLL
{
    public class DomeinController
    {
        private DocentBeheerder _docentBeheerder;
        private ProjectBeheerder _projectBeheerder;
        private StudentBeheerder _studentBeheerder;

        public DomeinController()
        {           
        }

        private static DomeinController _instance=null;

        public static DomeinController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DomeinController();
                }
                return _instance;
            }
            set
            {
            }
        }

        public ProjectBeheerder ProjectBeheerder
        {
            get
            {
                if (_projectBeheerder == null)
                {
                    _projectBeheerder = new ProjectBeheerder();
                }
                return _projectBeheerder;
            }
        }

        public StudentBeheerder StudentBeheerder
        {
            get
            {
                if (_studentBeheerder == null)
                {
                    _studentBeheerder = new StudentBeheerder();
                }
                return _studentBeheerder;
            }
        }

        public DocentBeheerder DocentBeheerder
        {
            get
            {
                if (_docentBeheerder == null)
                {
                    _docentBeheerder = new DocentBeheerder();
                }
                return _docentBeheerder;
            }
            set
            {
            }
        }
    }
}
