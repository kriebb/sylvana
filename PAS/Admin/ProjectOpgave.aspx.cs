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


    public partial class Site_ProjectOpgave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {
                btnInsert.Visible = false;
            }
        }
        protected void grvOpgaven_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            try
            {
                grvOpgaven.SelectedIndex = -1;
                grvOpgaven.DataBind();
                dvOpgaven.Visible = false;
                btnInsert.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = e.Exception.Message + "<br>"+ex.Message;
            }
        }
        protected void grvOpgaven_RowCreated(object sender, GridViewRowEventArgs e)
        {           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btn = (ImageButton)e.Row.Cells[e.Row.Cells.Count-1].Controls[0];
                btn.OnClientClick = "if (confirm('Ben je zeker deze projectopgave te verwijderen?') == false) return false;";
            }            
        }
        protected void grvOpgaven_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvOpgaven.ChangeMode(DetailsViewMode.Edit);
            dvOpgaven.HeaderText = "<center>...::Wijzig deze opgave::...</center>";
            dvOpgaven.DataBind();          
            dvOpgaven.Visible = true;
            btnInsert.Visible = false;
            btnBevestigHardDelete.Visible = false;
            lblMessage.Text = "";
        }      
        protected void dvOpgaven_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            try
            {
                grvOpgaven.SelectedIndex = -1;
                grvOpgaven.DataBind();
                dvOpgaven.Visible = false;
                btnInsert.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = e.Exception.Message + "<br>" + ex.Message;
            }
           
        }
        protected void dvOpgaven_ModeChanged(object sender, EventArgs e)
        {
            grvOpgaven.SelectedIndex = -1;            
            dvOpgaven.Visible = false;
            btnInsert.Visible = true;
            dvOpgaven.ChangeMode(DetailsViewMode.Edit);            
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            btnBevestigHardDelete.Visible = false;
            grvOpgaven.SelectedIndex = -1;
            dvOpgaven.Visible = true;
            dvOpgaven.HeaderText = "<center>...::Maak een nieuwe opgave aan::...</center>";
            lblMessage.Text = "";
            btnInsert.Visible = false;
            dvOpgaven.ChangeMode(DetailsViewMode.Insert);
        }
        protected void dvOpgaven_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                e.Cancel = true;
                lblMessage.Text = "Opdracht toevoegen is geannuleerd.";
            }
            else
            {
                e.Values["projectID"] = usProjecten.SelectedProjectid;
            }
        }
        protected void dvOpgaven_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            try
            {
                grvOpgaven.SelectedIndex = -1;
                dvOpgaven.HeaderText = "<center>...::Maak een nieuwe opgave aan::...</center>";
                dvOpgaven.Visible = false;
                dvOpgaven.ChangeMode(DetailsViewMode.Edit);
                grvOpgaven.DataBind();
                btnInsert.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = e.Exception.Message + "<br>" + ex.Message;
            }
        }
        protected void ProjectChanged(object sender, EventArgs e)
        {
            lblInschrijving.Visible = false;
            lblInschrijving.Text = "";
            if (usProjecten.SelectedProjectid >= 0)
            {
                btnInsert.Visible = true;
                grvOpgaven.Caption = "<b>...::Overzicht Projectopgaven::...</b>";
                grvOpgaven.DataSourceID = "odsOpgaven";
                grvOpgaven.DataBind();
                grvOpgaven.Visible = true;
                dvOpgaven.Visible = false;
                if (DomeinController.Instance.ProjectBeheerder.GetProjectenDictionary()[usProjecten.SelectedProjectid].inschrijvingBegonnen())
                {
                    lblInschrijving.Text="Opgelet: De inschrijvingen van dit project zijn begonnen.";
                    lblInschrijving.Visible = true;
                }
            }
            else
            {
                grvOpgaven.DataSourceID = null;
                dvOpgaven.Visible = false;
                btnInsert.Visible = false;

            }
            lblMessage.Text = "";
            btnBevestigHardDelete.Visible = false;
        }

        protected void dvOpgaven_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                lblMessage.Text = "Het wijzigen van de update is geannuleerd.";
                e.Cancel=true;
            }            
            
        }

        protected void odsOpgaven_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                if ((bool)e.ReturnValue)
                {
                    lblMessage.Text = "Verwijderen gelukt!";
                }
                else
                {

                    lblMessage.Text = "Nog niets verwijderd!<br>U kunt een CascadeDelete proberen.<br>Die knop is nu zichtbaar gemaakt.";
                    btnBevestigHardDelete.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = e.Exception.Message;
            }
        }
        protected void odsOpgavenCrud_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                if ((bool)e.ReturnValue)
                {
                    lblMessage.Text = "Bijwerken gelukt!";
                }
                else
                {
                    lblMessage.Text = "Bijwerken van record mislukt!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = e.Exception.Message + "<br>" + ex.Message;
            }
        }
        protected void odsOpgavenCrud_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                if ((bool)e.ReturnValue)
                {
                    lblMessage.Text = "Toevoegen gelukt!";
                }
                else
                {
                    lblMessage.Text = "Toevoegen van rij mislukt!";
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = e.Exception.Message + "<br>" + ex.Message;
            }
        }
        protected void btnBevestigHardDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int proj = Int32.Parse(Session["DEL_projectID"].ToString());
                int opgv = Int32.Parse(Session["DEL_opgaveID"].ToString());

                ProjectOpgave po = DomeinController.Instance.ProjectBeheerder.GetOpgaveByProjectID_OpgaveID(
                            proj,
                            opgv);
                if (DomeinController.Instance.ProjectBeheerder.HardDeleteProjectOpgave(po))
                {
                    lblMessage.Text = "Verwijderen gelukt!";
                }
                else
                {
                    lblMessage.Text = "Verwijderen van rij mislukt!";
                }
                btnBevestigHardDelete.Visible = false;
                grvOpgaven.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void grvOpgaven_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["DEL_opgaveID"] = grvOpgaven.DataKeys[0].Value ;
            Session["DEL_projectID"] = usProjecten.SelectedProjectid;
        }
}
