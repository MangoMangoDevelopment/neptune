using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.InertialElements
{
    [TestClass]
    public class MassTest
    {
        [TestMethod]
        public void ConstructMass()
        {
            double value = 1;
            Mass mass = new Mass(value);

            Assert.AreEqual(value, mass.Value);
        }
    }
}
