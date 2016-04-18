namespace UrdfToUnity.Parse.Xacro
{
    /// <summary>
    /// Defines the interface used to convert Xacro files to URDF files.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/xacro"/>
    public interface XacroManager
    {
        /// <summary>
        /// Converts the specified Xacro file to a URDF file.
        /// </summary>
        /// <param name="xacroFile">The file path to the Xacro file being input</param>
        /// <param name="urdfFile">The file path to the URDF file being output</param>
        void ConvertToUrdf(string xacroFile, string urdfFile);
    }
}
