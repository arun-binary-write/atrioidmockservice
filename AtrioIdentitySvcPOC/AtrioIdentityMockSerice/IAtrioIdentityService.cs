using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using AtrioIdentityMockSerice.DataContracts;

namespace AtrioIdentityMockSerice
{
    [ServiceContract]
    public interface IAtrioIdentityService
    {
        [WebGet(UriTemplate = "/permissions/user/{username}", ResponseFormat = WebMessageFormat.Xml)]
        [OperationContract]
        PermissionSet GetPermissionSet(string userName);
    }
}
