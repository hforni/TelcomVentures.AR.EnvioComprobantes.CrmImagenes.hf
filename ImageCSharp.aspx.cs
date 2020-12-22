namespace WebTelnetPdfCRM
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ImageCSharp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["FileName"] != null)
            {
                try
                {
                    NameValueCollection appSettings = ConfigurationManager.AppSettings;
                    appSettings["Archivo"].ToString();

                    // Read the file and convert it to Byte Array
                    string filePath = appSettings["ImageRouteTelnetPdf"].ToString();
                    string filename = this.Request.QueryString["FileName"];
                    string contenttype = "image/" +
                    Path.GetExtension(this.Request.QueryString["FileName"].Replace(".", ""));
                    FileStream fs = new FileStream(filePath + filename,
                    FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    br.Close();
                    fs.Close();
                    this.Response.Buffer = true;
                    this.Response.Charset = string.Empty;
                    this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    this.Response.ContentType = contenttype;
                    this.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                    this.Response.BinaryWrite(bytes);
                    this.Response.Flush();
                    this.Response.End();
                }
                catch
                {
                }
            }
        }
    }
}