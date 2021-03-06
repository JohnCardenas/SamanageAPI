﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SamanageAPI
{
    public sealed class Department : SamanageObject
    {
        #region Constants
        internal const string JSON_DESCRIPTION = "description";
        internal const string JSON_NAME        = "name";
        #endregion // Constants

        #region Fields
        private string _name;
        private string _description;
        #endregion // Fields

        #region Properties
        [JsonProperty(JSON_DESCRIPTION)]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        [Required]
        [JsonRequired]
        [JsonProperty(JSON_NAME)]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        #endregion // Properties

        #region Constructors
        [JsonConstructor]
        internal Department() : base () { }
        public Department(SamanageClient client) : base(client) { }
        #endregion // Constructors
    }
}
