using System;
using System.Xml;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;parent&gt; element from XML into a string object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joint"/>
    public sealed class ParentParser : AbstractUrdfXmlParser<string>
    {
        private static readonly string LINK_ATTRIBUTE_NAME = "link";


        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "parent";


        /// <summary>
        /// Parses a URDF &lt;parent&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;parent&gt; element</param>
        /// <returns>A string object with the name of the parent link parsed from the XML</returns>
        public override string Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute linkAttribute = GetAttributeFromNode(node, LINK_ATTRIBUTE_NAME);
            string parentName = Link.DEFAULT_NAME;

            if (linkAttribute == null)
            {
                // TODO: Log missing required <childe> name attribute encountered
            }
            else
            {
                parentName = linkAttribute.Value;
            }

            return parentName;
        }
    }
}
