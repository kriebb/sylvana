using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using PAS.DAL;

namespace PAS.BLL.DocentPackage
{
    public class DocentBeheerder
    {
        public DocentBeheerder()
        {
            
        }
        public List<DocentInDocentTeam> SelecteerLuikenByProjectAndTeam(int projectid, int teamid)
        {
            return DocentProvider.Instance.GetLuikenByProjectAndTeam(projectid, teamid);
        }

        public Dictionary<int, DocentTeam>.ValueCollection SelectDocentTeam(int opgaveid)
        {
            return DocentProvider.Instance.GetDocentTeamsByProjectOpgave(opgaveid).Values;
        }
        public void MakeDocentTeam(int opgaveid)
        {
            DocentProvider.Instance.InsertDocentTeam(opgaveid);
        }

        public void SetDocentInTeam(Docent docent, int luikid, int teamid)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateDocentInTeam(Docent docent, int luikid, int teamid)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteDocentTeam(int teamid)
        {
            DocentProvider.Instance.DeleteDocentTeam(teamid);
        }

        public bool ControleerLogin(string email, string paswoord)
        {
            return DocentProvider.Instance.controleerLogin(email, paswoord);
        }
        public List<Docent> SelecteerDocenten()
        {
            List<Docent> lijst =  DocentProvider.Instance.GetDocenten();
            Docent docent = new Docent();
            docent.DocentId = "-1";
            docent.Naam = "Niet ";
            docent.Voornaam = "geselecteerd";
            lijst.Insert(0, docent);
            return lijst;
        }
        public bool UpdateDocentTeam(int docentteamid, int projectluikid, string docentid)
        {
            return DocentProvider.Instance.UpdateDocentInDocentTeam(docentteamid, projectluikid, docentid);
        }
    }
}
