using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using MonoGameLibrary.Graphics;

namespace MonoGameLibraryTests;

public class TestableCore : Core
{
    private static GraphicsDevice? _testGraphicsDevice;

    public TestableCore(string title, int width, int height, bool fullScreen)
        : base(title, width, height, fullScreen) { }

    public new static void Initialize()
    {
        if (_testGraphicsDevice == null)
        {
            // Create a minimal Game to get a GraphicsDevice
            using var game = new Game();
            var graphicsManager = new GraphicsDeviceManager(game);
            game.RunOneFrame();
            _testGraphicsDevice = graphicsManager.GraphicsDevice;
        }

        GraphicsDevice = _testGraphicsDevice!;
        SpriteBatch = new SpriteBatch(_testGraphicsDevice!);
        Input = new InputManager();
    }

    public new void Update(GameTime gameTime) => base.Update(gameTime);

    public new void Draw(GameTime gameTime) => base.Draw(gameTime);
}
