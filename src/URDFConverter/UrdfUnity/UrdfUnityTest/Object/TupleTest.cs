using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Object;

namespace UrdfUnityTest.Object
{
    [TestClass]
    public class TupleTest
    {
        [TestMethod]
        public void ConstructTupleTwoItems()
        {
            int t1 = 1;
            int t2 = 2;
            Tuple<int, int> tuple = new Tuple<int, int>(t1, t2);

            Assert.AreEqual(t1, tuple.Item1);
            Assert.AreEqual(t2, tuple.Item2);
        }

        [TestMethod]
        public void ConstructTupleThreeItems()
        {
            int t1 = 1;
            int t2 = 2;
            int t3 = 3;
            Tuple<int, int, int> tuple = new Tuple<int, int, int>(t1, t2, t3);

            Assert.AreEqual(t1, tuple.Item1);
            Assert.AreEqual(t2, tuple.Item2);
            Assert.AreEqual(t3, tuple.Item3);
        }
    }
}
