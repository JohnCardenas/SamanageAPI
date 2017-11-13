using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SamanageAPI
{
    [JsonObject]
    public sealed class Site : SamanageObject
    {
        #region Fields
        private string _name;
        private string _location;
        private string _description;
        #endregion // Fields

        #region Properties
        [JsonProperty("description")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        [JsonProperty("location")]
        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        #endregion // Properties

        #region Constructors
        internal Site() : base () { }
        public Site(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
