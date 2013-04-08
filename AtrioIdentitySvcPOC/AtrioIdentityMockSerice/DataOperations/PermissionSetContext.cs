using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace AtrioIdentityMockSerice.DataOperations
{
    public class PermissionSetContext : TableServiceContext
    {
        public PermissionSetContext(string address, StorageCredentials credentials)
            : base(address, credentials)
        {

        }

        public IQueryable<PermessionSetEntity> Permissions
        {
            get
            {
                return this.CreateQuery<PermessionSetEntity>("permissionset");
            }

        }
    }
}