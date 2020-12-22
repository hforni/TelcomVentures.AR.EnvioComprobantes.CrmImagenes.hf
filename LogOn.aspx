<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogOn.aspx.cs" Inherits="WebTelnetPdfCRM.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Iniciar Sesión
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<form id="form1" runat="server" >
 <table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td align="center">
<asp:Login ID = "Login1" runat = "server" OnAuthenticate= "ValidateUser"></asp:Login>
 </td>
</tr>
</table>
</form>
</asp:Content>
