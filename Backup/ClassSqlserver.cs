using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebTelnetPdfCRM
{
    public class ClassSqlserver
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\TPDF\WebApplicationTelnetPDF\WebTelnetPdfCRM\App_Data\Db1Imagenes.mdf;Integrated Security=True;User Instance=True"; 

        public bool InsertarRegistro(string pFecha, string pImagen, int psalida, string pEmpresa, int pTipo, string pLetra, int pEmail, int pAdhesion)
        {
            

            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\TPDF\WebApplicationTelnetPDF\WebTelnetPdfCRM\App_Data\Db1Imagenes.mdf;Integrated Security=True;User Instance=True");
            SqlCommand CmdSql = new SqlCommand("INSERT INTO [imagenes] (FechaDisponibilidad, Imagen, Salida, Empresa, Tipo, Letra,Email,Adhesion) VALUES (@FechaDisponibilidad, @Imagen, @Salida, @Empresa, @Tipo, @Letra,@Email,@Adhesion)", conn);
            conn.Open();
            CmdSql.Parameters.AddWithValue("@FechaDisponibilidad", pFecha);
            CmdSql.Parameters.AddWithValue("@Imagen", pImagen);
            CmdSql.Parameters.AddWithValue("@Salida", psalida);
            CmdSql.Parameters.AddWithValue("@Empresa", pEmpresa);
            CmdSql.Parameters.AddWithValue("@Tipo", pTipo);
            CmdSql.Parameters.AddWithValue("@Letra", pLetra);
            CmdSql.Parameters.AddWithValue("@Email", pEmail);
            CmdSql.Parameters.AddWithValue("@Adhesion", pAdhesion);
            CmdSql.ExecuteNonQuery();
            conn.Close();

           
            return true;
        }

        public bool VerificarRegistro(string pFecha,  int psalida, string pEmpresa, int pTipo, string pLetra, int pEmail, int pAdhesion)
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
                    cmd.Parameters.Add(new SqlParameter("@Email",pEmail));
                    cmd.Parameters.Add(new SqlParameter("@Adhesion", pAdhesion));

                    SqlParameter countParameter = new SqlParameter("@resultado", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);  
                    conn.Open(); cmd.Connection = conn;
                    
                    cmd.ExecuteNonQuery();
                    int count = Int32.Parse(cmd.Parameters["@resultado"].Value.ToString());
                    retvalue = (count > 0) ? true : false;
                    conn.Close(); 
                    

                    
                    
                    

                }


            }
            /*
            SqlConn.Open();
            sqlcomm.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@a", SqlDbType.VarChar, 50);
            param.Direction = ParameterDirection.Input;
            param.Value = Username;
            sqlcomm.Parameters.Add(param);



            SqlParameter retval = new SqlParameter("@b", SqlDbType.VarChar, 50);
            retval.Direction = ParameterDirection.ReturnValue;
            sqlcomm.Parameters.Add(retval);

            sqlcomm.ExecuteNonQuery();
            SqlConn.Close();

            string retunvalue = retval.Value.ToString();


            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\TPDF\WebApplicationTelnetPDF\WebTelnetPdfCRM\App_Data\Db1Imagenes.mdf;Integrated Security=True;User Instance=True");
            SqlCommand sqlcomm = new SqlCommand();
            conn.Open();
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.CommandText = "StoredProcedureBuscaRegistro"; // Stored Procedure name
            sqlcomm.Parameters.AddWithValue("@FechaDisponibilidad", pFecha); // Input parameters
            sqlcomm.Parameters.AddWithValue("@Salida", psalida); // Input parameters
            sqlcomm.Parameters.AddWithValue("@Empresa", pEmpresa); // Input parameters
            sqlcomm.Parameters.AddWithValue("@Tipo", pTipo); // Input parameters
            sqlcomm.Parameters.AddWithValue("@Letra", pLetra); // Input parameters
            sqlcomm.Parameters.AddWithValue("@Email", pEmail); // Input parameters
            sqlcomm.Parameters.AddWithValue("@Adhesion", pAdhesion); // Input parameters
            // Your output parameter in Stored Procedure           
            var returnParam1 = new SqlParameter
            {
                ParameterName = "@resultado",
                Direction = ParameterDirection.Output,
                Size = 1
            };
            sqlcomm.Parameters.Add(returnParam1);

     

            sqlcomm.ExecuteNonQuery();

            int retunvalue = (int)sqlcomm.Parameters["@resultado"].Value;
            retvalue = (retunvalue > 0) ? true : false;
            
            */
            
            return retvalue;
                 
        }

        public DataSet DatosBusqueda(string FechaDesde,string FechaHasta,int tipo)
        {
            DataSet ds = new DataSet();
            string sql = "StoredProcedureGetData";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {

                    System.DateTime dFdesde = System.Convert.ToDateTime(FechaDesde);
                    System.DateTime dFhasta = System.Convert.ToDateTime(FechaHasta);
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