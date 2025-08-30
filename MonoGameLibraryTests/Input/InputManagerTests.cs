using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameLibraryTests.Input
{
    [TestClass()]
    public class InputManagerTests
    {
        [TestMethod]
        [TestCategory("Unit")]
        public void InputManagerTest()
        {
            var inputManager = new InputManager();

            Assert.IsNotNull(inputManager.Keyboard);
            Assert.IsNotNull(inputManager.Mouse);
            Assert.IsNotNull(inputManager.GamePads);
            Assert.AreEqual(4, inputManager.GamePads.Length);
            foreach (var gamePad in inputManager.GamePads)
            {
                Assert.IsNotNull(gamePad);
            }
        }
    }
}