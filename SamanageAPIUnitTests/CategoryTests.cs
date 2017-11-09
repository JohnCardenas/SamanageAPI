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
    public class CategoryTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a Category")]
        public void CategoryDeserializeTest()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(TestData.Category);
            Category cat;

            // Act
            cat = JsonConvert.DeserializeObject<Category>(json);

            // Assert
            cat.Should().NotBeNull();
            cat.Id.Should().Be((int)TestData.Category["id"]);
            cat.Name.Should().Be(TestData.Category["name"].ToString());
            cat.Children.Should().HaveCount((TestData.Category["children"] as List<Dictionary<string, object>>).Count);
        }
    }
}
