using System.Collections.Generic;
using System.Windows;

namespace Viewer.Geometry
{
    public sealed class PointComparer : IEqualityComparer<Point>
    {
        private readonly double m_epsilon;

        public PointComparer()
        {
            m_epsilon = MathHelper.Epsilon;
        }

        public PointComparer(double epsilon)
        {
            m_epsilon = epsilon;
        }

        public bool Equals(Point x, Point y)
        {
            return x.X.AlmostEquals(y.X, m_epsilon) &&
                   x.Y.AlmostEquals(y.Y, m_epsilon);
        }

        public int GetHashCode(Point obj)
        {
            return 42;
        }
    }
}