using System;

namespace UrdfUnity.Util
{
    /// <summary>
    /// Utility class for working with enums
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// Parses the enum type value represented by the provided string name
        /// </summary>
        /// <typeparam name="T">Type of enum being parsed</typeparam>
        /// <param name="value">String representation of the enum type value</param>
        /// <returns>Enum value of type T based on the string value</returns>
        public static T ToEnum<T> (this string name)
        {
            return (T)Enum.Parse(typeof(T), name, true);
        }
    }
}
