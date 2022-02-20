using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IranTech
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //string token = "JMxi59ZW7pm0uiCwbtOO5umc2DovKm80";
            //Response.Write(get(token, "V4"));
            //Response.Write(isOnline(token));

            Button1.Click += getdata;
            Button2.Click += putData;
            Button3.Click += check;
            Button4.Click += LEDon;
            Button5.Click += LEDoff;
            Button6.Click += TVonoff;
            Button7.Click += VolP;
            Button8.Click += VolM;
        }
        private string get(string token,string pin) {
            WebClient wc = new WebClient();
            string webData = "hi";
            try
            {
                Stream stream = wc.OpenRead("https://fra1.blynk.cloud/external/api/get?token="+token+"&"+pin);
                using (StreamReader reader = new StreamReader(stream))
                {
                    webData = reader.ReadToEnd();
                }
            }
            catch (Exception e) { webData = e.ToString(); }
            return webData;
        }
        private string put(string token, string pin,string value)
        {
            WebClient wc = new System.Net.WebClient();
            string webData = "hi";
            try
            {
                Stream stream = wc.OpenRead("https://fra1.blynk.cloud/external/api/update?token=" + token + "&" + pin + "="+value);
                using (StreamReader reader = new StreamReader(stream))
                {
                    webData = reader.ReadToEnd();
                }
            }
            catch (Exception e) { webData = e.ToString(); }
            return webData;
        }
        private string isOnline(string token) {
            WebClient wc = new System.Net.WebClient();
            string webData = "hi";
            try
            {
                Stream stream = wc.OpenRead("https://blynk.cloud/external/api/isHardwareConnected?token="+token);
                using (StreamReader reader = new StreamReader(stream))
                {
                    webData = reader.ReadToEnd();
                }
            }
            catch (Exception e) { webData = e.ToString(); }
            return webData;
        }
        protected void getdata(object sender, EventArgs e) {
            if(!TextBox3.Text.Equals("pin"))
                Label1.Text = get(TextBox1.Text, TextBox3.Text);
        }
        protected void putData(object sender, EventArgs e) {
            if (!TextBox3.Text.Equals("pin") && !TextBox4.Text.Equals("pin"))
                put(TextBox1.Text, TextBox3.Text,TextBox4.Text);
        }
        protected void check(object sender, EventArgs e) {
            Label2.Text = isOnline(TextBox1.Text);
        }
        protected void LEDon(object sender, EventArgs e)
        {
            put(TextBox1.Text, "V4", "1");
        }
        protected void LEDoff(object sender, EventArgs e)
        {
            put(TextBox1.Text, "V4", "0");
        }
        protected void TVonoff(object sender, EventArgs e)
        {
            put(TextBox1.Text, "V11", "2704");
        }
        protected void VolP(object sender, EventArgs e)
        {
            put(TextBox1.Text, "V11", "1168");
        }
        protected void VolM(object sender, EventArgs e)
        {
            put(TextBox1.Text, "V11", "3216");
        }
    }
}