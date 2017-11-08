using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanageAPI
{
    public enum ServiceApi
    {
        GLOBAL = 1,
        EUROPE = 2
    }
    
    public enum ApiVersion
    {
        v2_1 = 21
    }

    public enum ReferenceType
    {
        Unknown = 0,
        Email,
        Name,
        Number,
        Id
    }
}
