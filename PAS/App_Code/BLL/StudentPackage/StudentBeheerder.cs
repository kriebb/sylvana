using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using PAS.BLL.DocentPackage;
using PAS.DAL;

namespace PAS.BLL.StudentPackage
{
    public class StudentBeheerder
    {

        public StudentBeheerder()
        {
            
        }

        public void RegisterStudentGroep(List<Student> studenten)
        {
            throw new System.NotImplementedException();
        }

        public void GetStudentenFromWebservice()
        {
            throw new System.NotImplementedException();
        }

        public void SetStudentInGroep(Student student, int groepid)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveStudentInGroep(Student student, int groepid)
        {
            throw new System.NotImplementedException();
        }

        public void SetDocentTeamForGroep(int studentgroepid, DocentTeam docentteam)
        {
            throw new System.NotImplementedException();
        }

        public bool ControleerLogin(string username, string paswoord)
        {
            return StudentProvider.Instance.controleerLogin(username, paswoord);
        }
    }
}
