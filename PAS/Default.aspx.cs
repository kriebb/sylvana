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

using PAS.BLL;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!Roles.RoleExists("admin"))
                Roles.CreateRole("admin");
            if (!Roles.RoleExists("student"))
                Roles.CreateRole("student");
            Session["admin"] = false;
            Session["student"] = false;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (DomeinController.Instance.DocentBeheerder.ControleerLogin(txtLogin.Text, txtPaswoord.Text))
            {
                if (!Roles.IsUserInRole(txtLogin.Text, "admin"))
                {
                    Roles.AddUserToRole(txtLogin.Text, "admin");
                }
                FormsAuthentication.SetAuthCookie(txtLogin.Text, false);
                Session["username"] = txtLogin.Text;
                Session["admin"] = true;
                Session["student"] = false;
                Response.Redirect("~/Admin/Default.aspx");
            }
            else if (DomeinController.Instance.StudentBeheerder.ControleerLogin(txtLogin.Text, txtPaswoord.Text))
            {
                if (!Roles.IsUserInRole(txtLogin.Text, "student"))
                {
                    Roles.AddUserToRole(txtLogin.Text, "student");
                }
                FormsAuthentication.SetAuthCookie(txtLogin.Text, false);
                Session["username"] = txtLogin.Text;
                Session["admin"] = false;
                Session["student"] = true;
                Response.Redirect("~/Student/Inschrijven.aspx");
            }
            else
            {
                ErrorLabel.Text = "Login / paswoord niet correct";
                ErrorLabel.Visible = true;
            }
        }
    }
}
