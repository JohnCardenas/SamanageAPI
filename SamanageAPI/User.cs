using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SamanageAPI
{
    [JsonObject]
    public sealed class User : Principal
    {
        #region Constants
        internal const string JSON_CREATED      = "created_at";
        internal const string JSON_DEPARTMENT   = "department";
        internal const string JSON_LAST_LOGIN   = "last_login";
        internal const string JSON_MANAGER      = "reports_to";
        internal const string JSON_MOBILE_PHONE = "mobile_phone";
        internal const string JSON_PHONE        = "phone";
        internal const string JSON_ROLE         = "role";
        internal const string JSON_SITE         = "site";
        internal const string JSON_TITLE        = "title";
        #endregion // Constants

        #region Fields
        private DateTime? _created;
        private Department _department;
        private DateTime? _lastLogin;
        private User _manager;
        private string _mobilePhone;
        private string _phone;
        private Role _role;
        private Site _site;
        private string _title;
        #endregion // Fields

        #region Properties
        [JsonProperty(JSON_CREATED)]
        public DateTime? Created
        {
            get { return _created; }
            internal set { SetProperty(ref _created, value); }
        }

        [JsonProperty(JSON_DEPARTMENT)]
        public Department Department
        {
            get { return _department; }
            set { SetProperty(ref _department, value); }
        }

        [Required]
        [JsonRequired]
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        [JsonProperty(JSON_LAST_LOGIN)]
        public DateTime? LastLogin
        {
            get { return _lastLogin; }
            internal set { SetProperty(ref _lastLogin, value); }
        }

        [JsonProperty(JSON_MANAGER)]
        public User Manager
        {
            get { return _manager; }
            set { SetProperty(ref _manager, value); }
        }

        [JsonProperty(JSON_MOBILE_PHONE)]
        public string MobilePhone
        {
            get { return _mobilePhone; }
            set { SetProperty(ref _mobilePhone, value); }
        }

        [JsonProperty(JSON_PHONE)]
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        [JsonProperty(JSON_ROLE)]
        public Role Role
        {
            get { return _role; }
            internal set { SetProperty(ref _role, value); }
        }

        [JsonProperty(JSON_SITE)]
        public Site Site
        {
            get { return _site; }
            set { SetProperty(ref _site, value); }
        }

        [JsonProperty(JSON_TITLE)]
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion // Properties

        #region Constructors
        internal User() : base () { }
        public User(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
