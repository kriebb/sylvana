using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace PAS.BLL.DocentPackage
{
    public class Docent
    {
        private string _docentId;

        public string DocentId
        {
            get { return _docentId; }
            set { _docentId = value; }
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
        private bool _admin;

        public bool Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }
        private string _paswoord;

        public string Paswoord
        {
            get { return _paswoord; }
            set { _paswoord = value; }
        }
        public string NaamVoornaam
        {
            get { return Naam + " " + Voornaam; }
        }

        public Docent(string docentid, string naam, string voornaam, string email, string paswoord, bool admin)
        {
            DocentId = docentid;
            Naam = naam;
            Voornaam = voornaam;
            Email = email;
            Paswoord = paswoord;
            Admin = admin;
        }

        public Docent()
        {
        }
    }
}
