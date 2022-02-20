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
            string sql = "SELECT * from users WHERE username = '"+Request["username"] + "'";
            SqlCommand command = new SqlCommand(sql, conn);
            User user = new User();
            conn.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                reader.Read();
                    user.id = reader["id"].ToString();
                    user.name = reader["name"].ToString();
                    user.password = reader["password"].ToString();
                    user.username = reader["username"].ToString();
                }
                else {
                    user.id = "-1";
                    user.exception = "no data";
                }
            }
            catch (Exception ex)
            {
                user.id = "1";
                user.exception = "exception : " + ex.ToString();
            }
            conn.Close();
            Response.Write(user.toJson());
        }

        public class User
        {
            //fields
            public string id = "";
            public string name = "";
            public string username = "";
            public string password = "";
            public string exception = "";

            //to json function
            public string toJson()
            {
                return "{\"id\":\"" + id + "\"username\":\"" + username + "\",\"name\":\"" + name + "\",\"password\":\"" + password + "\",\"exception\":\"" + exception + "\"}";
            }
        }
    }








