using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements.VisualElements
{
    /// <summary>
    /// Represents the material of a visual element, including its colour and texture.
    /// </summary>
    public class Material
    {
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
        /// <param name="color">The colour of the materialL</param>
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
            Preconditions.IsNotEmpty(name);
            this.Name = name;
            this.Color = color;
            this.Texture = texture;
        }
    }
}
