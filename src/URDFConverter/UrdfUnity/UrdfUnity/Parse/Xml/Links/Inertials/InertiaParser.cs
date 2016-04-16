using System;
using System.Collections.Generic;
using System.Xml;
using NLog;
using UrdfUnity.Urdf.Models.Links.Inertials;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.Links.Inertials
{
    /// <summary>
    /// Parses a URDF &lt;inertia&gt; element from XML into a Inertia object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/inertial"/>
    /// <seealso cref="Urdf.Models.Links.Inertials.Inertia"/>
    public sealed class InertiaParser : AbstractUrdfXmlParser<Inertia>
    {
        /// <summary>
        /// The default value used if the inertia element is missing a required attribute.
        /// </summary>
        public static readonly double DEFAULT_VALUE = Double.NaN;

        private static readonly string IXX_ATTRIBUTE_NAME = "ixx";
        private static readonly string IXY_ATTRIBUTE_NAME = "ixy";
        private static readonly string IXZ_ATTRIBUTE_NAME = "ixz";
        private static readonly string IYY_ATTRIBUTE_NAME = "iyy";
        private static readonly string IYZ_ATTRIBUTE_NAME = "iyz";
        private static readonly string IZZ_ATTRIBUTE_NAME = "izz";


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "inertia";


        /// <summary>
        /// Parses a URDF &lt;inertia&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;inertia&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Inertia object with values parsed from the XML, or the default value of <c>Double.NaN</c> for missing attributes</returns>
        public override Inertia Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            Dictionary<string, XmlAttribute> attributes = new Dictionary<string, XmlAttribute>();
            Dictionary<string, double> values = new Dictionary<string, double>();

            attributes.Add(IXX_ATTRIBUTE_NAME, GetAttributeFromNode(node, IXX_ATTRIBUTE_NAME));
            attributes.Add(IXY_ATTRIBUTE_NAME, GetAttributeFromNode(node, IXY_ATTRIBUTE_NAME));
            attributes.Add(IXZ_ATTRIBUTE_NAME, GetAttributeFromNode(node, IXZ_ATTRIBUTE_NAME));
            attributes.Add(IYY_ATTRIBUTE_NAME, GetAttributeFromNode(node, IYY_ATTRIBUTE_NAME));
            attributes.Add(IYZ_ATTRIBUTE_NAME, GetAttributeFromNode(node, IYZ_ATTRIBUTE_NAME));
            attributes.Add(IZZ_ATTRIBUTE_NAME, GetAttributeFromNode(node, IZZ_ATTRIBUTE_NAME));

            foreach (string key in attributes.Keys)
            {
                if (attributes[key] == null)
                {
                    LogMissingRequiredAttribute(key);
                    values.Add(key, DEFAULT_VALUE);
                }
                else
                {
                    values.Add(key, RegexUtils.MatchDouble(attributes[key].Value, DEFAULT_VALUE));
                }
            }

            return new Inertia(values[IXX_ATTRIBUTE_NAME], values[IXY_ATTRIBUTE_NAME], values[IXZ_ATTRIBUTE_NAME],
                values[IYY_ATTRIBUTE_NAME], values[IYZ_ATTRIBUTE_NAME], values[IZZ_ATTRIBUTE_NAME]);
        }
    }
}
