using System;

namespace Viewer.Geometry
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
    }
}