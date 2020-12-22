namespace WebTelnetPdfCRM
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;

    public class ClassSqlserver
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\TPDF\WebApplicationTelnetPDF\WebTelnetPdfCRM\App_Data\Db1Imagenes.mdf;Integrated Security=True;User Instance=True";

        public bool InsertarRegistro(string pFecha, string pImagen, int psalida, string pEmpresa, int pTipo, string pLetra, int pEmail, int pAdhesion)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\TPDF\WebApplicationTelnetPDF\WebTelnetPdfCRM\App_Data\Db1Imagenes.mdf;Integrated Security=True;User Instance=True");
            SqlCommand cmdSql = new SqlCommand("INSERT INTO [imagenes] (FechaDisponibilidad, Imagen, Salida, Empresa, Tipo, Letra,Email,Adhesion) VALUES (@FechaDisponibilidad, @Imagen, @Salida, @Empresa, @Tipo, @Letra,@Email,@Adhesion)", conn);
            conn.Open();
            cmdSql.Parameters.AddWithValue("@FechaDisponibilidad", pFecha);
            cmdSql.Parameters.AddWithValue("@Imagen", pImagen);
            cmdSql.Parameters.AddWithValue("@Salida", psalida);
            cmdSql.Parameters.AddWithValue("@Empresa", pEmpresa);
            cmdSql.Parameters.AddWithValue("@Tipo", pTipo);
            cmdSql.Parameters.AddWithValue("@Letra", pLetra);
            cmdSql.Parameters.AddWithValue("@Email", pEmail);
            cmdSql.Parameters.AddWithValue("@Adhesion", pAdhesion);
            cmdSql.ExecuteNonQuery();
            conn.Close();

            return true;
        }

        public bool VerificarRegistro(string pFecha, int psalida, string pEmpresa, int pTipo, string pLetra, int pEmail, int pAdhesion)
        {
            bool retvalue;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("StoredProcedureBuscaRegistro"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FechaDisponibilidad", pFecha));
                    cmd.Parameters.Add(new SqlParameter("@Salida", psalida));
                    cmd.Parameters.Add(new SqlParameter("@Empresa", pEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@Tipo", pTipo));
                    cmd.Parameters.Add(new SqlParameter("@Letra", pLetra));
                    cmd.Parameters.Add(new SqlParameter("@Email", pEmail));
                    cmd.Parameters.Add(new SqlParameter("@Adhesion", pAdhesion));

                    SqlParameter countParameter = new SqlParameter("@resultado", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    int count = Int32.Parse(cmd.Parameters["@resultado"].Value.ToString());
                    retvalue = (count > 0) ? true : false;
                    conn.Close();
                }
            }
            return retvalue;

        }

        public DataSet DatosBusqueda(string fechaDesde, string fechaHasta, int tipo)
        {
            DataSet ds = new DataSet();
            string sql = "StoredProcedureGetData";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {

                    System.DateTime dFdesde = System.Convert.ToDateTime(fechaDesde);
                    System.DateTime dFhasta = System.Convert.ToDateTime(fechaHasta);
                    da.SelectCommand = new SqlCommand(sql, conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@FechaDesde", SqlDbType.Date).Value = dFdesde;
                    da.SelectCommand.Parameters.Add("@FechaHasta", SqlDbType.Date).Value = dFhasta;

                    da.Fill(ds, "result_name");

                    DataTable dt = ds.Tables["result_name"];



                }

                conn.Close();
            }

            return ds;

        }

    }
}