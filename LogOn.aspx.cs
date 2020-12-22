using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebTelnetPdfCRM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Login1.UserNameLabelText = "Usuario";
            Login1.PasswordLabelText = "Contraseña";
            Login1.RememberMeText = "Mantener la sesión iniciada";
            Login1.TitleText = "Ingresar";
            Login1.LoginButtonText = "Ingresar";
        }

        protected void ValidateUser(object sender, EventArgs e)
        {
            int userId = -1;
            if (this.Login1.UserName == "Claudia")
            {
                if (this.Login1.Password == "a23145")
                {
                    userId = 1;
                }
            
            }

            if (this.Login1.UserName == "Jimena")
            {
                if (this.Login1.Password == "b78yt4")
                {
                    userId = 1;
                }

            }


            if (this.Login1.UserName == "henu")
            {
                if (this.Login1.Password == "4454")
                {
                    userId = 1;
                }

            }

            switch (userId)
            {
                case -1:
                    this.Login1.FailureText = "El usuario y la password no son correctos.";
                    break;
                default:
                    HttpContext.Current.Session["User"] = this.Login1.UserName;
                    FormsAuthentication.RedirectFromLoginPage(this.Login1.UserName, this.Login1.RememberMeSet);
                    break;
            }
        }
    }
}