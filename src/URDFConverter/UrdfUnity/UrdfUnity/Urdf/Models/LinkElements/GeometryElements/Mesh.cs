using UrdfUnity.Util.Preconditions;

namespace UrdfUnity.Urdf.Models.LinkElements.GeometryElements
{
    /// <summary>
    /// Represents the the visual trimesh element of a link.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    public class Mesh
    {
        private static readonly ScaleAttribute DEFAULT_SCALE = new ScaleAttribute(1d, 1d, 1d);

        /// <summary>
        /// The mesh object file used to represent a geometry.
        /// </summary>
        /// <value>
        /// Required.
        /// </value>
        public string FileName { get; }

        /// <summary>
        /// The scale of the mesh axis-aligned-bounding-box. 
        /// </summary>
        /// <value>
        /// Optional. Default value is <c>DEFAULT_SCALE</c>.
        /// </value>
        public ScaleAttribute Scale { get; }

        /// <summary>
        /// The size of the mesh axis-aligned-bounding-box. 
        /// </summary>
        /// <value>
        /// Optional. Default value is null.
        /// </value>
        public SizeAttribute Size { get; }


        /// <summary>
        /// Creates a new instance of Mesh with the specified file name, default scale and default size.
        /// </summary>
        /// <param name="fileName">The mesh object file name</param>
        public Mesh(string fileName) : this(fileName, DEFAULT_SCALE)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Mesh with the specified file name, specified scale and default size.
        /// </summary>
        /// <param name="fileName">The mesh object file name</param>
        /// <param name="scale">The scale applied to the mesh object</param>
        public Mesh(string fileName, ScaleAttribute scale) : this(fileName, scale, null)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Mesh with the specified file name, default scale and specified size.
        /// </summary>
        /// <param name="fileName">The mesh object file name</param>
        /// <param name="size">The size of the mesh object</param>
        public Mesh(string fileName, SizeAttribute size) : this(fileName, DEFAULT_SCALE, size)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Mesh with the specified file name, specified scale and specified size.
        /// </summary>
        /// <param name="fileName">The mesh object file name</param>
        /// <param name="scale">The scale of the mesh object</param>
        /// <param name="size">The size of the mesh object. MAY BE NULL</param>
        public Mesh(string fileName, ScaleAttribute scale, SizeAttribute size)
        {
            Assert.IsNotEmpty(fileName);
            Assert.IsNotNull(scale);
            this.FileName = fileName;
            this.Scale = scale;
            this.Size = size;
        }
    }
}
