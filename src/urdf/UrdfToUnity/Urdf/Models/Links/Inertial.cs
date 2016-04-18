using UrdfToUnity.Urdf.Models.Links.Inertials;
using UrdfToUnity.Util;

namespace UrdfToUnity.Urdf.Models.Links
{
    /// <summary>
    /// Represents the inertial properties of a link.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/inertial"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    public sealed class Inertial
    {
        /// <summary>
        /// The pose of the inertial reference frame, relative to the link reference frame.
        /// </summary>
        /// <value>Required. Defaults to identity</value>
        public Origin Origin { get; set; }

        /// <summary>
        /// The mass of the link in kilograms.
        /// </summary>
        /// <value>Required.</value>
        public Mass Mass { get; set; }

        /// <summary>
        /// The 3x3 rotational inertia matrix, represented in the inertia frame.
        /// </summary>
        /// <value>Required.</value>
        public Inertia Inertia { get; set; }


        /// <summary>
        /// Creates a new instance of Inertial with the mass and inertia specified.
        /// </summary>
        /// <param name="mass">The link's mass. MUST NOT BE NULL</param>
        /// <param name="inertia">The link's inertia matrix. MUST NOT BE NULL</param>
        public Inertial(Mass mass, Inertia inertia) : this(Origin.DEFAULT_ORIGIN, mass, inertia)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Inertial with the inertial origin, mass and inertia specified.
        /// </summary>
        /// <param name="origin">The link's inertial reference frame. MUST NOT BE NULL</param>
        /// <param name="mass">The link's mass. MUST NOT BE NULL</param>
        /// <param name="inertia">The link's inertia matrix. MUST NOT BE NULL</param>
        public Inertial(Origin origin, Mass mass, Inertia inertia)
        {
            Preconditions.IsNotNull(origin, "Inertial origin property must not be null");
            Preconditions.IsNotNull(mass, "Inertial mass property must not be null");
            Preconditions.IsNotNull(inertia, "Inertial inertia property must not be null");

            this.Origin = origin;
            this.Mass = mass;
            this.Inertia = inertia;
        }

        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            XmlStringBuilder sb = new XmlStringBuilder(UrdfSchema.INERTIAL_ELEMENT_NAME)
                .AddSubElement(this.Mass.ToString()).AddSubElement(this.Inertia.ToString());

            if (!this.Origin.Equals(Origin.DEFAULT_ORIGIN))
            {
                sb.AddSubElement(this.Origin.ToString());
            }

            return sb.ToString();
        }

        protected bool Equals(Inertial other)
        {
            return Origin.Equals(other.Origin) && Mass.Equals(other.Mass) && Inertia.Equals(other.Inertia);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Inertial)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Origin.GetHashCode();
                hashCode = (hashCode * 397) ^ Mass.GetHashCode();
                hashCode = (hashCode * 397) ^ Inertia.GetHashCode();
                return hashCode;
            }
        }
    }
}
