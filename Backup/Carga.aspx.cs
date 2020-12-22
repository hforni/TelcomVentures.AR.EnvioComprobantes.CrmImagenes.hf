using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections.Specialized;
using System.Configuration;
using System.Security;
using Logger;

 
namespace WebTelnetPdfCRM
{


    public partial class Carga : System.Web.UI.Page
    {
        Logger.Logger logger = new Logger.Logger();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            LabelAviso.Text = "";
            checkvalues();
        }

        protected void ButtonAgregar_Click(object sender, EventArgs e)
        {
        
            string User = (string)HttpContext.Current.Session["User"].ToString();
            string sEmpresa ;
            DateTime fechaTomorrow = DateTime.Now.AddDays(1);
            DateTime fechaCargada = DateTime.Parse(TextBoxFecha.Text);
       //    if (fechaCargada <= fechaTomorrow)
           // {
           //     RequiredFieldValidator2.Visible = true;
          //      ErrorSummary.AddError("La fecha no puede ser menor al dia de Mañana", this);
           
         //  }
        //    else
       //  {


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
                string filePath = appSettings["ImageRouteTelnetPdf"].ToString();


                bool subida = false;
                List<string> listaResultado = new List<string>();

            if (RadioButtonListEmpresa.SelectedValue=="Telcom")
                sEmpresa = "1";
            else
                sEmpresa = "2";

               // if (sEmpresa == "Telcom")
              //  {
                    List<ListItem> selectedSalida = CheckBoxListSalida.Items.Cast<ListItem>()
                                             .Where(li => li.Selected)
                                             .ToList();

                    List<ListItem> selectedLetra = CheckBoxListLetra.Items.Cast<ListItem>()
                                             .Where(li => li.Selected)
                                             .ToList();

                    List<ListItem> selectedTipo = CheckBoxListTipo.Items.Cast<ListItem>()
                                         .Where(li => li.Selected)
                                         .ToList();

                    List<ListItem> selectedEmail = CheckBoxListEmail.Items.Cast<ListItem>()
                                         .Where(li => li.Selected)
                                         .ToList();

                    List<ListItem> selectedAdhesion = CheckBoxListAdhesion.Items.Cast<ListItem>()
                                         .Where(li => li.Selected)
                                         .ToList();


                    int oImagen = System.Convert.ToInt32(CheckBoxListNumImagen.SelectedValue);


                    foreach (ListItem oListItem in selectedSalida)
                    {
                        foreach (ListItem oListLetra in selectedLetra)
                        {
                            foreach (ListItem oListTipo in selectedTipo)
                            {
                                foreach (ListItem oListEmail in selectedEmail)
                                {

                                    foreach (ListItem oListAdhesion in selectedAdhesion)
                                    {

                                        int oSalida = System.Convert.ToInt32(oListItem.Value);
                                        string oLetra = oListLetra.Value;
                                        int oTipo = System.Convert.ToInt32(oListTipo.Value);
                                        int oEmail = System.Convert.ToInt32(oListEmail.Value);
                                        int oAdhesion = System.Convert.ToInt32(oListAdhesion.Value);


                                        ClassSqlserver cs = new ClassSqlserver();
                                        if (FileUpload1.HasFile)
                                        {
                                            //if (cs.VerificarRegistro(TextBoxFecha.Text, oSalida, oTipo, oLetra, oEmail, oAdhesion) == false)
                                            //{
                                                string filename = sGuidName();
                                                string sfile=SaveFile(FileUpload1.PostedFile, filename);
                                                subida = td.InsertaImagenesSQL(cp, oImagen, TextBoxFecha.Text, filename, oSalida, oTipo, oLetra, oEmail, oAdhesion, sEmpresa, User);
                                               //     , filename, oSalida, oTipo, oLetra, oEmail, oAdhesion);
                                                    // cs.InsertarRegistro(TextBoxFecha.Text, sfile, oSalida, "Telcom", oTipo, oLetra, oEmail, oAdhesion);
                                            
 
                                           // }
                                        }
                                        else
                                        {
                                            subida = false;
                                        }

                                        string semail = (oEmail == 1) ? "Tiene email " : "No tiene Email";
                                        string sAdherido = (oAdhesion == 1) ? "Adherido Email " : "No Adherido Email ";
                                        string sImagen = "Fecha : " + TextBoxFecha.Text + " Salida: " + fSalida(oSalida) + " " + fTipo(oTipo) + " " + oLetra + " " + semail + " " + sAdherido;
                                        string smensaje = (subida == false) ? " La imagen ya estaba subida " + sImagen : "Imagen subida correctamente " + sImagen;
                                        listaResultado.Add(smensaje);
                                    }
                                }
                            }
                        }

                    }
                    displayMembers(listaResultado);
                    BlanquearCampos();
               // }

         //if fecha  }
        }

        private string SaveFile(HttpPostedFile file,string filename)
        {
            try
            {
                NameValueCollection appSettings = ConfigurationManager.AppSettings;
                string savePath = appSettings["SavePAth"].ToString();        

                // Get the name of the file to upload.
                string fileName = filename;
                string pathToCheck = savePath + fileName;
                string tempfileName = "";
                
                if (System.IO.File.Exists(pathToCheck))
                {
                    logger.log("pathToCheck: " + pathToCheck);
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = savePath + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;
                    // Notify the user that the file name was changed.
                    LabelAviso.Text = "A file with the same name already exists." +
                        "<br />Your file was saved as " + fileName;
                }

                else
                {

                    savePath += fileName;
                    // Call the SaveAs method to save the uploaded
                    // file to the specified directory.
                    FileUpload1.SaveAs(savePath);

                }
                return filename;
            }
            catch (Exception ex)
            {
               string Error = ex.Message;
               logger.log(ex.Message + ": Carga.aspx SaveFile");
               return "";
            }

        }


