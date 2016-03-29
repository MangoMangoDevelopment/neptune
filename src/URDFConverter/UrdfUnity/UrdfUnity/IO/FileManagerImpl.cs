using System;
using System.IO;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Util;
using UrdfUnity.Parse;
using NLog;


namespace UrdfUnity.IO
{
    /// <summary>
    /// Handles file input/output for URDF and Xacro files
    /// </summary>
    public class FileManagerImpl : FileManager
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
        UrdfParser urdf = new UrdfParser();


        /// <summary>
        /// Reads the entire file into a string.
        /// </summary>
        /// <param name="path">Path of file to read</param>
        /// <returns>String representation of file, otherwise empty string.</returns>
        public string ReadFileToString (string path)
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
        /// Extracts a Robot model object from the provided URDF/Xacro filepath.
        /// </summary>
        /// <param name="filePath">Path of file containing urdf/xacro for the robot</param>
        /// <returns>
        ///     The Robot model parsed from the specified filepath if valid, otherwise <c>null<c>
        /// </returns>
        public Robot GetRobotFromFile(string filePath)
        {
            Robot robo = null;

            string contents = ReadFileToString(filePath);

            if (!contents.Equals(string.Empty))
            {
                FileType type = GetFileType(filePath);
                switch(type)
                {
                    case FileType.URDF:
                        robo = this.urdf.Parse(contents);
                        break;
                    case FileType.XACRO:
                        // TODO: Call Xacro parser
                        throw new NotImplementedException();
                        break;
                    default:
                        LOGGER.Warn("Unknown filetype supplied. Unable to parse robot.");
                        break;
                }
            }
            else
            {
                LOGGER.Warn("Empty or no file loaded.");
            }

            return robo;
        }


        /// <summary>
        /// Given a file path, this will extract the extention and return an
        /// enum type of FileType indicating whether we're trying to parse a
        /// URDF or XACRO file.
        /// </summary>
        /// <param name="file">This can be a string representation of the file path or just filename</param>
        /// <returns></returns>
        public FileType GetFileType(string fileName)
        {
            // remove the . from the get extension function, so that it can be properly translated to an enum type of FileType
            string extension = Path.GetExtension(fileName).Replace('.', '\0');
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
    }
}
