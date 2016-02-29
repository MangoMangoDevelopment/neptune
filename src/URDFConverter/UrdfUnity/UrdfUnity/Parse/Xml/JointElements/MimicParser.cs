using System;
using System.Xml;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;mimic&gt; element from XML into a Mimic object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Mimic"/>
    class MimicParser : XmlParser<Mimic>
    {
        /// <summary>
        /// Parses a URDF &lt;mimic&gt; element from XML.
        /// </summary>
        /// <param name="mimicNode">The XML node of a &lt;mimic&gt; element</param>
        /// <returns>An Mimic object parsed from the XML</returns>
        public Mimic Parse(XmlNode mimicNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
