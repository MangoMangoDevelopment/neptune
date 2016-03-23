using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements.VisualElements
{
    /// <summary>
    /// Represents the material of a visual element, including its colour and texture.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    public sealed class Material
    {
        public static readonly string DEFAULT_NAME = "missing_name";


        /// <summary>
        /// The name identifier of the material.
        /// </summary>
        /// <value>Required.</value>
        public string Name { get; }

        /// <summary>
        /// The color specification of the material.
        /// </summary>
        /// <value>Optional. MAY BE NULL.</value>
        public Color Color { get; }

        /// <summary>
        /// The texture of the material.
        /// </summary>
        /// <value>Optional. MAY BE NULL.</value>
        public Texture Texture { get; }


        /// <summary>
        /// Creates a new instance of Material with the specified name.
        /// </summary>
        /// <param name="name">The name identifier of the material</param>
        public Material(string name) : this(name, null, null)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Material with the specified name and colour.
        /// </summary>
        /// <param name="name">The name identifier of the material</param>
        /// <param name="color">The colour of the material</param>
        public Material(string name, Color color) : this(name, color, null)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Material with the specified name and texture.
        /// </summary>
        /// <param name="name">The name identifier of the material</param>
        /// <param name="texture">The texture of the material</param>
        public Material(string name, Texture texture) : this(name, null, texture)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Material with the specified name, colour and texture.
        /// </summary>
        /// <param name="name">The name identifier of the material</param>
        /// <param name="color">The colour of the material. MAY BE NULL</param>
        /// <param name="texture">The texture of the material. MAY BE NULL</param>
        public Material(string name, Color color, Texture texture)
        {
            Preconditions.IsNotEmpty(name, "Material name property must not be null or empty");
            this.Name = name;
            this.Color = color;
            this.Texture = texture;
        }

        protected bool Equals(Material other)
        {
            return string.Equals(Name, other.Name) && Equals(Color, other.Color) && Equals(Texture, other.Texture);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Material)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Color != null ? Color.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Texture != null ? Texture.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
