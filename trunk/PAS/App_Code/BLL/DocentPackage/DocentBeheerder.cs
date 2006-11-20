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
        

        public Dictionary<int,DocentTeam> GetDocentTeam(int opgaveid)
        {
            Dictionary<int,DocentTeam> docentteam_opgaveid;// = (Dictionary<int,DocentTeam>)HttpContext.Current.Cache["docentteam_opgaveid=" + opgaveid];
            //if (docentteam_opgaveid == null)
            {
                docentteam_opgaveid = DocentProvider.Instance.GetDocentTeamsByProjectOpgave(opgaveid);
                //HttpContext.Current.Cache["docentteam_opgaveid=" + opgaveid]=docentteam_opgaveid;
            }
            return docentteam_opgaveid;
        }

        public Dictionary<int, DocentTeam>.ValueCollection GetDocentTeam_ValueCollection(int opgaveid)
        {
            return this.GetDocentTeam(opgaveid).Values;
        }
        public void MakeDocentTeam(DocentTeam dt)
        {
            //Helpers.PurgeCache("docentteam_opgaveid="+dt.ProjectOpgave.OpgaveId);
            DocentProvider.Instance.InsertDocentTeam(dt);
        }
        public void DeleteDocentTeam(DocentTeam dt)
        {
            //Helpers.PurgeCache("docentteam_opgaveid=" + dt.ProjectOpgave.OpgaveId);
            DocentProvider.Instance.DeleteDocentTeam(dt.DocentTeamId);
        }

        public bool ControleerLogin(string email, string paswoord)
        {
            return DocentProvider.Instance.controleerLogin(email, paswoord);
        }
        public Dictionary<string,Docent> GetDocentenDictionary()
        {
            Dictionary<string,Docent> docenten;//= (Dictionary<string,Docent>)HttpContext.Current.Cache["getAllDocenten"];
            //if (docenten == null)
            {
                docenten = DocentProvider.Instance.GetDocenten();
                //HttpContext.Current.Cache["getAllDocenten"] = docenten;
            }
            return docenten;
        }
        public Dictionary<string, Docent>.ValueCollection GetDocentenValues()
        {
            return this.GetDocentenDictionary().Values;
        }
        public void UpdateDocentInDocentTeam(DocentTeam docentinteam)
        {
            //TODO:Cachen?
            Dictionary<int, string>.Enumerator tst = docentinteam.DocentMetProjectLuikInDitDocentTeam.GetEnumerator();            
            while (tst.MoveNext())
            {
                DocentProvider.Instance.UpdateDocentInDocentTeam(docentinteam.DocentTeamId,tst.Current.Key,tst.Current.Value);       
            }
            
        }
        public Dictionary<int, string> GetAlleDocentenInEenDocentTeam_ByDocentTeamID(int docentTeamID, int projectID, int opgaveID)
        {
            Dictionary<int, string> alleDocentenVanEenTeam;// = (Dictionary<int, string>)HttpContext.Current.Cache["alleDocentenVanEenTeam_docentTeamID=" + docentTeamID];
            //if (alleDocentenVanEenTeam == null)
            {
                alleDocentenVanEenTeam = DocentProvider.Instance.GetDocentInDocentTeam_ByDocentTeamIDEnProjectID(docentTeamID,projectID,opgaveID);
                //HttpContext.Current.Cache["alleDocentenVanEenTeam_docentTeamID=" + docentTeamID] = alleDocentenVanEenTeam;
            }
            return alleDocentenVanEenTeam;
        }


        public List<DocentInDocentTeam> GetLuikenEnDocenten(int docentTeamID,int projectID,int opgaveID)
        {
            Dictionary<int, string>.Enumerator advet = this.GetAlleDocentenInEenDocentTeam_ByDocentTeamID(docentTeamID,projectID,opgaveID).GetEnumerator();
            List<DocentInDocentTeam> lijst = new List<DocentInDocentTeam>();

            while (advet.MoveNext())
            {
                if (advet.Current.Key != null)

                    
                {
                    if (advet.Current.Value == null)
                    {
                        Docent d = new Docent();
                        d.DocentId="xxx";
                        d.Naam = "Geen Docent Toegewezen";
                        lijst.Add(new DocentInDocentTeam(DomeinController.Instance.ProjectBeheerder.GetProjectLuikByProjectID(projectID)[advet.Current.Key],d));

                    }
                    else
                    {
                        lijst.Add(new DocentInDocentTeam(DomeinController.Instance.ProjectBeheerder.GetProjectLuikByProjectID(projectID)[advet.Current.Key],
                        this.GetDocentenDictionary()[advet.Current.Value]));
                    }
                }
            }
            return lijst;
        }
    }
}
