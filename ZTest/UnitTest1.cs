using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string rez = Calculator.CalculatorHelper.GetInstance().Evaluate("2  + 2 / 2");

            Assert.IsTrue(rez.Equals("3"));
        }
    }
}
