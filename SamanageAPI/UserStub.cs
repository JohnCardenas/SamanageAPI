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
    public sealed class UserStub : Principal
    {
        #region Constants
        internal const string JSON_USER_ID = "user_id";
        #endregion // Constants

        #region Fields
        #endregion // Fields

        #region Properties
        public override int Id
        {
            get { return UserId; }
            protected set { base.Id = value; }
        }

        [JsonProperty(JSON_USER_ID)]
        internal int UserId
        {
            get; set;
        }
        #endregion // Properties

        #region Constructors
        [JsonConstructor]
        internal UserStub() : base () { }
        public UserStub(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
