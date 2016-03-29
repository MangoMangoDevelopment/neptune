using System;
using System.Collections.Generic;
using System.Xml;
using NLog;
using UrdfUnity.Parse.Xacro.Models;

namespace UrdfUnity.Parse.Xacro
{
    /// <summary>
    /// Parses a Xacro &lt;xacro:macro&gt; element from XML into a XacroMacro object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/xacro"/>
    /// <seealso cref="XacroMacro"/>
    public class XacroMacroParser : AbstractUrdfXmlParser<XacroMacro>
    {
        /// <summary>
        /// Format string for element name with placeholder for namespace, such as "xacro:macro".
        /// </summary>
        private static readonly string ELEMENT_NAME_FORMAT = "{0}:macro";

        private static readonly string NAME_ATTRIBUTE_NAME = "name";
        private static readonly string PARAMETERS_ATTRIBUTE_NAME = "params";
        private static readonly char PARAMETER_DELIMITER = ' ';


        protected override string ElementName { get; }
        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Creates a new instance of XacroMacroParser for the specified Xacro namespace.
        /// </summary>
        /// <param name="xacroNamespace">The namespace of Xacro elements in the XML</param>
        public XacroMacroParser(string xacroNamespace)
        {
            ElementName = String.Format(ELEMENT_NAME_FORMAT, xacroNamespace);
        }

        /// <summary>
        /// Parses a Xacro &lt;xacro:macro&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;xacro:macro&gt; element. MUST NOT BE NULL</param>
        /// <returns>The parsed <c>XacroMacro</c> object, otherwise <c>null</c> if the XML was malformed</returns>
        public override XacroMacro Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute nameAttribute = GetAttributeFromNode(node, NAME_ATTRIBUTE_NAME);
            XmlAttribute parametersAttribute = GetAttributeFromNode(node, PARAMETERS_ATTRIBUTE_NAME);

            XacroMacro macro = null;
            string name = String.Empty;
            List<string> parameters = new List<string>();

            if (nameAttribute == null)
            {
                LogMissingRequiredAttribute(NAME_ATTRIBUTE_NAME);
            }
            else
            {
                name = nameAttribute.Value;
            }

            if (parametersAttribute == null)
            {
                LogMissingOptionalAttribute(PARAMETERS_ATTRIBUTE_NAME);
            }
            else
            {
                parameters = new List<string>(parametersAttribute.Value.Split(PARAMETER_DELIMITER));
            }

            if (!String.IsNullOrEmpty(name))
            {
                macro = new XacroMacro(name, parameters, (XmlElement)node);
            }

            return macro;
        }
    }
}
