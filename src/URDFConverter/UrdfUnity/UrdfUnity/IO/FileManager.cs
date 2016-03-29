namespace UrdfUnity.IO
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
        /// 
        string ReadFileToString(string path);
        
    }

    /// <summary>
    /// Filetypes we care about in the file manager.
    /// </summary>
    public enum FileType
    {
        UNKNOWN,
        URDF,
        XACRO
    }
}
