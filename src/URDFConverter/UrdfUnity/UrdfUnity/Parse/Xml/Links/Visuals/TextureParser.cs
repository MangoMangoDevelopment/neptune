using System;
using System.Xml;
using NLog;
using UrdfUnity.Urdf.Models.Links.Visuals;

namespace UrdfUnity.Parse.Xml.Links.Visuals
{
    /// <summary>
    /// Parses a URDF &lt;texture&gt; element from XML into a Texture object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.Links.Visuals.Texture"/>
    public sealed class TextureParser : AbstractUrdfXmlParser<Texture>
    {
        private static readonly string FILE_NAME_ATTRIBUTE_NAME = "filename";


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "texture";


        /// <summary>
        /// Parses a URDF &lt;texture&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;texture&gt; element</param>
        /// <returns>A Texture object parsed from the XML</returns>
        public override Texture Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute fileNameAttribute = GetAttributeFromNode(node, FILE_NAME_ATTRIBUTE_NAME);
            string filename = Texture.DEFAULT_FILE_NAME;

            if (fileNameAttribute == null || String.IsNullOrEmpty(fileNameAttribute.Value))
            {
                LogMissingRequiredAttribute(FILE_NAME_ATTRIBUTE_NAME);
            }
            else
            {
                filename = fileNameAttribute.Value;
            }

            return new Texture(filename);
        }
    }
}
