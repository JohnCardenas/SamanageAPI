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
        #region Constants
        internal const string JSON_DESCRIPTION = "description";
        #endregion // Constants

        #region Fields
        private string _description;
        #endregion // Fields

        #region Properties
        [JsonProperty(JSON_DESCRIPTION)]
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
