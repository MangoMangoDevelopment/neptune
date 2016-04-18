using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Util;

namespace UrdfToUnity.Urdf.Models.Links.Geometries
{
    /// <summary>
    /// Represents the the visual trimesh element of a link.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    public class Mesh
    {
        /// <summary>
        /// The default file name that is assigned when the file name is missing.
        /// </summary>
        public static readonly string DEFAULT_FILE_NAME = "missing_file_name";

        private static readonly ScaleAttribute DEFAULT_SCALE = new ScaleAttribute(1d, 1d, 1d);

        /// <summary>
        /// The mesh object file used to represent a geometry.
        /// </summary>
        /// <value>Required.</value>
        public string FileName { get; }

        /// <summary>
        /// The scale of the mesh axis-aligned-bounding-box. 
        /// </summary>
        /// <value>Optional. Default value is <c>DEFAULT_SCALE</c>.</value>
        public ScaleAttribute Scale { get; }

        /// <summary>
        /// The size of the mesh axis-aligned-bounding-box. 
        /// </summary>
        /// <value>Optional. Default value is null.</value>
        public SizeAttribute Size { get; }


        /// <summary>
        /// Creates a new instance of Mesh with the specified file name, specified scale and specified size.
        /// An Mesh.Builder must be used to instantiate a Mesh with optional properties as specified.
        /// </summary>
        /// <param name="fileName">The mesh object file name. MUST NOT BE EMPTY</param>
        /// <param name="scale">The scale of the mesh object. MUST NOT BE NULL</param>
        /// <param name="size">The size of the mesh object. MAY BE NULL</param>
        private Mesh(string fileName, ScaleAttribute scale, SizeAttribute size)
        {
            Preconditions.IsNotEmpty(fileName, "Mesh file name property must not be null or empty");
            Preconditions.IsNotNull(scale, "Mesh scale property must not be null");
            this.FileName = fileName;
            this.Scale = scale;
            this.Size = size;
        }


        /// <summary>
        /// Helper class to build a new instance of Mesh with the optional properties if specified
        /// and default property values otherwise.
        /// </summary>
        public class Builder
        {
            private string fileName;
            private ScaleAttribute scale = DEFAULT_SCALE;
            private SizeAttribute size = null;


            /// <summary>
            /// Creates a new instance of Mesh.Builder.
            /// </summary>
            /// <param name="fileName">The mesh object file name. MUST NOT BE EMPTY</param>
            public Builder(string fileName)
            {
                Preconditions.IsNotEmpty(fileName, "Mesh file name property cannot be set to null or empty");
                this.fileName = fileName;
            }

            /// <summary>
            /// Creates a new instance of Mesh with the specified properties and default properties if not specified.
            /// </summary>
            /// <returns>A Mesh object with the properties set</returns>
            public Mesh Build()
            {
                return new Mesh(this.fileName, this.scale, this.size);
            }

            public Builder SetScale(ScaleAttribute scale)
            {
                Preconditions.IsNotNull(scale, "Mesh scale property cannot be set to null");
                this.scale = scale;
                return this;
            }

            public Builder SetSize(SizeAttribute size)
            {
                this.size = size;
                return this;
            }
        }


        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            XmlStringBuilder sb = new XmlStringBuilder(UrdfSchema.MESH_ELEMENT_NAME)
                .AddAttribute(UrdfSchema.FILE_NAME_ATTRIBUTE_NAME, this.FileName);

            if (this.Scale != DEFAULT_SCALE)
            {
                sb.AddAttribute(UrdfSchema.SCALE_ATTRIBUTE_NAME, this.Scale);
            }

            if (this.Size != null)
            {
                sb.AddAttribute(UrdfSchema.SIZE_ATTRIBUTE_NAME, this.Size);
            }

            return sb.ToString();
        }

        protected bool Equals(Mesh other)
        {
            return FileName.Equals(other.FileName) && Scale.Equals(other.Scale)
                && (Size != null ? Size.Equals(other.Size) : other.Size == null);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Mesh)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FileName.GetHashCode();
                hashCode = (hashCode * 397) ^ Scale.GetHashCode();
                hashCode = (hashCode * 397) ^ (Size != null ? Size.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
