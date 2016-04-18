using System.Xml;
using NLog;
using UrdfToUnity.Parse.Xml.Links.Inertials;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models;
using UrdfToUnity.Urdf.Models.Links;
using UrdfToUnity.Urdf.Models.Links.Inertials;

namespace UrdfToUnity.Parse.Xml.Links
{
    /// <summary>
    /// Parses a URDF &lt;inertial&gt; element from XML into a Inertial object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/inertial"/>
    /// <seealso cref="Urdf.Models.Links.Inertial"/>
    public sealed class InertialParser : AbstractUrdfXmlParser<Inertial>
    {
        private static readonly double DEFAULT_MASS = 0d;


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.INERTIAL_ELEMENT_NAME;

        private readonly OriginParser originParser = new OriginParser();
        private readonly MassParser massParser = new MassParser();
        private readonly InertiaParser inertiaParser = new InertiaParser();


        /// <summary>
        /// Parses a URDF &lt;inertial&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;inertial&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Inertial object parsed from the XML</returns>
        public override Inertial Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlElement originElement = GetElementFromNode(node, UrdfSchema.ORIGIN_ELEMENT_NAME);
            XmlElement massElement = GetElementFromNode(node, UrdfSchema.MASS_ELEMENT_NAME);
            XmlElement inertiaElement = GetElementFromNode(node, UrdfSchema.INERTIA_ELEMENT_NAME);

            Origin origin = ParseOrigin(originElement);
            Mass mass = ParseMass(massElement);
            Inertia inertia = ParseInertia(inertiaElement);

            return (origin != null) ? new Inertial(origin, mass, inertia) : new Inertial(mass, inertia);
        }

        private Origin ParseOrigin(XmlElement originElement)
        {
            if (originElement == null)
            {
                LogMissingOptionalAttribute(UrdfSchema.ORIGIN_ELEMENT_NAME);
                return null;
            }

            return this.originParser.Parse(originElement);
        }

        private Mass ParseMass(XmlElement massElement)
        {
            if (massElement == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.MASS_ELEMENT_NAME);
                return new Mass(DEFAULT_MASS);
            }

            return this.massParser.Parse(massElement);
        }

        private Inertia ParseInertia(XmlElement inertiaElement)
        {
            if (inertiaElement == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.INERTIA_ELEMENT_NAME);
                return new Inertia(InertiaParser.DEFAULT_VALUE, InertiaParser.DEFAULT_VALUE, InertiaParser.DEFAULT_VALUE,
                    InertiaParser.DEFAULT_VALUE, InertiaParser.DEFAULT_VALUE, InertiaParser.DEFAULT_VALUE);
            }

            return this.inertiaParser.Parse(inertiaElement);
        }
    }
}
