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
            //inc.Assignee.Should().NotBeNull();
            inc.Category.Should().NotBeNull();
            inc.Created.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.Incident["created_at"]));
            //inc.Creator.Should().NotBeNull();
            inc.Department.Should().NotBeNull();
            inc.Description.Should().Be(UnitTestHelpers.NullableString(TestData.Incident["description"]));
            inc.Due.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.Incident["due_at"]));
            inc.Id.Should().Be((int)TestData.Incident["id"]);
            inc.Name.Should().Be((string)TestData.Incident["name"]);
            inc.Number.Should().Be((int)TestData.Incident["number"]);
            inc.Priority.Should().Be(Enum.Parse(typeof(Priority), TestData.Incident["priority"].ToString()));
            //inc.Requester.Should().NotBeNull();
            inc.Site.Should().NotBeNull();
            inc.State.Should().Be((string)TestData.Incident["state"]);
            inc.Updated.Should().Be(UnitTestHelpers.NullableDateTimeConvert(TestData.Incident["updated_at"]));
        }
    }
}
