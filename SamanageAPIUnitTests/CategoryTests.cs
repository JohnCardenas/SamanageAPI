using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Newtonsoft.Json;
using SamanageAPI;
using SamanageAPI.JsonContractResolvers;

namespace SamanageAPIUnitTests
{
    [TestClass]
    public class CategoryTests
    {
        private string SerializedJson { get; set; }

        private Category ReferenceObject { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            SerializedJson = JsonConvert.SerializeObject(TestData.Category);
            ReferenceObject = JsonConvert.DeserializeObject<Category>(SerializedJson);
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
            cat.Id.Should().Be((int)TestData.Category[SamanageObject.JSON_ID]);
            cat.Name.Should().Be(TestData.Category[Category.JSON_NAME].ToString());
            cat.Children.Should().HaveCount((TestData.Category[Category.JSON_CHILDREN] as List<Dictionary<string, object>>).Count);
        }

        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serialization of a Category")]
        public void CategorySerializeTest()
        {
            // Arrange
            string outcomeJson;

            // Act
            outcomeJson = JsonConvert.SerializeObject(ReferenceObject, new JsonSerializerSettings { ContractResolver = new SamanageContractResolver() });
            var outcome = JsonConvert.DeserializeObject<Dictionary<string, object>>(outcomeJson);

            // Assert
            ReferenceObject.HasChanges.Should().BeFalse();
            outcomeJson.Should().NotBeNullOrEmpty();

            outcome.Keys.Should().NotContain(Category.JSON_CHILDREN);

            outcome[Category.JSON_NAME].Should().Be(ReferenceObject.Name);
        }

        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serializing a single changed Category property")]
        public void CategoryChangedSerializeTest()
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

            outcome[Category.JSON_NAME].Should().Be(ReferenceObject.Name);
        }

        [TestMethod]
        [TestCategory("Validation")]
        [Description("Tests validation for a new Category object")]
        public void CategoryHasErrorsTest()
        {
            // Arrange
            SamanageClient client = new SamanageClient();
            Category cat;

            // Act
            cat = new Category(client);

            // Assert
            cat.HasErrors.Should().BeTrue();
            cat.ErrorCount.Should().Be(1);
        }

        [TestMethod]
        [TestCategory("Validation")]
        [Description("Tests that no Category validation errors exist when required fields are filled out")]
        public void CategoryHasNoErrorsTest()
        {
            // Arrange
            SamanageClient client = new SamanageClient();
            string testName = "Testing Name 123";
            Category cat;

            // Act
            cat = new Category(client);
            cat.Name = testName;

            // Assert
            cat.HasErrors.Should().BeFalse();
        }
    }
}
