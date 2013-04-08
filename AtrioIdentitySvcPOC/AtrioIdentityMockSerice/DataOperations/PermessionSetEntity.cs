using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;

namespace AtrioIdentityMockSerice.DataOperations
{
    public class PermessionSetEntity : TableServiceEntity
    {
        public string UserName
        {
            get;
            set;
        }

        public string Permission
        {
            get;
            set;
        }
    }
}