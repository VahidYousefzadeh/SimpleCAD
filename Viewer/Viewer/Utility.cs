using System;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public static class Utility
    {
        public static double RandomDouble(double minimum, double maximum)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static Brush RandomBrush()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            Color color = Color.FromArgb(
                (byte)random.Next(256),
                (byte)random.Next(256),
                (byte)random.Next(256),
                (byte)random.Next(256));

            var brush = new SolidColorBrush(color);
            return Freeze(brush);

        }

        public static T Freeze<T>(T freezable) where T : Freezable
        {
            if (freezable.CanFreeze)
                freezable.Freeze();

            return freezable;
        }


    }
}