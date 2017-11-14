using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamanageAPI.UnitTesting;

namespace SamanageAPI
{
    public class SamanageClient : ISamanageClient
    {
        public bool Commit(SamanageObject obj)
        {
            return true;
        }
    }
}
