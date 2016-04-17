using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.Links.Geometries
{
    /// <summary>
    /// Represents the box shape of a link's geometry.
    /// </summary>
    public sealed class Box
    {
        /// <summary>
        /// The three side lengths of the box.
        /// </summary>
        /// <value>Required.</value>
        public SizeAttribute Size { get; }


        /// <summary>
        /// Creates a new instance of Box.
        /// </summary>
        /// <param name="size">The side lengths of the box</param>
        public Box(SizeAttribute size)
        {
            Preconditions.IsNotNull(size, "Box size property must not be null");
            Preconditions.IsTrue(size.Length > 0, "The length of a box must be greater than 0");
            Preconditions.IsTrue(size.Width > 0, "The width of a box must be greater than 0");
            Preconditions.IsTrue(size.Height > 0, "The height of a box must be greater than 0");
            this.Size = size;
        }

        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            return new XmlStringBuilder(UrdfSchema.BOX_ELEMENT_NAME)
                .AddAttribute(UrdfSchema.SIZE_ATTRIBUTE_NAME, this.Size)
                .ToString();
        }

        protected bool Equals(Box other)
        {
            return Size.Equals(other.Size);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Box)obj);
        }

        public override int GetHashCode()
        {
            return Size.GetHashCode();
        }
    }
}
