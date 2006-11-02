using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace PAS.BLL.StudentPackage
{
    public class Student
    {
        private int _studentNr;

        public int StudentNr
        {
            get { return _studentNr; }
            set { _studentNr = value; }
        }
        private string _naam;

        public string Naam
        {
            get { return _naam; }
            set { _naam = value; }
        }
        private string _voornaam;

        public string Voornaam
        {
            get { return _voornaam; }
            set { _voornaam = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _paswoord;

        public string Paswoord
        {
            get { return _paswoord; }
            set { _paswoord = value; }
        }
        private string _studiejaar;

        public string Studiejaar
        {
            get { return _studiejaar; }
            set { _studiejaar = value; }
        }

        private bool _geindividualiseerdStudietraject;

        public bool GeindividualiseerdStudietraject
        {
            get { return _geindividualiseerdStudietraject; }
            set { _geindividualiseerdStudietraject = value; }
        }

        public Student(int studentnr, string naam, string voornaam, string email, string paswoord, string studiejaar, bool geindividualiseerdstudietraject)
        {
            throw new System.NotImplementedException();
        }

        public Student()
        {
            throw new System.NotImplementedException();
        }
    }
}
