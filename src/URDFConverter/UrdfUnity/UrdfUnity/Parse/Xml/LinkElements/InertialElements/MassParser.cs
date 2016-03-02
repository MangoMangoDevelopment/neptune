using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.InertialElements
{
    /// <summary>
    /// Parses a URDF &lt;mass&gt; element from XML into a Mass object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.LinkElements.InertialElements.Mass"/>
    public class MassParser : XmlParser<Mass>
    {
        private static readonly double DEFAULT_MASS = 0d;


        /// <summary>
        /// Parses a URDF &lt;mass&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;mass&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Mass object with the value parsed from the XML, or the default value of 0 if no mass value parsed</returns>
        public Mass Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");
            return new Mass(RegexUtils.MatchDouble(node.InnerText, DEFAULT_MASS));
        }
    }
}
