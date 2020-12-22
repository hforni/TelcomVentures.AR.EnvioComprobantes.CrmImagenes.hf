using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Specialized;
using System.Configuration;

namespace WebTelnetPdfCRM
{
    public partial class ImageCSharp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          if (Request.QueryString["FileName"] != null)
        {
            try
            {
                NameValueCollection appSettings = ConfigurationManager.AppSettings;
                appSettings["Archivo"].ToString();

                // Read the file and convert it to Byte Array
                string filePath = appSettings["ImageRouteTelnetPdf"].ToString();
                string filename = Request.QueryString["FileName"];
                string contenttype = "image/" +
                Path.GetExtension(Request.QueryString["FileName"].Replace(".",""));
                FileStream fs = new FileStream(filePath + filename,
                FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                br.Close();
                fs.Close();
 
                //Write the file to response Stream
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contenttype;
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch
            {
            }
}
        }
    }
}