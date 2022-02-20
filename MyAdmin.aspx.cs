using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IranTech
{
    public partial class MyAdmin : System.Web.UI.Page
    {
        struct Column
        {
            public string name;
            public string type;
            public Column(string name, string type)
            {
                this.name = name;
                this.type = type;
            }
            public Column(string nameAndType) {
                string[] NTArray = nameAndType.Split(' ');
                name = NTArray[0];
                string type = "";
                for (int i = 1; i < NTArray.Length; i++)
                    type += " " + NTArray[i];
                this.type = type;
            }
            public override string ToString()
            {
                return name + " " + type;
            }
        }

        SqlConnection conn = new SqlConnection("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=myandroidteam_SampleDB;User ID=myandroidteam_SampleDB;Password=123456789;Persist Security Info=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Column> list = new List<Column>();
            DateTime dt = DateTime.Now;
            list.Add(new Column("time",dt.Second.ToString()));
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //list.Add(new Column("url", url.Remove(0,6)));
            Response.Write(insert("opened", list));
            conn.Close();
            Button1.Click += createClick;
            Button2.Click += dropClick;
            Button3.Click += truncateClick;
            Button4.Click += insertClick;
            Button5.Click += selectClick;
            Button6.Click += deleteClick;
            Button7.Click += updateClick;
        }
        protected void createClick(object sender,EventArgs e) {
            List<Column> columns = new List<Column>();
            string columnsString = TextBox2.Text;
            string[] columnarray = columnsString.Split('\n');
            foreach (string s in columnarray)
                columns.Add(new Column(s));
            Label1.Text = create(TextBox1.Text,columns);
        }

        protected void dropClick(object sender, EventArgs e) {
            Label2.Text = dropTable(TextBox3.Text);
        }

        protected void truncateClick(object sender, EventArgs e)
        {
            Label2.Text = truncateTable(TextBox3.Text);
        }
        protected void insertClick(object sender, EventArgs e)
        {
            List<Column> columns = new List<Column>();
            string columnsString = TextBox5.Text;
            string[] columnarray = columnsString.Split('\n');
            foreach (string s in columnarray)
                columns.Add(new Column(s));
            Label3.Text = insert(TextBox4.Text,columns);
        }
        protected void selectClick(object sender, EventArgs e)
        {
            Label4.Text = select(TextBox6.Text, null, TextBox7.Text.Length ==0 ? null : TextBox7.Text);
        }
        protected void deleteClick(object sender, EventArgs e) {
            Label5.Text = delete(TextBox8.Text, TextBox9.Text);
        }
        protected void updateClick(object sender, EventArgs e)
        {
            List<Column> list = new List<Column>();
            list.Add(new Column(TextBox12.Text));
            Label6.Text = update(TextBox10.Text, list, TextBox11.Text);
        }

        string create(string tablename, List<Column> columns)
        {
            
            try
            {
                string query = "CREATE TABLE " + tablename + " (";
                query += columns[0].ToString();
                for (int i = 1; i < columns.Count; i++)
                {
                    query += "," + columns[i].ToString(); ;
                }
                query += ");";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Table " + tablename + " created successfully";
            }
            catch (Exception e)
            {
                return "can't create "+tablename+" . something went wrong ! : " + e.ToString();
            }
        }
        string insert(string tablename, List<Column> columns)
        {

            try
            {
                string query = "INSERT INTO " + tablename + " (";
                query += columns[0].name;
                for (int i = 1; i < columns.Count; i++)
                {
                    query += "," + columns[i].name;
                }
                query += ")";
                query += " VALUES (";
                query += columns[0].type;
                for (int i = 1; i < columns.Count; i++)
                {
                    query += ", " + columns[i].type ;
                }
                query += ")";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Inserted into " + tablename;
            }
            catch (Exception e)
            {
                return "can't insert " + tablename +" for data "+ TextBox5.Text +" . something went wrong ! : " + e.ToString();
            }
        }
        string select(string tablename, string condition, string columns)
        {

            try
            {

                string query = "SELECT ";

                if (columns == null || columns.Length == 0)
                {
                    query += "*";
                }
                else
                {
                    query += columns;
                }

                query += " FROM " + tablename;

                if (condition != null)
                {
                    query += " WHERE " + condition;

                }

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                conn.Close();
                return "data selected from " + tablename + "successfully ";
            }
            catch (Exception e)
            {
                return "can't select from " + tablename + " . something went wrong ! : " + e.ToString();
            }

        }
        string update(string tablename, List<Column> updates, string condition)
        {

            try
            {
                string query = "UPDATE " + tablename + " SET ";
                query += updates[0].name + "='" + updates[0].type + "'";
                for (int i = 1; i < updates.Count; i++)
                {
                    query += "," + updates[i].name + "='" + updates[i].type + "'";
                }
                query += " WHERE " + condition;
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Updated " + tablename;
            }
            catch (Exception e)
            {
                return "can't update " + tablename + " . something went wrong ! : " + e.ToString();
            }
        }
        string delete(string tablename, string condition)
        {

            try
            {
                string query = "DELETE FROM " + tablename;
                query += " WHERE " + condition + ";";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Deleted [ " + condition + " ] from " + tablename;
            }
            catch (Exception e)
            {
                return "can't delete "+tablename+" . something went wrong ! : " + e.ToString();
            }
        }
        string dropTable(string tablename)
        {

            try
            {
                string query = "DROP TABLE " + tablename + ";";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return tablename + " Droped !";
            }
            catch (Exception e)
            {
                return "can't drop " + tablename + " . something went wrong ! : " + e.ToString();
            }
        }
        string truncateTable(string tablename)
        {

            try
            {
                string query = "TRUNCATE TABLE " + tablename + ";";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return tablename + " Truncated !";
            }
            catch (Exception e)
            {
                return "can't truncate " + tablename + " . something went wrong ! : " + e.ToString();
            }
        }

    }
}