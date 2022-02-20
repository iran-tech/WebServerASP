using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;

    public partial class _Default : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=irantech_SampleDB;User ID=irantech_SampleDB;Password=DBSamplePW;Persist Security Info=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * from functions where deviceid = '" + Request["deviceid"] + "'"
                + " AND functionname = " + Request["functionname"];
            SqlCommand command = new SqlCommand(sql, conn);
            Message message = new Message();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    message.id = "0";
                    message.text = "complete";
                    reader.Read();
                    //string data = "2394,602,1170,628,572,626,1222,580,670,526,1124,678,544,652,600,600,1300,498,598,602,  522, 678,  626, 574,  572, 25840,  2348, 650,  1174, 628,  596, 578,  1172, 652,  594, 606,  1226, 570,  546, 652,  574, 626,  1304, 496,  722, 480,  724, 472,  628, 574,  570, 25836,  2454, 544,  1252, 548,  652, 546,  1148, 652,  616, 580,  1146, 654,  546, 654,  620, 578,  1148, 650,  574, 626,  602, 598,  624, 578,  596,";
                    string data = reader["command"].ToString();
                    putData(Request["token"], "V1", reader["frequence"].ToString());
                    putData(Request["token"], "V3", data);
                    putData(Request["token"], "V2", (data.Split(',').Length - 1).ToString());
                }
                else {
                    message.id = "2";
                    message.exception = "operation invalid";
                }
            }
            catch (Exception ex)
            {
                message.id = "1";
                message.exception = "exception : " + ex.ToString();
            }
            conn.Close();
            Response.Write(message.toJson());
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








