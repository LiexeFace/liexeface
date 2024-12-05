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
    public class AddSprPageTests
    {
        [TestMethod()]
        public void CheckSprTest()
        {        
            SprTable c = new SprTable { Cod_Pred = 1, Name_Pred = "ОООТехноСервис" };
            bool expected = true;
            bool actual = AddSprPage.CheckSpr(c);
            Assert.AreEqual(expected, actual);
        }
    }
}