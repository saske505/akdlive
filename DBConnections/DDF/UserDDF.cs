using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnections.DDF
{
    public class UserDDF
    {
        private DataBaseConnection m_connection = new DataBaseConnection();

        public UserCredentials ValidateUser(string username, string userpass)
        {
            UserCredentials retval = new UserCredentials();
            SqlConnection sqlConnection = m_connection.GetConnection();

            byte[] data = System.Text.Encoding.ASCII.GetBytes(userpass);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            userpass = System.Text.Encoding.ASCII.GetString(data);

            string sqlQuery = "SELECT * FROM fn_UsersValidate(@par1,@par2)";

            if (sqlConnection != null)
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.Add("@par1", SqlDbType.VarChar).Value = username;
                sqlCommand.Parameters.Add("@par2", SqlDbType.VarChar).Value = userpass;


                DataBaseDataReader reader = new DataBaseDataReader(sqlCommand.ExecuteReader());



                while (reader.Read())
                {
                    UserCredentials item = new UserCredentials();

                    retval.ID = reader.GetString("U_ID");
                    retval.Name = reader.GetString("U_Name");
                    retval.Surname = reader.GetString("U_Surname");
                    retval.UserName = reader.GetString("U_UserName");
                    retval.Pass = "*****";
                    retval.Level = reader.GetInt32("u_Level");
                    retval.Clientlink = reader.GetString("u_c_id");
                }
            }
            return retval;
        }


        public bool DeleteUser(string id)
        {
            SqlConnection sqlConnection = m_connection.GetConnection();
            SqlCommand sqlCommand;

            string submitString = "";

            using (SqlCommand cmd = new SqlCommand("sp_DeleteUser", sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@U_id", id);

                cmd.ExecuteNonQuery();
            }

            //   submitString += sqlQuery + " ; ";

            sqlCommand = new SqlCommand(submitString, sqlConnection);

            if (submitString != "")
                sqlCommand.ExecuteNonQuery();

            return true;
        }


        public List<UserCredentials> GetUsers()
        {
            List<UserCredentials> retval = new List<UserCredentials>();
            SqlConnection sqlConnection = m_connection.GetConnection();

            string sqlQuery = "SELECT * FROM fn_GetUsers()";

            if (sqlConnection != null)
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                DataBaseDataReader reader = new DataBaseDataReader(sqlCommand.ExecuteReader());

                while (reader.Read())
                {
                    UserCredentials item = new UserCredentials();

                    item.ID = reader.GetString("U_ID");
                    item.Name = reader.GetString("U_Name");
                    item.Surname = reader.GetString("U_Surname");
                    item.UserName = reader.GetString("U_UserName");
                    item.Pass = "*****";
                    item.Level = reader.GetInt32("u_Level");
                    item.Clientlink = reader.GetString("u_c_id");
                    retval.Add(item);
                }
            }
            return retval;
        }

        public bool CreateAndUpdateUser(UserCredentials item)
        {

            SqlConnection sqlConnection = m_connection.GetConnection();
            SqlCommand sqlCommand;

            string submitString = "";

            using (SqlCommand cmd = new SqlCommand("sp_CreateUser", sqlConnection))
                {
          
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(item.Pass);
                    data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                    item.Pass = System.Text.Encoding.ASCII.GetString(data);

                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@U_Name", item.Name);
                    cmd.Parameters.AddWithValue("@U_Surname", item.Surname);
                    cmd.Parameters.AddWithValue("@U_UserName", item.UserName);
                    cmd.Parameters.AddWithValue("@U_Password", item.Pass);
                    cmd.Parameters.AddWithValue("@U_Level", item.Level);
                    cmd.Parameters.AddWithValue("@U_C_ID", item.Clientlink);

                    if (item.ID == "-1" || item.ID == null)
                    {
                        item.ID = Guid.NewGuid().ToString();
                        cmd.Parameters.AddWithValue("@U_ID", item.ID);
                        cmd.Parameters.AddWithValue("@NewUser", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@U_ID", item.ID);
                        cmd.Parameters.AddWithValue("@NewUser", 0);
                    }

                    cmd.ExecuteNonQuery();
                }

            //   submitString += sqlQuery + " ; ";

            sqlCommand = new SqlCommand(submitString, sqlConnection);

            if (submitString != "")
                sqlCommand.ExecuteNonQuery();

            return true;
        }

        public class UserCredentials
        {
            public string ID;
            public string Name;
            public string Surname;
            public string UserName;
            public string Pass;
            public string Clientlink;
            public int Level;
        }
    }
}
