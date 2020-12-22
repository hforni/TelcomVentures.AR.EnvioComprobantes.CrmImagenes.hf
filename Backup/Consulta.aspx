<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="WebTelnetPdfCRM.Consulta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">




<form id="Form1" runat="server" ><asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>  
        <h2>Consulta</h2>
    <asp:Table ID="Table1" runat="server"    CellPadding = "0" 
            CellSpacing="0" BorderStyle="None">
      
 <asp:TableRow ID="TableRow1" runat="server"  BorderStyle="None" VerticalAlign="Top">

  <asp:TableCell>
  Fecha:<asp:TextBox ID="TextBoxFechaHasta" runat="server" Width="70"></asp:TextBox><cc1:CalendarExtender
      ID="CalendarExtenderHasta" runat="server" TargetControlID="TextBoxFechaHasta" Format="dd/MM/yyyy" />
   </asp:TableCell>


   <asp:TableCell>
   <asp:Label ID="Label2" runat="server" Text="Salida"></asp:Label><br />
                       <asp:RadioButtonList ID="CheckBoxListSalida" runat="server">
                     <asp:ListItem Text="Pospago" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="RVPA" Value="2"></asp:ListItem>
                       <asp:ListItem Text="Recargas Prepago" Value="3"></asp:ListItem>
                       <asp:ListItem Text="Intercompany" Value="4"></asp:ListItem>
                       <asp:ListItem Text="Mayorista" Value="5"></asp:ListItem>
                       <asp:ListItem Text="Venta de Kits Callcenter" Value="6"></asp:ListItem>
                       <asp:ListItem Text="Venta de Kits Salesman" Value="7"></asp:ListItem>
 
  </asp:RadioButtonList>
  </asp:TableCell>
   
  <asp:TableCell>

                            <asp:Label ID="Label3" runat="server" Text="Empresa"></asp:Label><br />
                         <asp:RadioButtonList ID="RadioButtonListEmpresa" runat="server">
                         <asp:ListItem  Selected="True">Telcom</asp:ListItem>
                    <asp:ListItem>Eurosat</asp:ListItem>
                         </asp:RadioButtonList>
</asp:TableCell>

<asp:TableCell> 
               <asp:Label ID="Label4" runat="server" Text="Tipo"></asp:Label><br />
                 


               <asp:RadioButtonList ID="CheckBoxListTipo" runat="server">
    <asp:ListItem Text="Fact" Value="1"  Selected="True" />
    <asp:ListItem Text="NC" Value="3" />
</asp:RadioButtonList>

                      
                      </asp:TableCell>
                       <asp:TableCell>
                         <asp:Label ID="Label5" runat="server" Text="Letra"></asp:Label><br />
                    <asp:RadioButtonList  ID="CheckBoxListLetra" runat="server">
                     <asp:ListItem Text="A" Value="A" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                </asp:RadioButtonList>
              

                          </asp:TableCell>
                             <asp:TableCell>
                         <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label><br />
                    <asp:RadioButtonList ID="CheckBoxListEmail" runat="server">
                     <asp:ListItem Text="Si" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="No" Value="0"></asp:ListItem>
                               </asp:RadioButtonList>
                        </asp:TableCell>

                             <asp:TableCell>
                         <asp:Label ID="Label7" runat="server" Text="Adhesion"></asp:Label><br />
                    <asp:RadioButtonList ID="CheckBoxListAdhesion" runat="server">
                     <asp:ListItem Text="Si" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
      </asp:TableCell>
      <asp:TableCell>  <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" 
        onclick="ButtonBuscar_Click" />  </asp:TableCell>
                <asp:TableCell>  
                    <asp:Image ID="Image2" runat="server" Visible="false" />  </asp:TableCell>                
 </asp:TableRow>
 </asp:Table>
 <br /><br />

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    EmptyDataText="No se encontraron imagenes" DataKeyNames="Imagen" >
<Columns> 

<asp:BoundField DataField="Numero" HeaderText="Numero Imagen" ReadOnly="true" /> 
<asp:BoundField DataField="Salida" HeaderText="Salida" ReadOnly="true" /> 
<asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="true" DataFormatString="{0:d}" />   
<asp:BoundField DataField="Empresa" HeaderText="Empresa" ReadOnly="true" />  
<asp:BoundField DataField="Tipo" HeaderText="Tipo" ReadOnly="true" />  
<asp:BoundField DataField="Letra" HeaderText="Letra" ReadOnly="true" />  
<asp:BoundField DataField="Correo" HeaderText="Correo" ReadOnly="true" />  
<asp:BoundField DataField="Adhesion" HeaderText="Adhesion" ReadOnly="true" />  
  <asp:TemplateField HeaderText="Ver">
	<ItemTemplate>
        <asp:ImageButton ID="ImageButton1" runat="server" 
    ImageUrl="~/images/search.png" onclick="ImageButton1_Click"   RowIndex='<%# Container.DisplayIndex %>' />
   

	</ItemTemplate>
	</asp:TemplateField>
</Columns>
</asp:GridView>



<asp:Panel ID="panel_Popup" runat="server" CssClass="ModalPopup" Width="500" Height="450" BackColor="White" style="display:none" >
    <asp:Table ID="Table2" runat="server" Width="100%">
    <asp:TableRow runat="server"><asp:TableCell HorizontalAlign="Center" >Imagen</asp:TableCell></asp:TableRow>
    <asp:TableRow runat="server"><asp:TableCell HorizontalAlign="Center" > <asp:Image ID="Image1" runat="server" 
        ImageUrl="~/ImgAvisos/1b9782218d0b417992e3e5fc129b8d7c.png" Height="210" 
        Width="250" style="text-align: center" /> </asp:TableCell></asp:TableRow>
     
   <asp:TableRow runat="server"><asp:TableCell  HorizontalAlign="Center" ><asp:Button ID="btn_Cancel" runat="server" Text="Cerrar" /></asp:TableCell></asp:TableRow>
</asp:Table>     <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="panel_Popup"
     CancelControlID="btn_Cancel"   TargetControlID="BtnTarget" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender> <asp:Button ID="BtnTarget" runat="server" Text="Target" Style="display: none" />
</asp:Panel>
 </form>
</asp:Content>
