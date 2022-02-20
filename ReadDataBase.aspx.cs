using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IranTech
{
    public partial class ReadDataBase : System.Web.UI.Page
    {
        string sql = "";
        struct Column
        {
            public string name;
            public string type;
            public Column(string name, string type)
            {
                this.name = name;
                this.type = type;
            }
            public Column(string nameAndType)
            {
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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            List<model> all = new List<model>();
            string text = File.ReadAllText(@"C:\Users\Mohamad\source\repos\IranTech\IranTech\devices.txt");
            string[] devicesStr = text.Split(new string[] { "},{" }, StringSplitOptions.None);
            foreach (string str in devicesStr)
            {
                string temp = str[0] == '{' ? str + "}" : str[str.Length - 1] == '}' ? "{" + str : "{" + str + "}";
                model json = new model(temp);
                all.Add(json);
            }
            setType(all);
            foreach (model m in all) {
                
                List<Column> list = new List<Column>() ;
                list.Add(new Column("name", "'" + m.name + "'"));
                list.Add(new Column("type", "'" + m.type + "'"));
                list.Add(new Column("kind", "'"+m.brand+"'"));
                string str = insert("devices", list);

            }
            GridView1.DataSource = all;
            GridView1.DataBind();

            //JObject json = JObject.Parse(str);
            //Response.Write(text);
            //List<string> brands = BrandsList("http://www.remotecentral.com/cgi-bin/codes/");
            //List<model> list = getAll("http://www.remotecentral.com/cgi-bin/codes/");
            //Response.Write(model.toJson(list));
        }
        string insert(string tablename, List<Column> columns)
        {

            try
            {
                SqlConnection conn = new SqlConnection("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=irantech_SampleDB;User ID=irantech_SampleDB;Password=DBSamplePW;Persist Security Info=True;");
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
                    query += ", " + columns[i].type;
                }
                query += ")";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                this.sql = query;
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Inserted into " + tablename;
            }
            catch (Exception e)
            {
                return "can't insert " + tablename + " for data  . something went wrong ! : " + e.ToString();
            }
        }
        private void setType(List<model> list) {
            foreach (model m in list)
            {
                if (m.brand.Equals("canon"))
                    m.type = "cam";
                else if (m.name.ToLower().Contains("video projector")|| (m.name.ToLower().Contains("mp")))
                    m.type = "VP";
                else if (m.name.Contains("DVD") ||
                    m.name.Contains("CD") || m.name.ToLower().Contains("video"))
                    m.type = "DVD";
                else if (m.name.ToLower().Contains("audio")||
                    m.name.ToLower().Contains("ct-610") ||
                    m.name.ToLower().Contains("av-") ||
                    m.brand.ToLower().Contains("audio") ||
                    m.brand.ToLower().Contains("classe"))
                    m.type = "AS";
                else if (m.name.ToLower().Contains("tv"))
                    m.type = "TV";
                else if (m.name.ToLower().Contains("reciever"))
                    m.type = "reciever";
                else if (m.name.ToLower().Contains("monitor") || m.name.ToLower().Contains("lcd"))
                    m.type = "reciever";
                else if (m.name.ToLower().Contains("lc")|| m.brand.ToLower().Contains("light"))
                    m.type = "light";
                else if (m.brand.ToLower().Contains("niles"))
                    m.type = "safety";
                else if (m.name.ToLower().Contains("vcs"))
                    m.type = "VC";


            }
        
        }
        private List<model> getAll(string url) {
            List<model> result = new List<model>();
            List<string> brands = BrandsList(url);
            foreach(string brand in brands)
            {
                List<model> list = ModelTableString(url, brand);
                addAll<model>(result,list);
            }

            return result;
        }
        private void addAll<T>(List<T> main, List<T> add) {
            foreach (T t in add)
                main.Add(t);
        }
        private List<model> ModelTableString(string url,string brand)
        {
            string html = get(url+brand);
            string[] tr = html.Split(new string[] { "<tr>" }, StringSplitOptions.None);
            string[] a = tr[11].Split(new string[] { "<a" }, StringSplitOptions.None);
            List<string> herfs = new List<string>();
            List<string> names = new List<string>();
            List<model> result = new List<model>();
            for (int i = 1; i < a.Length; i++) { 
                string[] nodes = a[i].Split(new string[] { "\"" }, StringSplitOptions.None);
                string[] bo = nodes[2].Split('<');
                string[] bc = bo[0].Split('>');
                model model = new model();
                model.brand = brand;
                model.name = bc[1];
                result.Add(model);
                names.Add(bc[1]); 
                herfs.Add(nodes[1]);
            }
            //string sub = subBetween(html, "Please select a brand to continue:", "Return to the ");
            //return subBefore(sub, "3M");
            return result;
        }
        private string removeOdds(string str) {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
                if (c!= 60)
                    sb.Append(c);
            return sb.ToString();
        }
        private string toByteArrayString(string str) {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                int i = c;
                sb.Append(i.ToString()).Append(',');
            }
            return sb.ToString();
        }
        private List<string> BrandsList(string url) {
            string[] br = BarndTableString(url).Split('(');
            List<string> result = new List<string>();
            result.Add(subTo(br[0],"<"));
            foreach (string str in br)
                try
                {
                    string[] br2 = str.Split(')');
                    string[] br3 = br2[1].Split('>');
                    result.Add(subTo(br3[4],"<").Trim());
                }
                catch { }
            return result;
        }
        private string BarndTableString(string url) {
            string html = get(url);
            string sub = subBetween(html, "Please select a brand to continue:", "Return to the ");
            return subBefore(sub, "3M");
        }
        private string insertjim(string main) {
            StringBuilder sb = new StringBuilder();
            foreach (char c in main)
                sb.Append(c).Append("iran");
            return sb.ToString();
        }
        public string subBetween(string main, string from,string to)
        {
            string fromStr = subFrom(main, from);
            return  subTo(fromStr, to);
        }
        private string subBefore(string main, string before)
        {
            int start = WhereFirst(main, before);
            return main.Substring(start - before.Length + 1, main.Length - start - 1);
        }
        private string subFrom(string main,string from) {
            int start = WhereFirst(main,from);
            return main.Substring(start, main.Length - start - 1);
        }
        private string subTo(string main, string to)
        {
            int end = WhereFirst(main, to);
            return main.Substring(0,end-to.Length+1);
        }
        private string get(String url)
        {
            WebClient wc = new WebClient();
            string webData = "hi";
            try
            {
                Stream stream = wc.OpenRead(url);
                using (StreamReader reader = new StreamReader(stream))
                {
                    webData = reader.ReadToEnd();
                }
            }
            catch (Exception e) { webData = e.ToString(); }
            return webData;
        }
        private int WhereFirst(string main,string find) {
            int k = 0;
            for (int i = 0; i < main.Length; i++) {
                if (main[i] == find[k])
                {
                    if (k == find.Length - 1)
                    {
                        return i;
                    }
                    else
                    {
                        k++;
                    }
                }
                else
                {
                    k = 0;
                }
            }
            return -1;
        }
    }
    class model {
        public string brand { get; set; }
        public string type { get; set; } = "?";
        public string name { get; set; }
        public model() {
            
        }
        public model(string json) {
            brand = subBetween(json, "brand\":\"", "\",\"type");
            type = subBetween(json, "type\":\"", "\",\"name");
            name = subBetween(json, "name\":\"", "\"");
        }
        public string toJson() {
            return "{\"brand\":\"" + brand + "\",\"type\":\"" + type + "\",\"name\":\"" + name + "\"}";
        }
        public static string toJson(List<model> list) {
            StringBuilder sb = new StringBuilder("[");
            foreach (model m in list)
                sb.Append(m.toJson()).Append(",");
            sb.Append("]");
            return sb.ToString();
        }
        public string subBetween(string main, string from, string to)
        {
            string fromStr = subFrom(main, from);
            return subTo(fromStr, to);
        }
        private string subBefore(string main, string before)
        {
            int start = WhereFirst(main, before);
            return main.Substring(start - before.Length + 1, main.Length - start - 1);
        }
        private string subFrom(string main, string from)
        {
            int start = WhereFirst(main, from)+1;
            return main.Substring(start, main.Length - start - 1);
        }
        private string subTo(string main, string to)
        {
            int end = WhereFirst(main, to);
            return main.Substring(0, end - to.Length + 1);
        }
        private int WhereFirst(string main, string find)
        {
            int k = 0;
            for (int i = 0; i < main.Length; i++)
            {
                if (main[i] == find[k])
                {
                    if (k == find.Length - 1)
                    {
                        return i;
                    }
                    else
                    {
                        k++;
                    }
                }
                else
                {
                    k = 0;
                }
            }
            return -1;
        }
    }
}