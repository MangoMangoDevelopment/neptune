using System.Collections.Generic;
using System.Text;

namespace UrdfUnity.Util
{
    /// <summary>
    /// Utility class for building XML strings.  With exception to the overridden ToString() method,
    /// all methods in this class return the current instance so that method calls can be chained.
    /// </summary>
    public class XmlStringBuilder
    {
        private static readonly string NEW_LINE_STRING = "\r\n";
        private static readonly string INDENT_STRING = "  "; // Two spaces.


        private string element;
        private Dictionary<string, string> attributes = new Dictionary<string, string>();
        private List<string> subelements = new List<string>();


        /// <summary>
        /// Creates a new instance of XmlStringBuilder.
        /// </summary>
        /// <param name="element">The name of the XML element a string will be built for. MUST NOT BE NULL OR EMPTY</param>
        public XmlStringBuilder(string element)
        {
            Preconditions.IsNotEmpty(element, "An element name must be specified to build an XML string");
            this.element = element;
        }

        /// <summary>
        /// Resets this instance of XmlStringBuilder to reuse it to build a new XML string.
        /// </summary>
        /// <param name="element">The name of the XML element a string will be built for. MUST NOT BE NULL OR EMPTY</param>
        /// <returns>This instance of XmlStringBuilder</returns>
        public XmlStringBuilder Reset(string element)
        {
            Preconditions.IsNotEmpty(element, "A new element name must be specified to reset the XML string builder");
            this.element = element;
            this.attributes.Clear();
            this.subelements.Clear();
            return this;
        }

        /// <summary>
        /// Adds an attribute to the XML element string being built.  If an attribute of the same name
        /// has already been added, it will be replaced.
        /// </summary>
        /// <param name="attribute">The name of the attribute</param>
        /// <param name="value">The object value of the attribute, as per its ToString() value</param>
        /// <returns>This instance of XmlStringBuilder</returns>
        public XmlStringBuilder AddAttribute(string attribute, object value)
        {
            if (this.attributes.ContainsKey(attribute))
            {
                this.attributes[attribute] = value.ToString();
            }
            else
            {
                this.attributes.Add(attribute, value.ToString());
            }

            return this;
        }

        /// <summary>
        /// Adds a sub-element to the XML element string being built.
        /// </summary>
        /// <param name="subelement">The string representation of the sub-element</param>
        /// <returns>This instance of XmlStringBuilder</returns>
        public XmlStringBuilder AddSubElement(string subelement)
        {
            this.subelements.Add(subelement);
            return this;
        }

        /// <summary>
        /// Returns the XML string representation as specified by element name, attributes and sub-elements.
        /// </summary>
        /// <returns>The XML string representation as specified</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"<{this.element}");

            foreach (KeyValuePair<string, string> attribute in this.attributes)
            {
                sb.Append($" {attribute.Key}=");
                sb.Append($"\"{attribute.Value}\"");
            }

            if (this.subelements.Count == 0)
            {
                sb.Append("/>");
            }
            else
            {
                sb.AppendLine(">");

                foreach (string subelement in this.subelements)
                {
                    sb.AppendLine(subelement.Replace(NEW_LINE_STRING, INDENT_STRING + NEW_LINE_STRING));
                }

                sb.Append($"</{this.element}>");
            }

            return sb.ToString();
        }
    }
}
