using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamanageAPI;
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
            user.Created.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.User["created_at"]));
            user.Department.Should().NotBeNull();
            user.Disabled.Should().Be((bool)TestData.User["disabled"]);
            user.Email.Should().Be((string)TestData.User["email"]);
            user.Id.Should().Be((int)TestData.User["id"]);
            user.LastLogin.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.User["last_login"]));
            user.Manager.Should().NotBeNull();
            user.MobilePhone.Should().Be((string)TestData.User["mobile_phone"]);
            user.Name.Should().Be((string)TestData.User["name"]);
            user.Phone.Should().Be((string)TestData.User["phone"]);
            user.Role.Should().NotBeNull();
            user.Site.Should().NotBeNull();
            user.Title.Should().Be((string)TestData.User["title"]);

            user.Manager.Id.Should().Be((int)TestData.ManagerUser["id"]);
            user.Manager.Name.Should().Be((string)TestData.ManagerUser["name"]);
            user.Manager.Disabled.Should().Be((bool)TestData.ManagerUser["disabled"]);
            user.Manager.Title.Should().Be((string)TestData.ManagerUser["title"]);
            user.Manager.Email.Should().Be((string)TestData.ManagerUser["email"]);
            user.Manager.Created.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.ManagerUser["created_at"]));
            user.Manager.LastLogin.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.ManagerUser["last_login"]));
            user.Manager.Phone.Should().Be((string)TestData.ManagerUser["phone"]);
            user.Manager.MobilePhone.Should().Be((string)TestData.ManagerUser["mobile_phone"]);
            user.Manager.Department.Should().NotBeNull();
            user.Manager.Role.Should().NotBeNull();
            user.Manager.Site.Should().NotBeNull();
        }
    }
}
