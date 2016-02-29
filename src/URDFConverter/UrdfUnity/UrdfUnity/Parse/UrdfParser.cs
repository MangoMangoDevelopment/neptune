using System;
using UrdfUnity.Urdf.Models;

namespace UrdfUnity.Parse
{
    /// <summary>
    /// Parses a URDF XML file into Robot model object containing all of the data parsed from the URDF file.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML"/>
    /// <seealso cref="Urdf.Models.Robot"/>
    public class UrdfParser : Parser<Robot, string>
    {
        /// <summary>
        /// Parses a Robot model object from the specified URDF file.
        /// </summary>
        /// <param name="urdfFilePath">The filepath of the URDF file being parsed</param>
        /// <returns>The Robot object that was parsed from the URDF file</returns>
        public Robot Parse(string urdfFilePath)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
