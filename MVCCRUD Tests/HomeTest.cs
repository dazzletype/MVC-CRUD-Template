using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCCRUD.Controllers;
using NUnit.Framework;

namespace MVCCRUDTests
{
    [TestFixture]
    public class HomeTest
    {
        [TestFixtureSetUp]
        public void TestSetup()
        {
         
        }

        [Test]
        public void TestHomeIndex()
        {
            var pageNumber = 1;

            HomeController hc = new HomeController();
            System.Web.Mvc.ActionResult result = hc.Index(pageNumber);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), result);
        }
    }
}
