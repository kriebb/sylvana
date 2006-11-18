using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using PAS.DAL;
using PAS.BLL.ProjectPackage;

namespace PAS.BLL.DocentPackage
{
    public class DocentBeheerder
    {
        public DocentBeheerder()
        {
            
        }
        public List<DocentInDocentTeam> SelecteerLuikenByProjectAndTeam(int projectid, int teamid)
        {
            List<DocentInDocentTeam> docentindocentteam_project_team = (List<DocentInDocentTeam>)HttpContext.Current.Cache["docentindocentteam_project_team=" + projectid + "" + teamid];
            if (docentindocentteam_project_team == null)
            {
                docentindocentteam_project_team = DocentProvider.Instance.GetLuikenByProjectAndTeam(projectid, teamid);
                HttpContext.Current.Cache["docentindocentteam_project_team=" + projectid + "" + teamid] = docentindocentteam_project_team;
            }
            return docentindocentteam_project_team;
        }

        public List<DocentTeam> SelectDocentTeam(int opgaveid)
        {
            List<DocentTeam> docentteam_opgaveid = (List<DocentTeam>)HttpContext.Current.Cache["docentteam_opgaveid=" + opgaveid];
            if (docentteam_opgaveid == null)
            {
                docentteam_opgaveid = DocentProvider.Instance.GetDocentTeamsByProjectOpgave(opgaveid);
                HttpContext.Current.Cache["docentteam_opgaveid=" + opgaveid]=docentteam_opgaveid;
            }
            return docentteam_opgaveid;
        }
        public void MakeDocentTeam(DocentTeam dt)
        {
            DocentProvider.Instance.InsertDocentTeam(dt);
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
            List<Docent> docenten = (List<Docent>)HttpContext.Current.Cache["docenten"];
            if (docenten == null)
            {
                docenten = DocentProvider.Instance.GetDocenten();
                Docent docent = new Docent();
                docent.DocentId = "-1";
                docent.Naam = "Niet ";
                docent.Voornaam = "geselecteerd";
                docenten.Insert(0, docent);
                HttpContext.Current.Cache["docenten"] = docenten;
            }
            return docenten;
        }
        public bool UpdateDocentTeam(DocentInDocentTeam docentinteam)
        {
            return DocentProvider.Instance.UpdateDocentInDocentTeam(docentinteam);
        }
        //public void SetDocentInTeam(Docent docent, int luikid, int teamid)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void UpdateDocentInTeam(Docent docent, int luikid, int teamid)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
