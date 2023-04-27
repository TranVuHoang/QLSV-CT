using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Data;

namespace adao_lib
{
    public class BASE_DAO
    {
        private string strConnectionString;
        protected SqlConnection connection { get; set; }
        protected SqlTransaction transaction { get; set; }
        protected SqlCommand command { get; set; }
        public BASE_DAO()
        {
            //strConnectionString = ConfigurationManager.ConnectionStrings["RS_ConnectionString"].ConnectionString;
            strConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            connection = new SqlConnection(strConnectionString);
            connection.Open();
        }
        ~BASE_DAO()
        {

        }

        public void DisConnect()
        {

            connection.Close();
        }


        protected void executeNonQuery(string sql, Dictionary<string, object> lstParams)
        {
            command = new SqlCommand(sql, connection);
            command.CommandText = sql;
            command.CommandTimeout = 1000;
            command.Parameters.Clear();
           
            if (lstParams != null)
                foreach (KeyValuePair<string, object> pair in lstParams)
                {
                    command.Parameters.AddWithValue(pair.Key, pair.Value);
                }
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        protected void executeNonQuery(string sql)
        {
            command = new SqlCommand(sql, connection);
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        protected void executeNonQuery_LongTimeOut(string sql)
        {
            command.CommandText = sql;
            // Set the new command timeout to 60 seconds
            // in stead of the default 30
            command.CommandTimeout = 60;
            command.ExecuteNonQuery();
        }

        protected SqlDataReader executeReader(string sql, Dictionary<string, object> lstParams)
        {
            command = new SqlCommand(sql, connection);
            command.CommandText = sql;
            //command.CommandTimeout = 60;
            if (lstParams != null)
                command.Parameters.Clear();
            foreach (KeyValuePair<string, object> pair in lstParams)
            {
                command.Parameters.AddWithValue(pair.Key, pair.Value);
            }
            SqlDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();
            return reader;
        }

        protected SqlDataReader executeReader(string sql)
        {
            command = new SqlCommand(sql, connection);
            command.CommandText = sql;
            command.CommandTimeout = 60;
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }
        protected DataTable executeReaderDataTable(string sql, Dictionary<string, object> lstParams)
        {
            DataTable ds = new DataTable("results");
            command = new SqlCommand(sql, connection);
            command.CommandText = sql;
            command.CommandTimeout = 60;
            if (lstParams != null)
                command.Parameters.Clear();
            foreach (KeyValuePair<string, object> pair in lstParams)
            {
                command.Parameters.AddWithValue(pair.Key, pair.Value);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            return ds;
        }

        protected DataTable executeReaderDataTable(string sql)
        {
            DataTable ds = new DataTable("results");
            command = new SqlCommand(sql, connection);
            command.CommandText = sql;
            command.CommandTimeout = 60;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            return ds;
        }

        protected DataSet executeReaderDataset(string sql, Dictionary<string, object> lstParams)
        {
            DataSet ds = new DataSet();
            command = new SqlCommand(sql, connection);
            command.CommandText = sql;
            command.CommandTimeout = 60;
            if (lstParams != null)
                command.Parameters.Clear();
            foreach (KeyValuePair<string, object> pair in lstParams)
            {
                command.Parameters.AddWithValue(pair.Key, pair.Value);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds, "results");
            return ds;
        }

        protected DataSet executeReaderDataset(string sql)
        {
            DataSet ds = new DataSet();
            command = new SqlCommand(sql, connection);
            command.CommandText = sql;
            command.CommandTimeout = 60;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds, "results");
            return ds;
        }

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public void beginTransaction()
        {
            openConnection();
            command = connection.CreateCommand();
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
        }

        public void commitTransaction()
        {
            transaction.Commit();
            closeConnection();
        }

        public void rollbackTransaction()
        {
            try
            {
                transaction.Rollback();
                closeConnection();
            }
            catch (Exception) { }
        }

        #region  CHECK TABLE IS LOCKING OR NOT -SONNT

        public bool checkLockedTable(string TableName)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@TableName", TableName);
            string strSelect = "select COUNT(*) as lockNumber from sys.dm_tran_locks where resource_type = 'OBJECT' and resource_database_id = DB_ID()AND object_name(resource_associated_entity_id) = @TableName";
            SqlDataReader dr = executeReader(strSelect, parameters);
            if (!dr.HasRows)
            {
                dr.Close();
                return true;
            }
            else
                while (dr.Read())
                {

                    int count = dr.GetInt32(dr.GetOrdinal("lockNumber"));
                    if (count > 0) { dr.Close(); return true; }
                }
            dr.Close();
            return false;
        }

        #endregion

    }
}
