using System;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public static class Utility
    {
        public static double RandomDouble(Random random, double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static Brush RandomBrush(Random random)
        {
            Color color = Color.FromArgb(
                (byte)random.Next(130, 256),
                (byte)random.Next(130, 256),
                (byte)random.Next(130, 256),
                (byte)random.Next(130, 256));

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