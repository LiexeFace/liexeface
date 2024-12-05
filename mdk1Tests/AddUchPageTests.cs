using Microsoft.VisualStudio.TestTools.UnitTesting;
using mdk1.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdk1.AppData;

namespace mdk1.Pages.Tests
{
    [TestClass()]
    public class AddUchPageTests
    {
        [TestMethod()]
        public void CheckSprTest()
        {
            UchTable c = new UchTable { Num_z = 2, Name_Product = "Хлеб" };
            bool expected = true;
            bool actual = AddUchPage.CheckSpr(c);
            Assert.AreEqual(expected, actual);
        }
    }
}