using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using PAS.BLL.StudentPackage;

namespace PAS.DAL.SqlClient
{
    public class SqlStudentProvider : StudentProvider
    {
        /* Methodes voor de klasse Student */

        public override void InsertStudent(Student student)
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdateStudent(Student student)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteStudent(int studentid)
        {
            throw new System.NotImplementedException();
        }

        public override Student GetStudentByID(int studentid)
        {
            throw new System.NotImplementedException();
        }

        public override List<Student> GetStudenten()
        {
            throw new System.NotImplementedException();
        }

        /* Methodes voor de klasse StudentGroep */

        public override void InsertStudentGroep(StudentGroep studentgroep)
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdateStudentGroep(StudentGroep studentgroep)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteStudentGroep(int studentgroepid)
        {
            throw new System.NotImplementedException();
        }

        public override StudentGroep GetStudentGroepByID(int studentgroepid)
        {
            throw new System.NotImplementedException();
        }

        public override List<StudentGroep> GetStudentGroepen()
        {
            throw new System.NotImplementedException();
        }

        /* Methodes voor de klasse StudentInStudentGroep */

        public override void InsertStudentInStudentGroep(StudentGroep studentingroep)
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdateStudentInStudentGroep(StudentGroep studentingroep)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteStudentInStudentGroep(int studentingroepid)
        {
            throw new System.NotImplementedException();
        }

        public override StudentInStudentGroep GetStudentInStudentGroepByID(int studentingroepid)
        {
            throw new System.NotImplementedException();
        }

        public override StudentInStudentGroep GetStudentInStudentGroepByStudent(int studentid)
        {
            throw new System.NotImplementedException();
        }

        public override List<StudentInStudentGroep> GetStudentInStudentGroepen()
        {
            throw new System.NotImplementedException();
        }

        /* Methodes voor de klasse StudieTraject */

        public override void InsertStudieTraject(StudieTraject studietraject)
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdateStudieTraject(StudieTraject studietraject)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteStudieTraject(int studentid)
        {
            throw new System.NotImplementedException();
        }

        public override StudieTraject GetStudieTrajectByStudent(int studentid)
        {
            throw new System.NotImplementedException();
        }

        public override List<StudieTraject> GetStudieTrajecten()
        {
            throw new System.NotImplementedException();
        }

        public override bool controleerLogin(string login, string paswoord)
        {
            using (SqlConnection oConn = new SqlConnection(this.ConnectionString))
            {
                string strSQL = "select * from Student where studentNr=@login and paswoord=@paswoord";
                SqlCommand oCmd = new SqlCommand(strSQL, oConn);
                oCmd.Parameters.Add(new SqlParameter("@login", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@login"].Value = login;
                oCmd.Parameters.Add(new SqlParameter("@paswoord", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@paswoord"].Value = paswoord;
                oConn.Open();

                SqlDataReader oReader = oCmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    return true;
                }
                return false;
            }
        }
    }
}
