using System;
using System.Xml;
using NLog;
using UrdfUnity.Parse.Xacro.Models;

namespace UrdfUnity.Parse.Xacro
{
    /// <summary>
    /// Parses a Xacro &lt;xacro:property&gt; element from XML into a XacroProperty object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/xacro"/>
    /// <seealso cref="XacroProperty"/>
    public class XacroPropertyParser : AbstractUrdfXmlParser<XacroProperty>
    {
        /// <summary>
        /// Format string for element name with placeholder for namespace, such as "xacro:property".
        /// </summary>
        private static readonly string ELEMENT_NAME_FORMAT = "{0}:property";

        private static readonly string NAME_ATTRIBUTE_NAME = "name";
        private static readonly string VALUE_ATTRIBUTE_NAME = "value";
        private static readonly string DEFAULT_VALUE = String.Empty;


        protected override string ElementName { get; }
        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Creates a new instance of XacroPropertyParser for the specified Xacro namespace.
        /// </summary>
        /// <param name="xacroNamespace">The namespace of Xacro elements in the XML</param>
        public XacroPropertyParser(string xacroNamespace)
        {
            ElementName = String.Format(ELEMENT_NAME_FORMAT, xacroNamespace);
        }
        
        /// <summary>
        /// Parses a Xacro &lt;xacro:property&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;xacro:property&gt; element. MUST NOT BE NULL</param>
        /// <returns>The parsed <c>XacroProperty</c> object, otherwise <c>null</c> if the XML was malformed</returns>
        public override XacroProperty Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute nameAttribute = GetAttributeFromNode(node, NAME_ATTRIBUTE_NAME);
            XmlAttribute valueAttribute = GetAttributeFromNode(node, VALUE_ATTRIBUTE_NAME);

            XacroProperty property = null;
            string name = DEFAULT_VALUE;
            string value = DEFAULT_VALUE;

            if (nameAttribute == null)
            {
                LogMissingRequiredAttribute(NAME_ATTRIBUTE_NAME);
            }
            else
            {
                name = nameAttribute.Value;
            }

            if (valueAttribute == null)
            {
                LogMissingRequiredAttribute(VALUE_ATTRIBUTE_NAME);
            }
            else
            {
                value = valueAttribute.Value;
            }

            if (!name.Equals(DEFAULT_VALUE) && !value.Equals(DEFAULT_VALUE))
            {
                property = new XacroProperty(name, value);
            }

            return property;
        }
    }
}
