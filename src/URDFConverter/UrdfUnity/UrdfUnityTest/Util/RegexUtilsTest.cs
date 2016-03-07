using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Util;

namespace UrdfUnityTest.Util
{
    [TestClass]
    public class RegexUtilsTest
    {
        [TestMethod]
        public void RealNumberPattern()
        {
            Assert.AreEqual(@"-?\d*\.?\d+", RegexUtils.REAL_NUMBER_PATTERN);
        }

        [TestMethod]
        public void AttributeRegex()
        {
            Assert.IsTrue(RegexUtils.ATTRIBUTE_REGEX_THREE_REAL_NUMBERS.IsMatch("0 0 0"));
            Assert.IsTrue(RegexUtils.ATTRIBUTE_REGEX_THREE_REAL_NUMBERS.IsMatch("1 2 3"));
            Assert.IsTrue(RegexUtils.ATTRIBUTE_REGEX_THREE_REAL_NUMBERS.IsMatch("1.1 .1 0.1"));
            Assert.IsFalse(RegexUtils.ATTRIBUTE_REGEX_THREE_REAL_NUMBERS.IsMatch("a b c"));
            Assert.IsFalse(RegexUtils.ATTRIBUTE_REGEX_THREE_REAL_NUMBERS.IsMatch("111"));
            Assert.IsFalse(RegexUtils.ATTRIBUTE_REGEX_THREE_REAL_NUMBERS.IsMatch("111 222"));
        }

        [TestMethod]
        public void MatchDouble()
        {
            Assert.AreEqual(3.141592d, RegexUtils.MatchDouble("3.141592"));
            Assert.AreEqual(0d, RegexUtils.MatchDouble("0.000"));
            Assert.AreEqual(2d, RegexUtils.MatchDouble("2"));
            Assert.AreEqual(-12345.6789d, RegexUtils.MatchDouble("-12345.6789"));
            Assert.AreEqual(.25d, RegexUtils.MatchDouble(".25"));
        }

        [TestMethod]
        public void MatchDoubleDefaultValue()
        {
            double defaultValue = 0d;
            Assert.AreEqual(defaultValue, RegexUtils.MatchDouble("", defaultValue));
            Assert.AreEqual(defaultValue, RegexUtils.MatchDouble("no numbers", defaultValue));
            Assert.AreNotEqual(defaultValue, RegexUtils.MatchDouble("2"), defaultValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatchDoubleNoMatch()
        {
            RegexUtils.MatchDouble("not a number");
        }

        [TestMethod]
        public void MatchDoubles()
        {
            double pi = 3.14;
            double tau = 6.28;
            string numbersAsString = pi.ToString() + " " + tau.ToString();
            double[] results = RegexUtils.MatchDoubles(numbersAsString);

            Assert.IsTrue(results.Length == 2);
            Assert.AreEqual(pi, results[0]);
            Assert.AreEqual(tau, results[1]);
        }

        [TestMethod]
        public void MatchDoublesNoMatch()
        {
            double[] result = RegexUtils.MatchDoubles("no numbers");

            Assert.IsTrue(result.Length == 0);
        }
    }
}
