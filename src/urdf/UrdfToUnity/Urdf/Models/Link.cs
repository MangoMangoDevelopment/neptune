using System.Collections.Generic;
using System.Linq;
using UrdfToUnity.Urdf.Models.Links;
using UrdfToUnity.Util;

namespace UrdfToUnity.Urdf.Models
{
    /// <summary>
    /// Represents a rigid body with inertia, visual features and collision properties.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    public sealed class Link
    {
        /// <summary>
        /// The default name used when a Link needs to be instantiated without a name.
        /// </summary>
        public static readonly string DEFAULT_NAME = "missing_name";


        /// <summary>
        /// The name of the link.
        /// </summary>
        /// <value>Required.</value>
        public string Name { get; }

        /// <summary>
        /// The inertial properties of the link.
        /// </summary>
        /// <value>Optional. MAY BE NULL.</value>
        public Inertial Inertial { get; }

        /// <summary>
        /// The visual properties of the link. The union of geometry defined by each list item 
        /// forms the visual representation of the link.
        /// </summary>
        /// <value>Optional. MAY BE EMPTY.</value>
        public List<Visual> Visual { get; }

        /// <summary>
        /// The collision properties of the link. The union of geometry defined by each list item 
        /// forms the collision representation of the link.
        /// </summary>
        /// <value>Optional. MAY BE EMPTY.</value>
        public List<Collision> Collision { get; }


        /// <summary>
        /// Creates a new instance of Link with the specified properties. 
        /// A Link.Builder must be used to create a link to enforce required and default properties.
        /// </summary>
        /// <param name="name">The name of the link</param>
        /// <param name="inertial">The inertial properties of the link</param>
        /// <param name="visual">The visual properties of the link. MUST NOT BE NULL</param>
        /// <param name="collision">The collision properties of the link. MUST NOT BE NULL</param>
        private Link(string name, Inertial inertial, List<Visual> visuals, List<Collision> collisions)
        {
            Preconditions.IsNotEmpty(name, "Link name property cannot be set to null or empty");
            Preconditions.IsNotNull(visuals, "Link visual property cannot be set to null");
            Preconditions.IsNotNull(collisions, "Link collision property cannot be set to null");
            this.Name = name;
            this.Inertial = inertial;
            this.Visual = visuals;
            this.Collision = collisions;
        }

        /// <summary>
        /// Helper class to build a new instance of link with its defined properties.
        /// </summary>
        public class Builder
        {
            private string name;
            private Inertial inertial = null;
            private List<Visual> visual = new List<Visual>();
            private List<Collision> collision = new List<Collision>();


            /// <summary>
            /// Creates a new instance of Builder with the required Link properties.
            /// </summary>
            /// <param name="name">The name of the link</param>
            public Builder(string name)
            {
                Preconditions.IsNotEmpty(name, "name");
                this.name = name;
            }

            /// <summary>
            /// Creates a new instance of Link with the specified properties.
            /// </summary>
            /// <returns>A Link object with the properties set</returns>
            public Link Build()
            {
                return new Link(this.name, this.inertial, this.visual, this.collision);
            }

            public Builder SetInertial(Inertial inertial)
            {
                Preconditions.IsNotNull(inertial, "Link inertial property cannot be set to null");
                this.inertial = inertial;
                return this;
            }

            public Builder SetVisual(Visual visual)
            {
                Preconditions.IsNotNull(visual, "Link visual property cannot be set to null");
                this.visual = new List<Visual>();
                this.visual.Add(visual);
                return this;
            }

            public Builder SetVisual(List<Visual> visual)
            {
                Preconditions.IsNotNull(visual, "Link visual property cannot be set to null");
                this.visual = visual;
                return this;
            }

            public Builder SetCollision(Collision collision)
            {
                Preconditions.IsNotNull(collision, "Link collision property cannot be set to null");
                this.collision = new List<Collision>();
                this.collision.Add(collision);
                return this;
            }

            public Builder SetCollision(List<Collision> collision)
            {
                Preconditions.IsNotNull(collision, "Link collision property cannot be set to null");
                this.collision = collision;
                return this;
            }
        }


        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            XmlStringBuilder sb = new XmlStringBuilder(UrdfSchema.LINK_ELEMENT_NAME)
                .AddAttribute(UrdfSchema.NAME_ATTRIBUTE_NAME, this.Name);

            foreach (Visual visual in this.Visual)
            {
                sb.AddSubElement(visual.ToString());
            }

            foreach (Collision collision in this.Collision)
            {
                sb.AddSubElement(collision.ToString());
            }

            if (this.Inertial != null)
            {
                sb.AddSubElement(this.Inertial.ToString());
            }

            return sb.ToString();
        }

        protected bool Equals(Link other)
        {
            return string.Equals(Name, other.Name) && Equals(Inertial, other.Inertial)
                && Visual.SequenceEqual(other.Visual) && Collision.SequenceEqual(other.Collision);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Link)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Inertial != null ? Inertial.GetHashCode() : 0);
                foreach (var visual in Visual)
                {
                    hashCode = (hashCode * 397) ^ visual.GetHashCode();
                }
                foreach (var collision in Collision)
                {
                    hashCode = (hashCode * 397) ^ collision.GetHashCode();
                }
                return hashCode;
            }
        }
    }
}
