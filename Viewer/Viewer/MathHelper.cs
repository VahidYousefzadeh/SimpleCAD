using System;
using System.Windows;

namespace Viewer
{
    public static class MathHelper
    {
        /// <summary>
        /// Zero tolerance constant.
        /// </summary>
        public const double Epsilon = 1.0e-9;

        /// <summary>
        /// Compare two doubles for equality within a tolerance.
        /// </summary>
        public static bool AlmostEquals(this double a, double b, double epsilon = Epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        public static double DistanceTo(this Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}