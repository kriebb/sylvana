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
using PAS.BLL.ProjectPackage;
using PAS.BLL;

public delegate void SelectedOpgaveChanged(object sender, EventArgs e);

public partial class Usercontrols_OpgaveSelector : System.Web.UI.UserControl
{
    public event SelectedOpgaveChanged OpgaveChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlOpgaven.Visible = false;
        }
    }

    protected void ProjectChanged(object sender, EventArgs e)
    {
        if (ucProjectSelector.SelectedProjectid > 0)
        {
            ddlOpgaven.Visible = true;
            ddlOpgaven.DataTextField = "OpgaveTitel";
            ddlOpgaven.DataValueField = "OpgaveID";

            ddlOpgaven.DataSource = DomeinController.Instance.ProjectBeheerder.GetOpgavenByProjectId(ucProjectSelector.SelectedProjectid);
            ddlOpgaven.DataBind();
            ddlOpgaven.Items.Insert(0, new ListItem("--Selecteer Opgave--", "-1"));
            ddlOpgaven.AutoPostBack = true;
        }
        else
        {
            ddlOpgaven.Visible = false;
            ddlOpgaven.SelectedValue = "-1";
        }
    }
    protected void ddlOpgaven_SelectedIndexChanged(object sender, EventArgs e)
    {
        OpgaveChanged(this, e);
    }
    public int SelectedProjectid
    {
        get { return ucProjectSelector.SelectedProjectid; }
    }
    public int SelectedOpgaveId
    {
        get { 
            if (ddlOpgaven.SelectedValue.Equals(""))
            {
                return -1;
            }
            else
            {
                return int.Parse(ddlOpgaven.SelectedValue); 
            }
        }
    }
}
