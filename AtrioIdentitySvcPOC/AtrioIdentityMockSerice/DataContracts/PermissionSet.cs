using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AtrioIdentityMockSerice.DataContracts
{
    [DataContract]
    public class PermissionSet
    {
        [DataMember]
        public string UserName
        {
            get;
            set;
        }

        [DataMember]
        public List<string> Permessions
        {
            get;
            set;
        }
    }
}