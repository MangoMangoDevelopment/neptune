using System.Xml;
using NLog;
using UrdfUnity.Urdf;
using UrdfUnity.Urdf.Models;

namespace UrdfUnity.Parse.Xml.Joints
{
    /// <summary>
    /// Parses a URDF &lt;child&gt; element from XML into a string object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joint"/>
    public sealed class ChildParser : AbstractUrdfXmlParser<string>
    {
        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.CHILD_ELEMENT_NAME;



        /// <summary>
        /// Parses a URDF &lt;child&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;child&gt; element. MUST NOT BE NULL</param>
        /// <returns>A string object with the name of the child link parsed from the XML</returns>
        public override string Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute linkAttribute = GetAttributeFromNode(node, UrdfSchema.LINK_ATTRIBUTE_NAME);
            string childName = Link.DEFAULT_NAME;

            if (linkAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.LINK_ATTRIBUTE_NAME);
            }
            else
            {
                childName = linkAttribute.Value;
            }

            return childName;
        }
    }
}
