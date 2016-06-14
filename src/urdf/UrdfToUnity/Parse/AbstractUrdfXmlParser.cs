using System;
using System.Xml;
//using NLog;
using UrdfToUnity.Util;

namespace UrdfToUnity.Parse
{
    /// <summary>
    /// Abstract implementation of the Parser interface with helper methods for derived classes that parse URDF XML elements.
    /// </summary>
    /// <typeparam name="T">The model object type being parsed and returned</typeparam>
    /// <seealso cref="Parser{T, E}"/>
    public abstract class AbstractUrdfXmlParser<T> : XmlParser<T>
    {
        /// <summary>
        /// The name of the URDF XML element the derived implementation of XmlParser parses.
        /// </summary>
        protected abstract string ElementName { get; }

        /// <summary>
        /// The Logger instance for the derived class.
        /// </summary>
        //protected abstract Logger Logger { get; }


        /// <summary>
        /// Parses a URDF element from XML.
        /// </summary>
        /// <param name="node">The XML node of the URDF element being parsed</param>
        /// <returns>The model object parsed from the XML</returns>
        public abstract T Parse(XmlNode node);

        /// <summary>
        /// Helper method to validate the XmlNode parameter passed to XmlParser implementations.
        /// </summary>
        /// <param name="node">The XmlNode being validated. MUST NOT BE NULL</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided XmlNode is null</exception>
        /// <exception cref="ArgumentException">Thrown when the provided XmlNode doesn't match the expected element</exception>
        protected void ValidateXmlNode(XmlNode node)
        {
            Preconditions.IsNotNull(node, $"The provided XmlNode for parsing <{ElementName}> must not be null");
            Preconditions.IsTrue(node.Name.Equals(ElementName), $"The provided XmlNode for parsing <{ElementName}> is for mismatched <{node.Name}> node");
        }

        /// <summary>
        /// Helper method to get the specified XML attribute from the provided XmlNode and return it as an XmlAttribute.
        /// </summary>
        /// <param name="node">The XmlNode that is expected to have the specified attribute</param>
        /// <param name="attributeName">The name of the XML attribute</param>
        /// <returns>The XmlAttribute object if the attribute exists, otherwise <c>null</c></returns>
        protected XmlAttribute GetAttributeFromNode(XmlNode node, string attributeName)
        {
            ValidateXmlNode(node);
            return (node.Attributes != null) ? (XmlAttribute)node.Attributes.GetNamedItem(attributeName) : null;
        }

        /// <summary>
        /// Helper method to get the specified XML element from the provided XmlNode and return it as an XmlElement.
        /// </summary>
        /// <param name="node">The XmlNode that is expected to have the specified attribute</param>
        /// <param name="elementName">The name of the XML element</param>
        /// <returns>The XmlElement object if the element exists, otherwise <c>null</c></returns>
        protected XmlElement GetElementFromNode(XmlNode node, string elementName)
        {
            ValidateXmlNode(node);
            return (XmlElement)node.SelectSingleNode(elementName);
        }

        /// <summary>
        /// Logs an error message for the case where a required sub-element is missing for the XML element being parsed.
        /// </summary>
        /// <param name="element">The name of the sub-element that's missing</param>
        protected void LogMissingRequiredElement(string element)
        {
            //Logger.Warn("Parsing <{0}> element failed due to missing required \"{1}\" sub-element", ElementName, element);
        }

        /// <summary>
        /// Logs an debug message for the case where an optional sub-element isn't provided by the XML element being parsed.
        /// </summary>
        /// <param name="element">The name of the attribute that's missing</param>
        protected void LogMissingOptionalElement(string element)
        {
            //Logger.Debug("Parsing <{0}> element without optional \"{1}\" sub-element", ElementName, element);
        }

        /// <summary>
        /// Logs an error message for the case where a required attribute is missing for the XML element being parsed.
        /// </summary>
        /// <param name="attribute">The name of the attribute that's missing</param>
        protected void LogMissingRequiredAttribute(string attribute)
        {
            //Logger.Warn("Parsing <{0}> element failed due to missing required \"{1}\" attribute", ElementName, attribute);
        }

        /// <summary>
        /// Logs an debug message for the case where an optional attribute isn't provided by the XML element being parsed.
        /// </summary>
        /// <param name="attribute">The name of the attribute that's missing</param>
        protected void LogMissingOptionalAttribute(string attribute)
        {
            //Logger.Debug("Parsing <{0}> element without optional \"{1}\" attribute", ElementName, attribute);
        }

        /// <summary>
        /// Logs an error message for the case where a required attribute is malformed and can't be parsed.
        /// </summary>
        /// <param name="attribute">The name of the attribute that can be parsed</param>
        protected void LogMalformedAttribute(string attribute)
        {
            //Logger.Warn("Parsing <{0}> element failed due to malformed \"{1}\" attribute", ElementName, attribute);
        }
    }
}
