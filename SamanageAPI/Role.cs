using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SamanageAPI
{
    public class Role : SamanageObject
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

        #region Properties
        #endregion // Properties

        #region Constructors
        internal Role() : base() { }
        public Role(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
