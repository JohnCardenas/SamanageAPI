using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanageAPI.UnitTesting
{
    internal interface ISamanageClient
    {
        bool Commit(SamanageObject obj);
    }
}
