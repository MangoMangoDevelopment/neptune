using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.Links.Inertials
{
    /// <summary>
    /// Represents the moment of inertia of a link as the 3x3 rotational inertia matrix. 
    /// Because the rotational inertia matrix is symmetric, only the 6 above-diagonal elements 
    /// of this matrix are specified, using the attributes ixx, ixy, ixz, iyy, iyz, izz. 
    /// </summary>
    /// <remarks>
    /// The inertia parameters are specified with respect to the center of the mass in local link coordinate system.
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/inertial"/>
    public sealed class Inertia
    {
        /// <summary>
        /// The IXX component of the inertia matrix of the link.
        /// </summary>
        /// <value>Required.</value>
        public double Ixx { get; set; }

        /// <summary>
        /// The IXY component of the inertia matrix of the link.
        /// </summary>
        /// <value>Required.</value>
        public double Ixy { get; set; }

        /// <summary>
        /// The IXZ component of the inertia matrix of the link.
        /// </summary>
        /// <value>Required.</value>
        public double Ixz { get; set; }

        /// <summary>
        /// The IYY component of the inertia matrix of the link.
        /// </summary>
        /// <value>Required.</value>
        public double Iyy { get; set; }

        /// <summary>
        /// The IYZ component of the inertia matrix of the link.
        /// </summary>
        /// <value>Required.</value>
        public double Iyz { get; set; }

        /// <summary>
        /// The IZZ component of the inertia matrix of the link.
        /// </summary>
        /// <value>Required.</value>
        public double Izz { get; set; }


        /// <summary>
        /// Creates a new instance of Inertia with the matrix components specified.
        /// </summary>
        /// <param name="ixx">The IXX component of the inertia matrix of the link.</param>
        /// <param name="ixy">The IXY component of the inertia matrix of the link.</param>
        /// <param name="ixz">The IXZ component of the inertia matrix of the link.</param>
        /// <param name="iyy">The IYY component of the inertia matrix of the link.</param>
        /// <param name="iyz">The IYZ component of the inertia matrix of the link.</param>
        /// <param name="izz">The IZZ component of the inertia matrix of the link.</param>
        public Inertia(double ixx, double ixy, double ixz, double iyy, double iyz, double izz)
        {
            this.Ixx = ixx;
            this.Ixy = ixy;
            this.Ixz = ixz;
            this.Iyy = iyy;
            this.Iyz = iyz;
            this.Izz = izz;
        }

        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            return new XmlStringBuilder(UrdfSchema.INERTIA_ELEMENT_NAME)
                .AddAttribute(UrdfSchema.IXX_ATTRIBUTE_NAME, this.Ixx)
                .AddAttribute(UrdfSchema.IXY_ATTRIBUTE_NAME, this.Ixy)
                .AddAttribute(UrdfSchema.IXZ_ATTRIBUTE_NAME, this.Ixz)
                .AddAttribute(UrdfSchema.IYY_ATTRIBUTE_NAME, this.Iyy)
                .AddAttribute(UrdfSchema.IYZ_ATTRIBUTE_NAME, this.Iyz)
                .AddAttribute(UrdfSchema.IZZ_ATTRIBUTE_NAME, this.Izz)
                .ToString();
        }

        protected bool Equals(Inertia other)
        {
            return Ixx.Equals(other.Ixx) && Ixy.Equals(other.Ixy) && Ixz.Equals(other.Ixz)
                && Iyy.Equals(other.Iyy) && Iyz.Equals(other.Iyz) && Izz.Equals(other.Izz);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Inertia)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Ixx.GetHashCode();
                hashCode = (hashCode * 397) ^ Ixy.GetHashCode();
                hashCode = (hashCode * 397) ^ Ixz.GetHashCode();
                hashCode = (hashCode * 397) ^ Iyy.GetHashCode();
                hashCode = (hashCode * 397) ^ Iyz.GetHashCode();
                hashCode = (hashCode * 397) ^ Izz.GetHashCode();
                return hashCode;
            }
        }
    }
}
