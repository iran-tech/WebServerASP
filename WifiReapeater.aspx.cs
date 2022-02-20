using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
namespace c
{
    public partial class _Default : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {

            Message message = new Message();
            if (Request["act"].Equals("on"))
            {
                putData(Request["token"], "V6", "1");
                message.id = "0";
                message.text = "complete";
            }
            else if (Request["act"].Equals("off"))
            {
                putData(Request["token"], "V6", "0");
                message.id = "0";
                message.text = "complete";
            }
            else {
                message.id = "0";
                message.text = get(Request["token"], "V6");
            }
            Response.Write(message.ToString());
        }
        private string get(string token, string pin)
        {
            WebClient wc = new WebClient();
            string webData = "hi";
            try
            {
                Stream stream = wc.OpenRead("https://fra1.blynk.cloud/external/api/get?token=" + token + "&" + pin);
                using (StreamReader reader = new StreamReader(stream))
                {
                    webData = reader.ReadToEnd();
                }
            }
            catch (Exception e) { webData = e.ToString(); }
            return webData;
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