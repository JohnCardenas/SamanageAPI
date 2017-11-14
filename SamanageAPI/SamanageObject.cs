using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
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
        private bool _trackChanges = false;
        #endregion // Fields

        #region Properties
        [JsonRequired]
        [JsonProperty(JSON_ID)]
        /// <summary>
        /// ID of this object
        /// </summary>
        public virtual int Id { get; protected set; }

        [JsonIgnore]
        /// <summary>
        /// Reference to a SamanageClient, used for commits and other operations.
        /// </summary>
        internal SamanageClient Client { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// Indicates that the object is tracking changes
        /// </summary>
        private bool TrackChanges
        {
            get { return _trackChanges; }
            set { _trackChanges = value; }
        }

        [JsonIgnore]
        /// <summary>
        /// Contains a list of changes on this object since TrackChanges was set to true.
        /// </summary>
        internal HashSet<string> ChangedProperties { get; private set; }

        [JsonIgnore]
        /// <summary>
        /// If true, this object has been changed from the initial values.
        /// </summary>
        public bool HasChanges
        {
            get { return (ChangedProperties.Count > 0); }
        }
        #endregion // Properties

        #region Constructor
        /// <summary>
        /// Creates a blank SamanageObject belonging to the specified client.
        /// </summary>
        public SamanageObject(SamanageClient client) : this()
        {
            Client = client;
            TrackChanges = true;
        }

        /// <summary>
        /// Creates a blank object
        /// </summary>
        [JsonConstructor]
        internal SamanageObject()
        {
            ChangedProperties = new HashSet<string>();
        }
        #endregion // Constructor

        #region Model Methods
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (TrackChanges)
            {
                // Check if the object has a JsonProperty attribute
                var attributes = GetType().GetProperty(propertyName).GetCustomAttributes(typeof(JsonPropertyAttribute), true);

                // If so, push the property name onto the ChangedProperties list
                if (attributes.Length > 0)
                {
                    ChangedProperties.Add((attributes[0] as JsonPropertyAttribute).PropertyName);
                }
            }

            return base.SetProperty<T>(ref storage, value, propertyName);
        }

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

        #region Serialization Callbacks
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            TrackChanges = true;
        }
        #endregion // Serialization Callbacks
    }
}
