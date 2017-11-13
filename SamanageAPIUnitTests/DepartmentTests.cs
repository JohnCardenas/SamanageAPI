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
    public class DepartmentTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestCategory("Deserialization")]
        [Description("Tests deserialization of a Department")]
        public void DepartmentDeserializeTest()
        {
            // Arrange
            string json = JsonConvert.SerializeObject(TestData.Department);
            Department dept;

            // Act
            dept = JsonConvert.DeserializeObject<Department>(json);

            // Assert
            dept.Should().NotBeNull();
            dept.Description.Should().Be(UnitTestHelpers.NullableString(TestData.Department[Department.JSON_DESCRIPTION]));
            dept.Id.Should().Be((int)TestData.Department[SamanageObject.JSON_ID]);
            dept.Name.Should().Be(TestData.Department[Department.JSON_NAME].ToString());
        }
    }
}
