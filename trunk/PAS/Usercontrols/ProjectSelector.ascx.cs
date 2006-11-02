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

public delegate void SelectedProjectChanged(object sender, EventArgs e);

public partial class Usercontrols_ProjectSelector : System.Web.UI.UserControl
{
    public event SelectedProjectChanged ProjectChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlProjecten.DataSourceID = "odsProjecten";
            ddlProjecten.DataTextField = "projectTitel";
            ddlProjecten.DataValueField = "projectID";
            ddlProjecten.DataBind();
            ddlProjecten.Items.Insert(0, new ListItem("--Selecteer project--", "-1"));
            ddlProjecten.AutoPostBack = true;
        }

    }

    public int SelectedProjectid
    {
        get { return int.Parse(ddlProjecten.SelectedValue); }
    }

    protected void ddlProjecten_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProjectChanged(this, e);
    }
}
