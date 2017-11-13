using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Newtonsoft.Json;
using SamanageAPI;

namespace SamanageAPIUnitTests
{
    [TestClass]
    public class RoleTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a Role")]
        public void RoleDeserializeTest()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(TestData.Role);
            Role role;

            // Act
            role = JsonConvert.DeserializeObject<Role>(json);

            // Assert
            role.Should().NotBeNull();
            role.Description.Should().Be(UnitTestHelpers.NullableString(TestData.Role[Role.JSON_DESCRIPTION]));
            role.Id.Should().Be((int)TestData.Role[SamanageObject.JSON_ID]);
            role.Name.Should().Be(TestData.Role[Role.JSON_NAME].ToString());
        }
    }
}
