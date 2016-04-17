using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Util;

namespace UrdfUnityTest.Util
{
    [TestClass]
    public class XmlStringBuilderTest
    {
        [TestMethod]
        public void BuildXmlString()
        {
            string expected = "<hello a=\"b\">\r\n<subelement/>\r\n</hello>";
            XmlStringBuilder sb = new XmlStringBuilder("hello");

            sb.AddAttribute("a", "b").AddSubElement("<subelement/>");

            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void BuildXmlStringAttributesOnly()
        {
            string expected = "<hello a=\"b\" c=\"4\"/>";
            XmlStringBuilder sb = new XmlStringBuilder("hello");

            sb.AddAttribute("a", "b").AddAttribute("c", 4);

            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void BuildXmlStringSubElementsOnly()
        {
            string expected = "<hello>\r\n<one/>\r\n<two/>\r\n</hello>";
            XmlStringBuilder sb = new XmlStringBuilder("hello");

            sb.AddSubElement("<one/>").AddSubElement("<two/>");

            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void BuildXmlStringNoAttributesOrSubElements()
        {
            string expected = "<hello/>";
            XmlStringBuilder sb = new XmlStringBuilder("hello");

            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void ResetXmlString()
        {
            string expected = "<good bye=\"bye\"/>";
            XmlStringBuilder sb = new XmlStringBuilder("hello");

            sb.Reset("good").AddAttribute("bye", "bye");

            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void AddAttributeTwice()
        {
            string expected = "<hello a=\"2\"/>";
            XmlStringBuilder sb = new XmlStringBuilder("hello");

            sb.AddAttribute("a", 1).AddAttribute("a", 2);

            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildXmlStringNoElement()
        {
            new XmlStringBuilder("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildXmlStringNullElement()
        {
            new XmlStringBuilder(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ResetXmlStringNoElement()
        {
            new XmlStringBuilder("hello").Reset("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ResetXmlStringNullElement()
        {
            new XmlStringBuilder("hello").Reset(null);
        }
    }
}
