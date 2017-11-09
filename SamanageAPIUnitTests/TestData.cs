using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanageAPIUnitTests
{
    public static class TestData
    {
        #region Assignee
        public static readonly Dictionary<string, object> Assignee = new Dictionary<string, object>
        {
            { "id",          2889544                               },
            { "name",        "Financial Applications"              },
            { "description", "The Financial Applications group"    },
            { "email",       null                                  },
            { "disabled",    false                                 },
            { "is_user",     false                                 },
            { "user_id",     null                                  },
            {
                "avatar", new Dictionary<string, object>
                {
                    { "type",  "group"  },
                    { "color", "#2eabcf"}
                }
            },
            { "memberships", new List<Dictionary<string,object>>() }
        };
        #endregion // Assignee

        #region Category
        public static readonly Dictionary<string, object> Category = new Dictionary<string, object>
        {
            { "id",                   617815          },
            { "name",                 "Root Category" },
            { "default_tags",         "root category" },
            {
                "children", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        { "id",                  617816             },
                        { "name",                "Child Category 1" },
                        { "default_tags",        "child category 1" },
                        { "children",            null               },
                        { "parent_id",           617815             },
                        { "default_assignee_id", null               }
                    },
                    new Dictionary<string, object>
                    {
                        { "id",                  617817             },
                        { "name",                "Child Category 2" },
                        { "default_tags",        "child category 2" },
                        { "parent_id",           617815             },
                        { "default_assignee_id", null               }
                    }
                }
            },
            { "parent_id",           null             },
            { "default_assignee_id", 2886810          }
        };
        #endregion // Category

        #region Department
        public static readonly Dictionary<string, object> Department = new Dictionary<string, object>
        {
            { "id",                  77491        },
            { "name",                "Facilities" },
            { "description",         null         },
            { "default_assignee_id", 287641       }
        };
        #endregion // Department

        #region Role
        public static readonly Dictionary<string, object> Role = new Dictionary<string, object>
        {
            { "id",                  347290                                         },
            { "name",                "Administrator"                                },
            { "description",         "This is the all powerful administrator user!" },
            { "portal",              false                                          },
            { "show_my_tasks",       false                                          }
        };
        #endregion // Role

        #region Site
        public static readonly Dictionary<string, object> Site = new Dictionary<string, object>
        {
            { "id",              68684                          },
            { "name",            "Site 1a"                      },
            { "location",        "Somewhere"                    },
            { "description",     "Site Description"             },
            { "time_zone",       "Pacific Time (USA & Canada)"  },
            { "language",        "-1"                           },
            { "business_record", new Dictionary<string, object>
                {
                    { "id",          41202                                        },
                    { "name",        "Default Business Hours"                     },
                    { "description", "This is your initial business hours record" }
                }
            }
        };
        #endregion // Site

        #region StubUser
        public static readonly Dictionary<string, object> StubUser = new Dictionary<string, object>
        {
            { "id",                                2601810                         },
            { "account_id",                        54573                           },
            { "user_id",                           2681794                         },
            { "email",                             "test.request@domain.com"       },
            { "name",                              "Test Request"                  },
            { "disabled",                          false                           },
            { "has_gravatar",                      false                           },
            { "customer_satisfaction_survey_time", "2017-10-18T17:35:25.000-07:00" },
            {
                "avatar", new Dictionary<string, object>
                {
                    { "type",     "initials" },
                    { "color",    "#0bc46f"  },
                    { "initials", "TR"       }
                }
            }
        };
        #endregion // StubUser

        #region Custom Fields
        public static readonly List<object> CustomFields = new List<object>();
        #endregion // Custom Fields

        #region ManagerUser
        public static readonly Dictionary<string, object> ManagerUser = new Dictionary<string, object>
        {
            { "group_id",             2890621                            },
            { "is_user",              true                               },
            { "id",                   2685340                            },
            { "name",                 "Test Manager"                     },
            { "disabled",             false                              },
            { "title",                "Test Manager Title"               },
            { "email",                "test.manager@domain.com"          },
            { "created_at",           "2017-10-13T09:58:40.000-07:00"    },
            { "last_login",           null                               },
            { "phone",                "+1 654 567-8765"                  },
            { "mobile_phone",         ""                                 },
            { "department",           Department                         },
            { "role",                 Role                               },
            { "salt",                 "2835969028f722e78f2553edc4476f5a" },
            {
                "group_ids", new List<int>
                {
                    2890621,
                }
            },
            { "custom_fields_values", CustomFields                       },
            {
                "avatar", new Dictionary<string, object>
                {
                    { "type",     "initials" },
                    { "color",    "#fa7911"  },
                    { "initials", "TM"       }
                }
            },
            { "mfa_enabled",          true                               },
            { "site",                 Site                               }
        };
        #endregion // ManagerUser

        #region User
        public static readonly Dictionary<string, object> User = new Dictionary<string, object>
        {
            { "id",                   2681704                            },
            { "name",                 "Test User"                        },
            { "disabled",             true                               },
            { "title",                "Test Title"                       },
            { "email",                "test.user@domain.com"             },
            { "created_at",           "2017-10-11T14:26:23.000-07:00"    },
            { "last_login",           "2017-10-18T14:16:41.000-07:00"    },
            { "phone",                "+1 234 567-8900"                  },
            { "mobile_phone",         "+1 987 654-3210"                  },
            { "department",           Department                         },
            { "role",                 Role                               },
            { "salt",                 "fe69aacd9ca9353d31e0078056bc996b" },
            {
                "group_ids", new List<int>
                {
                    2886808,
                    2886809,
                    2889538
                }
            },
            { "custom_fields_values", CustomFields                       },
            {
                "avatar", new Dictionary<string, object>
                {
                    { "type", "image"                          },
                    { "image_class", "avatar_image"            },
                    { "sso_image_class", ""                    },
                    { "avatar_url", "http://path/to/image.png" }
                }
            },
            { "mfa_enabled",          false                              },
            { "reports_to",           ManagerUser                        },
            { "site",                 Site                               }
        };
        #endregion // User

        #region Incident
        public static readonly Dictionary<string, object> Incident = new Dictionary<string, object>
        {
            { "id",                    18994033                                                      },
            { "number",                25                                                            },
            { "name",                  "Test Incident"                                               },
            { "description",           "<p>Test Incident Description</p>"                            },
            { "description_no_html",   "Test Incident Description"                                   },
            { "state",                 "Closed"                                                      },
            { "priority",              "Medium"                                                      },
            { "category",              Category                                                      },
            { "subcategory",           (Category["children"] as List<Dictionary<string, object>>)[0] },
            { "assignee",              Assignee                                                      },
            { "requester",             StubUser                                                      },
            { "created_at",            "2017-10-18T17:35:22.000-07:00"                               },
            { "updated_at",            "2017-10-22T02:20:38.000-07:00"                               },
            { "due_at",                null                                                          },
            { "sla_violations",        new List<Dictionary<string,object>>()                         },
            { "number_of_comments",    0                                                             },
            { "user_saw_all_comments", true                                                          },
            { "is_service_request",    true                                                          },
            { "created_by",            StubUser                                                      },
            { "custom",                null                                                          },
            { "href",                  "https://path/to/incident"                                    },
            { "site",                  Site                                                          },
            { "department",            Department                                                    },
            { "cc",                    new List<Dictionary<string, object>>()                        },
            { "custom_fields_values",  CustomFields                                                  },
            { "price",                 null                                                          },
            { "incidents",             new List<Dictionary<string, object>>()                        },
            { "changes",               new List<Dictionary<string, object>>()                        },
            { "tasks",                 new List<Dictionary<string, object>>()                        },
            { "time_tracks",           new List<Dictionary<string, object>>()                        },
            { "solutions",             new List<Dictionary<string, object>>()                        },
            { "assets",                new List<Dictionary<string, object>>()                        },
            { "mobiles",               new List<Dictionary<string, object>>()                        },
            { "other_assets",          new List<Dictionary<string, object>>()                        },
            { "configuration_items",   new List<Dictionary<string, object>>()                        }
        };
        #endregion // Incident
    }
}
