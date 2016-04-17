﻿using System.Xml;
using NLog;
using UrdfUnity.Urdf;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Urdf.Models.Joints;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.Joints
{
    /// <summary>
    /// Parses a URDF &lt;axis&gt; element from XML into a Axis object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joints.Axis"/>
    public sealed class AxisParser : AbstractUrdfXmlParser<Axis>
    {
        // Defaults to (1,0,0)
        private static readonly double DEFAULT_X_VALUE = 1;
        private static readonly double DEFAULT_Y_VALUE = 0;
        private static readonly double DEFAULT_Z_VALUE = 0;


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.AXIS_ELEMENT_NAME;


        /// <summary>
        /// Parses a URDF &lt;axis&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;axis&gt; element. MUST NOT BE NULL</param>
        /// <returns>An Axis object parsed from the XML</returns>
        public override Axis Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute xyzAttribute = GetAttributeFromNode(node, UrdfSchema.XYZ_ATTRIBUTE_NAME);
            XyzAttribute xyz = new XyzAttribute(DEFAULT_X_VALUE, DEFAULT_Y_VALUE, DEFAULT_Z_VALUE);

            if (xyzAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.XYZ_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(xyzAttribute.Value, 3))
                {
                    LogMalformedAttribute(UrdfSchema.XYZ_ATTRIBUTE_NAME);
                }
                else
                {
                    double[] values = RegexUtils.MatchDoubles(xyzAttribute.Value);
                    xyz = new XyzAttribute(values[0], values[1], values[2]);
                }
            }

            return new Axis(xyz);
        }
    }
}
