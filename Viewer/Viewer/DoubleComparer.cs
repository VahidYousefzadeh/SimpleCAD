using System.Collections.Generic;

namespace Viewer
{
    public class DoubleComparer : IEqualityComparer<double>
    {
        private readonly double m_epsilon;
        public DoubleComparer(double epsilon = MathHelper.Epsilon)
        {
            m_epsilon = epsilon;
        }

        public bool Equals(double a, double b)
        {
            return a.AlmostEquals(b, m_epsilon);
        }

        public int GetHashCode(double d)
        {
            return 42;
        }
    }
}