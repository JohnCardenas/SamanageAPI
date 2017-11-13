using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamanageAPI;
using FluentAssertions;
using Newtonsoft.Json;

namespace SamanageAPIUnitTests
{
    [TestClass]
    public class IncidentTests
    {
        [TestInitialize]
        public void Initialize()
        {
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
    }
}
