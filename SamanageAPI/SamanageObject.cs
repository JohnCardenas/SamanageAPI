using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SamanageAPI
{
    public abstract class SamanageObject : ModelBase
    {
        #region Constants
        internal const string JSON_ID = "id";
        #endregion // Constants

        #region Fields
        private SamanageClient _client;
        #endregion // Fields

        #region Properties
        [JsonProperty(JSON_ID)]
        /// <summary>
        /// Data layer for modifications to this object
        /// </summary>
        public virtual int Id { get; protected set; }

        /// <summary>
        /// Reference to a SamanageClient, used for commits and other operations.
        /// </summary>
        private SamanageClient Client { get; set; }
        #endregion // Properties

        #region Constructor
        /// <summary>
        /// Creates a blank SamanageObject belonging to the specified client.
        /// </summary>
        public SamanageObject(SamanageClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Creates a blank object
        /// </summary>
        internal SamanageObject() {}
        #endregion // Constructor

        #region Model Methods
        /// <summary>
        /// Commits the changes to this object to the Samanage API
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Commit()
        {
            if (Client == null)
                throw new InvalidOperationException("Attempted commit of object without an associated client!");

            if (HasErrors)
                return false;

            return Client.Commit(this); // Debugging
        }
        #endregion // Model Methods
    }
}
