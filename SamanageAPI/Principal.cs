using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SamanageAPI.JsonConverters;

namespace SamanageAPI
{
    [JsonObject]
    public abstract class Principal : SamanageObject
    {
        #region Constants
        internal const string JSON_NAME     = "name";
        internal const string JSON_EMAIL    = "email";
        internal const string JSON_DISABLED = "disabled";
        #endregion // Constants

        #region Fields
        protected string _name;
        protected string _email;
        protected bool _disabled;
        #endregion // Fields

        #region Properties
        [Required]
        [JsonRequired]
        [JsonProperty(JSON_NAME)]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty(JSON_EMAIL)]
        public virtual string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        [JsonProperty(JSON_DISABLED)]
        public bool Disabled
        {
            get { return _disabled; }
            set { SetProperty(ref _disabled, value); }
        }
        #endregion // Properties

        #region Constructors
        internal Principal() : base () { }
        public Principal(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
