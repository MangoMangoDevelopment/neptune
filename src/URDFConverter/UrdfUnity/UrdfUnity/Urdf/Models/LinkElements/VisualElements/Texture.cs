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

        protected bool Equals(Texture other)
        {
            return string.Equals(FileName, other.FileName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Texture) obj);
        }

        public override int GetHashCode()
        {
            return FileName.GetHashCode();
        }
    }
}
