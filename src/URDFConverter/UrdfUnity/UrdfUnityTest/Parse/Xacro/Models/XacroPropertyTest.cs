using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xacro.Models;

namespace UrdfUnityTest.Parse.Xacro.Models
{
    [TestClass]
    public class XacroPropertyTest
    {
        [TestMethod]
        public void ConstructXacroProperty()
        {
            string name = "name";
            string value = "value";
            XacroProperty property = new XacroProperty(name, value);

            Assert.AreEqual(name, property.Name);
            Assert.AreEqual(value, property.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructXacroPropertyNullName()
        {
            new XacroProperty(null, "value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructXacroPropertyNullValue()
        {
            new XacroProperty("name", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructXacroPropertyNoName()
        {
            new XacroProperty("", "value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructXacroPropertyNoValue()
        {
            new XacroProperty("name", "");
        }
    }
}
