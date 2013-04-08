using AtrioIdentityMockSerice.DataContracts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace AtrioWebRole
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClaimsIdentity identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            Claim nameClaim = identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier);// Live ID does not give Email Claim Type.
            
            string token = GetTokenFromACS("http://127.0.0.1:8080/AtrioIdentityService.svc");
            WebClient client = new WebClient();

            string headerValue = string.Format("WRAP access_token=\"{0}\"", token);

            client.Headers.Add("Authorization", headerValue);


            Stream stream = client.OpenRead(string.Format(@"http://127.0.0.1:8080/AtrioIdentityService.svc/permissions/user/{0}", nameClaim.Value));

            StreamReader reader = new StreamReader(stream);
            String response = reader.ReadToEnd();
            PermissionSet permissionSet = Deserialize(response, typeof(PermissionSet)) as PermissionSet;
            
            if (!permissionSet.Permessions.Contains("button1"))
            {
                Button1.Visible = false;
            }
            
            if (!permissionSet.Permessions.Contains("button2"))
            {
                Button2.Visible = false;
            }
        }

        public static object Deserialize(string xml, Type toType)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(toType);
                return deserializer.ReadObject(stream);
            }
        }

        private string GetTokenFromACS(string scope)
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