using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements.GeometryElements
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
            this.Size = size;
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
