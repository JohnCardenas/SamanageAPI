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
    public class DepartmentTests
    {
        private string SerializedJson { get; set; }
        private Department ReferenceObject { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            SerializedJson = JsonConvert.SerializeObject(TestData.Department);
            ReferenceObject = JsonConvert.DeserializeObject<Department>(SerializedJson);
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a Department")]
        public void DepartmentDeserializeTest()
        {
            // Arrange
            Department dept;

            // Act
            dept = JsonConvert.DeserializeObject<Department>(SerializedJson);

            // Assert
            dept.Should().NotBeNull();
            dept.Description.Should().Be(UnitTestHelpers.NullableString(TestData.Department[Department.JSON_DESCRIPTION]));
            dept.Id.Should().Be((int)TestData.Department[SamanageObject.JSON_ID]);
            dept.Name.Should().Be(TestData.Department[Department.JSON_NAME].ToString());
        }

        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serialization of a Department")]
        public void DepartmentSerializeTest()
        {
            // Arrange
            string outcomeJson;

            // Act
            outcomeJson = JsonConvert.SerializeObject(ReferenceObject, new JsonSerializerSettings { ContractResolver = new SamanageContractResolver() });
            var outcome = JsonConvert.DeserializeObject<Dictionary<string, object>>(outcomeJson);

            // Assert
            ReferenceObject.HasChanges.Should().BeFalse();
            outcomeJson.Should().NotBeNullOrEmpty();

            outcome.Keys.Should().NotContain(Department.JSON_DESCRIPTION);

            Convert.ToInt32(outcome[Department.JSON_ID]).Should().Be(ReferenceObject.Id);
            outcome[Department.JSON_NAME].Should().Be(ReferenceObject.Name);
        }

        [TestMethod]
        [TestCategory("Serialization")]
        [Description("Tests serializing a single changed Department property")]
        public void DepartmentChangedSerializeTest()
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

            outcome[Department.JSON_NAME].Should().Be(ReferenceObject.Name);
        }

        [TestMethod]
        [TestCategory("Validation")]
        [Description("Tests validation for a new Department object")]
        public void DepartmentHasErrorsTest()
        {
            // Arrange
            SamanageClient client = new SamanageClient();
            Department dept;

            // Act
            dept = new Department(client);

            // Assert
            dept.HasErrors.Should().BeTrue();
            dept.ErrorCount.Should().Be(1);
        }

        [TestMethod]
        [TestCategory("Validation")]
        [Description("Tests that no Department validation errors exist when required fields are filled out")]
        public void DepartmentHasNoErrorsTest()
        {
            // Arrange
            SamanageClient client = new SamanageClient();
            string testName = "Testing Name 123";
            Department dept;

            // Act
            dept = new Department(client);
            dept.Name = testName;

            // Assert
            dept.HasErrors.Should().BeFalse();
        }
    }
}
