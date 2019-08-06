using System.Collections.Generic;
using System.Windows;

namespace Viewer.Geometry
{
    public sealed class PointComparer : IEqualityComparer<Point>
    {
        private readonly double m_epsilon;
        public PointComparer(double epsilon = MathHelper.Epsilon)
        {
            m_epsilon = epsilon;
        }

        public bool Equals(Point a, Point b)
        {

            return a.X.AlmostEquals(b.X, m_epsilon) &&
                   a.Y.AlmostEquals(b.Y, m_epsilon);
        }

        public int GetHashCode(Point obj)
        {
            return 42;
        }
    }
}