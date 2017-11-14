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
using SamanageAPI.JsonContractResolvers;

namespace SamanageAPIUnitTests
{
    [TestClass]
    public class GroupTests
    {
        private string SerializedJson { get; set; }
        private Group ReferenceObject { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            SerializedJson = JsonConvert.SerializeObject(TestData.Group);
            ReferenceObject = JsonConvert.DeserializeObject<Group>(SerializedJson);
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a Group to a Principal")]
        public void PrincipalGroupDeserializeTest()
        {
            // Arrange
            Principal principal;

            // Act
            JsonConverter[] converters = { new PrincipalConverter() };
            principal = JsonConvert.DeserializeObject<Principal>(SerializedJson, new JsonSerializerSettings() { Converters = converters });

            // Assert
            principal.Should().NotBeNull();
            principal.Should().BeOfType<Group>();
            principal.As<Group>().Description.Should().Be((string)TestData.Group[Group.JSON_DESCRIPTION]); // Test property unique to a Group
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a Group")]
        public void GroupDeserializeTest()
        {
            // Arrange
            Group group;

            // Act
            group = JsonConvert.DeserializeObject<Group>(SerializedJson);

            // Assert
            group.Should().NotBeNull();
            group.Description.Should().Be(UnitTestHelpers.NullableString(TestData.Group[Group.JSON_DESCRIPTION]));
            group.Id.Should().Be((int)TestData.Group[SamanageObject.JSON_ID]);
            group.Name.Should().Be(TestData.Group[Principal.JSON_NAME].ToString());
        }
        
        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serialization of a Group")]
        public void GroupSerializeTest()
        {
            // Arrange
            string outcomeJson;

            // Act
            outcomeJson = JsonConvert.SerializeObject(ReferenceObject, new JsonSerializerSettings { ContractResolver = new SamanageContractResolver() });
            var outcome = JsonConvert.DeserializeObject<Dictionary<string, object>>(outcomeJson);

            // Assert
            ReferenceObject.HasChanges.Should().BeFalse();
            outcomeJson.Should().NotBeNullOrEmpty();

            outcome.Keys.Should().NotContain(Group.JSON_DESCRIPTION);
            outcome.Keys.Should().NotContain(Group.JSON_DISABLED);
            outcome.Keys.Should().NotContain(Group.JSON_EMAIL);

            outcome[Group.JSON_NAME].Should().Be(ReferenceObject.Name);
            Convert.ToInt32(outcome[Group.JSON_ID]).Should().Be(ReferenceObject.Id);
        }

        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serialization of a Group with a single changed property")]
        public void GroupChangedSerializeTest()
        {
            // Arrange
            string outcomeJson;
            string testingName = "New Name 123";

            // Act
            ReferenceObject.Name = testingName;
            outcomeJson = JsonConvert.SerializeObject(ReferenceObject, new JsonSerializerSettings { ContractResolver = new SamanageContractResolver() });
            var outcome = JsonConvert.DeserializeObject<Dictionary<string, object>>(outcomeJson);

            // Assert
            ReferenceObject.HasChanges.Should().BeTrue();
            outcomeJson.Should().NotBeNullOrEmpty();

            outcome[Group.JSON_NAME].Should().Be(ReferenceObject.Name);
        }

        [TestMethod]
        [TestCategory("Validation")]
        [Description("Tests validation for a new Group object")]
        public void GroupHasErrorsTest()
        {
            // Arrange
            SamanageClient client = new SamanageClient();
            Group group;

            // Act
            group = new Group(client);

            // Assert
            group.HasErrors.Should().BeTrue();
            group.ErrorCount.Should().Be(1);
        }

        [TestMethod]
        [TestCategory("Validation")]
        [Description("Tests that no Group validation errors exist when required fields are filled out")]
        public void GroupHasNoErrorsTest()
        {
            // Arrange
            SamanageClient client = new SamanageClient();
            string testName = "Testing Name 123";
            Group group;

            // Act
            group = new Group(client);
            group.Name = testName;

            // Assert
            group.HasErrors.Should().BeFalse();
        }
    }
}
