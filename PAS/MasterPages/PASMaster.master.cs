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

public partial class PASMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((bool)Session["admin"])
            {

                MenuItem menuitem = new MenuItem();
                menuitem.Text = "Home";
                menuitem.Value = "Home";
                menuitem.NavigateUrl = "~/Admin/Default.aspx";
                MainMenu.Items.Add(menuitem);

                menuitem = new MenuItem();
                menuitem.Text = "Inschrijven";
                menuitem.Value = "Inschrijven";
                menuitem.NavigateUrl = "~/Student/Inschrijven.aspx";
                MainMenu.Items.Add(menuitem);

                menuitem = new MenuItem();
                menuitem.Text = "Docentteams";
                menuitem.Value = "Docentteams";
                menuitem.NavigateUrl = "~/Admin/DocentTeam.aspx";
                MainMenu.Items.Add(menuitem);

                menuitem = new MenuItem();
                menuitem.Text = "Projectopgaven";
                menuitem.Value = "Projectopgaven";
                menuitem.NavigateUrl = "~/Admin/ProjectOpgave.aspx";
                MainMenu.Items.Add(menuitem);

                menuitem = new MenuItem();
                menuitem.Text = "Logout";
                menuitem.Value = "Logout";
                menuitem.NavigateUrl = "~/Logout.aspx";
                MainMenu.Items.Add(menuitem);
            }
            else if ((bool)Session["student"])
            {
                MenuItem menuitem = new MenuItem();
                menuitem.Text = "Inschrijven";
                menuitem.Value = "Inschrijven";
                menuitem.NavigateUrl = "~/Student/Inschrijven.aspx";
                MainMenu.Items.Add(menuitem);

                menuitem = new MenuItem();
                menuitem.Text = "Logout";
                menuitem.Value = "Logout";
                menuitem.NavigateUrl = "~/Logout.aspx";
                MainMenu.Items.Add(menuitem);
            }
            else
            {
                MenuItem menuitem = new MenuItem();
                menuitem.Text = "Login";
                menuitem.Value = "Login";
                menuitem.NavigateUrl = "~/Default.aspx";
                MainMenu.Items.Add(menuitem);
            }
        }

        string PageName = Page.AppRelativeVirtualPath;
        foreach (MenuItem i in MainMenu.Items)
        {
            if (i.NavigateUrl.ToUpper() == PageName.ToUpper())
            {
                i.Selected = true;
                //i.Selectable = false;
            }
        }
        lblTitle.Text = Page.Title;
    }
    public string PaginaTitel
    {
        set { lblTitle.Text = value; }
    }
}
