﻿using System;
using System.ComponentModel;
using System.Drawing;

namespace LanExchange.Plugin.WinForms.Utils
{
    public static class BitmapUtils
    {
        public static Bitmap MadeNewBitmap(Bitmap bmp, int deltaHue)
        {
            var result = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    var oldColor = bmp.GetPixel(x, y);
                    var newHue = oldColor.GetHue() + deltaHue;
                    if (newHue > 360)
                        newHue = newHue - 360;
                    var newColor = FromAhsb(oldColor.A, newHue, oldColor.GetSaturation(), oldColor.GetBrightness());
                    result.SetPixel(x, y, newColor);
                }
            return result;
        }

        /// <summary>
        /// Creates a Color from alpha, hue, saturation and brightness.
        /// </summary>
        /// <param name="alpha">The alpha channel value.</param>
        /// <param name="hue">The hue value.</param>
        /// <param name="saturation">The saturation value.</param>
        /// <param name="brightness">The brightness value.</param>
        /// <returns>A Color with the given values.</returns>
        [Localizable(false)]
        public static Color FromAhsb(int alpha, float hue, float saturation, float brightness)
        {

            if (0 > alpha || 255 < alpha)
            {
                throw new ArgumentOutOfRangeException("alpha", alpha,
                  "Value must be within a range of 0 - 255.");
            }
            if (0f > hue || 360f < hue)
            {
                throw new ArgumentOutOfRangeException("hue", hue,
                  "Value must be within a range of 0 - 360.");
            }
            if (0f > saturation || 1f < saturation)
            {
                throw new ArgumentOutOfRangeException("saturation", saturation,
                  "Value must be within a range of 0 - 1.");
            }
            if (0f > brightness || 1f < brightness)
            {
                throw new ArgumentOutOfRangeException("brightness", brightness,
                  "Value must be within a range of 0 - 1.");
            }

            if (saturation < 1E-10) // is equal zero?
            {
                return Color.FromArgb(alpha, Convert.ToInt32(brightness * 255),
                  Convert.ToInt32(brightness * 255), Convert.ToInt32(brightness * 255));
            }

            float fMax, fMid, fMin;

            if (0.5 < brightness)
            {
                fMax = brightness - (brightness * saturation) + saturation;
                fMin = brightness + (brightness * saturation) - saturation;
            }
            else
            {
                fMax = brightness + (brightness * saturation);
                fMin = brightness - (brightness * saturation);
            }

            var iSextant = (int)Math.Floor(hue / 60f);
            if (300f <= hue)
            {
                hue -= 360f;
            }
            hue /= 60f;
            hue -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);
            if (0 == iSextant % 2)
            {
                fMid = hue * (fMax - fMin) + fMin;
            }
            else
            {
                fMid = fMin - hue * (fMax - fMin);
            }

            var iMax = Convert.ToInt32(fMax * 255);
            var iMid = Convert.ToInt32(fMid * 255);
            var iMin = Convert.ToInt32(fMin * 255);

            switch (iSextant)
            {
                case 1:
                    return Color.FromArgb(alpha, iMid, iMax, iMin);
                case 2:
                    return Color.FromArgb(alpha, iMin, iMax, iMid);
                case 3:
                    return Color.FromArgb(alpha, iMin, iMid, iMax);
                case 4:
                    return Color.FromArgb(alpha, iMid, iMin, iMax);
                case 5:
                    return Color.FromArgb(alpha, iMax, iMin, iMid);
                default:
                    return Color.FromArgb(alpha, iMax, iMid, iMin);
            }
        }

    }
}