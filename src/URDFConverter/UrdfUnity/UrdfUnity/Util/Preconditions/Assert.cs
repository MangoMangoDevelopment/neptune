using System;

namespace UrdfUnity.Util.Preconditions
{
    /// <summary>
    /// Utility class for parameter validation.
    /// </summary>
    public static class Assert
    {
        /// <summary>
        /// Asserts that the object is not null, throwing an <c>ArgumentNullException</c> with the provided message otherwise.
        /// </summary>
        /// <param name="obj">The object being validated as not null</param>
        /// <param name="paramName">The name of the parameter being validated</param>
        public static void IsNotNull(Object obj, String paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
