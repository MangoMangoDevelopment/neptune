using System;
using NLog;

namespace UrdfUnity.Util
{
    /// <summary>
    /// Utility class for parameter validation.
    /// </summary>
    public static class Preconditions
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
        

        /// <summary>
        /// Asserts that the object is not null, logging an error message and throwing an 
        /// <c>ArgumentNullException</c> with the provided parameter name otherwise.
        /// </summary>
        /// <param name="obj">The object being validated as not null</param>
        /// <param name="msg">The message that is logged before throwing the exception</param>
        /// <param name="paramName">The name of the parameter being validated. Default value is <c>null</c></param>
        /// <exception cref="System.ArgumentNullException">Thrown when the object is null</exception>
        public static void IsNotNull(object obj, string msg, string paramName = null)
        {
            if (obj == null)
            {
                LOGGER.Error(msg);
                throw (paramName != null) ? new ArgumentNullException(paramName) : new ArgumentNullException();
            }
        }

        /// <summary>
        /// Asserts that the string is not empty, logging an error message and throwing an 
        /// <c>ArgumentException</c> with the provided parameter name otherwise.
        /// </summary>
        /// <param name="str">The string being validated as not empty</param>
        /// <param name="msg">The message that is logged before throwing the exception</param>
        /// <param name="paramName">The name of the parameter being validated. Default value is <c>null</c></param>
        /// <exception cref="System.ArgumentException">Thrown when the string is empty</exception>
        public static void IsNotEmpty(string str, string msg, string paramName = null)
        {
            if (String.IsNullOrEmpty(str) || String.IsNullOrWhiteSpace(str))
            {
                LOGGER.Error(msg);
                throw (paramName != null) ? new ArgumentException(paramName) : new ArgumentException();
            }
        }

        /// <summary>
        /// Asserts that the value is within the range of the specified lower and upper boundaries inclusive,
        /// logging an error message and throwing an <c>ArgumentException</c> otherwise.
        /// </summary>
        /// <param name="value">The value being validated as in the range [lowerBound, upperBound]</param>
        /// <param name="lowerBound">The lower boundary of the range being checked</param>
        /// <param name="upperBound">The upper boundary of the range being checked</param>
        /// <param name="msg">The message that is logged before throwing the exception</param>
        /// <param name="paramName">The name of the parameter being validated. Default value is <c>null</c></param>
        /// <exception cref="System.Exception">Thrown when the lowerBound parameter is greater than the upperBound parameter</exception>"
        /// <exception cref="System.ArgumentException">Thrown when the value is outside the specified range</exception>
        public static void IsWithinRange(double value, double lowerBound, double upperBound, string msg, string paramName = null)
        {
            if (lowerBound > upperBound)
            {
                throw new Exception("Range validation cannot have lower bound that is greater than upper bound");
            }
            if (value < lowerBound || value > upperBound)
            {
                LOGGER.Error(msg);
                throw (paramName != null) ? new ArgumentException(paramName) : new ArgumentException();
            }
        }

        /// <summary>
        /// Asserts that the value is <c>true</c>, logging an error message and throwing an 
        /// <c>ArgumentException</c> with the provided parameter name otherwise.
        /// </summary>
        /// <param name="value">The value being validated as true</param>
        /// <param name="msg">The message that is logged before throwing the exception</param>
        /// <param name="paramName">The name of the parameter being validated. Default value is <c>null</c></param>
        /// <exception cref="System.ArgumentException">Thrown when the value is false</exception>
        public static void IsTrue(bool value, string msg, string paramName = null)
        {
            if (!value)
            {
                LOGGER.Error(msg);
                throw (paramName != null) ? new ArgumentException(paramName) : new ArgumentException();
            }
        }
    }
}
