using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xacro;
using UrdfUnity.Parse.Xacro.Models;

namespace UrdfUnityTest.Parse.Xacro
{
    [TestClass]
    public class XacroMacroParserTest
    {
        [TestMethod]
        public void ParseXacroMacro()
        {
            string nameSpace = "xacro";
            string name = "theName";
            string param1 = "param1";
            string param2 = "param2";
            string parameters = String.Format("{0} {1}", param1, param2);
            XmlNode node = GetXmlNode(nameSpace, name, parameters);

            XacroMacroParser parser = new XacroMacroParser(nameSpace);
            XacroMacro macro = parser.Parse(node);

            Assert.AreEqual(name, macro.Name);
            Assert.IsTrue(macro.Parameters.Count == 2);
            Assert.AreEqual(param1, macro.Parameters[0]);
            Assert.AreEqual(param2, macro.Parameters[1]);
            Assert.IsNotNull(macro.Xml);
            Assert.IsFalse(macro.Xml.IsEmpty);
        }

        [TestMethod]
        public void ParseXacroMacroNoParams()
        {
            string nameSpace = "xacro";
            string name = "theName";
            string format = @"<?xml version='1.0'?>
                <robot xmlns:{0}='http://www.ros.org/wiki/xacro' name='robot' >
                    <{0}:macro name='{1}'>
                    </{0}:macro>
                </robot>";
            string xml = String.Format(format, nameSpace, name);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            XmlNode node = xmlDoc.DocumentElement.GetElementsByTagName("macro", "http://www.ros.org/wiki/xacro")[0];

            XacroMacroParser parser = new XacroMacroParser(nameSpace);
            XacroMacro macro = parser.Parse(node);

            Assert.AreEqual(name, macro.Name);
            Assert.IsTrue(macro.Parameters.Count == 0);
            Assert.IsNotNull(macro.Xml);
            Assert.IsFalse(macro.Xml.IsEmpty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseXacroMacroNullNode()
        {
            new XacroMacroParser("namespace").Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseXacroPropertyMismatchedNamespace()
        {
            XacroPropertyParser parser = new XacroPropertyParser("notXacro");
            XmlNode node = GetXmlNode("xacro", "name", "params");

            parser.Parse(node);
        }

        private XmlNode GetXmlNode(string nameSpace, string name, string parameters)
        {
            string format = @"<?xml version='1.0'?>
                <robot xmlns:{0}='http://www.ros.org/wiki/xacro' name='robot' >
                    <{0}:macro name='{1}' params='{2}'>
                    </{0}:macro>
                </robot>";
            string xml = String.Format(format, nameSpace, name, parameters);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlReader.Create(new StringReader(xml)));

            return xmlDoc.DocumentElement.GetElementsByTagName("macro", "http://www.ros.org/wiki/xacro")[0];
        }
    }
}
