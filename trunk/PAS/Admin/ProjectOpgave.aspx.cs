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
using System.Data.SqlClient;


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
            if (e.Exception != null)
            {
                lblMessage.Text = "Er is een probleem met het verwijderen<br> ";
                if (e.Exception.InnerException != null)
                {
                    lblMessage.Text+= PASMaster.onderzoekException(e.Exception.InnerException);
                }
                e.ExceptionHandled = true;
                btnBevestigHardDelete.Visible = true;
            }
            else
            {
                grvOpgaven.SelectedIndex = -1;
                grvOpgaven.DataBind();
                dvOpgaven.Visible = false;
                btnInsert.Visible = true;
                btnBevestigHardDelete.Visible = false;
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
            dvOpgaven.HeaderText = "<p align='center'>...::Wijzig deze opgave::...</p>";
            dvOpgaven.DataBind();          
            dvOpgaven.Visible = true;
            btnInsert.Visible = false;
            btnBevestigHardDelete.Visible = false;
            lblMessage.Text = "";          
        }      
        protected void dvOpgaven_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                lblMessage.Text = "Er is een probleem met het updaten<br>";
                if (e.Exception.InnerException != null)
                {
                    lblMessage.Text += PASMaster.onderzoekException(e.Exception.InnerException);
                }
                e.ExceptionHandled = true;
                e.KeepInEditMode = true;
            }
            else
            {
                grvOpgaven.SelectedIndex = -1;
                grvOpgaven.DataBind();
                dvOpgaven.Visible = false;
                btnInsert.Visible = true;
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
            dvOpgaven.HeaderText = "<p align='center'>...::Maak een nieuwe opgave aan::...</p>";
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
            if (e.Exception != null)
            {
                lblMessage.Text = "Er is een probleem met het toevoegen<br>";
                if (e.Exception.InnerException != null)
                {
                    lblMessage.Text += PASMaster.onderzoekException(e.Exception.InnerException);
                }
                e.ExceptionHandled = true;
                e.KeepInInsertMode = true;
            }
            else
            {
                grvOpgaven.SelectedIndex = -1;
                dvOpgaven.HeaderText = "<p align='center'>...::Maak een nieuwe opgave aan::...</p>";
                dvOpgaven.Visible = false;
                dvOpgaven.ChangeMode(DetailsViewMode.Edit);
                grvOpgaven.DataBind();
                btnInsert.Visible = true;
            }


        }


        protected void ProjectChanged(object sender, EventArgs e)
        {
            lblInschrijving.Visible = false;
            lblInschrijving.Text = "";
            btnInsert.Visible = true;
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
                    btnInsert.Visible = false;
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
                lblMessage.Text = "Het wijzigen van de update is geannuleerd.<br>";
                e.Cancel=true;
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
