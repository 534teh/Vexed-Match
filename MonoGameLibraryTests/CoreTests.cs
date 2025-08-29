using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Moq;

namespace MonoGameLibraryTests;

[TestClass()]
public class CoreTests
{
    [TestMethod()]
    public void Constructor_SetsInstanceAndProperties()
    {
        var core = new Core("Test", 800, 600, false);

        Assert.AreEqual(core, Core.Instance);
        Assert.AreEqual(800, Core.Graphics.PreferredBackBufferWidth);
        Assert.AreEqual(600, Core.Graphics.PreferredBackBufferHeight);
        Assert.IsFalse(Core.Graphics.IsFullScreen);
        Assert.AreEqual("Test", core.Window.Title);
        Assert.AreEqual("Content", Core.Content.RootDirectory);
        Assert.IsTrue(core.IsMouseVisible);
    }

    [TestMethod()]
    public void Constructor_ThrowsIfMultipleInstances()
    {
        var _ = new Core("Test", 800, 600, false);
        
        Assert.ThrowsException<InvalidOperationException>(() => new Core("Test2", 1024, 768, true));
    }

    [TestMethod()]
    public void Initialize_SetsGraphicsDeviceAndSpriteBatchAndInput()
    {
        var _ = new TestableCore("Test", 800, 600, false);
        TestableCore.Initialize();

        Assert.IsNotNull(Core.GraphicsDevice);
        Assert.IsNotNull(Core.SpriteBatch);
        Assert.IsNotNull(Core.Input);
    }

    [TestMethod()]
    public void ChangeScene_SetsNextSceneIfDifferent()
    {
        var _ = new Core("Test", 800, 600, false);
        var scene1 = new Mock<Scene>().Object;
        var scene2 = new Mock<Scene>().Object;

        Core.s_activeScene = scene1;
        Core.ChangeScene(scene2);

        Assert.AreEqual(scene2, Core.s_nextScene);
    }

    [TestMethod()]
    public void ChangeScene_DoesNotSetNextSceneIfSame()
    {
        var _ = new Core("Test", 800, 600, false);
        var scene1 = new Mock<Scene>().Object;

        Core.s_activeScene = scene1;
        Core.ChangeScene(scene1);

        Assert.IsNull(Core.s_nextScene);
    }

    [TestMethod()]
    public void Update_TransitionsSceneIfNextSceneSet()
    {
        var core = new TestableCore("Test", 800, 600, false);
        TestableCore.Initialize();

        var sceneMock = new Mock<Scene>();
        sceneMock.Setup(s => s.Initialize());
        sceneMock.Setup(s => s.Dispose());

        Core.s_nextScene = sceneMock.Object;

        core.Update(new GameTime());

        Assert.AreEqual(sceneMock.Object, Core.s_activeScene);
        sceneMock.Verify(s => s.Initialize(), Times.Once);
        sceneMock.Verify(s => s.Dispose(), Times.Never);
    }

    [TestMethod()]
    public void Update_UpdatesActiveScene()
    {
        var core = new TestableCore("Test", 800, 600, false);
        TestableCore.Initialize();

        var sceneMock = new Mock<Scene>();
        sceneMock.Setup(s => s.Update(It.IsAny<GameTime>()));

        Core.s_activeScene = sceneMock.Object;

        core.Update(new GameTime());

        sceneMock.Verify(s => s.Update(It.IsAny<GameTime>()), Times.Once);
    }

    [TestMethod()]
    public void Draw_DrawsActiveScene()
    {
        var core = new TestableCore("Test", 800, 600, false);
        TestableCore.Initialize();

        var sceneMock = new Mock<Scene>();
        sceneMock.Setup(s => s.Draw(It.IsAny<GameTime>()));

        Core.s_activeScene = sceneMock.Object;

        core.Draw(new GameTime());

        sceneMock.Verify(s => s.Draw(It.IsAny<GameTime>()), Times.Once);
    }

    [TestCleanup]
    public void CleanupCoreSingleton()
    {
        typeof(Core)
            .GetField("s_instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)?
            .SetValue(null, null);
    }
}