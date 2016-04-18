using System.Xml;

namespace UrdfToUnity.Parse
{
    /// <summary>
    /// Defines the interface implemented by classes parsing from XML.
    /// </summary>
    /// <typeparam name="T">The object type being parsed and returned</typeparam>
    /// <seealso cref="Parser{T, E}"/>
    public interface XmlParser<T> : Parser<T, XmlNode>
    {
    }
}