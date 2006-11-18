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
using System.Data.SqlClient;
using System.Net.Mail;

public partial class PASMaster : System.Web.UI.MasterPage
{
    public static string onderzoekException(Exception inner)
    {
        String tekst = "";
        if (inner is SqlException)
        {
            tekst += inner.Message;
        }
        else
        {            
                if (inner is NoNullAllowedException)
                    tekst += "Er zijn velden niet goed ingevuld. <br>Gelieve alles goed na te kijken.<br>" + inner.Message;
                else
                {
                    if (inner is ArgumentException)
                    {
                        string paramName = ((ArgumentException)inner).ParamName;
                        tekst += string.Concat("De ", paramName, " parameterwaarde is slecht.<br>" + inner.Message);
                    }
                    else
                    {
                        if (inner is ApplicationException)
                        {
                            tekst += "Fout in de applicatie:<br>" + inner.Message;
                        }
                        else
                        {
                            if (inner is System.Data.Common.DbException)
                            {
                                tekst += "We ondervinden problemen met onze database. <br>Probeer later opnieuw.<br>" + inner.Message;
                            }
                            String bericht = "";
                            do
                            {
                                bericht = "<br>" + inner.Message + "<br>";
                            }
                            while (inner.InnerException != null);
                            tekst += bericht;
                            
                         
                        string strBody = "<html><body><b>Begin log op"+DateTime.Today+"</b><br>" +tekst+
                           " <font color=\"red\">-einde log-</font></body></html>";

                        MailMessage msgMail = new MailMessage("info@hogent.be", "kristof@riebbels.be", "ErrorLog", strBody);

                        msgMail.IsBodyHtml = true;
                            SmtpClient newClient = new SmtpClient();
                        newClient.Host = "relay.skynet.be";
                        
                        newClient.Send(msgMail);
                        tekst += "<br>Deze fout is gemaild naar de administrator.</br>";


                    }
                }
            }
        }
        return tekst;
    }

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
