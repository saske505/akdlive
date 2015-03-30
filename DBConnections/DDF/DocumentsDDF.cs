using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnections.DDF
{
   public  class DocumentsDDF
    {
       private DataBaseConnection m_connection = new DataBaseConnection();

       public List<DocumentsDDFs> GetDocumentList()
       {
           List<DocumentsDDFs> retval = new List<DocumentsDDFs>();
           SqlConnection sqlConnection = m_connection.GetConnection();

           string sqlQuery = "SELECT * FROM fn_GetDocuments()";

           if (sqlConnection != null)
           {
               SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
               DataBaseDataReader reader = new DataBaseDataReader(sqlCommand.ExecuteReader());

               while (reader.Read())
               {
                   DocumentsDDFs item = new DocumentsDDFs();
                   

                    item.D_ID  = reader.GetString("D_ID");
                    item.D_C_ID = reader.GetString("D_C_ID");
                    item.D_AttorneyMatterRef = reader.GetString("D_AttorneyMatterRef");
                    item.D_PlaintiffTitle = reader.GetString("D_PlaintiffTitle");
                    item.D_PlaintiffInitial = reader.GetString("D_PlaintiffInitial");
                    item.D_PlaintiffName = reader.GetString("D_PlaintiffName");
                    item.D_PlaintiffSurname = reader.GetString("D_PlaintiffSurname");
                    item.D_DefendantTitle = reader.GetString("D_DefendantTitle");
                    item.D_DefendantInitial = reader.GetString("D_DefendantInitial");
                    item.D_DefendantName = reader.GetString("D_DefendantName");
                    item.D_DefendantSurname = reader.GetString("D_DefendantSurname");
                    item.D_ProcessType = reader.GetString("D_ProcessType");
                    item.D_ServiceType = reader.GetString("D_ServiceType");
                    item.D_CaseNumber = reader.GetString("D_CaseNumber");
                    item.D_District = reader.GetString("D_District");
                    item.D_Court = reader.GetString("D_Court");
                    item.D_MessengerOfCourt = reader.GetString("D_MessengerOfCourt");
                    retval.Add(item);
               }

           }
           return retval;
       }

       public List<DocumentsDDFs> GetDocumentList(string batchid)
       {
           List<DocumentsDDFs> retval = new List<DocumentsDDFs>();
           SqlConnection sqlConnection = m_connection.GetConnection();

           string sqlQuery = "SELECT * FROM AKDDocuments where d_b_id =  '" + batchid + "'";

           if (sqlConnection != null)
           {
               SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
               DataBaseDataReader reader = new DataBaseDataReader(sqlCommand.ExecuteReader());

               while (reader.Read())
               {
                   DocumentsDDFs item = new DocumentsDDFs();

                   //item.id = reader.GetString("d_id");
                   //item.accountNumber = reader.GetString("d_AccountNumber");
                   //item.post1 = reader.GetString("d_Post1");
                   //item.post2 = reader.GetString("d_Post2");
                   //item.post3 = reader.GetString("d_Post3");
                   //item.city = reader.GetString("d_City");
                   //item.postalCode = reader.GetString("d_PostalCode");
                   //item.docType = reader.GetString("d_DocType");
                   //item.court = reader.GetString("d_Court");
                   //item.batchID = reader.GetString("d_b_id");
                   //item.clientID = reader.GetString("d_c_id");
               }

           }
           return retval;
       }


       public bool CreateAndUpdateDoc(DocumentsDDFs item)
       {
           List<DocumentsDDFs> retval = new List<DocumentsDDFs>();
           SqlConnection sqlConnection = m_connection.GetConnection();

           using (SqlCommand cmd = new SqlCommand("sp_CreateDocument", sqlConnection))
           {
               cmd.CommandType = CommandType.StoredProcedure;


               cmd.Parameters.AddWithValue("@D_C_ID", item.D_C_ID);
               cmd.Parameters.AddWithValue("@D_B_ID","00000000-0000-0000-0000-000000000021");
               cmd.Parameters.AddWithValue("@D_AttorneyMatterRef", item.D_AttorneyMatterRef);
               cmd.Parameters.AddWithValue("@D_PlaintiffTitle", item.D_PlaintiffTitle);
               cmd.Parameters.AddWithValue("@D_PlaintiffInitial", item.D_PlaintiffInitial);
               cmd.Parameters.AddWithValue("@D_PlaintiffName", item.D_PlaintiffName);
               cmd.Parameters.AddWithValue("@D_PlaintiffSurname", item.D_PlaintiffSurname);
               cmd.Parameters.AddWithValue("@D_DefendantTitle", item.D_DefendantTitle);
               cmd.Parameters.AddWithValue("@D_DefendantInitial", item.D_DefendantInitial);
               cmd.Parameters.AddWithValue("@D_DefendantName", item.D_DefendantName);
               cmd.Parameters.AddWithValue("@D_DefendantSurname", item.D_DefendantSurname);
               cmd.Parameters.AddWithValue("@D_ProcessType", item.D_ProcessType);
               cmd.Parameters.AddWithValue("@D_ServiceType", item.D_ServiceType);
               cmd.Parameters.AddWithValue("@D_CaseNumber", item.D_CaseNumber);
               cmd.Parameters.AddWithValue("@D_District", item.D_District);
               cmd.Parameters.AddWithValue("@D_Court", item.D_Court);
               cmd.Parameters.AddWithValue("@D_MessengerOfCourt", item.D_MessengerOfCourt);

               if (item.D_ID == "-1" || item.D_ID == null)
               {
                   item.D_ID = Guid.NewGuid().ToString();
                   cmd.Parameters.AddWithValue("@D_ID", item.D_ID);
                   cmd.Parameters.AddWithValue("@newRecord", 1);
               }
               else
               {
                   cmd.Parameters.AddWithValue("@D_ID", item.D_ID);
                   cmd.Parameters.AddWithValue("@newRecord", 0);

                   //cmd.Parameters.AddWithValue("@B_C_ID", "00000000-0000-0000-0000-000000000021");
                   //cmd.Parameters.AddWithValue("@B_CreatedBy", "00000000-0000-0000-0000-000000000002");
                   //cmd.Parameters.AddWithValue("@B_CreatedOn", DateTime.Now);

               }

               cmd.ExecuteNonQuery();
           }

           //string values ="'"+ doc.id +"','"+ doc.accountNumber +"','"+ doc.post1 +"','"+ doc.post2 +"','"+ doc.post3 +"','"+ doc.city +"','"+ doc.postalCode +"','"+ doc.docType +"','"+ doc.court +"','"+ doc.batchID +"','"+ doc.clientID+"'";
           //string sqlQuery = "EXEC TheStoredProcedure" + values;

           //if (sqlConnection != null)
           //{
           //    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
           //    sqlCommand.ExecuteReader();
           //}
           return true;
       }


        public class DocumentsDDFs
        {
            public string D_ID;
            public string D_C_ID ;
            public string D_B_ID;
            public string D_AttorneyMatterRef;
            public string D_PlaintiffTitle;
            public string D_PlaintiffInitial;
            public string D_PlaintiffName;
            public string D_PlaintiffSurname;
            public string D_DefendantTitle;
            public string D_DefendantInitial;
            public string D_DefendantName;
            public string D_DefendantSurname ;
            public string D_ProcessType;
            public string D_ServiceType;
            public string D_CaseNumber;
            public string D_District;
            public string D_Court;
            public string D_MessengerOfCourt;
        }

    }
}
