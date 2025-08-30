using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibraryTests;

[TestClass]
public class CoreTests
{
    private Core? _core;

    [TestInitialize]
    public void TestInitialize()
    {
        _core = new Core("Test", 800, 600, false, new InputManager());
    }

    [TestMethod]
    [TestCategory("Integration")]
    public void ChangeScene_SetsNewScene()
    {
        var scene = new TestableScene();
        _core!.ChangeScene(scene);

        // Scene change happens on next update
        Assert.AreNotEqual(scene, _core.ActiveScene);

        _core.RunOneFrame();

        Assert.AreEqual(scene, _core.ActiveScene);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public void ChangeScene_DisposesOldScene()
    {
        var oldScene = new TestableScene();
        var newScene = new TestableScene();
        _core!.ActiveScene = oldScene;

        _core.ChangeScene(newScene);

        _core.RunOneFrame();

        Assert.IsTrue(oldScene.IsDisposed);
        Assert.IsFalse(newScene.IsDisposed);
        Assert.AreEqual(newScene, _core.ActiveScene);
        Assert.IsNull(_core.NextScene);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public void Update_UpdatesActiveScene()
    {
        var sceneMock = new Mock<TestableScene>();
        sceneMock.Setup(s => s.Update(It.IsAny<GameTime>()));

        _core!.ActiveScene = sceneMock.Object;

        _core.RunOneFrame();

        sceneMock.Verify(s => s.Update(It.IsAny<GameTime>()), Times.Once);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public void Draw_DrawsActiveScene()
    {
        var sceneMock = new Mock<TestableScene>();
        sceneMock.Setup(s => s.Draw(It.IsAny<GameTime>()));

        _core!.ActiveScene = sceneMock.Object;

        _core.RunOneFrame();

        sceneMock.Verify(s => s.Draw(It.IsAny<GameTime>()), Times.Once);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void ChangeScene_SetsNextSceneIfDifferent()
    {
        var scene1 = new TestableScene();
        var scene2 = new TestableScene();
        _core!.ActiveScene = scene1;

        _core.ChangeScene(scene2);

        Assert.AreEqual(scene2, _core.NextScene);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void ChangeScene_DoesNotSetNextSceneIfSame()
    {
        var scene1 = new TestableScene();
        _core!.ActiveScene = scene1;

        _core.ChangeScene(scene1);

        Assert.IsNull(_core.NextScene);
    }
}