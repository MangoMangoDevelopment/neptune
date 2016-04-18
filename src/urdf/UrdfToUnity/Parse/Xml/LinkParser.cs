using System.Collections.Generic;
using System.Xml;
using NLog;
using UrdfToUnity.Parse.Xml.Links;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models;
using UrdfToUnity.Urdf.Models.Links;
using UrdfToUnity.Urdf.Models.Links.Visuals;

namespace UrdfToUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;link&gt; element from XML into a Link object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.Link"/>
    public sealed class LinkParser : AbstractUrdfXmlParser<Link>
    {
        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.LINK_ELEMENT_NAME;


        private readonly InertialParser inertialParser = new InertialParser();
        private readonly VisualParser visualParser;
        private readonly CollisionParser collisionParser = new CollisionParser();


        /// <summary>
        /// Creates a new instance of LinkParser.
        /// </summary>
        /// <param name="materialDictionary"></param>
        public LinkParser(Dictionary<string, Material> materialDictionary)
        {
            this.visualParser = new VisualParser(materialDictionary);
        }

        /// <summary>
        /// Parses a URDF &lt;link&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;link&gt; element</param>
        /// <returns>A Link object parsed from the XML</returns>
        public override Link Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute nameAttribute = GetAttributeFromNode(node, UrdfSchema.NAME_ATTRIBUTE_NAME);
            XmlElement inertialElement = GetElementFromNode(node, UrdfSchema.INERTIAL_ELEMENT_NAME);
            XmlNodeList Visuals = node.SelectNodes(UrdfSchema.VISUAL_ELEMENT_NAME);
            XmlNodeList collisionElements = node.SelectNodes(UrdfSchema.COLLISION_ELEMENT_NAME);

            Link.Builder builder;

            if (nameAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.NAME_ATTRIBUTE_NAME);
                builder = new Link.Builder(Link.DEFAULT_NAME);
            }
            else
            {
                builder = new Link.Builder(nameAttribute.Value);
            }

            if (inertialElement != null)
            {
                builder.SetInertial(this.inertialParser.Parse(inertialElement));
            }

            if (Visuals != null)
            {
                builder.SetVisual(ParseVisuals(Visuals));
            }

            if (collisionElements != null)
            {
                builder.SetCollision(ParseCollisions(collisionElements));
            }

            return builder.Build();
        }

        private List<Visual> ParseVisuals(XmlNodeList nodeList)
        {
            List<Visual> visuals = new List<Visual>();

            if (nodeList == null || nodeList.Count == 0)
            {
                LogMissingRequiredElement(UrdfSchema.VISUAL_ELEMENT_NAME);
            }
            else
            {
                foreach (XmlNode node in nodeList)
                {
                    visuals.Add(this.visualParser.Parse(node));
                }
            }

            return visuals;
        }

        private List<Collision> ParseCollisions(XmlNodeList nodeList)
        {
            List<Collision> collisions = new List<Collision>();

            if (nodeList == null || nodeList.Count == 0)
            {
                LogMissingOptionalElement(UrdfSchema.COLLISION_ELEMENT_NAME);
            }
            else
            {
                foreach (XmlNode node in nodeList)
                {
                    collisions.Add(this.collisionParser.Parse(node));
                }
            }

            return collisions;
        }
    }
}
