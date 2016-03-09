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
        public void IsMatchNDoubles()
        {
            Assert.IsTrue(RegexUtils.IsMatchNDoubles("0 0 0", 3));
            Assert.IsTrue(RegexUtils.IsMatchNDoubles("1 2 3", 3));
            Assert.IsTrue(RegexUtils.IsMatchNDoubles("-1 -2 -3", 3));
            Assert.IsTrue(RegexUtils.IsMatchNDoubles("1.1 .1 0.1", 3));
            Assert.IsTrue(RegexUtils.IsMatchNDoubles("1.1 .1 0.1 0", 4));

            Assert.IsFalse(RegexUtils.IsMatchNDoubles("1.1 .1 0.1", 1));
            Assert.IsFalse(RegexUtils.IsMatchNDoubles("", 3));
            Assert.IsFalse(RegexUtils.IsMatchNDoubles("a b c", 3));
            Assert.IsFalse(RegexUtils.IsMatchNDoubles("111", 3));
            Assert.IsFalse(RegexUtils.IsMatchNDoubles("111 222", 3));
            Assert.IsFalse(RegexUtils.IsMatchNDoubles("1.1 .1 0.1", 4));

            Assert.IsTrue(RegexUtils.IsMatchNDoubles("1 2 3", 3, " "));
            Assert.IsTrue(RegexUtils.IsMatchNDoubles("1-2-3", 3, "-"));
            Assert.IsTrue(RegexUtils.IsMatchNDoubles("1 then 2 then 3", 3, " then "));
            Assert.IsFalse(RegexUtils.IsMatchNDoubles("1 then 2 then 3", 3, " and "));
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

        [TestMethod]
        public void IsMatchNInts()
        {
            Assert.IsTrue(RegexUtils.IsMatchNInts("0 0 0", 3));
            Assert.IsTrue(RegexUtils.IsMatchNInts("1 2 3", 3));
            Assert.IsTrue(RegexUtils.IsMatchNInts("-1 -2 -3", 3));
            Assert.IsTrue(RegexUtils.IsMatchNInts("-1 0 1 0", 4));

            Assert.IsFalse(RegexUtils.IsMatchNInts("1 2 3", 1));
            Assert.IsFalse(RegexUtils.IsMatchNInts("1.1 .1 0.1", 3));
            Assert.IsFalse(RegexUtils.IsMatchNInts("", 3));
            Assert.IsFalse(RegexUtils.IsMatchNInts("a b c", 3));
            Assert.IsFalse(RegexUtils.IsMatchNInts("111", 3));
            Assert.IsFalse(RegexUtils.IsMatchNInts("111 222", 3));
            Assert.IsFalse(RegexUtils.IsMatchNInts("1 1 1", 4));

            Assert.IsTrue(RegexUtils.IsMatchNInts("1 2 3", 3, " "));
            Assert.IsTrue(RegexUtils.IsMatchNInts("1-2-3", 3, "-"));
            Assert.IsTrue(RegexUtils.IsMatchNInts("1 then 2 then 3", 3, " then "));
            Assert.IsFalse(RegexUtils.IsMatchNInts("1 then 2 then 3", 3, " and "));
        }

        [TestMethod]
        public void MatchInt()
        {
            Assert.AreEqual(3141592, RegexUtils.MatchInt("3141592"));
            Assert.AreEqual(0, RegexUtils.MatchInt("0"));
            Assert.AreEqual(2, RegexUtils.MatchInt("2"));
            Assert.AreEqual(-12345, RegexUtils.MatchInt("-12345"));
            Assert.AreEqual(-2, RegexUtils.MatchInt("-2.2"));
        }

        [TestMethod]
        public void MatchIntDefaultValue()
        {
            int defaultValue = 0;
            Assert.AreEqual(defaultValue, RegexUtils.MatchInt("", defaultValue));
            Assert.AreEqual(defaultValue, RegexUtils.MatchInt("no numbers", defaultValue));
            Assert.AreNotEqual(defaultValue, RegexUtils.MatchInt("2"), defaultValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatchIntNoMatch()
        {
            RegexUtils.MatchInt("not a number");
        }

        [TestMethod]
        public void MatchInts()
        {
            double three = 3;
            double six = 6;
            string numbersAsString = three.ToString() + " " + six.ToString();
            int[] results = RegexUtils.MatchInts(numbersAsString);

            Assert.IsTrue(results.Length == 2);
            Assert.AreEqual(three, results[0]);
            Assert.AreEqual(six, results[1]);
        }

        [TestMethod]
        public void MatchIntsNoMatch()
        {
            int[] result = RegexUtils.MatchInts("no numbers");

            Assert.IsTrue(result.Length == 0);
        }
    }
}
