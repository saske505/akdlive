using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DBConnections
{
    public class DataBaseConnection
    {
     //   private SqlDataSource sqlDBSource = new SqlDataSource("SERVER=qjfwgzv40o.database.windows.net;Database=AKD_Document_Management;UID=datora;PWD=d@t0r@.001", "");

       // private SqlDataSource sqlDBSource = new SqlDataSource("SERVER=qjfwgzv40o.database.windows.net;Database=AKD_Document_Management;UID=datora;PWD=d@t0r@.001", "");

        public SqlConnection GetConnection()
        {
            SqlConnection sqlCon = null;
            
            //   string connectionStr = "SERVER=user;Database=SQLDiagramDesigner;UID=sa;PWD=d@t0r@.001";
            // string connectionStr = @"SERVER=DEV-DATORA\DATORA;Database=test;UID=sa;PWD=d@t0r@.001";

           // string connectionStr = @"SERVER=qjfwgzv40o.database.windows.net;Database=AKD_Document_Management;UID=datora;PWD=d@t0r@.001";
          ///  string connectionStr = @"SERVER=192.168.0.113;Database=AKD_Document_Management;UID=sa;PWD=d@t0r@.001";
          ///  
            string connectionStr = @"SERVER=192.168.0.166;Database=AKD_Document_Management;UID=sa;PWD=d@t0r@.001";

            try
            {
                sqlCon = new SqlConnection(connectionStr);//(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                sqlCon.Open();
            }
            catch(Exception exe)
            {
                return null;
            }

            return sqlCon;
        }

        //public SqlDataSource GetConnectionSource()
        //{
        //     return new SqlDataSource("SERVER=qjfwgzv40o.database.windows.net;Database=AKD_Document_Management;UID=datora;PWD=d@t0r@.001","");
        //}

        public bool CloseSQlConnection(SqlConnection sqlCon)
        {
            try
            {
                sqlCon.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
