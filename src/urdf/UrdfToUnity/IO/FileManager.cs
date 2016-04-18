using System.Xml;
using UrdfToUnity.Urdf.Models;

namespace UrdfToUnity.IO
{
    /// <summary>
    /// Defines the generic interface implemented by utility parsing classes.
    /// </summary>
    public interface FileManager
    {
        /// <summary>
        /// Reads a file given the provided path and returns the object.
        /// </summary>
        /// <param name="path">The path of file to be read</param>
        /// <returns>The object that was found in the file</returns>
        string ReadFileToString(string path);

        /// <summary>
        /// Reads the entire file into a XmlDocument.
        /// </summary>
        /// <param name="path">Path of file to read</param>
        /// <returns>Root node of the XML file if the file is valid XML, otherwise <c>null</c></returns>
        XmlNode ReadXmlNodeFromFile(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Robot GetRobotFromFile(string filePath);
    }


    /// <summary>
    /// File types we care about in the file manager.
    /// </summary>
    public enum FileType
    {
        UNKNOWN,
        URDF,
        XACRO
    }
}
