using System.Xml;

namespace UrdfUnity.Parse
{
    /// <summary>
    /// Defines the interface implemented by classes parsing from XML.
    /// </summary>
    /// <typeparam name="T">The object type being parsed and returned</typeparam>
    /// <seealso cref="Parser{T, E}"/>
    interface XmlParser<T> : Parser<T, XmlNode>
    {
    }
}
