<%@ Page Language="C#" MasterPageFile="~/MasterPages/PASMaster.master" AutoEventWireup="true" CodeFile="ProjectOpgave.aspx.cs" Inherits="Site_ProjectOpgave" Title="Projectopgaven" Theme="PASTheme" StylesheetTheme="PASTheme" %>

<%@ Register Src="../Usercontrols/ProjectSelector.ascx" TagName="ProjectSelector"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ProjectSelector ID="usProjecten" runat="server" OnProjectChanged="ProjectChanged"/>
    &nbsp;&nbsp;<asp:ObjectDataSource ID="odsOpgaven" runat="server" SelectMethod="GetOpgavenByProjectId"
        TypeName="PAS.BLL.ProjectPackage.ProjectBeheerder" DataObjectTypeName="PAS.BLL.ProjectPackage.ProjectOpgave" DeleteMethod="DeleteProjectOpgave" InsertMethod="MakeProjectOpgave" UpdateMethod="UpdateProjectOpgave" OnDeleted="odsOpgaven_Deleted" OnDeleting="odsOpgaven_Deleting">
        <SelectParameters>
            <asp:ControlParameter ControlID="usProjecten" DefaultValue="-1" Name="projectid"
                PropertyName="SelectedProjectid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsOpgavenCrud" runat="server" DataObjectTypeName="PAS.BLL.ProjectPackage.ProjectOpgave"
        DeleteMethod="DeleteProjectOpgave" InsertMethod="MakeProjectOpgave" SelectMethod="GetOpgaveByProjectID_OpgaveID"
        TypeName="PAS.BLL.ProjectPackage.ProjectBeheerder" UpdateMethod="UpdateProjectOpgave" OnInserted="odsOpgavenCrud_Inserted" OnUpdated="odsOpgavenCrud_Updated">
        <SelectParameters>
            <asp:ControlParameter ControlID="usProjecten" DefaultValue="-1" Name="projectid"
                PropertyName="SelectedProjectid" Type="Int32" />
            <asp:ControlParameter ControlID="grvOpgaven" DefaultValue="-1" Name="opgaveid" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br /><br />
    <asp:GridView ID="grvOpgaven" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="OpgaveId,ProjectID,OpgaveTitel,KorteOmschrijving,AantalStudentenPerGroep,AantalGroepen"
        DataSourceID="odsOpgaven" GridLines="None" OnRowCreated="grvOpgaven_RowCreated"
        OnRowDeleted="grvOpgaven_RowDeleted" OnSelectedIndexChanged="grvOpgaven_SelectedIndexChanged" Height="126px" Visible="False" Width="620px" EmptyDataText="Er zijn geen projectopgaven gevonden" OnRowDeleting="grvOpgaven_RowDeleting">
        <Columns>
            <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID"
                Visible="False" />
            <asp:BoundField DataField="OpgaveId" HeaderText="OpgaveId" SortExpression="OpgaveId"
                Visible="False" />
            <asp:BoundField DataField="OpgaveTitel" HeaderText="Titel" SortExpression="OpgaveTitel" />
            <asp:BoundField DataField="KorteOmschrijving" HeaderText="Omschr." SortExpression="KorteOmschrijving" />
            <asp:BoundField DataField="AantalStudentenPerGroep" HeaderText="#S/G" SortExpression="AantalStudentenPerGroep" />
            <asp:BoundField DataField="AantalGroepen" HeaderText="#G" SortExpression="AantalGroepen" />
            <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/icon-cancel.gif" DeleteImageUrl="~/Images/icon-delete.gif"
                EditImageUrl="~/Images/icon-edit.gif" ShowDeleteButton="True" UpdateImageUrl="~/Images/icon-save.gif" SelectImageUrl="~/Images/View.gif" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <asp:DetailsView ID="dvOpgaven" runat="server" AutoGenerateRows="False" BackColor="White"
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="OpgaveId,ProjectID,OpgaveTitel,KorteOmschrijving,AantalGroepen,AantalStudentenPerGroep"
        DataSourceID="odsOpgavenCrud" DefaultMode="Edit" GridLines="Horizontal" Height="1px"
        OnItemUpdated="dvOpgaven_ItemUpdated" OnModeChanged="dvOpgaven_ModeChanged"
        Visible="False" Width="621px" OnItemInserted="dvOpgaven_ItemInserted" OnItemInserting="dvOpgaven_ItemInserting" HorizontalAlign="Center" OnItemUpdating="dvOpgaven_ItemUpdating" EmptyDataText="Geen gedetailleerde gegevens gevonden!">
        <Fields>
            <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID"
                Visible="False" />
            <asp:BoundField DataField="OpgaveId" HeaderText="OpgaveId" SortExpression="OpgaveId"
                Visible="False" />
            <asp:TemplateField HeaderText="OpgaveTitel" SortExpression="OpgaveTitel">
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditOpgaveTitel" runat="server" Text='<%# Bind("OpgaveTitel") %>' CausesValidation="True" MaxLength="50" ValidationGroup="EditInsertError"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqfEditOpgaveTitel" runat="server" ControlToValidate="txtEditOpgaveTitel"
                        CssClass="Validation" Display="Dynamic" ErrorMessage="Verplicht" ValidationGroup="EditInsertError">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtInsertOpgaveTitel" runat="server" Text='<%# Bind("OpgaveTitel") %>' MaxLength="50" ValidationGroup="EditInsertError"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfqInsertOpgaveTitel" runat="server" ControlToValidate="txtInsertOpgaveTitel"
                        CssClass="Validation" Display="Dynamic" ErrorMessage="*" ValidationGroup="EditInsertError">Verplicht!</asp:RequiredFieldValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("OpgaveTitel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="KorteOmschrijving" SortExpression="KorteOmschrijving">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("KorteOmschrijving") %>' MaxLength="50"></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("KorteOmschrijving") %>' Rows="50"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("KorteOmschrijving") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="AantalStudentenPerGroep" SortExpression="AantalStudentenPerGroep">
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditAantalStudentenPerGroep" runat="server" Text='<%# Bind("AantalStudentenPerGroep") %>' ValidationGroup="EditInsertError"></asp:TextBox>&nbsp;
                    <asp:RegularExpressionValidator ID="revEditAantal" runat="server" ControlToValidate="txtEditAantalStudentenPerGroep"
                        CssClass="Validation" Display="Dynamic" ErrorMessage="Moet >= 0 ! Maar is niet vereist"
                        ValidationExpression="[0-9]?[0-9]?[0-9]?" ValidationGroup="EditInsertError">*</asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtInsertAantalStudentenPerGroep" runat="server" Text='<%# Bind("AantalStudentenPerGroep") %>' ValidationGroup="EditInsertError"></asp:TextBox>&nbsp;
                    <asp:RegularExpressionValidator ID="rveInsertAanal" runat="server" ControlToValidate="txtInsertAantalStudentenPerGroep"
                        CssClass="Validation" Display="Dynamic" ErrorMessage="Moet >= 0 ! Maar is niet vereist"
                        ValidationExpression="[0-9]?[0-9]?[0-9]?" ValidationGroup="EditInsertError">*</asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("AantalStudentenPerGroep") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="AantalGroepen" SortExpression="AantalGroepen">
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditAantalGroepen" runat="server" Text='<%# Bind("AantalGroepen") %>' ValidationGroup="EditInsertError"></asp:TextBox>&nbsp;
                    <asp:RegularExpressionValidator ID="rveEditGroep" runat="server" ControlToValidate="txtEditAantalGroepen"
                        CssClass="Validation" Display="Dynamic" ErrorMessage="Moet >= 0 ! Maar is niet vereist"
                        ValidationExpression="[0-9]?[0-9]?[0-9]?" ValidationGroup="EditInsertError">*</asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="txtInsertAantalGroepen" runat="server" Text='<%# Bind("AantalGroepen") %>' ValidationGroup="EditInsertError"></asp:TextBox>
                    &nbsp;
                    <asp:RegularExpressionValidator ID="rveInsertGroep" runat="server" ControlToValidate="txtInsertAantalGroepen"
                        CssClass="Validation" Display="Dynamic" ErrorMessage="Moet >= 0 ! Maar is niet vereist"
                        ValidationExpression="[0-9]?[0-9]?[0-9]?" ValidationGroup="EditInsertError">*</asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("AantalGroepen") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update"
                        ImageUrl="~/Images/icon-save.gif" Text="Update" ValidationGroup="EditInsertError" />&nbsp;<asp:ImageButton
                            ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                            ImageUrl="~/Images/icon-cancel.gif" Text="Cancel" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Insert"
                        ImageUrl="~/Images/icon-save.gif" Text="Insert" ValidationGroup="EditInsertError" />&nbsp;<asp:ImageButton
                            ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                            ImageUrl="~/Images/icon-cancel.gif" Text="Cancel" />
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        ImageUrl="~/Images/icon-edit.gif" Text="Edit" />&nbsp;<asp:ImageButton ID="ImageButton2"
                            runat="server" CausesValidation="False" CommandName="New" ImageUrl="~/Images/icon-edit.gif"
                            Text="New" />
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
    <asp:Label ID="lblMessage" runat="server" SkinID="warning"></asp:Label><br />
    <asp:ValidationSummary ID="vsInsertEdit" runat="server" CssClass="Validation" ValidationGroup="EditInsertError" />
    <br />
    <asp:Button ID="btnInsert" runat="server" CssClass="button" OnClick="btnInsert_Click"
        Text="InsertModus" />
    <asp:Button ID="btnBevestigHardDelete" runat="server" OnClick="btnBevestigHardDelete_Click"
        Text="Bevestig verwijder!" Visible="False" /><br />


</asp:Content>

