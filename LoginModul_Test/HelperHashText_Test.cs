using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LoginModul_Test
{
    [TestClass]
    public class HelperHashText_Test
    {
        [TestMethod]
        public void U_Helper_HashText_NullText_Output()
        {
            long l = LoginModul.Helper.HashText(null);
            Assert.IsTrue(l > 0);
        }
        [TestMethod]
        public void U_Helper_HashText_EmptyText_Output()
        {
            long l = LoginModul.Helper.HashText("");
            Assert.IsTrue(l > 0);
        }
        [TestMethod]
        public void U_Helper_HashText_DifferentText_Output()
        {
            long l1 = LoginModul.Helper.HashText("123456");
            long l2 = LoginModul.Helper.HashText("123457");
            Assert.IsTrue(l1 != l2);

            long l3 = LoginModul.Helper.HashText("a");
            long l4 = LoginModul.Helper.HashText("A");
            Assert.IsTrue(l3 != l4);

            long l5 = LoginModul.Helper.HashText("cereo");
            long l6 = LoginModul.Helper.HashText("ceReo");
            Assert.IsTrue(l5 != l6);
        }
        [TestMethod]
        public void U_Helper_HashText_SameText_Output()
        {
            long l1 = LoginModul.Helper.HashText("123456");
            long l3 = LoginModul.Helper.HashText("a");
            long l5 = LoginModul.Helper.HashText("ceReo");

            System.Threading.Thread.Sleep(123);

            long l2 = LoginModul.Helper.HashText("123456");
            long l4 = LoginModul.Helper.HashText("a");
            long l6 = LoginModul.Helper.HashText("ceReo");

            Assert.IsTrue(l1 == l2);
            Assert.IsTrue(l3 == l4);
            Assert.IsTrue(l5 == l6);
        }
        [TestMethod]
        public void U_Helper_HashText_ManyDifferentText_Output()
        {
            /* Converting text into a number does not guarantee that unique text has a unique number. 
               Therefore it is dangerous to use random generated texts as there could occur two 
               different texts that translate into the same number.
               Keeping the amount of texts low, minimizes the chances. */
            List<long> ll = new List<long>();
            for (int i = 0; i < 1000; i++) // 
            {
                ll.Add(LoginModul.Helper.HashText(System.Guid.NewGuid().ToString()));
            }
            CollectionAssert.AllItemsAreUnique(ll);
        }
    }
}
