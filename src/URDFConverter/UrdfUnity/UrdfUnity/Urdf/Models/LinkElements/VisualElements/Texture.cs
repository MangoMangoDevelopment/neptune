using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements.VisualElements
{
    /// <summary>
    /// Represents the texture of a material of a visual element's material.
    /// </summary>
    public class Texture
    {
        /// <summary>
        /// The file name of the texture for the visual element.
        /// </summary>
        public string FileName { get; }


        /// <summary>
        /// Creates a new instance of Texture.
        /// </summary>
        /// <param name="fileName">The file name of the texture</param>
        public Texture(string fileName)
        {
            Preconditions.IsNotEmpty(fileName);
            this.FileName = fileName;
        }
    }
}
