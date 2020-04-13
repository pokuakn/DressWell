using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DressWell.Presentation.Interfaces;
using Ninject;

namespace DressWell.BusinessServices.Tests
{
    [TestClass]
    public class BusinessServicesTest
    {
        //Initialize Bootstrapper for dependency injection
        IKernel container = Bootstrapper.Init();

        [TestMethod]
        public void TakePajamasOffFirst_Test()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            string commands = "HOT 7, 1";
            context.AcceptUserInput(commands);
            var expected = "fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PutOnSocksJacketWhenHot_Test()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            string commands = "HOT 5, 3";
            context.AcceptUserInput(commands);
            var expected = "fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SocksWhenHot()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            String commands = "HOT 8, 3";
            context.AcceptUserInput(commands);
            var expected = "Removing PJs, fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void JacketWhenHot()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            String commands = "HOT 8, 5";
            context.AcceptUserInput(commands);
            var expected = "Removing PJs, fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SocksAfterShoes()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            String commands = "COLD 8, 6, 1, 2, 3";
            context.AcceptUserInput(commands);
            var expected = "Removing PJs, pants, fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PantsAfterShoes()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            String commands = "COLD 8, 1, 6, 2, 3";
            context.AcceptUserInput(commands);
            var expected = "Removing PJs, fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShirtAfterJacket()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            String commands = "COLD 8, 3, 5, 4";
            context.AcceptUserInput(commands);
            var expected = "Removing PJs, socks, fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShirtAfterHeadwear()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            String commands = "HOT 8, 6, 2, 4";
            context.AcceptUserInput(commands);
            var expected = "Removing PJs, shorts, fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotEnoughClothes()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            String commands = "COLD 8, 6, 3, 4, 2, 5, 7";
            context.AcceptUserInput(commands);
            var expected = "Removing PJs, pants, socks, shirt, hat, jacket, fail";

;            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NotValidCommandCOLD()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            String commands = "COLD 8, 16, 3, 4, 2, 5, 7";
            context.AcceptUserInput(commands);
            var expected = "Removing PJs, fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NotValidCommandHOT()
        {
            //-- Arrange
            IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();
            String commands = "HOT 8, -1, 3, 4, 2, 5, 7";
            context.AcceptUserInput(commands);
            var expected = "Removing PJs, fail";

            //-- Act
            var actual = context.GetDressingOrder();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
