using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SamanageAPI.JsonConverters;

namespace SamanageAPI
{
    [JsonObject]
    public sealed class Incident : SamanageObject
    {
        #region Fields
        private Principal _assignee;
        private Category _category;
        private DateTime? _created;
        private User _creator;
        private Department _department;
        private string _description;
        private DateTime? _due;
        private string _name;
        private int _number;
        private Priority _priority;
        private User _requester;
        private Site _site;
        private string _state;
        private DateTime? _updated;
        #endregion // Fields

        #region Properties
        [JsonProperty("assignee")]
        [JsonConverter(typeof(PrincipalConverter))]
        public Principal Assignee
        {
            get { return _assignee; }
            set { SetProperty(ref _assignee, value); }
        }

        [JsonProperty("category")]
        public Category Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        [JsonProperty("created_at")]
        public DateTime? Created
        {
            get { return _created; }
            internal set { SetProperty(ref _created, value); }
        }

        /*[JsonProperty("created_by")]
        public User Creator
        {
            get { return _creator; }
            internal set { SetProperty(ref _creator, value); }
        }*/

        [JsonProperty("department")]
        public Department Department
        {
            get { return _department; }
            set { SetProperty(ref _department, value); }
        }

        [JsonProperty("description")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        [JsonProperty("due_at")]
        public DateTime? Due
        {
            get { return _due; }
            internal set { SetProperty(ref _due, value); }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty("number")]
        public int Number
        {
            get { return _number; }
            internal set { SetProperty(ref _number, value); }
        }

        [JsonProperty("priority")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority
        {
            get { return _priority; }
            set { SetProperty(ref _priority, value); }
        }

        /*[JsonProperty("requester")]
        public User Requester
        {
            get { return _requester; }
            set { SetProperty(ref _requester, value); }
        }*/

        [JsonProperty("site")]
        public Site Site
        {
            get { return _site; }
            set { SetProperty(ref _site, value); }
        }

        [JsonProperty("state")]
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        [JsonProperty("updated_at")]
        public DateTime? Updated
        {
            get { return _updated; }
            internal set { SetProperty(ref _updated, value); }
        }
        #endregion // Properties

        #region Constructors
        internal Incident() : base() { }
        public Incident(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
