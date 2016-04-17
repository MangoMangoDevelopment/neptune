using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.Links.Visuals
{
    /// <summary>
    /// Represents the texture of a material of a visual element's material.
    /// </summary>
    public sealed class Texture
    {
        /// <summary>
        /// The default file name that is assigned when the file name is missing.
        /// </summary>
        public static readonly string DEFAULT_FILE_NAME = "missing_file_name";


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
            Preconditions.IsNotEmpty(fileName, "Texture file name property must not be null or empty");
            this.FileName = fileName;
        }

        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            return new XmlStringBuilder(UrdfSchema.TEXTURE_ELEMENT_NAME)
                .AddAttribute(UrdfSchema.FILE_NAME_ATTRIBUTE_NAME, this.FileName)
                .ToString();
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
            return Equals((Texture)obj);
        }

        public override int GetHashCode()
        {
            return FileName.GetHashCode();
        }
    }
}
