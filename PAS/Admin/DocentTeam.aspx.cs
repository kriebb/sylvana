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
                ((DropDownList)gr.FindControl("ddlDocenten")).Items.Insert(0, new ListItem("--Selecteer docent--", "-1"));
                string docentid = lijst[teller].DocentID;
                if(docentid.Equals("xxx"))
                {
                    ((DropDownList)gr.FindControl("ddlDocenten")).SelectedIndex=0;
                }
                else
                {
                    ((DropDownList)gr.FindControl("ddlDocenten")).SelectedValue = docentid;
                }
                teller++;
            }
        }
        else
        {
            grvProjectLuiken.Visible = false;
            btnUpdate.Visible = false;
            grvProjectLuiken.DataSourceID = null;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int docentteamid = Int32.Parse(ddlTeams.SelectedValue);
        int opgaveid = ucOpgaveSelector.SelectedOpgaveId;



        foreach (GridViewRow gr in grvProjectLuiken.Rows)
        {
            if ((DropDownList)gr.FindControl("ddlDocenten") != null)
            {
                foreach (GridViewRow gr2 in grvProjectLuiken.Rows)
                {
                    int luikid = Int32.Parse(((Label)gr.FindControl("lblLuikID")).Text);
                    int luikid2 = Int32.Parse(((Label)gr2.FindControl("lblLuikID")).Text);
                    string docent = ((DropDownList)gr.FindControl("ddlDocenten")).SelectedValue.ToString();
                    string docent2 = ((DropDownList)gr2.FindControl("ddlDocenten")).SelectedValue.ToString();
                    if (docent == docent2  && luikid != luikid2)
                    {
                        lblMessage.Text = "Fout Updaten docent. Docent kan niet worden toegekend aan twee projectluiken.";
                        break;
                    }
                    else
                    {
                        string docentid = ((DropDownList)gr.FindControl("ddlDocenten")).SelectedValue.ToString();
                        int projectluikid = Int32.Parse(((Label)gr.FindControl("lblLuikID")).Text);

                        DocentTeam dt = DomeinController.Instance.DocentBeheerder.GetDocentTeam(opgaveid)[docentteamid];
                        Dictionary<int, string> docentMetLuik = new Dictionary<int, string>();
                        docentMetLuik.Add(projectluikid, docentid);
                        dt.DocentMetProjectLuikInDitDocentTeam = docentMetLuik;

                        DomeinController.Instance.DocentBeheerder.UpdateDocentInDocentTeam(dt);
                    }
                }
            }
            lblMessage.Text = "Aanpassen geslaagd!";
            lblMessage.Visible = true;
        }
        
    }
}
