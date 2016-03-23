﻿using System;
using System.Xml;

namespace UrdfUnity.Util
{
    /// <summary>
    /// Utility class with helper methods for parsing XML.
    /// </summary>
    public static class XmlParsingUtils
    {
        /// <summary>
        /// Validates the XmlNode parameter passed to XmlParser implementations.
        /// </summary>
        /// <param name="node">The XmlNode being validated</param>
        /// <param name="nodeName">The expected element name of the XmlNode</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided XmlNode is null</exception>
        /// <exception cref="ArgumentException">Thrown when the provided XmlNode doesn't match the expected element</exception>
        //public static void ValidateXmlNode(XmlNode node, string nodeName)
        //{
        //    Preconditions.IsNotNull(node, "The provided XmlNode for parsing <{nodeName}> must not be null");
        //    Preconditions.IsTrue(node.Name.Equals(nodeName), "The provided XmlNode for parsing <{nodeName}> is for mismatched <{node.Name}> node");
        //}

        ///// <summary>
        ///// Gets the specified XML attribute from the provided XmlNode and returns it as an XmlAttribute.
        ///// </summary>
        ///// <param name="node">The XmlNode that is expected to have the specified attribute</param>
        ///// <param name="attributeName">The name of the XML attribute</param>
        ///// <returns>The XmlAttribute object if the attribute exists, otherwise <c>null</c></returns>
        //public static XmlAttribute GetAttributeFromNode(XmlNode node, string attributeName)
        //{
        //    Preconditions.IsNotNull(node, "node");
        //    return (node.Attributes != null) ? (XmlAttribute)node.Attributes.GetNamedItem(attributeName) : null;
        //}

        ///// <summary>
        ///// Gets the specified XML element from the provided XmlNode and returns it as an XmlElement.
        ///// </summary>
        ///// <param name="node">The XmlNode that is expected to have the specified attribute</param>
        ///// <param name="elementName">The name of the XML element</param>
        ///// <returns>The XmlElement object if the element exists, otherwise <c>null</c></returns>
        //public static XmlElement GetElementFromNode(XmlNode node, string elementName)
        //{
        //    return (XmlElement)node.SelectSingleNode(elementName);
        //}
    }
}
