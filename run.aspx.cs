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
    public partial class run : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            Response.Write(dt.ToString());
            int[] rawData = { 2394,602,1170,628,572,626,1222,580,670,526,1124,678,544,652,600,600,1300,498,598,602,  522, 678,  626, 574,  572, 25840,  2348, 650,  1174, 628,  596, 578,  1172, 652,  594, 606,  1226, 570,  546, 652,  574, 626,  1304, 496,  722, 480,  724, 472,  628, 574,  570, 25836,  2454, 544,  1252, 548,  652, 546,  1148, 652,  616, 580,  1146, 654,  546, 654,  620, 578,  1148, 650,  574, 626,  602, 598,  624, 578,  596};
            string data = "2394,602,1170,628,572,626,1222,580,670,526,1124,678,544,652,600,600,1300,498,598,602,  522, 678,  626, 574,  572, 25840,  2348, 650,  1174, 628,  596, 578,  1172, 652,  594, 606,  1226, 570,  546, 652,  574, 626,  1304, 496,  722, 480,  724, 472,  628, 574,  570, 25836,  2454, 544,  1252, 548,  652, 546,  1148, 652,  616, 580,  1146, 654,  546, 654,  620, 578,  1148, 650,  574, 626,  602, 598,  624, 578,  596,";
            put("x1CkiZ569Ht5e9SRyIcz5hlCAvq3893n", "V1", "38");
            put("x1CkiZ569Ht5e9SRyIcz5hlCAvq3893n", "V3", data);
            put("x1CkiZ569Ht5e9SRyIcz5hlCAvq3893n", "V2", rawData.Length.ToString());
            DateTime dt2 = DateTime.Now;
            Response.Write(dt2.ToString());
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

        private string put(string token, string pin, string value)
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
    }
}