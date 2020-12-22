namespace WebTelnetPdfCRM
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using System.Security;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Logger;

    public partial class Carga : System.Web.UI.Page
    {
        Logger logger = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            this.LabelAviso.Text = string.Empty;
            this.checkvalues();
        }

        protected void ButtonAgregar_Click(object sender, EventArgs e)
        {

            string user = (string)HttpContext.Current.Session["User"].ToString();
            string sEmpresa;
            DateTime fechaTomorrow = DateTime.Now.AddDays(1);
            DateTime fechaCargada = DateTime.Parse(TextBoxFecha.Text);
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

            if (this.RadioButtonListEmpresa.SelectedValue == "Telcom")
            {
                sEmpresa = "1";
            }
            else
            {
                sEmpresa = "2";
            }

            List<ListItem> selectedSalida = this.CheckBoxListSalida.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .ToList();

            List<ListItem> selectedLetra = this.CheckBoxListLetra.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .ToList();

            List<ListItem> selectedTipo = this.CheckBoxListTipo.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .ToList();

            List<ListItem> selectedEmail = this.CheckBoxListEmail.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .ToList();

            List<ListItem> selectedAdhesion = this.CheckBoxListAdhesion.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .ToList();


            int oImagen = System.Convert.ToInt32(this.CheckBoxListNumImagen.SelectedValue);


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
                                if (this.FileUpload1.HasFile)
                                {
                                    string filename = this.GuidName();
                                    string sfile = this.SaveFile(this.FileUpload1.PostedFile, filename);
                                    subida = td.InsertaImagenesSQL(oImagen, this.TextBoxFecha.Text, filename, oSalida, oTipo, oLetra, oEmail, oAdhesion, sEmpresa, user);                                  
                                }
                                else
                                {
                                    subida = false;
                                }

                                string email = (oEmail == 1) ? "Tiene email " : "No tiene Email";
                                string adherido = (oAdhesion == 1) ? "Adherido Email " : "No Adherido Email ";
                                string imagen = "Fecha : " + this.TextBoxFecha.Text + " Salida: " + this.CategoriaComprobante(oSalida) + " " + this.TipoComprobante(oTipo) + " " + oLetra + " " + email + " " + adherido;
                                string mensaje = (subida == false) ? " La imagen ya estaba subida " + imagen : "Imagen subida correctamente " + imagen;
                                listaResultado.Add(mensaje);
                            }
                        }
                    }
                }

            }
            this.DisplayMembers(listaResultado);
            this.BlanquearCampos();

        }

        private string SaveFile(HttpPostedFile file, string filename)
        {
            try
            {
                NameValueCollection appSettings = ConfigurationManager.AppSettings;
                string savePath = appSettings["SavePAth"].ToString();

                // Get the name of the file to upload.
                string fileName = filename;
                string pathToCheck = savePath + fileName;
                string tempfileName = string.Empty;

                if (System.IO.File.Exists(pathToCheck))
                {
                    this.logger.log("pathToCheck: " + pathToCheck);
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
                    this.LabelAviso.Text = "A file with the same name already exists." +
                    "<br />Your file was saved as " + fileName;
                }
                else
                {
                    savePath += fileName;
                    // Call the SaveAs method to save the uploaded
                    // file to the specified directory.
                    this.FileUpload1.SaveAs(savePath);
                }
                return filename;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                this.logger.log(ex.Message + ": Carga.aspx SaveFile");
                return string.Empty;
            }

        }


        private void BlanquearCampos()
        {
            this.TextBoxFecha.Text = string.Empty;
            foreach (ListItem item in this.CheckBoxListSalida.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                }
            }

            foreach (ListItem item in CheckBoxListTipo.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                }
            }



            foreach (ListItem item in CheckBoxListLetra.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                }
            }


            foreach (ListItem item in CheckBoxListEmail.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                }
            }


            foreach (ListItem item in CheckBoxListAdhesion.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.BlanquearCampos();
        }

        private string GuidName()
        {
            string retvalue;
            Guid g = Guid.NewGuid();
            string guidString = System.Convert.ToString(g);
            guidString = guidString.Replace("-", string.Empty);
            guidString = guidString.Replace("=", string.Empty);
            guidString = guidString.Replace("+", string.Empty);
            guidString = guidString.Replace("\\", string.Empty);
            retvalue = guidString + ".png";
            return retvalue;
        }

        private string CategoriaComprobante(int ivalor)
        {
            string retvalue;
            retvalue = string.Empty;
            switch (ivalor)
            {
                case 1:
                    retvalue = "Pospago";
                    break;
                case 2:
                    retvalue = "RPA";
                    break;
                case 3:
                    retvalue = "Recargas Prepago";
                    break;
                case 4:
                    retvalue = "Intercompany";
                    break;
                case 5:
                    retvalue = "Mayorista";
                    break;
                case 6:
                    retvalue = "Venta de Kits";
                    break;
                case 7:
                    retvalue = "Salesman";
                    break;
            }

            return retvalue;

        }

        private void DisplayMembers(List<String> respuestas)
        {
            var text = string.Empty;
            foreach (String s in respuestas)
            {
                this.BulletedList1.Items.Add(s.ToString());

            }

        }


        private string TipoComprobante(int ivalor)
        {
            string retvalue;
            retvalue = string.Empty;
            switch (ivalor)
            {
                case 2:
                    retvalue = "Factura";
                    break;
                case 3:
                    retvalue = "Nota de Crédito";
                    break;

            }

            return retvalue;

        }


        protected void onCheckBoxListNumImageChanged(object sender, EventArgs e)
        {
            this.checkvalues();
        }


        protected void onCheckBoxListSalidaChanged(object sender, EventArgs e)
        {
            this.checkvalues();
        }



        private void checkvalues()
        {
            this.Image2.Width = 280;
            this.Image2.Height = 450;
            if (this.CheckBoxListNumImagen.SelectedItem.Text == "1")
            {

                ListItem listItem = this.CheckBoxListSalida.Items.FindByText("RVPA");
                if (listItem != null)
                {
                    listItem.Selected = false;
                }

                listItem = this.CheckBoxListSalida.Items.FindByText("Recargas Prepago");
                if (listItem != null)
                {
                    listItem.Selected = false;
                }

                listItem = this.CheckBoxListSalida.Items.FindByText("Intercompany");
                if (listItem != null)
                {
                    listItem.Selected = false;
                }

                listItem = this.CheckBoxListSalida.Items.FindByText("Mayorista");
                if (listItem != null)
                {
                    listItem.Selected = false;
                }

                listItem = this.CheckBoxListSalida.Items.FindByText("Venta de Kits Callcenter");
                if (listItem != null)
                {
                    listItem.Selected = false;
                }

                listItem = this.CheckBoxListSalida.Items.FindByText("Venta de Kits Salesman");
                if (listItem != null)
                {
                    listItem.Selected = false;
                }

                this.Image2.Visible = true;
                this.Image2.ImageUrl = "ImageCSharp.aspx?FileName=f_sal1.jpg";
                this.Image2.DataBind();


            }

            if ((this.CheckBoxListNumImagen.SelectedItem.Text == "2") || (CheckBoxListNumImagen.SelectedItem.Text == "3") || (CheckBoxListNumImagen.SelectedItem.Text == "4") || (CheckBoxListNumImagen.SelectedItem.Text == "5") || (CheckBoxListNumImagen.SelectedItem.Text == "6"))
            {
                ListItem listItem = this.CheckBoxListSalida.Items.FindByText("Pospago");
                if (listItem != null)
                {
                    listItem.Selected = false;
                }

                this.Image2.Visible = true;
                this.Image2.ImageUrl = "ImageCSharp.aspx?FileName=f_sal2.jpg";
                this.Image2.DataBind();
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
            this._message = message;
        }

        public string ErrorMessage
        {
            get { return this._message; }
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