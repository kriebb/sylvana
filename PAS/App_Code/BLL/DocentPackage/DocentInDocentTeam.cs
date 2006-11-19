using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using PAS.BLL.ProjectPackage;
using System.Collections.Generic;

namespace PAS.BLL.DocentPackage
{
    public class DocentInDocentTeam
    {
        private DocentTeam dt;

        public DocentTeam DocentTeam
        {
            get { return dt; }
            set { dt = value; }
        }

        private ProjectLuik pl;

        public ProjectLuik ProjectLuik
        {
            get { return pl; }
            set { pl = value; }
        }
        public String ProjectLuikNaam
        {
            get { return pl.LuikTitel; }
        }


        private Docent docent;

        public Docent Docent
        {
            get { return docent; }
            set { docent=value; }
        }
        public String DocentNaam
        {
            get { return docent.NaamVoornaam; }
        }
		
        
        public DocentInDocentTeam(ProjectLuik pl, Docent docent)
        {
            this.ProjectLuik = pl;
            this.Docent = docent;
        }

    }
}
