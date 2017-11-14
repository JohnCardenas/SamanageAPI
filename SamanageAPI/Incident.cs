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
        #region Constants
        internal const string JSON_ASSIGNEE           = "assignee";
        internal const string JSON_CATEGORY           = "category";
        internal const string JSON_CREATED            = "created_at";
        internal const string JSON_CREATOR            = "created_by";
        internal const string JSON_DEPARTMENT         = "department";
        internal const string JSON_DESCRIPTION        = "description";
        internal const string JSON_DUE                = "due_at";
        internal const string JSON_IS_SERVICE_REQUEST = "is_service_request";
        internal const string JSON_NAME               = "name";
        internal const string JSON_NUMBER             = "number";
        internal const string JSON_PRIORITY           = "priority";
        internal const string JSON_REQUESTER          = "requester";
        internal const string JSON_SITE               = "site";
        internal const string JSON_STATE              = "state";
        internal const string JSON_UPDATED            = "updated_at";
        #endregion // Constants

        #region Fields
        private Principal _assignee;
        private Category _category;
        private DateTime? _created;
        private UserStub _creator;
        private Department _department;
        private string _description;
        private DateTime? _due;
        private bool _isServiceRequest;
        private string _name;
        private int _number;
        private Priority _priority;
        private UserStub _requester;
        private Site _site;
        private string _state;
        private DateTime? _updated;
        #endregion // Fields

        #region Properties
        [JsonProperty(JSON_ASSIGNEE)]
        [JsonConverter(typeof(PrincipalConverter))]
        public Principal Assignee
        {
            get { return _assignee; }
            set { SetProperty(ref _assignee, value); }
        }

        [JsonProperty(JSON_CATEGORY)]
        public Category Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        [JsonProperty(JSON_CREATED)]
        public DateTime? Created
        {
            get { return _created; }
            internal set { SetProperty(ref _created, value); }
        }

        [JsonProperty(JSON_CREATOR)]
        public UserStub Creator
        {
            get { return _creator; }
            internal set { SetProperty(ref _creator, value); }
        }

        [JsonProperty(JSON_DEPARTMENT)]
        public Department Department
        {
            get { return _department; }
            set { SetProperty(ref _department, value); }
        }

        [JsonProperty(JSON_DESCRIPTION)]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        [JsonProperty(JSON_DUE)]
        public DateTime? Due
        {
            get { return _due; }
            internal set { SetProperty(ref _due, value); }
        }

        [JsonProperty(JSON_IS_SERVICE_REQUEST)]
        public bool IsServiceRequest
        {
            get { return _isServiceRequest; }
            internal set { SetProperty(ref _isServiceRequest, value); }
        }

        [JsonProperty(JSON_NAME)]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty(JSON_NUMBER)]
        public int Number
        {
            get { return _number; }
            internal set { SetProperty(ref _number, value); }
        }

        [JsonProperty(JSON_PRIORITY)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority
        {
            get { return _priority; }
            set { SetProperty(ref _priority, value); }
        }

        [JsonProperty(JSON_REQUESTER)]
        public UserStub Requester
        {
            get { return _requester; }
            set { SetProperty(ref _requester, value); }
        }

        [JsonProperty(JSON_SITE)]
        public Site Site
        {
            get { return _site; }
            set { SetProperty(ref _site, value); }
        }

        [JsonProperty(JSON_STATE)]
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        [JsonProperty(JSON_UPDATED)]
        public DateTime? Updated
        {
            get { return _updated; }
            internal set { SetProperty(ref _updated, value); }
        }
        #endregion // Properties

        #region Constructors
        [JsonConstructor]
        internal Incident() : base() { }
        public Incident(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
