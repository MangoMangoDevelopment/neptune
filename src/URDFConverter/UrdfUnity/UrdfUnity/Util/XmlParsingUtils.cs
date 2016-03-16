using System.Xml;

namespace UrdfUnity.Util
{
    /// <summary>
    /// Utility class with helper methods for parsing XML.
    /// </summary>
    public static class XmlParsingUtils
    {
        /// <summary>
        /// Gets the specified XML attribute from the provided XmlNode and returns it as an XmlAttribute.
        /// </summary>
        /// <param name="node">The XmlNode that is expected to have the specified attribute</param>
        /// <param name="attributeName">The name of the XML attribute</param>
        /// <returns>The XmlAttribute object if the attribute exists, otherwise <c>null</c></returns>
        public static XmlAttribute GetAttributeFromNode(XmlNode node, string attributeName)
        {
            Preconditions.IsNotNull(node, "node");
            return (node.Attributes != null) ? (XmlAttribute)node.Attributes.GetNamedItem(attributeName) : null;
        }

        /// <summary>
        /// Gets the specified XML element from the provided XmlNode and returns it as an XmlElement.
        /// </summary>
        /// <param name="node">The XmlNode that is expected to have the specified attribute</param>
        /// <param name="elementName">The name of the XML element</param>
        /// <returns>The XmlElement object if the element exists, otherwise <c>null</c></returns>
        public static XmlElement GetElementFromNode(XmlNode node, string elementName)
        {
            return (XmlElement)node.SelectSingleNode(elementName);
        }
    }
}
