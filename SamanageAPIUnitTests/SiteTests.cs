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
    public class SiteTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a Site")]
        public void SiteDeserializeTest()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(TestData.Site);
            Site site;

            // Act
            site = JsonConvert.DeserializeObject<Site>(json);

            // Assert
            site.Should().NotBeNull();
            site.Description.Should().Be(UnitTestHelpers.NullableString(TestData.Site["description"]));
            site.Id.Should().Be((int)TestData.Site["id"]);
            site.Name.Should().Be(TestData.Site["name"].ToString());
        }
    }
}
