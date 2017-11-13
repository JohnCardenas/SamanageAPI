using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SamanageAPI
{
    [JsonObject]
    public sealed class Category : SamanageObject
    {
        #region Constants
        internal const string JSON_NAME     = "name";
        internal const string JSON_CHILDREN = "children";
        #endregion // Constants

        #region Fields
        private string _name;
        private List<Category> _children = new List<Category>();
        #endregion // Fields

        #region Properties
        [JsonProperty(JSON_NAME)]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty(JSON_CHILDREN)]
        public List<Category> Children
        {
            get { return _children; }
            internal set { SetProperty(ref _children, value); }
        }
        #endregion // Properties

        #region Constructors
        internal Category() : base() { }
        public Category(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
