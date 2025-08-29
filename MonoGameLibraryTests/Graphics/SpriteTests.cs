using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace MonoGameLibraryTests.Graphics
{
    [TestClass()]
    public class SpriteTests
    {
        [TestMethod()]
        public void Sprite_DefaultValues_AreCorrect()
        {
            var sprite = new Sprite();

            Assert.AreEqual(Color.White, sprite.Color);
            Assert.AreEqual(0.0f, sprite.Rotation);
            Assert.AreEqual(Vector2.One, sprite.Scale);
            Assert.AreEqual(Vector2.Zero, sprite.Origin);
            Assert.AreEqual(SpriteEffects.None, sprite.Effects);
            Assert.AreEqual(0.0f, sprite.LayerDepth);
        }

        [TestMethod()]
        public void Sprite_CopyConstructor_CopiesProperties()
        {
            var region = new TextureRegion();
            var original = new Sprite(region)
            {
                Color = Color.Red,
                Rotation = 1.5f,
                Scale = new Vector2(2, 2),
                Origin = new Vector2(1, 1),
                Effects = SpriteEffects.FlipHorizontally,
                LayerDepth = 0.5f
            };

            var copy = new Sprite(original);

            Assert.AreEqual(original.Region, copy.Region);
            Assert.AreEqual(original.Color, copy.Color);
            Assert.AreEqual(original.Rotation, copy.Rotation);
            Assert.AreEqual(original.Scale, copy.Scale);
            Assert.AreEqual(original.Origin, copy.Origin);
            Assert.AreEqual(original.Effects, copy.Effects);
            Assert.AreEqual(original.LayerDepth, copy.LayerDepth);
        }
    }
}