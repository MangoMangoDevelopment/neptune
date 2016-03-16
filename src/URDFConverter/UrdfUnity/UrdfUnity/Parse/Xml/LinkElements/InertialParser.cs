using System.Xml;
using UrdfUnity.Parse.Xml.LinkElements.InertialElements;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements
{
    /// <summary>
    /// Parses a URDF &lt;inertial&gt; element from XML into a Inertial object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/inertial"/>
    /// <seealso cref="Urdf.Models.LinkElements.Inertial"/>
    public class InertialParser : XmlParser<Inertial>
    {
        private static readonly string ORIGIN_ELEMENT_NAME = "origin";
        private static readonly string MASS_ELEMENT_NAME = "mass";
        private static readonly string INERTIA_ELEMENT_NAME = "inertia";

        private readonly OriginParser originParser = new OriginParser();
        private readonly MassParser massParser = new MassParser();
        private readonly InertiaParser inertiaParser = new InertiaParser();


        /// <summary>
        /// Parses a URDF &lt;inertial&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;inertial&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Inertial object parsed from the XML</returns>
        public Inertial Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlElement originElement = XmlParsingUtils.GetElementFromNode(node, ORIGIN_ELEMENT_NAME);
            XmlElement massElement = XmlParsingUtils.GetElementFromNode(node, MASS_ELEMENT_NAME);
            XmlElement inertiaElement = XmlParsingUtils.GetElementFromNode(node, INERTIA_ELEMENT_NAME);

            Origin origin = ParseOrigin(originElement);
            Mass mass = ParseMass(massElement);
            Inertia inertia = ParseInertia(inertiaElement);

            return (origin != null) ? new Inertial(origin, mass, inertia) : new Inertial(mass, inertia);
        }

        private Origin ParseOrigin(XmlElement originElement)
        {
            if (originElement == null)
            {
                // TODO: Log that optional origin was not provided.
                return null;
            }

            return this.originParser.Parse(originElement);
        }

        private Mass ParseMass(XmlElement massElement)
        {
            if (massElement == null)
            {
                // TODO: Log missing required mass element.
                return new Mass(MassParser.DEFAULT_MASS);
            }

            return this.massParser.Parse(massElement);
        }

        private Inertia ParseInertia(XmlElement inertiaElement)
        {
            if (inertiaElement == null)
            {
                // TODO: Log missing required inertia element.
                return new Inertia(InertiaParser.DEFAULT_VALUE, InertiaParser.DEFAULT_VALUE, InertiaParser.DEFAULT_VALUE,
                    InertiaParser.DEFAULT_VALUE, InertiaParser.DEFAULT_VALUE, InertiaParser.DEFAULT_VALUE);
            }

            return this.inertiaParser.Parse(inertiaElement);
        }
    }
}
