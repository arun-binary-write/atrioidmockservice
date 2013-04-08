using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace SecurityModule
{
    class SWTModule : IHttpModule
    {

        string serviceNamespace = RoleEnvironment.GetConfigurationSettingValue("namespace");
        string acsHostName = "accesscontrol.windows.net";
        string trustedTokenPolicyKey = RoleEnvironment.GetConfigurationSettingValue("key");
        string trustedAudience = RoleEnvironment.GetConfigurationSettingValue("realm");


        void IHttpModule.Dispose()
        {

        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {            
            string headerValue = HttpContext.Current.Request.Headers.Get("Authorization");

            // check that a value is there
            if (string.IsNullOrEmpty(headerValue))
            {
                throw new ApplicationException("unauthorized");
            }

            // check that it starts with 'WRAP'
            if (!headerValue.StartsWith("WRAP "))
            {
                throw new ApplicationException("unauthorized");
            }

            string[] nameValuePair = headerValue.Substring("WRAP ".Length).Split(new char[] { '=' }, 2);

            if (nameValuePair.Length != 2 ||
                nameValuePair[0] != "access_token" ||
                !nameValuePair[1].StartsWith("\"") ||
                !nameValuePair[1].EndsWith("\""))
            {
                throw new ApplicationException("unauthorized");
            }

            // trim off the leading and trailing double-quotes
            string token = nameValuePair[1].Substring(1, nameValuePair[1].Length - 2);

            // create a token validator
            TokenValidator validator = new TokenValidator(
                this.acsHostName,
                this.serviceNamespace,
                this.trustedAudience,
                this.trustedTokenPolicyKey);

            // validate the token
            if (!validator.Validate(token))
            {
                throw new ApplicationException("unauthorized");
            }

        }
    }
}
