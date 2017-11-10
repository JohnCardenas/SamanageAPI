using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Newtonsoft.Json;
using SamanageAPI;
using SamanageAPI.JsonConverters;

namespace SamanageAPIUnitTests
{
    [TestClass]
    public class GroupTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a Group to a Principal")]
        public void PrincipalGroupDeserializeTest()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(TestData.Group);
            Principal principal;

            // Act
            JsonConverter[] converters = { new PrincipalConverter() };
            principal = JsonConvert.DeserializeObject<Principal>(json, new JsonSerializerSettings() { Converters = converters });

            // Assert
            principal.Should().NotBeNull();
            principal.Should().BeOfType<Group>();
            principal.As<Group>().Description.Should().Be((string)TestData.Group["description"]); // Test property unique to a Group
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a Group")]
        public void GroupDeserializeTest()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(TestData.Group);
            Group group;

            // Act
            group = JsonConvert.DeserializeObject<Group>(json);

            // Assert
            group.Should().NotBeNull();
            group.Description.Should().Be(UnitTestHelpers.NullableString(TestData.Group["description"]));
            group.Id.Should().Be((int)TestData.Group["id"]);
            group.Name.Should().Be(TestData.Group["name"].ToString());
        }
    }
}
