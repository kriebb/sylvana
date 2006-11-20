using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using PAS.BLL.DocentPackage;
using PAS.BLL.ProjectPackage;
using PAS.BLL;

public partial class Site_DocentTeam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            pnlTeams.Visible = false;
            btnUpdate.Visible = false;
        }
    }

    protected void OpgaveChanged(object sender, EventArgs e)
    {
        if (ucOpgaveSelector.SelectedOpgaveId > 0)
        {
            pnlTeams.Visible = true;
            Dictionary<int, DocentTeam>.ValueCollection lijst = DomeinController.Instance.DocentBeheerder.GetDocentTeam_ValueCollection(ucOpgaveSelector.SelectedOpgaveId);
            if (lijst != null)
            {
                ddlTeams.DataSourceID = "odsDocentTeams";
                ddlTeams.DataTextField = "docentTeamId";
                ddlTeams.DataValueField = "docentTeamId";
                ddlTeams.DataBind();
            }
            ddlTeams.Items.Insert(0, new ListItem("--Selecteer team--", "-1"));
            ddlTeams.AutoPostBack = true;
        }
        else
        {
            pnlTeams.Visible = false;
            ddlTeams.DataSourceID = null;
        }
    }
    protected void btnNieuwTeam_Click(object sender, EventArgs e)
    {
        /*
        DocentTeam dt = new DocentTeam();
        dt.DocentTeamId = ucOpgaveSelector.SelectedOpgaveId;
        DomeinController.Instance.DocentBeheerder.MakeDocentTeam(dt);
        ddlTeams.DataSourceID = "odsDocentTeams";
        ddlTeams.DataTextField = "docentTeamId";
        ddlTeams.DataValueField = "docentTeamId";
        ddlTeams.DataBind();    
        ddlTeams.Items.Insert(0, new ListItem("--Selecteer team--", "-1"));
        ddlTeams.AutoPostBack = true;
        ddlTeams.SelectedIndex = ddlTeams.Items.Count-1;
        pnlTeams.Visible = true;
        grvProjectLuiken.Visible = true;
         */
    }
    protected void btnVerwijderTeam_Click(object sender, EventArgs e)
    {
        /*
        if (int.Parse(ddlTeams.SelectedValue) > 0)
        {
            DomeinController.Instance.DocentBeheerder.DeleteDocentTeam(int.Parse(ddlTeams.SelectedValue));
            ddlTeams.DataSourceID = "odsDocentTeams";
            ddlTeams.DataTextField = "docentTeamId";
            ddlTeams.DataValueField = "docentTeamId";
            ddlTeams.DataBind();
            ddlTeams.Items.Insert(0, new ListItem("--Selecteer team--", "-1"));
            ddlTeams.AutoPostBack = true;
        }
        else
        {
            grvProjectLuiken.DataSourceID = null;
            grvProjectLuiken.Visible = false;
            btnUpdate.Visible = false;
        }*/
    }
    protected void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlTeams.SelectedValue) > 0)
        {
            //grvProjectLuiken.Visible = true;
            btnUpdate.Visible = true;
            grvProjectLuiken.DataSourceID = "odsDocentInDocentTeam";
            pnlTeams.Visible = true;
            grvProjectLuiken.DataBind();
            List<DocentInDocentTeam> lijst = ((List<DocentInDocentTeam>)odsDocentInDocentTeam.Select());
            int teller = 0;
            foreach (GridViewRow gr in grvProjectLuiken.Rows)
            {
                
                string docentid = lijst[teller].DocentID;
                teller++;

                ((DropDownList)gr.FindControl("ddlDocenten")).SelectedValue = docentid;
                /*if (!(docentid.Equals("xxx")))
                {
                    
                }
                else
                {
                    ((DropDownList)gr.FindControl("ddlDocenten")).SelectedValue = "xxx";
                }*/
            }
        }
        else
        {
            //grvProjectLuiken.Visible = false;
            btnUpdate.Visible = false;
            //grvProjectLuiken.DataSourceID = null;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        //List<string> messages = new List<string>();
        int docentteamid = Int32.Parse(ddlTeams.SelectedValue);
        int opgaveid = ucOpgaveSelector.SelectedOpgaveId;
        //int projectid=Int32.Parse(ucOpgaveSelector.SelectedProjectid);



        foreach (GridViewRow gR in grvProjectLuiken.Rows)
        {
            if ((DropDownList)gR.FindControl("ddlDocenten") != null)
            {
                string docentid = ((DropDownList)gR.FindControl("ddlDocenten")).SelectedValue.ToString();
                int projectluikid = Int32.Parse(((Label)gR.FindControl("lblLuikID")).Text);

                DocentTeam dt = DomeinController.Instance.DocentBeheerder.GetDocentTeam(opgaveid)[docentteamid];
                Dictionary<int, string> docentMetLuik = new Dictionary<int, string>();
                docentMetLuik.Add(projectluikid, docentid);
                dt.DocentMetProjectLuikInDitDocentTeam = docentMetLuik;

                DomeinController.Instance.DocentBeheerder.UpdateDocentInDocentTeam(dt);
            }
        }
        //    string luik = ((Label)gR.FindControl("lblLuikTitel")).Text;
        //    if (gelukt==false)
        //    {
        //        messages.Add("Updaten " + luik + " gelukt");
        //    }
        //    else
        //    {
        //        messages.Add("Updaten " + luik + " mislukt!");
        //    }
        //}
        //foreach (string s in messages)
        //{
        //    lblMessage.Text = s;
        //}
        //lblMessage.Visible = true;
    }
    protected void ddlDocenten_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
