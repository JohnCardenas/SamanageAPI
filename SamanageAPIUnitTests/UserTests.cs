using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamanageAPI;
using SamanageAPI.JsonConverters;
using FluentAssertions;
using Newtonsoft.Json;

namespace SamanageAPIUnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a User as a Principal")]
        public void PrincipalUserDeserializeTest()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(TestData.User);
            Principal principal;

            // Act
            JsonConverter[] converters = { new PrincipalConverter() };
            principal = JsonConvert.DeserializeObject<Principal>(json, new JsonSerializerSettings() { Converters = converters });

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
            string json = JsonConvert.SerializeObject(TestData.User);
            User user;

            // Act
            user = JsonConvert.DeserializeObject<User>(json);

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

            user.Manager.Id.Should().Be((int)TestData.ManagerUser[SamanageObject.JSON_ID]);
            user.Manager.Name.Should().Be((string)TestData.ManagerUser[Principal.JSON_NAME]);
            user.Manager.Disabled.Should().Be((bool)TestData.ManagerUser[Principal.JSON_DISABLED]);
            user.Manager.Title.Should().Be((string)TestData.ManagerUser[User.JSON_TITLE]);
            user.Manager.Email.Should().Be((string)TestData.ManagerUser[Principal.JSON_EMAIL]);
            user.Manager.Created.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.ManagerUser[User.JSON_CREATED]));
            user.Manager.LastLogin.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.ManagerUser[User.JSON_LAST_LOGIN]));
            user.Manager.Phone.Should().Be((string)TestData.ManagerUser[User.JSON_PHONE]);
            user.Manager.MobilePhone.Should().Be((string)TestData.ManagerUser[User.JSON_MOBILE_PHONE]);
            user.Manager.Department.Should().NotBeNull();
            user.Manager.Role.Should().NotBeNull();
            user.Manager.Site.Should().NotBeNull();
        }
    }
}
