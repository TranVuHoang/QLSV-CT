using abo_lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adao_lib
{
    public class StudentUpdate : BASE_DAO
    {
        public DataTable GetDataTable ()
        {           
            DataTable dt = new DataTable();
            dt = executeReaderDataTable("SELECT * FROM [STUDENT]");
           
            return dt;
        }

        public DataTable getAllStudent()
        {
            DataTable ds = executeReaderDataTable("SELECT * FROM [STUDENT]");
          
            return ds;
        }

        public DataTable getStudent(string pID)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ID", pID);
            DataTable ds = executeReaderDataTable("SELECT * FROM [STUDENT] WHERE [id] = @ID", parameters);
           
            return ds;
        }

        public void Insert(Student pt)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", pt.id);
            parameters.Add("@hoten", pt.hoten);
            parameters.Add("@ngaysinh", pt.ngaysinh);
            parameters.Add("@que", pt.que);
            parameters.Add("@diem", pt.diem);

            executeNonQuery("INSERT INTO [STUDENT]([id], [hoten], [ngaysinh], [que], [diem])" +
                " VALUES(@id,@hoten,@ngaysinh, @que, @diem)", parameters);
        }

        public void Update(Student pt)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", pt.id);
            parameters.Add("@hoten", pt.hoten);
            parameters.Add("@ngaysinh", pt.ngaysinh);
            parameters.Add("@que", pt.que);
            parameters.Add("@diem", pt.diem);

            executeNonQuery("UPDATE [STUDENT] SET [hoten] = @hoten, [ngaysinh] = @ngaysinh, [que] = @que, [diem] = @diem" +
                " WHERE [ID] = @id", parameters);
        }

        public void Delete(string id)
        {
            Dictionary <string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);

            executeNonQuery("DELETE FROM [STUDENT] WHERE id = @id", parameters);
        }
    }
    
}
