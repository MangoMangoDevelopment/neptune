﻿using System.Xml;
//using NLog;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models;

namespace UrdfToUnity.Parse.Xml.Joints
{
    /// <summary>
    /// Parses a URDF &lt;parent&gt; element from XML into a string object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joint"/>
    public sealed class ParentParser : AbstractUrdfXmlParser<string>
    {
        //protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.PARENT_ELEMENT_NAME;


        /// <summary>
        /// Parses a URDF &lt;parent&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;parent&gt; element</param>
        /// <returns>A string object with the name of the parent link parsed from the XML</returns>
        public override string Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute linkAttribute = GetAttributeFromNode(node, UrdfSchema.LINK_ATTRIBUTE_NAME);
            string parentName = Link.DEFAULT_NAME;

            if (linkAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.LINK_ATTRIBUTE_NAME);
            }
            else
            {
                parentName = linkAttribute.Value;
            }

            return parentName;
        }
    }
}
