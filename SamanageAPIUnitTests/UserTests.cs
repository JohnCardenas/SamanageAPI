using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamanageAPI;
using SamanageAPI.JsonConverters;
using SamanageAPI.JsonContractResolvers;
using FluentAssertions;
using Newtonsoft.Json;

namespace SamanageAPIUnitTests
{
    [TestClass]
    public class UserTests
    {
        private string SerializedJson { get; set; }

        private User ReferenceObject { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            SerializedJson = JsonConvert.SerializeObject(TestData.User);
            ReferenceObject = JsonConvert.DeserializeObject<User>(SerializedJson);
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a User as a Principal")]
        public void PrincipalUserDeserializeTest()
        {
            // Arrange
            Principal principal;

            // Act
            JsonConverter[] converters = { new PrincipalConverter() };
            principal = JsonConvert.DeserializeObject<Principal>(SerializedJson, new JsonSerializerSettings() { Converters = converters });

            // Assert
            principal.Should().NotBeNull();
            principal.Should().BeOfType<User>();
            principal.As<User>().Phone.Should().Be((string)TestData.User[User.JSON_PHONE]); // Test for data that is unique to a User
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a User")]
        public void UserDeserializeTest()
        {
            // Arrange
            User user;

            // Act
            user = JsonConvert.DeserializeObject<User>(SerializedJson);

            // Assert
            user.Should().NotBeNull();
            user.Created.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.User[User.JSON_CREATED]));
            user.Department.Should().NotBeNull();
            user.Disabled.Should().Be((bool)TestData.User[Principal.JSON_DISABLED]);
            user.Email.Should().Be((string)TestData.User[Principal.JSON_EMAIL]);
            user.Id.Should().Be((int)TestData.User[SamanageObject.JSON_ID]);
            user.LastLogin.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.User[User.JSON_LAST_LOGIN]));
            user.Manager.Should().NotBeNull();
            user.MobilePhone.Should().Be((string)TestData.User[User.JSON_MOBILE_PHONE]);
            user.Name.Should().Be((string)TestData.User[Principal.JSON_NAME]);
            user.Phone.Should().Be((string)TestData.User[User.JSON_PHONE]);
            user.Role.Should().NotBeNull();
            user.Site.Should().NotBeNull();
            user.Title.Should().Be((string)TestData.User[User.JSON_TITLE]);

            user.Manager.Created.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.ManagerUser[User.JSON_CREATED]));
            user.Manager.Department.Should().NotBeNull();
            user.Manager.Disabled.Should().Be((bool)TestData.ManagerUser[Principal.JSON_DISABLED]);
            user.Manager.Email.Should().Be((string)TestData.ManagerUser[Principal.JSON_EMAIL]);
            user.Manager.Id.Should().Be((int)TestData.ManagerUser[SamanageObject.JSON_ID]);
            user.Manager.LastLogin.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.ManagerUser[User.JSON_LAST_LOGIN]));
            user.Manager.MobilePhone.Should().Be((string)TestData.ManagerUser[User.JSON_MOBILE_PHONE]);
            user.Manager.Name.Should().Be((string)TestData.ManagerUser[Principal.JSON_NAME]);
            user.Manager.Phone.Should().Be((string)TestData.ManagerUser[User.JSON_PHONE]);
            user.Manager.Title.Should().Be((string)TestData.ManagerUser[User.JSON_TITLE]);
            user.Manager.Role.Should().NotBeNull();
            user.Manager.Site.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serializing a User to JSON without modifying any properties")]
        public void UserSerializeTest()
        {
            // Arrange
            string outcomeJson;

            // Act
            outcomeJson = JsonConvert.SerializeObject(ReferenceObject, new JsonSerializerSettings { ContractResolver = new SamanageContractResolver() });

            // Assert
            ReferenceObject.HasChanges.Should().BeFalse();
            outcomeJson.Should().NotBeNullOrEmpty();

            var outcome = JsonConvert.DeserializeObject<Dictionary<string, object>>(outcomeJson);

            outcome.Keys.Should().NotContain(User.JSON_CREATED);
            outcome.Keys.Should().NotContain(User.JSON_DEPARTMENT);
            outcome.Keys.Should().NotContain(User.JSON_LAST_LOGIN);
            outcome.Keys.Should().NotContain(User.JSON_MANAGER);
            outcome.Keys.Should().NotContain(User.JSON_MOBILE_PHONE);
            outcome.Keys.Should().NotContain(User.JSON_NAME);
            outcome.Keys.Should().NotContain(User.JSON_PHONE);
            outcome.Keys.Should().NotContain(User.JSON_ROLE);
            outcome.Keys.Should().NotContain(User.JSON_SITE);
            outcome.Keys.Should().NotContain(User.JSON_TITLE);

            outcome[User.JSON_EMAIL].Should().Be(ReferenceObject.Email);
            Convert.ToInt32(outcome[SamanageObject.JSON_ID]).Should().Be(ReferenceObject.Id);
        }

        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serializing a single changed User property")]
        public void UserChangeSerializationTest()
        {
            // Arrange
            string outcomeJson;
            string testingString = "ASDF 1 2 3";

            // Act
            ReferenceObject.Title = testingString;
            outcomeJson = JsonConvert.SerializeObject(ReferenceObject, new JsonSerializerSettings { ContractResolver = new SamanageContractResolver() });

            // Assert
            ReferenceObject.HasChanges.Should().BeTrue();
            outcomeJson.Should().NotBeNullOrEmpty();
            var outcome = JsonConvert.DeserializeObject<Dictionary<string, object>>(outcomeJson);
            outcome[User.JSON_TITLE].Should().Be(testingString);
        }
    }
}
