namespace UrdfUnity.Urdf.Models.LinkElements.GeometryElements
{
    /// <summary>
    /// Represents the box shape of a link's geometry.
    /// </summary>
    public class Box
    {
        /// <summary>
        /// The three side lengths of the box.
        /// </summary>
        public SizeAttribute Size { get; }


        /// <summary>
        /// Creates a new instance of Box.
        /// </summary>
        /// <param name="size">The side lengths of the box</param>
        public Box(SizeAttribute size)
        {
            this.Size = size;
        }
    }
}
