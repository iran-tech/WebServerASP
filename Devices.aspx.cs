using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
namespace n
{
    public partial class _Default : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=irantech_SampleDB;User ID=irantech_SampleDB;Password=DBSamplePW;Persist Security Info=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * from devices";
            SqlCommand command = new SqlCommand(sql, conn);
            conn.Open();
            List<Device> list = new List<Device>();
            Device device = new Device();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                device.id = "2";
                device.exception = "no data";
                while (reader.Read())
                {
                    Device d = new Device();
                    d.id = reader["id"].ToString();
                    d.name = reader["name"].ToString();
                    d.type = reader["type"].ToString();
                    d.kind = reader["kind"].ToString();
                    list.Add(d);
                }
            }
            catch (Exception ex)
            {
                device.id = "1";
                device.exception = "exception : " + ex.ToString();
                list.Add(device);
            }
            conn.Close();
            if (list.Count == 0)
                list.Add(device);
            Response.Write(Device.toJson(list));
        }

        public class Device
        {
            //fields
            public string id = "";
            public string kind = "";
            public string type = "";
            public string name = "";
            public string exception = "";

            //to json function
            public string toJson()
            {
                return "{\"id\":\"" + id + "\"text\":\"" + kind + "\",\"name\":\"" + name + "\",\"type\":\"" + type + "\",\"exception\":\"" + exception + "\"}";
            }
            public static string toJson(List<Device> list)
            {
                StringBuilder sb = new StringBuilder("[");
                foreach (Device d in list)
                    sb.Append(d.toJson()).Append(",");
                sb[sb.Length - 1] = ']';
                return sb.ToString();
            }
        }
    }
}







