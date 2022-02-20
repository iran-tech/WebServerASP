using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Net.Http;
namespace IranTech
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task<string> s = MethodAsync();

            //Response.Write(s.Result);
        }
        public async System.Threading.Tasks.Task<string> MethodAsync()
        {
            var baseAddress = new Uri("http://blynk-cloud.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                var res = await httpClient.GetAsync("4ae3851817194e2596cf1b7103603ef8/get/D8");
                string resData = await res.Content.ReadAsStringAsync();
                using (var response = await httpClient.GetAsync("4ae3851817194e2596cf1b7103603ef8/get/D8"))
                {

                    string responseData = await response.Content.ReadAsStringAsync();
                    Response.Write(responseData);
                    return responseData;
                }
            }
        }
    }
}