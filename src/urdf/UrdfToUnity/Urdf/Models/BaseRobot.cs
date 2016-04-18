using UrdfToUnity.Urdf.Models.Attributes;

namespace UrdfToUnity.Urdf.Models
{
    /// <summary>
    /// Defines the interface for a base robot model object that allows additional components to be
    /// added after the model is created.  The implementation will create a Link and Joint object modeling
    /// the component being added.
    /// </summary>
    interface BaseRobot
    {
        /// <summary>
        /// Adds a new component represented as a mesh or primitive box to the robot model.
        /// </summary>
        /// <param name="component">The component object being added. MUST NOT BE NULL</param>
        /// <param name="parent">The name of the parent link that this component is linked to. MUST NOT BE NULL OR EMPTY</param>
        /// <param name="xyz">The XYZ offset of this component from its parent link. MUST NOT BE NULL</param>
        /// <param name="rpy">The RPY offset of this component from its parent link. MUST NOT BE NULL</param>
        /// <returns>The name of the added Link object if the component was successfully added, otherwise <c>null</c></returns>
        string AddComponent(Component component, string parent, XyzAttribute xyz, RpyAttribute rpy);

        /// <summary>
        /// Adds a new component represented as a Robot model object to the robot model.
        /// </summary>
        /// <param name="component">The Robot model object being added. MUST NOT BE NULL</param>
        /// <param name="parent">The name of the parent link on this Robot that this component is linked to. MUST NOT BE NULL OR EMPTY</param>
        /// <param name="child">The name of the child link on the component Robot that this component is linked by. MUST NOT BE NULL OR EMPTY</param>
        /// <param name="xyz">The XYZ offset of this component from its parent link. MUST NOT BE NULL</param>
        /// <param name="rpy">The RPY offset of this component from its parent link. MUST NOT BE NULL</param>
        /// <returns>The name of the added Link object if the component was successfully added, otherwise <c>null</c></returns>
        string AddComponent(Robot component, string parent, string child, XyzAttribute xyz, RpyAttribute rpy);
    }
}
