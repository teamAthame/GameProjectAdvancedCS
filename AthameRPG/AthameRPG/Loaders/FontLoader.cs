using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Loaders
{
    public static class FontLoader
    {
        private const string ArialBigSizePath = @"../Content/Fonts/ArialBig";
        private const string SmallSizeLetterPath = "../Content/Fonts/SmallLetters";

        public static SpriteFont SmallSizeFont { get; private set; }
        public static SpriteFont BigSizeFont { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            //this.Content = new ContentManager(Content.ServiceProvider, "Content");
            BigSizeFont = content.Load<SpriteFont>(ArialBigSizePath);
            SmallSizeFont = content.Load<SpriteFont>(SmallSizeLetterPath);
        }
    }
}
