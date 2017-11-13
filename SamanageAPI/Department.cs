using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SamanageAPI
{
    public sealed class Department : SamanageObject
    {
        #region Fields
        private string _name;
        private string _description;
        #endregion // Fields

        #region Properties
        [JsonProperty("description")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        #endregion // Properties

        #region Constructors
        internal Department() : base () { }
        public Department(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
