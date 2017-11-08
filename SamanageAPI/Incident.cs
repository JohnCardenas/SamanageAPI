using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanageAPI
{
    public sealed class Incident : SamanageObject
    {
        #region Properties
        public string Name
        {
            get { return GetPrimitiveValue<string>("name"); }
            set { SetPrimitiveValue("name", value); }
        }

        public string Requester
        {
            get { return GetReferenceValue("requester", ReferenceType.Email); }
            set { SetReferenceValue("requester", ReferenceType.Email, value); }
        }
        #endregion // Properties

        #region Constructors
        internal Incident(ExpandoObject obj, bool existingObject = false, bool readOnly = true) : base(obj, existingObject, readOnly) { }
        public Incident(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
