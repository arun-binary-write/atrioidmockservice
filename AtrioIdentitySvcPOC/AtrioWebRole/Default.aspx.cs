using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AtrioWebRole
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = GetTokenFromACS("http://127.0.0.1:8080/AtrioIdentityService.svc");
            WebClient client = new WebClient();

            string headerValue = string.Format("WRAP access_token=\"{0}\"", token);

            client.Headers.Add("Authorization", headerValue);


            Stream stream = client.OpenRead(@"http://127.0.0.1:8080/AtrioIdentityService.svc/permissions/user/arunprasad.rn@hotmail.com");

            StreamReader reader = new StreamReader(stream);
            String response = reader.ReadToEnd();
        }

        private  string GetTokenFromACS(string scope)
        {
            string wrapPassword = "P@ssw0rd";
            string wrapUsername = "atrioidentityservice";

            // request a token from ACS
            WebClient client = new WebClient();
            client.BaseAddress = string.Format("https://{0}.{1}", "atrioidentityaccesspoc", "accesscontrol.windows.net");

            NameValueCollection values = new NameValueCollection();
            values.Add("wrap_name", wrapUsername);
            values.Add("wrap_password", wrapPassword);
            values.Add("wrap_scope", scope);

            byte[] responseBytes = client.UploadValues("WRAPv0.9/", "POST", values);

            string response = Encoding.UTF8.GetString(responseBytes);

            Console.WriteLine("\nreceived token from ACS: {0}\n", response);

            return HttpUtility.UrlDecode(
                response
                .Split('&')
                .Single(value => value.StartsWith("wrap_access_token=", StringComparison.OrdinalIgnoreCase))
                .Split('=')[1]);
        }
    }
}