        private void BlanquearCampos()
        {
            TextBoxFecha.Text = "";
            foreach (ListItem item in CheckBoxListSalida.Items)
            {
            //check anything out here
            if (item.Selected)
              item.Selected= false;
            }

            foreach (ListItem item in CheckBoxListTipo.Items)
            {
            //check anything out here
            if (item.Selected)
              item.Selected= false;
            }



            foreach (ListItem item in CheckBoxListLetra.Items)
            {
            //check anything out here
            if (item.Selected)
              item.Selected= false;
            }


            foreach (ListItem item in CheckBoxListEmail.Items)
            {
            //check anything out here
            if (item.Selected)
              item.Selected= false;
            }


            foreach (ListItem item in CheckBoxListAdhesion.Items)
            {
            //check anything out here
            if (item.Selected)
              item.Selected= false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BlanquearCampos();
        }

        private string sGuidName()
        {
            string retvalue;
            Guid g = Guid.NewGuid();
            string GuidString = System.Convert.ToString(g);
            GuidString = GuidString.Replace("-", "");
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("\\", "");
            retvalue = GuidString + ".png";
            return retvalue;
        }

        private string fSalida(int ivalor)
        { 
        string retvalue;
        retvalue = "";
        switch(ivalor)
            {
                case(1):
                retvalue="Pospago";
                break;
              case(2):
                retvalue = "RPA";
                break;
              case (3):
                retvalue = "Recargas Prepago";
                break;
              case (4):
                retvalue = "Intercompany";
                break;
              case (5):
                retvalue = "Mayorista";
                break;
              case (6):
                retvalue = "Venta de Kits";
                break;
              case (7):
                retvalue = "Salesman";
                break;
            }

        return retvalue;

        }

        private void displayMembers(List<String> respuestas)
        {
            var text = string.Empty;
            foreach (String s in respuestas)
            {
                BulletedList1.Items.Add(s.ToString());
              
            }
          
        }


        private string fTipo(int ivalor)
        {
            string retvalue;
            retvalue = "";
            switch (ivalor)
            {
                case (2):
                    retvalue = "Factura";
                    break;
                case (3):
                    retvalue = "Nota de Crédito";
                    break;
    
            }

            return retvalue;

        }


        protected void onCheckBoxListNumImageChanged(object sender, EventArgs e)
        {
            checkvalues();
        }


        protected void onCheckBoxListSalidaChanged(object sender, EventArgs e)
        {
            checkvalues();
        }



        private void checkvalues()
        {
            Image2.Width = 280;
            Image2.Height = 450;
               if (CheckBoxListNumImagen.SelectedItem.Text == "1")
            {

                ListItem listItem = this.CheckBoxListSalida.Items.FindByText("RVPA");
                if(listItem != null) listItem.Selected = false;
                listItem = this.CheckBoxListSalida.Items.FindByText("Recargas Prepago");
                if(listItem != null) listItem.Selected = false;
                listItem = this.CheckBoxListSalida.Items.FindByText("Intercompany");
                if(listItem != null) listItem.Selected = false;
                listItem = this.CheckBoxListSalida.Items.FindByText("Mayorista");
                if(listItem != null) listItem.Selected = false;
                listItem = this.CheckBoxListSalida.Items.FindByText("Venta de Kits Callcenter");
                if(listItem != null) listItem.Selected = false;
                listItem = this.CheckBoxListSalida.Items.FindByText("Venta de Kits Salesman");
                if(listItem != null) listItem.Selected = false;

                Image2.Visible = true;
                Image2.ImageUrl = "ImageCSharp.aspx?FileName=f_sal1.jpg";
                Image2.DataBind();


            }

               if ((CheckBoxListNumImagen.SelectedItem.Text == "2") || (CheckBoxListNumImagen.SelectedItem.Text == "3") || (CheckBoxListNumImagen.SelectedItem.Text == "4") || (CheckBoxListNumImagen.SelectedItem.Text == "5") || (CheckBoxListNumImagen.SelectedItem.Text == "6"))
               {
                   ListItem listItem = this.CheckBoxListSalida.Items.FindByText("Pospago");
                   if (listItem != null) listItem.Selected = false;

                   Image2.Visible = true;
                   Image2.ImageUrl = "ImageCSharp.aspx?FileName=f_sal2.jpg";
                   Image2.DataBind();
               }
        
        }
     
    }


    public class ErrorSummary : IValidator
    {
        string _message;

        public static void AddError(string message, Page page)
        {
            ErrorSummary error = new ErrorSummary(message);
            page.Validators.Add(error);
        }

        private ErrorSummary(string message)
        {
            _message = message;
        }

        public string ErrorMessage
        {
            get { return _message; }
            set { }
        }

        public bool IsValid
        {
            get { return false; }
            set { }
        }

        public void Validate()
        { }
    }
}