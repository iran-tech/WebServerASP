using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
namespace g
{
    public partial class _Default : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=irantech_SampleDB;User ID=irantech_SampleDB;Password=DBSamplePW;Persist Security Info=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "UPDATE users SET username = '" + Request["username"] + "', password = '" + Request["password"] + "', name = '" + Request["name"]+"' WHERE id = "+Request["id"];
            SqlCommand command = new SqlCommand(sql, conn);
            conn.Open();
            Message message = new Message();
            try
            {
                command.ExecuteScalar();
                message.id = "0";
                message.text = "complete";
            }
            catch (Exception ex)
            {
                message.id = "1";
                message.exception = "error : " + ex.ToString();
            }
            conn.Close();
            Response.Write(message.toJson());
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







