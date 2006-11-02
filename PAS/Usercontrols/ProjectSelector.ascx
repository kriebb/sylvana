<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProjectSelector.ascx.cs" Inherits="Usercontrols_ProjectSelector" %>
<asp:DropDownList ID="ddlProjecten" runat="server" DataSourceID="odsProjecten" DataTextField="ProjectTitel"
    DataValueField="ProjectId" OnSelectedIndexChanged="ddlProjecten_SelectedIndexChanged">
</asp:DropDownList>
<asp:ObjectDataSource ID="odsProjecten" runat="server" SelectMethod="GetProjecten"
    TypeName="PAS.BLL.ProjectPackage.ProjectBeheerder"></asp:ObjectDataSource>
