using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
    public partial class _Default : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=irantech_SampleDB;User ID=irantech_SampleDB;Password=DBSamplePW;Persist Security Info=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * from devices";
            SqlCommand command = new SqlCommand(sql, conn);
            conn.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                
            }
            catch (Exception ex)
            {
                Response.Write("error : "+ex.ToString());
            }
            conn.Close();
            
        }

    }








