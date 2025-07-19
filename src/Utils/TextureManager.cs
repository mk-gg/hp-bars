using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

namespace HPBars.src.Utils
{
    public static class TextureManager
    {
        private static Texture2D _pixel;
        private static Texture2D _border;

        public static Texture2D Pixel => _pixel ??= CreatePixelTexture();
        public static Texture2D Border => _border;

        public static void LoadTextures(IModHelper helper)
        {
            _border = helper.ModContent.Load<Texture2D>("assets/textures/border.png");
        }

        public static void UnloadTextures()
        {
            _pixel?.Dispose();
            _pixel = null;
        }

        private static Texture2D CreatePixelTexture()
        {
            var texture = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });
            return texture;
        }

        public static Color GetBarColor()
        {
            return new Color(172, 50, 50, 255);
        }

        public static Color GetBorderColor()
        {
            return Color.White;
        }
    }
}
