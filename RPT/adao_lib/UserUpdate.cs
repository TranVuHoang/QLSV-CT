using abo_lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adao_lib
{
    public class UsersUpdate : BASE_DAO
    {
        public UsersUpdate() : base()
        {

        }

        public DataTable getUserPass(string username, string pass)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@username", username);
            parameters.Add("@password", pass);
            DataTable ds = executeReaderDataTable("SELECT * FROM [Users] WHERE [username] = @username AND [password] = @password", parameters);
            return ds;
        }
        public DataTable getUser(string pID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ID", pID);
            DataTable ds = executeReaderDataTable("SELECT * FROM [Users] ", parameters);
            return ds;
        }

    }
}
