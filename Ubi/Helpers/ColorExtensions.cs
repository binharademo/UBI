using System;
using Xamarin.Forms;
using UXDivers.Grial;

namespace Xamarin.Forms
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
    }
}
