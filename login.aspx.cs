using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace A
{
    public partial class _Default : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=irantech_SampleDB;User ID=irantech_SampleDB;Password=DBSamplePW;Persist Security Info=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT * from Users where " + (Request["field"].Contains("@") ? "email" : "username") + " = '" + Request["field"] + "'"
                +" AND password = " + Request["password"];
            Data exception = new Data();
            List<Data> list = new List<Data>();
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                exception.exception = "not found";
                if (reader.HasRows)
                {
                    reader.Read();
                    sql = "SELECT * from devices_of_users where user_id = " + reader["id"].ToString();
                    conn.Close();
                    conn.Open();
                    command = new SqlCommand(sql, conn);
                    reader = command.ExecuteReader();
                    while (reader.Read()) {
                        Data data = new Data();
                        data.id = reader["id"].ToString();
                        data.device_name = reader["device_name"].ToString();
                        data.device_type = reader["device_type"].ToString();
                        data.device_id = reader["device_id"].ToString();
                        data.status = reader["status"].ToString();
                        data.token = reader["token"].ToString();
                        data.exception = "";
                        list.Add(data);
                    }
                }

            }
            catch (Exception ex)
            {
                exception.exception = "cannot oprating ! " + ex.ToString();
                list.Add(exception);
            }
            if (list.Count == 0)
                list.Add(exception);
            Response.Write(Data.toJson(list));
            conn.Close();
        }

        public class Data
        {
            //fields
            public string id = "";
            public string device_name = "";
            public string device_type = "";
            public string device_id = "";
            public string user_id = "";
            public string token = "";
            public string status = "";
            public string exception = "";

            //to json function
            public string toJson()
            {
                return "{\"id\":\"" + id  + "\",\"device_name\":\"" + device_name + "\",\"device_type\":\"" + device_type
                    + "\",\"device_id\":\"" + device_id + "\",\"user_id\":\"" + user_id + "\",\"status\":\"" + status + "\",\"token\":\"" + token
                    + "\",\"exception\":\"" + exception + "\"}";
            }
            public static string toJson(List<Data> list) {
                StringBuilder sb = new StringBuilder("[");
                foreach (Data d in list)
                    sb.Append(d.toJson()).Append(",");
                sb[sb.Length - 1] = ']';
                return sb.ToString();
            }
        }
    }
}







