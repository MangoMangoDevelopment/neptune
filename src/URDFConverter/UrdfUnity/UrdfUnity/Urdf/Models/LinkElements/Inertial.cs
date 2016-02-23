using UrdfUnity.Urdf.Models.LinkElements.InertialElements;

namespace UrdfUnity.Urdf.Models.LinkElements
{
    /// <summary>
    /// Represents the inertial properties of a link.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/inertial"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="InertialOrigin"/>
    public class Inertial
    {
        /// <summary>
        /// The pose of the inertial reference frame, relative to the link reference frame.
        /// </summary>
        public Origin Origin { get; set; }

        /// <summary>
        /// The mass of the link in kiligrams.
        /// </summary>
        public Mass Mass { get; set; }

        /// <summary>
        /// The 3x3 rotational inertia matrix, represented in the inertia frame.
        /// </summary>
        public Inertia Inertia { get; set; }


        /// <summary>
        /// Creates a new instance of Inertial with the mass and inertia specified.
        /// </summary>
        /// <param name="mass">The link's mass</param>
        /// <param name="inertia">The link's inertia matrix</param>
        public Inertial(Mass mass, Inertia inertia) : this(new Origin(), mass, inertia)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Inertial with the inertial origin, mass and inertia specified.
        /// </summary>
        /// <param name="origin">The link's inertial reference frame</param>
        /// <param name="mass">The link's mass</param>
        /// <param name="inertia">The link's inertia matrix</param>
        public Inertial(Origin origin, Mass mass, Inertia inertia)
        {
            this.Origin = origin;
            this.Mass = mass;
            this.Inertia = inertia;
        }
    }
}
