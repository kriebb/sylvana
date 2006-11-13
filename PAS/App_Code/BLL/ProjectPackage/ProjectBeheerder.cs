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
            if (validatie(opgave))
            {
                return ProjectProvider.Instance.InsertProjectOpgave(opgave);
            }
            else
            {
                return false;
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
                return false;
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
                return false;
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
                return false;
            }
        }

        private bool validatie(ProjectOpgave opgave)
        {
            ProjectOpgave vorigeOpgave = this.GetOpgaveByProjectID_OpgaveID(opgave.ProjectID, opgave.OpgaveId) ;
            if (vorigeOpgave == null) //nieuw record gevonden
            { return normalValidation(opgave); }
            else //oud record vergelijken
            {
                if (opgave.Project.inschrijvingBegonnen())
                {   
                    if (opgave.AantalGroepen < vorigeOpgave.AantalGroepen)
                    { return false; }
                    if (opgave.AantalStudentenPerGroep != vorigeOpgave.AantalStudentenPerGroep)
                    { return false; }                    
                }
                return normalValidation(opgave);
            }
        }
        private bool normalValidation(ProjectOpgave opgave)
        {
            if (opgave.AantalGroepen <= 0)
            { return false; }
            if (opgave.AantalStudentenPerGroep <= 0)
            { return false; }
            if (opgave.OpgaveTitel == null)
            { return false; }
            else
            {
                if (opgave.OpgaveTitel.Equals(""))
                { return false; }
            }
            if (opgave.Project == null)
            {
                return false;
            }
            return true;
        }

        /*public List<ProjectLuik> GetProjectLuikByProjectID(string projectid)
        {
            throw new System.NotImplementedException();
        }*/
    }
}
