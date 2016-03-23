using System;

namespace UrdfUnity.Util
{
    /// <summary>
    /// This is a utility class for working with enums 
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// This provides an assesible method for parsing a string into
        /// desired enum type.
        /// </summary>
        /// <typeparam name="T">desired enum type</typeparam>
        /// <param name="value">string representation of the enum type value</param>
        /// <returns>Enum value of type T based on the string value</returns>
        public static T ToEnum<T> (this string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }
}
