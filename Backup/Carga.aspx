<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carga.aspx.cs" Inherits="WebTelnetPdfCRM.Carga" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Carga de Imagenes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="Form1" runat="server"><asp:ScriptManager runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true" >
        </asp:ScriptManager><h2>Carga</h2>
    <asp:Table ID="Table1" runat="server">
        
       <asp:TableRow ID="TableRow1" runat="server" ForeColor="Teal" BorderStyle="None" VerticalAlign="Top">
              
                           <asp:TableCell>
                    <asp:Label ID="Label1" runat="server" Text="Fecha"></asp:Label><br />
                    <asp:TextBox ID="TextBoxFecha" runat="server" Width="70"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1"
                        runat="server" TargetControlID="TextBoxFecha" Format="dd/MM/yyyy" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ingrese una Fecha" Text="*" ControlToValidate="TextBoxFecha"></asp:RequiredFieldValidator>
                </asp:TableCell>
              <asp:TableCell>
               <asp:Label runat="server" Text="Nº de Imagen"></asp:Label><br />
                      <asp:RadioButtonList ID="CheckBoxListNumImagen" runat="server" OnSelectedIndexChanged="onCheckBoxListNumImageChanged" AutoPostBack="True">
                     <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="2" Value="2"></asp:ListItem>
                      <asp:ListItem Text="3" Value="3"></asp:ListItem>
                      <asp:ListItem Text="4" Value="4"></asp:ListItem>
                      <asp:ListItem Text="5" Value="5"></asp:ListItem>
                      <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                </asp:RadioButtonList>

              </asp:TableCell>
             
    

                <asp:TableCell>
                    <asp:Label ID="Label2" runat="server" Text="Salida"></asp:Label><br />
                    <asp:CheckBoxList ID="CheckBoxListSalida" runat="server"  OnSelectedIndexChanged="onCheckBoxListSalidaChanged" AutoPostBack="True">
                     <asp:ListItem Text="Pospago" Value="1"></asp:ListItem>
                      <asp:ListItem Text="RVPA" Value="2"></asp:ListItem>
                       <asp:ListItem Text="Recargas Prepago" Value="3"></asp:ListItem>
                       <asp:ListItem Text="Intercompany" Value="4"></asp:ListItem>
                       <asp:ListItem Text="Mayorista" Value="5"></asp:ListItem>
                       <asp:ListItem Text="Venta de Kits Callcenter" Value="6"></asp:ListItem>
                       <asp:ListItem Text="Venta de Kits Salesman" Value="7"></asp:ListItem>
                    
                    </asp:CheckBoxList>
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
                    <asp:CheckBoxList ID="CheckBoxListTipo" runat="server">
                      <asp:ListItem Text="Fact" Value="1"  Selected="True" />
                      <asp:ListItem Text="NC" Value="3" />
                                </asp:CheckBoxList>
                      
                      </asp:TableCell>

                       <asp:TableCell>
                         <asp:Label ID="Label5" runat="server" Text="Letra"></asp:Label><br />
                    <asp:CheckBoxList ID="CheckBoxListLetra" runat="server">
                     <asp:ListItem Text="A" Value="A"></asp:ListItem>
                      <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                </asp:CheckBoxList>
                        </asp:TableCell>


                             <asp:TableCell>
                         <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label><br />
                    <asp:CheckBoxList ID="CheckBoxListEmail" runat="server">
                     <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                      <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:CheckBoxList>
                        </asp:TableCell>

                             <asp:TableCell>
                         <asp:Label ID="Label7" runat="server" Text="Adhesion"></asp:Label><br />
                    <asp:CheckBoxList ID="CheckBoxListAdhesion" runat="server">
                     <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                      <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:CheckBoxList>
                        </asp:TableCell>  <asp:TableCell>
                
                <asp:Label ID="Label8" runat="server" Text="Imagen"></asp:Label><br />
                    <asp:FileUpload ID="FileUpload1" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="Ingrese una imagen" Text="*" ControlToValidate="FileUpload1"></asp:RequiredFieldValidator>

               </asp:TableCell>
                        <asp:TableCell> <asp:Button ID="ButtonAgregar" runat="server" Text="Agregar" onclick="ButtonAgregar_Click" />
                           </asp:TableCell>

                            <asp:TableCell>  
                    <asp:Image ID="Image2" runat="server" Visible="false" />  </asp:TableCell>   


            </asp:TableRow>
             <asp:TableRow ID="TableRow2" runat="server" ForeColor="Teal" BorderStyle="None" VerticalAlign="Top">
             <asp:TableCell ColumnSpan="9">
                 <asp:BulletedList ID="BulletedList1" runat="server">
                 </asp:BulletedList>
                 
                  <asp:Label ID="LabelAviso" runat="server" Text="Label"></asp:Label>

                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
             </asp:TableCell>
             </asp:TableRow>
    </asp:Table> 
    </form>
</asp:Content>
