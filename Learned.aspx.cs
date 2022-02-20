using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
namespace b
{
    public partial class _Default : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=irantech_SampleDB;User ID=irantech_SampleDB;Password=DBSamplePW;Persist Security Info=True;");
        protected void Page_Load(object sender, EventArgs e)
        {

            Message message = new Message();
            string sql = "SELECT * from learns WHERE token = '"+Request["token"]+"'";
            SqlCommand command = new SqlCommand(sql, conn);
            conn.Open();
            try {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    
                    if (reader["value"].ToString().Equals("nothing"))
                    {
                        message.id = "2";
                        message.text = "no value";
                    }
                    else {
                        if (Request["function"].Equals("run"))
                        {
                            putData(Request["token"], "V1", "38");
                            putData(Request["token"], "V3", reader["value"].ToString());
                            putData(Request["token"], "V2", (reader["value"].ToString().Length - 1).ToString());
                            message.id = "0";
                            message.text = "complete running";
                        }
                        else {
                            conn.Close();
                            conn.Open();
                            sql = "INSERT INTO functions (deviceid,functionname,command,frequence) " +
                                "VALUES ( "+Request["deviceid"] +",'" + Request["functionname"] + "','" + reader["value"].ToString() + "',38)";
                            command = new SqlCommand(sql, conn);
                            command.ExecuteScalar();
                            conn.Close();
                            conn.Open();
                            sql = "DELETE FROM learns WHERE id = "+reader["id"];
                            command = new SqlCommand(sql, conn);
                            command.ExecuteScalar();
                            message.id = "0";
                            message.text = "Swiched";
                        }
                    }
                }
                else
                {
                    message.id = "2";
                    message.exception = "token invalid";
                }
            } catch (Exception ex)
            {
                message.id = "1";
                message.exception = "exception : " + ex.ToString();
            }
            conn.Close();
            Response.Write(message.ToString());
        }
        private string putData(string token, string pin, string value)
        {
            WebClient wc = new System.Net.WebClient();
            string webData = "hi";
            try
            {
                Stream stream = wc.OpenRead("https://fra1.blynk.cloud/external/api/update?token=" + token + "&" + pin + "=" + value);
                using (StreamReader reader = new StreamReader(stream))
                {
                    webData = reader.ReadToEnd();
                }
            }
            catch (Exception e) { webData = e.ToString(); }
            return webData;
        }
        public class Message
        {
            //fields
            public string id = "";
            public string text = "";
            public string exception = "";

            //to json function
            public string toJson()
            {
                return "{\"id\":\"" + id + "\"text\":\"" + text + "\",\"exception\":\"" + exception + "\"}";
            }
        }
    }
}