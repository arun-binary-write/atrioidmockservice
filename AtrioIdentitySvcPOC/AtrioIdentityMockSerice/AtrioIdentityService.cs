using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using AtrioIdentityMockSerice.DataContracts;
using AtrioIdentityMockSerice.DataOperations;

namespace AtrioIdentityMockSerice
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AtrioIdentityService : IAtrioIdentityService
    {
        public DataContracts.PermissionSet GetPermissionSet(string userName)
        {
            PermissionSet permissionSet = null;

            var cloudStorageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("storageconnectionstring"));
            PermissionSetContext context = new PermissionSetContext(cloudStorageAccount.TableEndpoint.AbsoluteUri, cloudStorageAccount.Credentials);
            
            var permissionsFromStorage = (from permession in context.Permissions
                                          where permession.UserName == userName.ToLower()
                                          select permession).ToList();

            permissionSet = new PermissionSet();
            permissionSet.UserName = userName;
            permissionSet.Permessions = new List<string>();
            permissionsFromStorage.ForEach(permission => 
            {
                
                permissionSet.Permessions.Add(permission.Permission);

            });

            return permissionSet;
        }
    }
}