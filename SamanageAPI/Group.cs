using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SamanageAPI
{
    [JsonObject]
    public sealed class Group : Principal
    {
        #region Fields
        private string _description;
        #endregion // Fields

        #region Properties
        [JsonProperty("description")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        #endregion // Properties

        #region Constructors
        internal Group() : base () { }
        public Group(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
