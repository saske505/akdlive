using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnections
{
    public class DataBaseDataReader
    {
        private DateTime defaultDate;
        private byte[] defaultByte;
        public SqlDataReader reader;

        public DataBaseDataReader(SqlDataReader reader)
        {
            this.defaultDate = DateTime.MinValue;
            this.reader = reader;
        }

        public int GetInt32(String column)
        {
            int data = (reader.IsDBNull(reader.GetOrdinal(column)))
                ? (int)0 : (int)reader[column];
            return data;
        }

        public short GetInt16(String column)
        {
            short data = (reader.IsDBNull(reader.GetOrdinal(column)))
                ? (short)0 : (short)reader[column];
            return data;
        }

        public float GetFloat(String column)
        {
            int iX = reader.GetOrdinal(column);
            float data = (reader.IsDBNull(reader.GetOrdinal(column)))
                ? 0 : float.Parse(reader[column].ToString());
            return data;
        }

        public bool GetBoolean(String column)
        {
            bool data = (reader.IsDBNull(reader.GetOrdinal(column)))
                ? false : (bool)reader[column];
            return data;
        }

        public String GetString(String column)
        {
            String data = (reader.IsDBNull(reader.GetOrdinal(column)))
                ? "" : reader[column].ToString();
            return data;
        }

        public byte[] GetByte(String column)
        {
            byte[] data = null;
            if (reader.IsDBNull(reader.GetOrdinal(column)))
                return defaultByte;
            else
            {
                data = new byte[reader[column].ToString().Length];
                data = (byte[])reader[column];
            }
            return data;
        }

        public DateTime GetDateTime(String column)
        {
            DateTime data = (reader.IsDBNull(reader.GetOrdinal(column)))
                ? defaultDate : (DateTime)reader[column];
            return data;
        }
        public bool Read()
        {
            return this.reader.Read();
        }
    }
}
