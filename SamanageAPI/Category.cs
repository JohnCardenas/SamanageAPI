using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SamanageAPI
{
    [JsonObject]
    public class Category : SamanageObject
    {
        #region Fields
        private string _name;
        private List<Category> _children = new List<Category>();
        #endregion // Fields

        #region Properties
        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty("children")]
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
