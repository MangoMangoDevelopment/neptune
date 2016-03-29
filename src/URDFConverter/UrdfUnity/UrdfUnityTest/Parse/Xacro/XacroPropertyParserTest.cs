using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xacro;
using UrdfUnity.Parse.Xacro.Models;

namespace UrdfUnityTest.Parse.Xacro
{
    [TestClass]
    public class XacroPropertyParserTest
    {
        [TestMethod]
        public void ParseXacroProperty()
        {
            string nameSpace = "xacro";
            string name = "theName";
            string value = "theValue";
            
            XacroPropertyParser parser = new XacroPropertyParser(nameSpace);
            XmlNode node = GetXmlNode(nameSpace, name, value);

            XacroProperty property = parser.Parse(node);

            Assert.AreEqual(name, property.Name);
            Assert.AreEqual(value, property.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseXacroPropertyNullNode()
        {
            new XacroPropertyParser("namespace").Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseXacroPropertyMismatchedNamespace()
        {
            XacroPropertyParser parser = new XacroPropertyParser("notXacro");
            XmlNode node = GetXmlNode("xacro", "name", "value");

            parser.Parse(node);
        }

        [TestMethod]
        public void ParseXacroPropertyMalformed()
        {
            string xml = @"<?xml version='1.0'?>
                <robot xmlns:xacro='http://www.ros.org/wiki/xacro' name='robot' >
                    <xacro:property value='noName' />
                </robot>";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            XmlNode node = xmlDoc.DocumentElement.GetElementsByTagName("property", "http://www.ros.org/wiki/xacro")[0];

            XacroPropertyParser parser = new XacroPropertyParser("xacro");
            XacroProperty property = parser.Parse(node);

            Assert.IsNull(property);
        }

        private XmlNode GetXmlNode(string nameSpace, string name, string value)
        {
            string format = @"<?xml version='1.0'?>
                <robot xmlns:{0}='http://www.ros.org/wiki/xacro' name='robot' >
                    <{0}:property name='{1}' value='{2}' />
                </robot>";
            string xml = String.Format(format, nameSpace, name, value);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlReader.Create(new StringReader(xml)));

            return xmlDoc.DocumentElement.GetElementsByTagName("property", "http://www.ros.org/wiki/xacro")[0];
        }
    }
}
