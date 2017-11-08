using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamanageAPI;
using FluentAssertions;

namespace SamanageAPITests.UnitTests
{
    [TestClass]
    public class IncidentTests
    {
        [TestMethod]
        [TestCategory("Unit Tests")]
        [Description("Basic test of the Incident data model")]
        public void IncidentDataModelTest()
        {
            // Arrange
            const string testName = "Test Incident Name 12345";
            const string testRequester = "test.user@domain.com";

            SamanageClient c = new SamanageClient();
            Incident i = new Incident(c);

            // Act
            i.Name = testName;
            i.Requester = testRequester;

            // Assert
            i.Name.Should().Be(testName);
            i.Requester.Should().Be(testRequester);
        }
    }
}
