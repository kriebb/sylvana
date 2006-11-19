using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using PAS.BLL.ProjectPackage;
using System.Data;
using System.Web.Configuration;

namespace PAS.DAL
{
    public abstract class ProjectProvider : DataAccess
    {
        

        public ProjectProvider()
        {
            this.ConnectionString = WebConfigurationManager.ConnectionStrings["PAS"].ConnectionString;
        }

        static private ProjectProvider _instance = null;
        static public ProjectProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (ProjectProvider)Activator.CreateInstance(Type.GetType(WebConfigurationManager.AppSettings["providerType"] + ".SqlProjectProvider"));
                return _instance;
            }
        }

        /* Methodes voor de klasse Project */

        public abstract Project GetProjectByID(int projectid);
        
        public abstract Dictionary<int,Project> GetProjecten();

        /* Mappers voor de klasse Project */

        //TODO!
        /*Als er een null waarde wordt gevonden in de inschrijvingVan en de inschrijvingTot,
         * Dan geven we die de minimumWaarde momenteel. Dit moet nog veranderd worden*/

        protected virtual Project GetProjectFromReader(IDataRecord oRecord)
        {
            return new Project(
                
                Int32.Parse(oRecord["projectID"].ToString()), 
                oRecord["projectTitel"].ToString(), 
                Int32.Parse(oRecord["studieJaar"].ToString()),          
                  (oRecord["inschrijvingVan"] == DBNull.Value) ? DateTime.MinValue : (DateTime)oRecord["inschrijvingVan"],
                  (oRecord["inschrijvingTot"] == DBNull.Value) ? DateTime.MaxValue : (DateTime)oRecord["inschrijvingTot"]);
        }

        protected virtual Dictionary<int,Project> GetProjectCollectionFromReader(IDataReader oReader)
        {
            Dictionary<int,Project> projecten = new Dictionary<int,Project>();
            while (oReader.Read())
            {
                Project p = GetProjectFromReader(oReader);
                projecten.Add(p.ProjectId,p);
            }
            return projecten;
        }

    
        /* Methodes voor de klasse ProjectLuik */

        public abstract ProjectLuik GetProjectLuikByProjectluikID(int projectluikid);

        public abstract Dictionary<int,ProjectLuik> GetProjectLuiken();
    
       

        /* Methodes voor de klasse ProjectOpgave */

        public abstract bool InsertProjectOpgave(ProjectOpgave projectopgave);

        public abstract bool UpdateProjectOpgave(ProjectOpgave projectopgave);

        public abstract bool DeleteProjectOpgave(int projectopgaveid);

        public abstract bool HardDeleteProjectOpgave(int projectopgaveid);

        public abstract ProjectOpgave GetProjectOpgaveByID(int projectopgaveid);

        public abstract Dictionary<int,ProjectOpgave> GetProjectOpgaven();

        public abstract Dictionary<int,ProjectOpgave> GetProjectOpgavenByProjectID(int projectID);

        /* Mappers voor de klasse ProjectOpgave */

        protected virtual ProjectOpgave GetProjectOpgaveFromReader(IDataRecord oRecord)
        {
            return new ProjectOpgave
            (
               Int32.Parse(oRecord["opgaveID"].ToString()),
               oRecord["opgaveTitel"].ToString(),
               oRecord["korteOmschrijving"].ToString(),
               Int32.Parse(oRecord["aantalStudentenPerGroep"].ToString()),
               Int32.Parse(oRecord["aantalGroepen"].ToString()),
               Int32.Parse(oRecord["projectId"].ToString())
            );            
        }

        protected virtual Dictionary<int, ProjectOpgave> GetProjectOpgaveCollectionFromReader(IDataReader oReader)
        {
            Dictionary<int, ProjectOpgave> projectOpgaven = new Dictionary<int,ProjectOpgave>();
            while (oReader.Read())
            {
                ProjectOpgave po = GetProjectOpgaveFromReader(oReader);
                projectOpgaven.Add(po.OpgaveId,po);
            }
            return projectOpgaven;
        }


        /* Mappers voor de klasse ProjectLuik */
        public abstract Dictionary<int, ProjectLuik> GetProjectLuikenByProjectID(int projectid);

        

        protected virtual ProjectLuik GetProjectLuikFromReader(IDataRecord oRecord)
        {
            return new ProjectLuik((int)oRecord["luikid"], oRecord["luiktitel"].ToString(), (int)oRecord["percentage"], int.Parse(oRecord["projectid"].ToString()));
        }

        protected virtual Dictionary<int, ProjectLuik> GetProjectLuikCollectionFromReader(IDataReader oReader)
        {
            Dictionary<int, ProjectLuik> projectluiken = new Dictionary<int, ProjectLuik>();
            while (oReader.Read())
            {
                ProjectLuik pl = GetProjectLuikFromReader(oReader);
                projectluiken.Add(pl.LuikId, pl);
            }
            return projectluiken;
        }




    }
}
