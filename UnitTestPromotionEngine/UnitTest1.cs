using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine;
using System.Collections.Generic;

namespace UnitTestPromotionEngine
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Promotion p = new Promotion();
            int i = 3;
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict = p.SKUTable();
            bool b = Assert.Equals(dict.Count, i);
                
        }
    }
}
