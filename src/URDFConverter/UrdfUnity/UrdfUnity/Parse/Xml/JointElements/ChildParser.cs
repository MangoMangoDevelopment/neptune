using System;
using System.Xml;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;child&gt; element from XML into a string object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joint"/>
    class ChildParser : XmlParser<string>
    {
        /// <summary>
        /// Parses a URDF &lt;child&gt; element from XML.
        /// </summary>
        /// <param name="childNode">The XML node of a &lt;child&gt; element</param>
        /// <returns>A string object with the name of the child link parsed from the XML</returns>
        public string Parse(XmlNode childNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
