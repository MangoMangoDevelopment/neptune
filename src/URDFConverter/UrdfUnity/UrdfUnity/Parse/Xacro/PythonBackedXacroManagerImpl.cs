using System;
using System.Diagnostics;
using UrdfUnity.IO;

namespace UrdfUnity.Parse.Xacro
{
    /// <summary>
    /// Implements the XacroManager interface by using the python ROS Xacro package to convert
    /// Xacro files to URDF.
    /// </summary>
    /// <remarks>
    /// There is some setup required to be able to use the ROS Xacro package.  See neptune/lib/README.md for reference.
    /// </remarks>
    /// <seealso cref="XacroManager"/>
    /// <seealso cref="http://wiki.ros.org/xacro"/>
    public class PythonBackedXacroManagerImpl : XacroManager
    {
        private readonly string pythonPath;
        private readonly string xacroPath;


        /// <summary>
        /// Creates a new instance of PythonBackedXacroManagerImpl, loading the file paths to
        /// python.exe and xacro.py from the config file in Config/xacro.config.
        /// </summary>
        public PythonBackedXacroManagerImpl()
        {
            this.pythonPath = ConfigFileReader.GetPythonPath();
            this.xacroPath = ConfigFileReader.GetXacroPath();
        }

        /// <summary>
        /// Converts the specified Xacro file to a URDF file using the ROS python Xacro package.
        /// </summary>
        /// <param name="xacroFile">The file path to the Xacro file being input</param>
        /// <param name="urdfFile">The file path to the URDF file being output</param>
        public void ConvertToUrdf(string xacroFile, string urdfFile)
        {
            ProcessStartInfo start = new ProcessStartInfo();

            start.FileName = this.pythonPath;
            start.Arguments = String.Format("{0} {1} {2} {3} {4}", this.xacroPath, "--inorder", "-o", urdfFile, xacroFile);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;

            Process.Start(start);
        }
    }
}