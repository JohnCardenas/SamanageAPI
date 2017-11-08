using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamanageAPI.Utility;

namespace SamanageAPI
{
    public static class SamanageReference
    {
        internal static string ReferenceTypeToString(ReferenceType refType)
        {
            return refType.ToString().ToLower();
        }
    }
}
