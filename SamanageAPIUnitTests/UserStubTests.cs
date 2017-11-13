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
    public class UserStubTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a UserStub object")]
        public void UserStubDeserializeTest()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(TestData.UserStub);
            UserStub stub;

            // Act
            stub = JsonConvert.DeserializeObject<UserStub>(json);

            // Assert
            stub.Should().NotBeNull();
            stub.Disabled.Should().Be((bool)TestData.UserStub["disabled"]);
            stub.Email.Should().Be((string)TestData.UserStub["email"]);
            stub.Id.Should().Be((int)TestData.UserStub["user_id"]);
            stub.Name.Should().Be((string)TestData.UserStub["name"]);
        }
    }
}
