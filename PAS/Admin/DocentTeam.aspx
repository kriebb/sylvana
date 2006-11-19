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
    <asp:GridView ID="grvProjectLuiken" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Luik" SortExpression="LuikNaam">
                <EditItemTemplate>
                    <asp:Label ID="lblLuik" runat="server" Text='<%# bind("LuikNaam") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblLuikTitel" runat="server" Text='<%# Bind("LuikNaam") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Docent">
                <EditItemTemplate>
                    &nbsp;
                </EditItemTemplate>
                <ItemTemplate>
                    &nbsp;<asp:DropDownList ID="ddlDocenten" runat="server" DataSourceID="odsDocenten" DataTextField='NaamVoornaam'
                        DataValueField="DocentId" SelectedValue='<%# bind("docentid") %>'>
                    </asp:DropDownList><asp:ObjectDataSource ID="odsDocenten" runat="server" SelectMethod="SelecteerDocenten"
                        TypeName="PAS.BLL.DocentPackage.DocentBeheerder"></asp:ObjectDataSource>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Luik ID" Visible="False">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LuikID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblLuikId" runat="server" Text='<%# Bind("LuikID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <asp:ObjectDataSource ID="odsLuiken" runat="server" SelectMethod="SelecteerLuikenByProjectAndTeam" TypeName="PAS.BLL.DocentPackage.DocentBeheerder">
            <SelectParameters>
                <asp:ControlParameter ControlID="ucOpgaveSelector" Name="projectid" PropertyName="SelectedProjectId"
                    Type="Int32" />
                <asp:ControlParameter ControlID="ddlTeams" Name="teamid" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Button ID="btnUpdate" runat="server" Text="Update Team" OnClick="btnUpdate_Click" />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="Label" Visible="false"></asp:Label></asp:Panel>
</asp:Content>

