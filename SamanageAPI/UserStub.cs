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
    public class UserStub : Principal
    {
        #region Fields
        #endregion // Fields

        #region Properties
        public override int Id
        {
            get { return UserId; }
            protected set { base.Id = value; }
        }

        [JsonProperty("user_id")]
        internal int UserId
        {
            get; set;
        }
        #endregion // Properties

        #region Constructors
        internal UserStub() : base () { }
        public UserStub(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
