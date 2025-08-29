using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using Moq;

namespace MonoGameLibraryTests.Graphics
{
    [TestClass]
    public class TextureRegionTests
    {
        // Helper to create a valid GraphicsDevice for tests
        private static GraphicsDevice CreateTestGraphicsDevice()
        {
            var pp = new PresentationParameters
            {
                BackBufferWidth = 1,
                BackBufferHeight = 1,
                DeviceWindowHandle = IntPtr.Zero
            };

            // Use Moq to create a mock GraphicsAdapter
            var adapterMock = new Mock<GraphicsAdapter>(MockBehavior.Strict);
            var adapter = adapterMock.Object;

            var featureLevel = GraphicsProfile.HiDef;
            return new GraphicsDevice(adapter, featureLevel, pp);
        }

        [TestMethod]
        public void DefaultConstructor_InitializesProperties()
        {
            var region = new TextureRegion();
            Assert.IsNull(region.Texture);
            Assert.AreEqual(default(Rectangle), region.SourceRectangle);
        }

        [TestMethod]
        public void Constructor_WithTextureAndRectangle_SetsProperties()
        {
            var graphicsDevice = CreateTestGraphicsDevice();
            var texture = new Mock<Texture2D>(MockBehavior.Strict, graphicsDevice, 10, 10, false, SurfaceFormat.Color).Object;
            var rect = new Rectangle(1, 2, 3, 4);

            var region = new TextureRegion(texture, rect);

            Assert.AreEqual(texture, region.Texture);
            Assert.AreEqual(rect, region.SourceRectangle);
        }

        [TestMethod]
        public void Constructor_WithTextureAndCoordinates_SetsProperties()
        {
            var graphicsDevice = CreateTestGraphicsDevice();
            var texture = new Mock<Texture2D>(MockBehavior.Strict, graphicsDevice, 10, 10, false, SurfaceFormat.Color).Object;
            int x = 5, y = 6, w = 7, h = 8;

            var region = new TextureRegion(texture, x, y, w, h);

            Assert.AreEqual(texture, region.Texture);
            Assert.AreEqual(new Rectangle(x, y, w, h), region.SourceRectangle);
        }

        [TestMethod]
        public void Width_And_Height_Properties_ReturnCorrectValues()
        {
            var region = new TextureRegion
            {
                SourceRectangle = new Rectangle(0, 0, 123, 456)
            };

            Assert.AreEqual(123, region.Width);
            Assert.AreEqual(456, region.Height);
        }

        [TestMethod]
        public void Draw_Overload1_CallsDrawWithDefaultParameters()
        {
            var graphicsDevice = CreateTestGraphicsDevice();
            var texture = new Mock<Texture2D>(MockBehavior.Strict, graphicsDevice, 10, 10, false, SurfaceFormat.Color).Object;
            var spriteBatchMock = new Mock<SpriteBatch>(MockBehavior.Strict, graphicsDevice);
            var region = new TextureRegion(texture, 1, 2, 3, 4);

            spriteBatchMock
                .Setup(sb => sb.Draw(
                    texture,
                    It.IsAny<Vector2>(),
                    region.SourceRectangle,
                    It.IsAny<Color>(),
                    0.0f,
                    Vector2.Zero,
                    Vector2.One,
                    SpriteEffects.None,
                    0.0f
                ));

            region.Draw(spriteBatchMock.Object, new Vector2(10, 20), Color.Red);

            spriteBatchMock.Verify(sb => sb.Draw(
                texture,
                new Vector2(10, 20),
                region.SourceRectangle,
                Color.Red,
                0.0f,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.None,
                0.0f
            ), Times.Once);
        }

        [TestMethod]
        public void Draw_Overload2_CallsDrawWithScaleAsVector2()
        {
            var graphicsDevice = CreateTestGraphicsDevice();
            var texture = new Mock<Texture2D>(MockBehavior.Strict, graphicsDevice, 10, 10, false, SurfaceFormat.Color).Object;
            var spriteBatchMock = new Mock<SpriteBatch>(MockBehavior.Strict, graphicsDevice);
            var region = new TextureRegion(texture, 1, 2, 3, 4);

            float scale = 2.5f;
            spriteBatchMock
                .Setup(sb => sb.Draw(
                    texture,
                    It.IsAny<Vector2>(),
                    region.SourceRectangle,
                    It.IsAny<Color>(),
                    It.IsAny<float>(),
                    It.IsAny<Vector2>(),
                    new Vector2(scale, scale),
                    It.IsAny<SpriteEffects>(),
                    It.IsAny<float>()
                ));

            region.Draw(
                spriteBatchMock.Object,
                new Vector2(10, 20),
                Color.Blue,
                1.0f,
                new Vector2(1, 1),
                scale,
                SpriteEffects.FlipHorizontally,
                0.5f
            );

            spriteBatchMock.Verify(sb => sb.Draw(
                texture,
                new Vector2(10, 20),
                region.SourceRectangle,
                Color.Blue,
                1.0f,
                new Vector2(1, 1),
                new Vector2(scale, scale),
                SpriteEffects.FlipHorizontally,
                0.5f
            ), Times.Once);
        }

        [TestMethod]
        public void Draw_Overload3_CallsSpriteBatchDrawWithAllParameters()
        {
            var graphicsDevice = CreateTestGraphicsDevice();
            var texture = new Mock<Texture2D>(MockBehavior.Strict, graphicsDevice, 10, 10, false, SurfaceFormat.Color).Object;
            var spriteBatchMock = new Mock<SpriteBatch>(MockBehavior.Strict, graphicsDevice);
            var region = new TextureRegion(texture, 1, 2, 3, 4);

            var position = new Vector2(5, 6);
            var color = Color.Green;
            float rotation = 0.75f;
            var origin = new Vector2(2, 3);
            var scale = new Vector2(1.5f, 2.5f);
            var effects = SpriteEffects.FlipVertically;
            float layerDepth = 0.9f;

            spriteBatchMock
                .Setup(sb => sb.Draw(
                    texture,
                    position,
                    region.SourceRectangle,
                    color,
                    rotation,
                    origin,
                    scale,
                    effects,
                    layerDepth
                ));

            region.Draw(spriteBatchMock.Object, position, color, rotation, origin, scale, effects, layerDepth);

            spriteBatchMock.Verify(sb => sb.Draw(
                texture,
                position,
                region.SourceRectangle,
                color,
                rotation,
                origin,
                scale,
                effects,
                layerDepth
            ), Times.Once);
        }
    }
}