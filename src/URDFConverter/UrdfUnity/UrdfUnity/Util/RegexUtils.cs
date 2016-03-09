using System;
using System.Text;
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
        public static readonly string REAL_NUMBER_PATTERN = @"-?\d*\.?\d+";

        /// <summary>
        /// The Regex pattern for matching whole numbers (such as <c>int</c>s and <c>short</c>s) from a string.
        /// </summary>
        public static readonly string WHOLE_NUMBER_PATTERN = @"-?\d+.{0}";
        
        /// <summary>
        /// A Regex object used for matching real numbers as per <c>REAL_NUMBER_PATTERN</c>.
        /// </summary>
        private static readonly Regex REAL_NUMBER_REGEX = new Regex(REAL_NUMBER_PATTERN);

        /// <summary>
        /// A Regex object used for matching real numbers as per <c>REAL_NUMBER_PATTERN</c>.
        /// </summary>
        private static readonly Regex WHOLE_NUMBER_REGEX = new Regex(WHOLE_NUMBER_PATTERN);

        private const string DEFAULT_DELIMITER = " ";


        /// <summary>
        /// Checks if the provided string matches the specified number of delimited real number values.
        /// </summary>
        /// <param name="input">The string being checked for a match of delimited real number values</param>
        /// <param name="n">The number of real numbers being matched for</param>
        /// <param name="delimiter">The delimiter between real numbers. Default value is <c>" "</c></param>
        /// <returns><c>true</c> if the input string is matched to <c>n</c> real numbers, otherwise <c>false</c></returns>
        public static Boolean IsMatchNDoubles(string input, int n, string delimiter = DEFAULT_DELIMITER)
        {
            StringBuilder pattern = new StringBuilder("^");

            for (int i = 0; i < n; i++)
            {
                if (i > 0)
                {
                    pattern.AppendFormat("\\{0}+", delimiter);
                }
                pattern.Append(REAL_NUMBER_PATTERN);
            }
            pattern.Append("$");

            Regex regex = new Regex(pattern.ToString());

            return regex.IsMatch(input);
        }


        /// <summary>
        /// Converts the first instance of a real number value in a string into a <c>double</c>.
        /// </summary>
        /// <param name="input">The string being matched as a double value. MUST BE A VALID MATCH</param>
        /// <returns>The <c>double</c> value found in the input string</returns>
        public static double MatchDouble(string input)
        {
            Preconditions.IsTrue(REAL_NUMBER_REGEX.IsMatch(input));

            Match match = REAL_NUMBER_REGEX.Match(input);
            return Double.Parse(match.Value);
        }

        /// <summary>
        /// Converts the first instance of a real number value in a string into a <c>double</c>.
        /// </summary>
        /// <param name="input">The string being matched as a double value. MUST BE A VALID MATCH</param>
        /// <param name="defaultValue">A default value to return if the input string doesn't contain a number</param>
        /// <returns>The <c>double</c> value if found in the input string, otherwise the specified default value</returns>
        public static double MatchDouble(string input, double defaultValue)
        {
            if(!REAL_NUMBER_REGEX.IsMatch(input))
            {
                return defaultValue;
            }
            
            return MatchDouble(input);
        }

        /// <summary>
        /// Converts all instances of a real number value in a string into a <c>double</c> array.
        /// </summary>
        /// <param name="input">The string being matched as a double values</param>
        /// <returns>An array of <c>double</c> values if found in the input string, otherwise an empty array</returns>
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

        /// <summary>
        /// Checks if the provided string matches the specified number of delimited whole number values.
        /// </summary>
        /// <param name="input">The string being checked for a match of delimited whole number values</param>
        /// <param name="n">The number of whole numbers being matched for</param>
        /// <param name="delimiter">The delimiter between real numbers. Default value is <c>" "</c></param>
        /// <returns><c>true</c> if the input string is matched to <c>n</c> whole numbers, otherwise <c>false</c></returns>
        public static Boolean IsMatchNInts(string input, int n, string delimiter = DEFAULT_DELIMITER)
        {
            StringBuilder pattern = new StringBuilder("^");

            for (int i = 0; i < n; i++)
            {
                if (i > 0)
                {
                    pattern.AppendFormat("\\{0}+", delimiter);
                }
                pattern.Append(WHOLE_NUMBER_PATTERN);
            }
            pattern.Append("$");

            Regex regex = new Regex(pattern.ToString());

            return regex.IsMatch(input);
        }


        /// <summary>
        /// Converts the first instance of a whole number value in a string into an <c>int</c>.
        /// </summary>
        /// <param name="input">The string being matched as a int value. MUST BE A VALID MATCH</param>
        /// <returns>The <c>int</c> value found in the input string</returns>
        public static int MatchInt(string input)
        {
            Preconditions.IsTrue(WHOLE_NUMBER_REGEX.IsMatch(input));

            Match match = WHOLE_NUMBER_REGEX.Match(input);
            return Int32.Parse(match.Value);
        }

        /// <summary>
        /// Converts the first instance of a whole number value in a string into a <c>double</c>.
        /// </summary>
        /// <param name="input">The string being matched as a whole value. MUST BE A VALID MATCH</param>
        /// <param name="defaultValue">A default value to return if the input string doesn't contain a number</param>
        /// <returns>The <c>int</c> value if found in the input string, otherwise the specified default value</returns>
        public static int MatchInt(string input, int defaultValue)
        {
            if (!WHOLE_NUMBER_REGEX.IsMatch(input))
            {
                return defaultValue;
            }

            return MatchInt(input);
        }

        /// <summary>
        /// Converts all instances of a whole number value in a string into an <c>int</c> array.
        /// </summary>
        /// <param name="input">The string being matched as a int values</param>
        /// <returns>An array of <c>int</c> values if found in the input string, otherwise an empty array</returns>
        public static int[] MatchInts(string input)
        {
            MatchCollection matches = WHOLE_NUMBER_REGEX.Matches(input);
            int[] ints = new int[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                ints[i] = Int32.Parse(matches[i].Value);
            }

            return ints;
        }
    }
}
