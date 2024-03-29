using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using PAS.DAL;
using PAS.DAL.SqlClient;

namespace PAS.BLL.ProjectPackage
{
    public class ProjectBeheerder
    {
        public ProjectBeheerder()
        {
        }

        public Dictionary<int, Project> GetProjectenDictionary()
        {
            ﻿Dictionary<int, Project> project_alle = (Dictionary<int, Project>)HttpContext.Current.Cache["project_alle"];
             if (project_alle == null)
             {
                 project_alle = ProjectProvider.Instance.GetProjecten();
                 HttpContext.Current.Cache["project_alle"] = project_alle;
             }
             return project_alle;
        }
        public Dictionary<int,Project>.ValueCollection GetProjecten()
        {
            return GetProjectenDictionary().Values;           
        }

        public Dictionary<int, ProjectOpgave> GetOpgavenByProjectIdDictionary(int projectid)
        {
                ﻿Dictionary<int, ProjectOpgave> projectopgave_projectid = (Dictionary<int, ProjectOpgave>)HttpContext.Current.Cache["projectopgave_projectid="+projectid];
                 if (projectopgave_projectid == null)
                 {
                     projectopgave_projectid = ProjectProvider.Instance.GetProjectOpgavenByProjectID(projectid);
                     HttpContext.Current.Cache["projectopgave_projectid=" + projectid] = projectopgave_projectid;
                 }
                 return projectopgave_projectid;
        }
        public Dictionary<int,ProjectOpgave>.ValueCollection GetOpgavenByProjectId(int projectid)
        { 
             return GetOpgavenByProjectIdDictionary(projectid).Values;           
        }
        public ProjectOpgave GetOpgaveByProjectID_OpgaveID(int projectid,int opgaveid)
        {            
            Dictionary<int,ProjectOpgave> op =  GetOpgavenByProjectIdDictionary(projectid);
            if (op.ContainsKey(opgaveid))
            {
                return op[opgaveid];
            }
            else
            {
                return SqlProjectProvider.Instance.GetProjectOpgaveByID(opgaveid);
            }
        }

        public bool MakeProjectOpgave(ProjectOpgave opgave)
        {
            Helpers.PurgeCache("projectopgave_projectid="+opgave.Project.ProjectId);
            if (normalValidation(opgave))
            {
                return ProjectProvider.Instance.InsertProjectOpgave(opgave);
            }
            else
            {
                throw new ApplicationException("de validatie van de opgava is niet correct verlopen"); ;
            }
        }

        public bool UpdateProjectOpgave(ProjectOpgave opgave)
        {
            if(validatie(opgave))
            {
            Helpers.PurgeCache("projectopgave_projectid="+opgave.ProjectID);
            return ProjectProvider.Instance.UpdateProjectOpgave(opgave);
            }
            else
            {
                throw new ApplicationException("de validatie van de opgava is niet correct verlopen"); ;
            }
        }

        public bool DeleteProjectOpgave(ProjectOpgave opgave)
        {
            if (validatie(opgave))
            {
                Helpers.PurgeCache("projectopgave_projectid=" + opgave.ProjectID);
                return ProjectProvider.Instance.DeleteProjectOpgave(opgave.OpgaveId);
            }
            else
            {
                throw new ApplicationException("de validatie van de opgava is niet correct verlopen"); ;
            }
        }
        public bool HardDeleteProjectOpgave(ProjectOpgave opgave)
        {
            if (validatie(opgave))
            {
                Helpers.PurgeCache("projectopgave_projectid=" + opgave.ProjectID);
                return ProjectProvider.Instance.HardDeleteProjectOpgave(opgave.OpgaveId);
            }
            else
            {
                throw new ApplicationException("de validatie van de opgava is niet correct verlopen"); ;
            }
        }
        
        private bool validatie(ProjectOpgave opgave)
        {
            ProjectOpgave vorigeOpgave = this.GetOpgaveByProjectID_OpgaveID(opgave.ProjectID, opgave.OpgaveId) ;
                if (opgave.Project.inschrijvingBegonnen())
                {   
                    if (opgave.AantalGroepen < vorigeOpgave.AantalGroepen)
                    { throw new ApplicationException("Je mag de aantal groepen niet meer verkleinen"); }
                    if (opgave.AantalStudentenPerGroep != vorigeOpgave.AantalStudentenPerGroep)
                    { throw new ApplicationException("Je mag het aantal studenten per groep niet meer wijzigen"); }                    
                }
                return normalValidation(opgave);
            
        }
        private bool normalValidation(ProjectOpgave opgave)
        {
            if (opgave.AantalGroepen < 0)
            { throw new ApplicationException("Aantal groepen mogen niet < 0"); }
            if (opgave.AantalStudentenPerGroep < 0)
            { throw new ApplicationException("Aantal studenten per groep mogen niet < 0"); }
                if (opgave.OpgaveTitel.Equals(""))
                { throw new ApplicationException("Er moet een opgavetitel bekend zijn."); }
           
            if (opgave.Project == null)
            {
                throw new ApplicationException("Er moet voor een project gekozen zijn!");
            }
            return true;
        }

        public Dictionary<int,ProjectLuik> GetProjectLuikByProjectID(int projectid)
        {
            Dictionary<int,ProjectLuik> projectluik = (Dictionary<int,ProjectLuik>)HttpContext.Current.Cache["projectluik_projectid=" + projectid];
            if (projectluik == null)
            {
                projectluik = ProjectProvider.Instance.GetProjectLuikenByProjectID(projectid);
                
                HttpContext.Current.Cache["projectluik_projectid=" + projectid] = projectluik;
            }
            return projectluik;
        }
    }
}
