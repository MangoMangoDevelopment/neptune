using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Util;

namespace UrdfUnityTest.Util
{
    [TestClass]
    public class XmlParsingUtilsTest
    {
        [TestMethod]
        public void GetAttributeFromNode()
        {
            string attributeName = "attribute";
            string attributeValue = "value";
            string xml = String.Format("<hello {0}='{1}'/>", attributeName, attributeValue);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            XmlAttribute xmlAttribute = XmlParsingUtils.GetAttributeFromNode(xmlDoc.DocumentElement, attributeName);

            Assert.IsNotNull(xmlAttribute);
            Assert.AreEqual(attributeValue, xmlAttribute.Value);
            Assert.IsNull(XmlParsingUtils.GetAttributeFromNode(xmlDoc.DocumentElement, "blahblah"));
        }
    }
}
