namespace AthameRPG.GameEngine.Loaders
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public static class FontLoader
    {
        private const string ArialBigSizePath = @"../Content/Fonts/ArialBig";
        private const string SmallSizeLetterPath = "../Content/Fonts/SmallLetters";
        private const string MediumSizeLetterPath = "../Content/Fonts/Comic Sans MS";
        
        public static SpriteFont SmallSizeFont { get; private set; }
        public static SpriteFont BigSizeFont { get; private set; }
        public static SpriteFont MediumSizeLetter { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            BigSizeFont = content.Load<SpriteFont>(ArialBigSizePath);
            SmallSizeFont = content.Load<SpriteFont>(SmallSizeLetterPath);
            MediumSizeLetter = content.Load<SpriteFont>(MediumSizeLetterPath);
        }
    }
}
