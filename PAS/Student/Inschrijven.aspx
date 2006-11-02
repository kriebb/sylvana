<%@ Page Language="C#" MasterPageFile="~/MasterPages/PASMaster.master" AutoEventWireup="true" CodeFile="Inschrijven.aspx.cs" Inherits="Student_Inschrijven" Title="Inschrijven" StylesheetTheme="PASTheme" Theme="PASTheme" %>

<%@ Register Src="../Usercontrols/OpgaveSelector.ascx" TagName="OpgaveSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:OpgaveSelector ID="ucOpgaveSelector" runat="server" />
</asp:Content>

