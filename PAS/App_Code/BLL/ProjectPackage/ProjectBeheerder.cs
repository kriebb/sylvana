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
            Helpers.PurgeCache("projectopgave");
            return ProjectProvider.Instance.InsertProjectOpgave(opgave);
        }

        public bool UpdateProjectOpgave(ProjectOpgave opgave)
        {
            Helpers.PurgeCache("projectopgave");
            return ProjectProvider.Instance.UpdateProjectOpgave(opgave);
        }

        public bool DeleteProjectOpgave(ProjectOpgave opgave)
        {
            Helpers.PurgeCache("projectopgave");
            return ProjectProvider.Instance.DeleteProjectOpgave(opgave.OpgaveId);
        }
        public bool HardDeleteProjectOpgave(ProjectOpgave opgave)
        {
            Helpers.PurgeCache("");//Voorlopig beetje drastisch, maar anders dirty problemen
            return ProjectProvider.Instance.HardDeleteProjectOpgave(opgave.OpgaveId);
        }

        public List<ProjectLuik> GetProjectLuikByProjectID(string projectid)
        {
            throw new System.NotImplementedException();
        }
    }
}
