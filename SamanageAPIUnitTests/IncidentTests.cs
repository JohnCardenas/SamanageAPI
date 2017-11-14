using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamanageAPI;
using FluentAssertions;
using Newtonsoft.Json;
using SamanageAPI.JsonContractResolvers;

namespace SamanageAPIUnitTests
{
    [TestClass]
    public class IncidentTests
    {
        private string SerializedJson { get; set; }
        private Incident ReferenceObject { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            SerializedJson = JsonConvert.SerializeObject(TestData.Incident);
            ReferenceObject = JsonConvert.DeserializeObject<Incident>(SerializedJson);
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of an Incident")]
        public void IncidentDeserializeTest()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(TestData.Incident);
            Incident inc;

            // Act
            inc = JsonConvert.DeserializeObject<Incident>(json);

            // Assert
            inc.Should().NotBeNull();
            inc.Assignee.Should().NotBeNull();
            inc.Category.Should().NotBeNull();
            inc.Created.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.Incident[Incident.JSON_CREATED]));
            inc.Creator.Should().NotBeNull();
            inc.Department.Should().NotBeNull();
            inc.Description.Should().Be(UnitTestHelpers.NullableString(TestData.Incident[Incident.JSON_DESCRIPTION]));
            inc.Due.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.Incident[Incident.JSON_DUE]));
            inc.Id.Should().Be((int)TestData.Incident[SamanageObject.JSON_ID]);
            inc.IsServiceRequest.Should().Be((bool)TestData.Incident[Incident.JSON_IS_SERVICE_REQUEST]);
            inc.Name.Should().Be((string)TestData.Incident[Incident.JSON_NAME]);
            inc.Number.Should().Be((int)TestData.Incident[Incident.JSON_NUMBER]);
            inc.Priority.Should().Be(Enum.Parse(typeof(Priority), TestData.Incident[Incident.JSON_PRIORITY].ToString()));
            inc.Requester.Should().NotBeNull();
            inc.Site.Should().NotBeNull();
            inc.State.Should().Be((string)TestData.Incident[Incident.JSON_STATE]);
            inc.Updated.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.Incident[Incident.JSON_UPDATED]));
        }

        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serialization of an Incident")]
        public void IncidentSerializeTest()
        {
            // Arrange
            string outcomeJson;

            // Act
            outcomeJson = JsonConvert.SerializeObject(ReferenceObject, new JsonSerializerSettings { ContractResolver = new SamanageContractResolver() });
            var outcome = JsonConvert.DeserializeObject<Dictionary<string, object>>(outcomeJson);

            // Assert
            ReferenceObject.HasChanges.Should().BeFalse();
            outcomeJson.Should().NotBeNullOrEmpty();

            outcome.Keys.Should().NotContain(Incident.JSON_ASSIGNEE);
            outcome.Keys.Should().NotContain(Incident.JSON_CATEGORY);
            outcome.Keys.Should().NotContain(Incident.JSON_CREATED);
            outcome.Keys.Should().NotContain(Incident.JSON_CREATOR);
            outcome.Keys.Should().NotContain(Incident.JSON_DEPARTMENT);
            outcome.Keys.Should().NotContain(Incident.JSON_DESCRIPTION);
            outcome.Keys.Should().NotContain(Incident.JSON_DUE);
            outcome.Keys.Should().NotContain(Incident.JSON_IS_SERVICE_REQUEST);
            outcome.Keys.Should().NotContain(Incident.JSON_NUMBER);
            outcome.Keys.Should().NotContain(Incident.JSON_PRIORITY);
            outcome.Keys.Should().NotContain(Incident.JSON_REQUESTER);
            outcome.Keys.Should().NotContain(Incident.JSON_SITE);
            outcome.Keys.Should().NotContain(Incident.JSON_STATE);
            outcome.Keys.Should().NotContain(Incident.JSON_UPDATED);

            Convert.ToInt32(outcome[Incident.JSON_ID]).Should().Be(ReferenceObject.Id);
            outcome[Incident.JSON_NAME].Should().Be(ReferenceObject.Name);
        }

        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serialization of an Incident with a single changed property")]
        public void IncidentChangedSerializeTest()
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

            outcome[Incident.JSON_NAME].Should().Be(ReferenceObject.Name);
        }

        [TestMethod]
        [TestCategory("Validation")]
        [Description("Tests validation for a new Incident object")]
        public void IncidentHasErrorsTest()
        {
            // Arrange
            SamanageClient client = new SamanageClient();
            Incident inc;

            // Act
            inc = new Incident(client);

            // Assert
            inc.HasErrors.Should().BeTrue();
            inc.ErrorCount.Should().Be(1);
        }

        [TestMethod]
        [TestCategory("Validation")]
        [Description("Tests that no Incident validation errors exist when required fields are filled out")]
        public void IncidentHasNoErrorsTest()
        {
            // Arrange
            SamanageClient client = new SamanageClient();
            string testName = "Testing Name 123";
            Incident inc;

            // Act
            inc = new Incident(client);
            inc.Name = testName;

            // Assert
            inc.HasErrors.Should().BeFalse();
        }
    }
}
