using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Util;

namespace UrdfUnityTest.Util
{
    public enum Testing
    {
        UPPERCASE,
        camelCase,
        PascalCase,
        lowercase
    }


    [TestClass]
    public class EnumUtilsTest
    {

        [TestMethod]
        public void EnumParsingExact()
        {
            Testing type;
            type = EnumUtils.ToEnum<Testing>("UPPERCASE");
            Assert.AreEqual(Testing.UPPERCASE, type);

            type = EnumUtils.ToEnum<Testing>("camelCase");
            Assert.AreEqual(Testing.camelCase, type);

            type = EnumUtils.ToEnum<Testing>("PascalCase");
            Assert.AreEqual(Testing.PascalCase, type);

            type = EnumUtils.ToEnum<Testing>("lowercase");
            Assert.AreEqual(Testing.lowercase, type);
        }


        [TestMethod]
        public void EnumParsingLowerCase()
        {
            Testing type;
            type = EnumUtils.ToEnum<Testing>("uppercase");
            Assert.AreEqual(Testing.UPPERCASE, type);

            type = EnumUtils.ToEnum<Testing>("camelcase");
            Assert.AreEqual(Testing.camelCase, type);

            type = EnumUtils.ToEnum<Testing>("pascalcase");
            Assert.AreEqual(Testing.PascalCase, type);

            type = EnumUtils.ToEnum<Testing>("lowercase");
            Assert.AreEqual(Testing.lowercase, type);
        }


        [TestMethod]
        public void EnumParsingUpperCase()
        {
            Testing type;
            type = EnumUtils.ToEnum<Testing>("UPPERCASE");
            Assert.AreEqual(Testing.UPPERCASE, type);

            type = EnumUtils.ToEnum<Testing>("CAMELCASE");
            Assert.AreEqual(Testing.camelCase, type);

            type = EnumUtils.ToEnum<Testing>("PASCALCASE");
            Assert.AreEqual(Testing.PascalCase, type);

            type = EnumUtils.ToEnum<Testing>("LOWERCASE");
            Assert.AreEqual(Testing.lowercase, type);
        }


        [TestMethod]
        public void EnumParsingMixCase()
        {
            Testing type;
            type = EnumUtils.ToEnum<Testing>("UpperCase");
            Assert.AreEqual(Testing.UPPERCASE, type);

            type = EnumUtils.ToEnum<Testing>("CamelCase");
            Assert.AreEqual(Testing.camelCase, type);

            type = EnumUtils.ToEnum<Testing>("PascalCase");
            Assert.AreEqual(Testing.PascalCase, type);

            type = EnumUtils.ToEnum<Testing>("LowerCase");
            Assert.AreEqual(Testing.lowercase, type);
        }
    }
}
