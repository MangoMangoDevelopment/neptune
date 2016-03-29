using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xacro.Models;

namespace UrdfUnityTest.Parse.Xacro.Models
{
    [TestClass]
    public class XacroMacroTest
    {
        [TestMethod]
        public void ConstructXacroMacro()
        {
            string name = "name";
            List<string> parameters = new List<string>();
            XmlElement xml = GetXmlElement();

            XacroMacro macro = new XacroMacro(name, parameters, xml);

            Assert.AreEqual(name, macro.Name);
            Assert.AreEqual(parameters, macro.Parameters);
            Assert.AreEqual(xml, macro.Xml);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructXacroMacroNoName()
        {
            new XacroMacro("", new List<string>(), GetXmlElement());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructXacroMacroNullName()
        {
            new XacroMacro(null, new List<string>(), GetXmlElement());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructXacroMacroNullParameters()
        {
            new XacroMacro("name", null, GetXmlElement());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructXacroMacroNullXml()
        {
            new XacroMacro("name", new List<string>(), null);
        }

        private XmlElement GetXmlElement()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlReader.Create(new StringReader("<xml />")));
            return xmlDoc.DocumentElement;
        }
    }
}
