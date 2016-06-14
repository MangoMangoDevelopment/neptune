using System.IO;
//using NLog;

namespace UrdfToUnity.IO
{
    /// <summary>
    /// Reads and exposes the xacro.config file properties.
    /// </summary>
    public static class ConfigFileReader
    {
        private static readonly string CONFIG_CHAR = "=";

        private static readonly string CONFIG_PATH;
        private static readonly string PYTHON_PATH;
        private static readonly string XACRO_PATH;

        //private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Initializes the ConfigFileReader class by loading the file paths to Python and Xacro
        /// from the xacro.config file in the Config directory.
        /// </summary>
        static ConfigFileReader()
        {
            CONFIG_PATH = $"{Directory.GetCurrentDirectory()}\\Config\\xacro.config";

            StreamReader configFile = new StreamReader(CONFIG_PATH);
            string pythonConfig = configFile.ReadLine();
            string xacroConfig = configFile.ReadLine();

            configFile.Close();

            if (pythonConfig == null)
            {
                //LOGGER.Warn("Failed to load Python path from config file");
            }
            else
            {
                PYTHON_PATH = pythonConfig.Substring(pythonConfig.IndexOf(CONFIG_CHAR) + 1);
            }

            if (xacroConfig == null)
            {
                //LOGGER.Warn("Failed to load Xacro path from config file");
            }
            else
            {
                XACRO_PATH = xacroConfig.Substring(xacroConfig.IndexOf(CONFIG_CHAR) + 1);
            }

            //LOGGER.Info("Paths to python [{0}] and xacro [{1}] loaded", PYTHON_PATH, XACRO_PATH);
        }

        /// <summary>
        /// Returns the file path to the Python executable.
        /// </summary>
        /// <returns>The file path to the Python executable</returns>
        public static string GetPythonPath()
        {
            return PYTHON_PATH;
        }

        /// <summary>
        /// Returns the file path to the Xacro python script.
        /// </summary>
        /// <returns>The file path to the Xacro python script</returns>
        public static string GetXacroPath()
        {
            return XACRO_PATH;
        }
    }
}
