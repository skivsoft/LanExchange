using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Misc
{
    public class AnimationHelper
    {
        /// <summary>
        /// 100ms delay between stages.
        /// </summary>
        public const int DELAY = 250;

        public const string WORKING = "working";

        private static readonly IDictionary<string, int> animations = new Dictionary<string, int>();
        private readonly int numStages;
        private int currentStage;

        /// <summary>
        /// Splits picture onto small images and register its in App.Images.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="picture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void Register(IImageManager imageManager, string prefix, Bitmap picture, int width, int height)
        {
            if (imageManager == null || picture == null) return;
            var cols = picture.Width/width;
            var rows = picture.Height/height;
            animations.Add(prefix, cols * rows);
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                {
                    var rect = new Rectangle(col*width, row*height, width, height);
                    var cropped = picture.Clone(rect, picture.PixelFormat);
                    var index = row * cols + col;
                    imageManager.RegisterImage(prefix+index.ToString(CultureInfo.InvariantCulture), cropped, null);
                }
        }

        public AnimationHelper(string prefix)
        {
            Prefix = prefix;
            int count;
            numStages = animations.TryGetValue(prefix, out count) ? count : 0;
            currentStage = 0;
        }

        public string Prefix { get; }

        public string GetNextImageName()
        {
            currentStage = (currentStage + 1) % numStages;
            if (currentStage == 0) currentStage++;
            return Prefix + currentStage.ToString(CultureInfo.InvariantCulture);
        }
    }
}
