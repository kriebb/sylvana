<%@ Page Language="C#" MasterPageFile="~/MasterPages/PASMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Login" StylesheetTheme="PASTheme" Theme="PASTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<p>Login: <asp:TextBox id="txtLogin" runat="server" ValidationGroup="login"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ControlToValidate="txtLogin"
        ErrorMessage="Login verplicht"></asp:RequiredFieldValidator><br />
Password: <asp:TextBox ID="txtPaswoord" runat="server" TextMode="Password" ValidationGroup="login"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvPaswoord" runat="server" ControlToValidate="txtPaswoord"
        ErrorMessage="Paswoord verplicht" ValidationGroup="login"></asp:RequiredFieldValidator><br/>
<asp:Button id="btnLogin" runat="server" OnClick="btnLogin_Click"
	 Text="Login" ValidationGroup="login"/></p>
    <p>
<asp:Label id="ErrorLabel" runat="Server" ForeColor="Red"
	 Visible="false"/></p>
</asp:Content>

