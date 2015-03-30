using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DBConnections.DDF
{
    public class ClientsDDF
    {
        private DataBaseConnection m_connection = new DataBaseConnection();

        //public bool GetClientValid(string name)
        //{

        //    return false;
        //    ClientDDFs retval = new ClientDDFs();

        //    SqlConnection sqlConnection = m_connection.GetConnection();

        //    string sqlQuery = "SELECT * FROM fn_GetClientList()";

        //    if (sqlConnection != null)
        //    {
        //        SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
        //        DataBaseDataReader reader = new DataBaseDataReader(sqlCommand.ExecuteReader());

        //        while (reader.Read())
        //        {
        //            retval.id = reader.GetString("c_id");
        //            retval.name = reader.GetString("c_ClientName");
        //            StaticClass.s_LogedOnClient = retval;
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public List<ClientDDFs> GetALLClinetListSource()
        {
            List<ClientDDFs> retval = new List<ClientDDFs>();

            SqlConnection sqlConnection = m_connection.GetConnection();

            string sqlQuery = "SELECT * FROM fn_GetClientList()";

            if (sqlConnection != null)
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                DataBaseDataReader reader = new DataBaseDataReader(sqlCommand.ExecuteReader());

                while (reader.Read())
                {
                    ClientDDFs item = new ClientDDFs();
                    item.id = reader.GetString("c_id");
                    item.name = reader.GetString("C_Client_Name");
                    item.RegNo = reader.GetString("C_RegNo");
                    item.VatNo = reader.GetString("C_VatNo");
                    item.PhysicalAddress1 = reader.GetString("C_PhysicalAddress_Line1");
                    item.PhysicalAddress2 = reader.GetString("C_PhysicalAddress_Line2");
                    item.PhysicalAddress3 = reader.GetString("C_PhysicalAddress_Line3");
                    item.PhysicalAddress4 = reader.GetString("C_PhysicalAddress_Line4");
                    item.PhysicalAddressPostal = reader.GetString("C_PhysicalAddress_Postal");
                    item.PhysicalAddressCity = reader.GetString("C_PhysicalAddress_City");
                    item.PhysicalAddressCountry = reader.GetString("C_PhysicalAddress_Country");
                    item.ClientContact1 = reader.GetString("C_ClientContact1");
                    item.ClientContact1Tel = reader.GetString("C_ClientContact1Tel");
                    item.ClientContact1Email = reader.GetString("C_ClientContact1Email");
                    item.ClientContact2 = reader.GetString("C_ClientContact2");
                    item.ClientContact2Tel = reader.GetString("C_ClientContact2Tel");
                    item.ClientContact2Email = reader.GetString("C_ClientContact2Email");

                    retval.Add(item);
                }
            }
            return retval;
        }

        //public SqlDataSource GetClinetListSource()
        //{
        //    SqlDataSource retval = m_connection.GetConnectionSource();

        //    retval.SelectCommand = "SELECT * FROM fn_GetClientList()";
        //    retval.DataBind();

        //    return retval;
        //}

        public bool CreateAndUpdateClients(ClientDDFs item)
        {
            SqlConnection sqlConnection = m_connection.GetConnection();
            SqlCommand sqlCommand;

            string submitString = "";

            

            using (SqlCommand cmd = new SqlCommand("sp_CreateClient", sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@C_Client_Name", item.name);
                cmd.Parameters.AddWithValue("@C_RegNo", item.RegNo);
                cmd.Parameters.AddWithValue("@C_VatNo", item.VatNo);
                cmd.Parameters.AddWithValue("@C_PhysicalAddress_Line1", item.PhysicalAddress1);
                cmd.Parameters.AddWithValue("@C_PhysicalAddress_Line2", item.PhysicalAddress2);
                cmd.Parameters.AddWithValue("@C_PhysicalAddress_Line3", item.PhysicalAddress3);
                cmd.Parameters.AddWithValue("@C_PhysicalAddress_Line4", item.PhysicalAddress4);
                cmd.Parameters.AddWithValue("@C_PhysicalAddress_Postal", item.PhysicalAddressPostal);
                cmd.Parameters.AddWithValue("@C_PhysicalAddress_City", item.PhysicalAddressCity);
                cmd.Parameters.AddWithValue("@C_PhysicalAddress_Country", item.PhysicalAddressCountry);
                cmd.Parameters.AddWithValue("@C_ClientContact1", item.ClientContact1);
                cmd.Parameters.AddWithValue("@C_ClientContact1Tel", item.ClientContact1Tel);
                cmd.Parameters.AddWithValue("@C_ClientContact1Email", item.ClientContact1Email);
                cmd.Parameters.AddWithValue("@C_ClientContact2", item.ClientContact2);
                cmd.Parameters.AddWithValue("@C_ClientContact2Tel", item.ClientContact2Tel);
                cmd.Parameters.AddWithValue("@C_ClientContact2Email", item.ClientContact2Email);

                if (item.id == "-1" || item.id == null)
                {
                    item.id = Guid.NewGuid().ToString();
                    cmd.Parameters.AddWithValue("@C_ID", item.id);
                    cmd.Parameters.AddWithValue("@newRecord", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@C_ID", item.id);
                    cmd.Parameters.AddWithValue("@newRecord", 0);
                }

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public class ClientDDFs
        {
            public string id;
            public string name;
            public string RegNo;
            public string VatNo;
            public string PhysicalAddress1;
            public string PhysicalAddress2;
            public string PhysicalAddress3;
            public string PhysicalAddress4;
            public string PhysicalAddressPostal;
            public string PhysicalAddressCity;
            public string PhysicalAddressCountry;
            public string ClientContact1;
            public string ClientContact1Tel;
            public string ClientContact1Email;
            public string ClientContact2;
            public string ClientContact2Tel;
            public string ClientContact2Email;
        }
    }
}
