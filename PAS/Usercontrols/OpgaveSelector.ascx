<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpgaveSelector.ascx.cs" Inherits="Usercontrols_OpgaveSelector" %>
<%@ Register Src="ProjectSelector.ascx" TagName="ProjectSelector" TagPrefix="uc1" %>
<uc1:ProjectSelector id="ucProjectSelector" runat="server" onProjectChanged = "ProjectChanged">
</uc1:ProjectSelector><br />
<br />
<asp:DropDownList ID="ddlOpgaven" runat="server" OnSelectedIndexChanged="ddlOpgaven_SelectedIndexChanged">
</asp:DropDownList>&nbsp;
