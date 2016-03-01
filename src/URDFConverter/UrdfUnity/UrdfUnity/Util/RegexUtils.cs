using System;
using System.Text.RegularExpressions;

namespace UrdfUnity.Util
{
    /// <summary>
    /// Utility class for using Regex patterns for parsing values from strings.
    /// </summary>
    static public class RegexUtils
    {
        /// <summary>
        /// The Regex pattern for matching real numbers (such as <c>float</c>s and <c>double</c>s) from a string.
        /// </summary>
        public static readonly string REAL_NUMBER_PATTERN = @"-?\d+\.?\d*";

        /// <summary>
        /// A Regex object used for matching real numbers as per <c>REAL_NUMBER_PATTERN</c>.
        /// </summary>
        private static readonly Regex REAL_NUMBER_REGEX = new Regex(REAL_NUMBER_PATTERN);


        /// <summary>
        /// Converts the first instance of a real number value in a string into a <c>double</c>.
        /// </summary>
        /// <param name="input">The string being matched as a double value. MUST BE A VALID MATCH</param>
        /// <returns>The double value found in the input string</returns>
        public static double MatchDouble(string input)
        {
            Preconditions.IsTrue(REAL_NUMBER_REGEX.IsMatch(input));

            Match match = REAL_NUMBER_REGEX.Match(input);
            return Double.Parse(match.Value);
        }

        /// <summary>
        /// Converts all instances of a real number value in a string into a <c>double</c> array.
        /// </summary>
        /// <param name="input">The string being matched as a double values</param>
        /// <returns>An array of double values found in the input string</returns>
        public static double[] MatchDoubles(string input)
        {
            MatchCollection matches = REAL_NUMBER_REGEX.Matches(input);
            double[] doubles = new double[matches.Count];

            for(int i = 0; i < matches.Count; i++)
            {
                doubles[i] = Double.Parse(matches[i].Value);
            }

            return doubles;
        }
    }
}
