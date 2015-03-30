using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnections.DDF
{
    public class BatchesDDF
    {
        private DataBaseConnection m_connection = new DataBaseConnection();

        public string InsertBatch(BatchesDDFs bat)
        {

            Guid retval = new Guid();
            retval = Guid.NewGuid();
            bat.id = retval.ToString();
            bat.createdOn = DateTime.Now;

            SqlConnection sqlConnection = m_connection.GetConnection();

            string values = "'";// + bat.id + "','" + bat.name + "','" + bat.clientID  + "','" + bat.createdOn + "','" + bat.createdOn + "'";
            string sqlQuery = "EXEC fn_CreateBatch " + values;

            if (sqlConnection != null)
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.ExecuteReader();
            }
            return bat.id;
        }

        public bool CreateAndUpdateBatch(BatchesDDFs item)
        {
            SqlConnection sqlConnection = m_connection.GetConnection();

            using (SqlCommand cmd = new SqlCommand("sp_CreateBatch", sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@B_Description", item.description);
                
                cmd.Parameters.AddWithValue("@B_ModifiedDate", item.modOn);
                cmd.Parameters.AddWithValue("@B_ModifiedBy", item.modBy);

                if (item.id == "-1" || item.id == null)
                {
                    item.id = Guid.NewGuid().ToString();
                    cmd.Parameters.AddWithValue("@B_ID", item.id);
                    cmd.Parameters.AddWithValue("@newRecord", 1);

                    cmd.Parameters.AddWithValue("@B_C_ID", item.clientID);
                    cmd.Parameters.AddWithValue("@B_CreatedBy", item.createdBy);
                    cmd.Parameters.AddWithValue("@B_CreatedOn", item.createdOn);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@B_ID", item.id);
                    cmd.Parameters.AddWithValue("@newRecord", 0);

                    cmd.Parameters.AddWithValue("@B_C_ID", "00000000-0000-0000-0000-000000000021");
                    cmd.Parameters.AddWithValue("@B_CreatedBy", "00000000-0000-0000-0000-000000000002");
                    cmd.Parameters.AddWithValue("@B_CreatedOn", DateTime.Now);

                }

                cmd.ExecuteNonQuery();
            }

            //   submitString += sqlQuery + " ; ";

            return true;
        }


        

        public List<BatchesDDFs> GetBatchesByClientID()
        {
            List<BatchesDDFs> retval = new List<BatchesDDFs>();

            SqlConnection sqlConnection = m_connection.GetConnection();

            string sqlQuery = "SELECT * FROM fn_GetBatch()";

            if (sqlConnection != null)
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                DataBaseDataReader reader = new DataBaseDataReader(sqlCommand.ExecuteReader());

                while (reader.Read())
                {
                    BatchesDDFs item = new BatchesDDFs();
                    item.id = reader.GetString("B_ID");
                    item.description = reader.GetString("B_Description");
                    item.clientID = reader.GetString("B_C_ID");
                    item.createdBy = reader.GetString("B_CreatedBy");
                    item.createdOn = reader.GetDateTime("B_CreatedOn");
                    item.modBy = reader.GetString("B_ModifiedBy");
                    item.modOn = reader.GetDateTime("B_ModifiedDate");
                   
                    retval.Add(item);
                }
            }
            return retval;
        }



        public class BatchesDDFs
        {
            public string id;
            public string description;
            public string clientID;
            public string createdBy;
            public DateTime createdOn;
            public string modBy;
            public DateTime modOn;
        }
    }
}
