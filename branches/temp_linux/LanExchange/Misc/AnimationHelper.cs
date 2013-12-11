using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using LanExchange.SDK;

namespace LanExchange.Misc
{
    public class AnimationHelper
    {
        /// <summary>
        /// 100ms delay between stages.
        /// </summary>
        public const int DELAY = 250;

        public const string WORKING = "working";

        private static readonly IDictionary<string, int> s_Animations = new Dictionary<string, int>();
        private readonly int m_NumStages;
        private int m_Stage;

        /// <summary>
        /// Splits picture onto small images and register its in App.Images.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="picture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void Register(string prefix, Bitmap picture, int width, int height)
        {
            if (App.Images == null) return;
            var cols = picture.Width/width;
            var rows = picture.Height/height;
            s_Animations.Add(prefix, cols * rows);
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                {
                    var rect = new Rectangle(col*width, row*height, width, height);
                    var cropped = picture.Clone(rect, picture.PixelFormat);
                    var index = row * cols + col;
                    App.Images.RegisterImage(prefix+index.ToString(CultureInfo.InvariantCulture), cropped, null);
                }
        }

        public AnimationHelper(string prefix)
        {
            Prefix = prefix;
            int count;
            m_NumStages = s_Animations.TryGetValue(prefix, out count) ? count : 0;
            m_Stage = 0;
        }

        public string Prefix { get; private set; }

        public string GetNextImageName()
        {
            m_Stage = (m_Stage + 1) % m_NumStages;
            if (m_Stage == 0) m_Stage++;
            return Prefix + m_Stage.ToString();
        }
    }
}
