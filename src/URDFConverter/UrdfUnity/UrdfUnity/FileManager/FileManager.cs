using System;
using System.IO;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Util;
using UrdfUnity.Parse;
using NLog;


namespace UrdfUnity.FileManager
{
    /// <summary>
    /// Filetypes we care about in the file manager.
    /// </summary>
    public enum FileType
    {
        UNKNOWN,
        URDF,
        XACRO
    }


    /// <summary>
    /// This class will be handling the FileIO 
    /// </summary>
    public class FileManager
    {
        Logger LOGGER;
        

        /// <summary>
        /// Base constructor, for initializing components
        /// </summary>
        public FileManager()
        {
            // initializing the logger for this class.
            LOGGER = LogManager.GetCurrentClassLogger();
        }


        /// <summary>
        /// We have a URDF/XACRO filepath that needs to be extracted to be
        /// a Robot model. 
        /// </summary>
        /// <param name="filePath">Path of file containing urdf/xacro for the robot</param>
        /// <returns>
        ///     Robot|null - A Robot model if a valid file path and content for a robot. 
        ///     A null returned when invalid filepath or content is supplied.
        /// </returns>
        public Robot GetRobotFromFile(string filePath)
        {
            Robot robo = null;
            string fileString = "";
            try
            {
                using (StreamReader fp = new StreamReader(filePath))
                {
                    fileString = fp.ReadToEnd();
                }
            }
            catch (FileNotFoundException fnf_ex)
            {
                LOGGER.Error("File not found \"{0}\".");
            }


            if (!fileString.Equals(string.Empty))
            {
                FileType type = GetFileType(filePath);
                switch(type)
                {
                    case FileType.URDF:
                        UrdfParser urdf = new UrdfParser();
                        robo = urdf.Parse(fileString);
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
            catch (OverflowException ex)
            {
                LOGGER.Error("File type has not been defined.");
            }
            catch (ArgumentException arg_ex)
            {
                LOGGER.Error("File type contains white space or is null.");
            }
            return type;
        }
    }
}
