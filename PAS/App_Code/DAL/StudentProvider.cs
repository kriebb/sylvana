using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Web.Configuration;

using PAS.BLL.StudentPackage;
using System.Data;

namespace PAS.DAL
{
    public abstract class StudentProvider : DataAccess
    {
        private static StudentProvider _instance;

        public StudentProvider()
        {
            this.ConnectionString = WebConfigurationManager.ConnectionStrings["PAS"].ConnectionString;
        }

        public static StudentProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (StudentProvider)Activator.CreateInstance(Type.GetType(WebConfigurationManager.AppSettings["providerType"] + ".SqlStudentProvider"));
                return _instance;
            }
        }

        /* Methodes voor de klasse Student */

        public abstract void InsertStudent(Student student);

        public abstract bool UpdateStudent(Student student);

        public abstract bool DeleteStudent(int studentid);

        public abstract Student GetStudentByID(int studentid);

        public abstract List<Student> GetStudenten();

        /* Mappers voor de klasse Student */

        protected virtual Student GetStudentFromReader(IDataRecord oRecord)
        {
            throw new System.NotImplementedException();
        }

        protected virtual List<Student> GetStudentCollectionFromReader(IDataReader oReader)
        {
            throw new System.NotImplementedException();
        }

        /* Methodes voor de klasse StudentGroep */

        public abstract void InsertStudentGroep(StudentGroep studentgroep);

        public abstract bool UpdateStudentGroep(StudentGroep studentgroep);

        public abstract bool DeleteStudentGroep(int studentgroepid);

        public abstract StudentGroep GetStudentGroepByID(int studentgroepid);

        public abstract List<StudentGroep> GetStudentGroepen();

        /* Mappers voor de klasse StudentGroep */

        protected virtual StudentGroep GetStudentGroepFromReader(IDataRecord oRecord)
        {
            throw new System.NotImplementedException();
        }

        protected virtual List<StudentGroep> GetStudentGroepCollectionFromReader(IDataReader oReader)
        {
            throw new System.NotImplementedException();
        }

        /* Methodes voor de klasse StudentInStudentGroep */

        public abstract void InsertStudentInStudentGroep(StudentGroep studentingroep);

        public abstract bool UpdateStudentInStudentGroep(StudentGroep studentingroep);

        public abstract bool DeleteStudentInStudentGroep(int studentingroepid);

        public abstract StudentInStudentGroep GetStudentInStudentGroepByID(int studentingroepid);

        public abstract StudentInStudentGroep GetStudentInStudentGroepByStudent(int studentid);

        public abstract List<StudentInStudentGroep> GetStudentInStudentGroepen();

        /* Mappers voor de klasse StudentInStudentGroep */

        protected virtual StudentInStudentGroep GetStudentInStudentGroepFromReader(IDataRecord oRecord)
        {
            throw new System.NotImplementedException();
        }

        protected virtual List<StudentInStudentGroep> GetStudentInStudentGroepCollectionFromReader(IDataReader oReader)
        {
            throw new System.NotImplementedException();
        }

        /* Methodes voor de klasse StudieTraject */

        public abstract void InsertStudieTraject(StudieTraject studietraject);

        public abstract bool UpdateStudieTraject(StudieTraject studietraject);

        public abstract bool DeleteStudieTraject(int studentid);

        public abstract StudieTraject GetStudieTrajectByStudent(int studentid);

        public abstract List<StudieTraject> GetStudieTrajecten();

        /* Mappers voor de klasse StudieTraject */

        protected virtual StudieTraject GetStudieTrajectFromReader(IDataRecord oRecord)
        {
            throw new System.NotImplementedException();
        }

        protected virtual List<StudieTraject> GetStudieTrajectCollectionFromReader(IDataReader oReader)
        {
            throw new System.NotImplementedException();
        }

        public abstract bool controleerLogin(string login, string paswoord);
    }
}
