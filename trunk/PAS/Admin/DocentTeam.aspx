<%@ Page Language="C#" MasterPageFile="~/MasterPages/PASMaster.master" AutoEventWireup="true" CodeFile="DocentTeam.aspx.cs" Inherits="Site_DocentTeam" Title="Docentteams" Theme="PASTheme" StylesheetTheme="PASTheme" %>

<%@ Register Src="../Usercontrols/OpgaveSelector.ascx" TagName="OpgaveSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:OpgaveSelector ID="ucOpgaveSelector" runat="server" OnOpgaveChanged="OpgaveChanged" />
    &nbsp;<br />
    <br />
    <asp:Panel runat="server" ID="pnlTeams">
    <asp:DropDownList ID="ddlTeams" runat="server" DataSourceID="odsDocentTeams"
        DataTextField="DocentTeamId" DataValueField="DocentTeamId" OnSelectedIndexChanged="ddlTeams_SelectedIndexChanged">
    </asp:DropDownList><br />
    <asp:ObjectDataSource ID="odsDocentTeams" runat="server" SelectMethod="GetDocentTeam_ValueCollection" TypeName="PAS.BLL.DocentPackage.DocentBeheerder">
        <SelectParameters>
            <asp:ControlParameter ControlID="ucOpgaveSelector" Name="opgaveid" PropertyName="SelectedOpgaveId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:Button ID="btnNieuwTeam" runat="server" Text="Maak nieuw team" OnClick="btnNieuwTeam_Click" />&nbsp;
    <asp:Button ID="btnVerwijderTeam" runat="server" Text="Verwijder team" OnClick="btnVerwijderTeam_Click" /><br />
    <br />
        &nbsp;&nbsp;
        <asp:GridView ID="grvProjectLuiken" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="ProjectLuikNaam" SortExpression="ProjectLuikNaam">
                    <EditItemTemplate>
                        &nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLuiknaam" runat="server" Text='<%# Bind("ProjectLuikNaam") %>'></asp:Label>
                        <asp:Label ID="lblLuikID" runat="server" Text='<%# bind("ProjectLuikID") %>' Visible="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DocentNaam" SortExpression="DocentNaam">
                    <EditItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlDocenten" runat="server" DataSourceID="odsDocenten" DataTextField="NaamVoornaam" DataValueField="DocentID" >
                        </asp:DropDownList><asp:ObjectDataSource ID="odsDocenten" runat="server" SelectMethod="GetDocentenValues"
                            TypeName="PAS.BLL.DocentPackage.DocentBeheerder"></asp:ObjectDataSource>
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsDocentInDocentTeam" runat="server" SelectMethod="GetLuikenEnDocenten"
            TypeName="PAS.BLL.DocentPackage.DocentBeheerder">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlTeams" Name="docentTeamID" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="ucOpgaveSelector" Name="projectID" PropertyName="SelectedProjectID"
                    Type="Int32" />
                <asp:ControlParameter ControlID="ucOpgaveSelector" Name="opgaveID" PropertyName="SelectedOpgaveID"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Button ID="btnUpdate" runat="server" Text="Update Team" OnClick="btnUpdate_Click" /><br />
        <asp:Label ID="lblMessage" runat="server" Text="Label" Visible="false"></asp:Label></asp:Panel>
</asp:Content>

