using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.Security;
using System.Collections.Specialized;
using System.Configuration;

namespace WebTelnetPdfCRM
{
    public partial class Consulta : System.Web.UI.Page
    {

        DataSet ds = new DataSet();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            DateTime dFechaHoy = System.DateTime.Today;
            DateTime d1MEs = dFechaHoy.AddMonths(-1);
   
            CalendarExtenderHasta.SelectedDate = dFechaHoy;

            /*
       
            */

        }

        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {

           TelnetData.TelnetData td = new TelnetData.TelnetData();
           TelnetData.ClassParametrosTelnetData cp = new TelnetData.ClassParametrosTelnetData();
           NameValueCollection appSettings = ConfigurationManager.AppSettings;

           cp.Archivo = appSettings["Archivo"].ToString();
           cp.Archivo2 = appSettings["Archivo2"].ToString();
           cp.CantidadCamposxRegistro = appSettings["CantidadCamposxRegistro"].ToString();
           cp.CnnStringSqlServerSinPassword = appSettings["CnnStringSqlServerSinPassword"].ToString();
           cp.CuentaUniverse = appSettings["CuentaUniverse"].ToString();
           cp.IVA = appSettings["IVA"].ToString();
           cp.PasswordSqlServerEncriptado = appSettings["PasswordSqlServerEncriptado"].ToString();
           cp.PasswordUniverseEncriptado = appSettings["PasswordUniverseEncriptado"].ToString();
           cp.Path = appSettings["Path"].ToString();
           cp.Path2 = appSettings["Path2"].ToString();
           cp.ServerUniverse = appSettings["ServerUniverse"].ToString();
           cp.UsuarioUniverse = appSettings["UsuarioUniverse"].ToString();
          


          // IList<string> l1 = td.fGetImagenesComprobante(cp, "1", "A", "1", "1", "1", "02/02/2005");
           
            
         //  ClassSqlserver cs = new ClassSqlserver();
         //  DataSet ds = cs.DatosBusqueda(TextBoxFechaDesde.Text,TextBoxFechaHasta.Text,1);

           string fecha = TextBoxFechaHasta.Text;
           string salida = CheckBoxListSalida.SelectedValue;
           string letra = CheckBoxListLetra.SelectedValue;
           string tipo = CheckBoxListTipo.SelectedValue;
           string email = CheckBoxListEmail.SelectedValue;
           string adhesion = CheckBoxListAdhesion.SelectedValue;
           string sEmpresa;

           if (RadioButtonListEmpresa.SelectedValue == "Telcom")
               sEmpresa = "1";
           else
               sEmpresa = "2";

           GridView1.DataSource = td.fGetDataTableComprobante(cp, salida, letra, tipo, email, adhesion, fecha,sEmpresa);
           GridView1.DataBind();

           Image2.Width = 280;
           Image2.Height = 450;
           switch (salida)
           {
               case "1":
                   Image2.Visible = true;
                   Image2.ImageUrl = "ImageCSharp.aspx?FileName=f_sal1.jpg";
                   Image2.DataBind();
                   break;

               case "2":
               case "3":
               case "5":
               case "6":
               case "7":
                   Image2.Visible = true;
                   Image2.ImageUrl = "ImageCSharp.aspx?FileName=f_sal2.jpg";
                   Image2.DataBind();
                   break;

               case "4":
                   Image2.Visible = false;
                   break;

           }
           

           

          
               
          

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            ImageButton ibtn1 = sender as ImageButton;
            int rowIndex = Convert.ToInt32(ibtn1.Attributes["RowIndex"]);
            string imagen = (string)GridView1.DataKeys[rowIndex].Value;
            string ImageFile = appSettings["ImageRouteTelnetPdf"].ToString() + imagen; 
           // string ImageFile = @"C:\\Inetpub\\wwwroot\\wsGenerarPDF\\Images\\" + imagen;
            string ImagePath = @"../ImgAvisos/" + imagen;
            System.Drawing.Image img = System.Drawing.Image.FromFile(ImageFile);
            //Image1.ImageUrl = im1;
            int x = img.Width;
            int y= img.Height;
            int[] proporcion = ArrayProporcion(x, y, 400);
            
            
             Image1.Width = proporcion[0];
             Image1.Height = proporcion[1];
             Image1.ImageUrl = "ImageCSharp.aspx?FileName=" + imagen;
             Image1.DataBind();
            ModalPopupExtender1.Show();
          
        }

        public int[] ArrayProporcion(int sourceWidth, int sourceHeight, int maxval)
        {
            int[] retvalue= new int[2];

            var ratioX = (double)maxval / sourceWidth;
            var ratioY = (double)maxval / sourceHeight;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(sourceWidth * ratio);
            var newHeight = (int)(sourceHeight * ratio);


            retvalue[0] = newWidth;
            retvalue[1] = newHeight;
            return retvalue;

        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TelnetData.TelnetData td = new TelnetData.TelnetData();
            TelnetData.ClassParametrosTelnetData cp = new TelnetData.ClassParametrosTelnetData();
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            cp.Archivo = appSettings["Archivo"].ToString();
            cp.Archivo2 = appSettings["Archivo2"].ToString();
            cp.CantidadCamposxRegistro = appSettings["CantidadCamposxRegistro"].ToString();
            cp.CnnStringSqlServerSinPassword = appSettings["CnnStringSqlServerSinPassword"].ToString();
            cp.CuentaUniverse = appSettings["CuentaUniverse"].ToString();
            cp.IVA = appSettings["IVA"].ToString();
            cp.PasswordSqlServerEncriptado = appSettings["PasswordSqlServerEncriptado"].ToString();
            cp.PasswordUniverseEncriptado = appSettings["PasswordUniverseEncriptado"].ToString();
            cp.Path = appSettings["Path"].ToString();
            cp.Path2 = appSettings["Path2"].ToString();
            cp.ServerUniverse = appSettings["ServerUniverse"].ToString();
            cp.UsuarioUniverse = appSettings["UsuarioUniverse"].ToString();

         // var d= td.InsertaImagenes(cp,1,"10/10/2015","prerp.jpg",1,1,"A",1,1);
         
          
        }
    }
}