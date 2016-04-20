using System;
using System.IO;
using System.Xml;
using UrdfToUnity.Urdf.Models;
using UrdfToUnity.Util;
using UrdfToUnity.Parse.Xml;
using UrdfToUnity.Parse.Xacro;
using NLog;

namespace UrdfToUnity.IO
{
    /// <summary>
    /// Handles file input/output for URDF and Xacro files
    /// </summary>
    public class FileManagerImpl : FileManager
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        XmlDocument xmlDoc = new XmlDocument();
        RobotParser urdf = new RobotParser();
        //XacroManager xacro = new PythonBackedXacroManagerImpl();


        /// <summary>
        /// Reads the entire file into a string.
        /// </summary>
        /// <param name="path">Path of file to read</param>
        /// <returns>String representation of file, otherwise empty string.</returns>
        public string ReadFileToString(string path)
        {
            string fileAsString = String.Empty;

            try
            {
                using (StreamReader fileReader = new StreamReader(path))
                {
                    fileAsString = fileReader.ReadToEnd();
                }
            }
            catch (FileNotFoundException e)
            {
                LOGGER.Warn("Error file not found \"{0}\"", path);
            }

            return fileAsString;
        }


        /// <summary>
        /// Reads the entire file into a XmlDocument.
        /// </summary>
        /// <param name="path">Path of file to read</param>
        /// <returns>Root node of the XML file if the file is valid XML, otherwise <c>null</c></returns>
        public XmlNode ReadXmlNodeFromFile(string path)
        {
            XmlNode node = null;

            try
            {
                using (StreamReader fileReader = new StreamReader(path))
                {
                    xmlDoc.Load(fileReader);
                }
                node = xmlDoc.DocumentElement;
            }
            catch (FileNotFoundException e)
            {
                LOGGER.Warn("Error invalid XML found \"{0}\"", path);
            }

            return node;
        }

        /// <summary>
        /// Extracts a Robot model object from the provided URDF/Xacro filepath.
        /// </summary>
        /// <param name="filePath">Path of file containing urdf/xacro for the robot</param>
        /// <returns>
        /// The Robot model parsed from the specified filepath if valid, otherwise <c>null</c>
        /// </returns>
        public Robot GetRobotFromFile(string filePath)
        {
            Robot robo = null;
            XmlNode root = this.ReadXmlNodeFromFile(filePath);
            FileType type = FileManagerImpl.GetFileType(filePath);

            if (root == null)
            {
                LOGGER.Warn("Unable to read valid XML node from file path [{0}]", filePath);
                return null;
            }

            switch (type)
            {
                case FileType.URDF:
                    robo = this.urdf.Parse(root);
                    break;

                case FileType.XACRO:
                    //string tempFile = filePath + ".urdf";
                    //this.xacro.ConvertToUrdf(filePath, tempFile);
                    //robo = this.GetRobotFromFile(tempFile);
                    //File.Delete(tempFile);
                    throw new NotImplementedException();
                    break;

                default:
                    LOGGER.Warn("Unknown filetype supplied. Unable to parse robot.");
                    break;
            }

            return robo;
        }

        /// <summary>
        /// Given a file path, this will extract the extension and return an
        /// enum type of FileType indicating whether we're trying to parse a
        /// URDF or XACRO file.
        /// </summary>
        /// <param name="file">This can be a string representation of the file path or just filename</param>
        /// <returns>The <c>FileType</c> value corresponding to the file type</returns>
        static public FileType GetFileType(string fileName)
        {
            // remove the . from the get extension function, so that it can be properly translated to an enum type of FileType
            string extension = Path.GetExtension(fileName).Replace(".", "");

            FileType type = FileType.UNKNOWN;
            try
            {
                type = EnumUtils.ToEnum<FileType>(extension);
            }
            catch (OverflowException e)
            {
                LOGGER.Warn("File type has not been defined.");
            }
            catch (ArgumentException e)
            {
                LOGGER.Warn("File type contains white space or is null.");
            }

            return type;
        }

        /// <summary>
        /// Provides the name of the file from the specified file path.
        /// </summary>
        /// <param name="path">The file path including the file name</param>
        /// <param name="includeExtension">Specifies if the file name is returned with its file extension</param>
        /// <returns>
        /// The name of the file with its extension if <c>includeExtension = true</c>, otherwise
        /// the name of the file without its extension.
        /// </returns>
        static public string GetFileName(string path, bool includeExtension = true)
        {
            if (includeExtension)
            {
                return Path.GetFileName(path);
            }
            else
            {
                return Path.GetFileNameWithoutExtension(path);
            }
        }
    }
}
