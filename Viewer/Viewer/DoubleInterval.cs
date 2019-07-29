namespace Viewer
{
    public sealed class DoubleInterval
    {
        private readonly double m_a;
        private readonly double m_b;

        public DoubleInterval(double a, double b)
        {
            m_a = a;
            m_b = b;
        }

        public bool InRange(double value, double epsilon = MathHelper.Epsilon)
        {
            return m_a - epsilon <= value && value <= m_b + epsilon;
        }

        public bool Intersects(DoubleInterval other, double epsilon = MathHelper.Epsilon)
        {
            return InRange(other.m_a, epsilon) || 
                   InRange(other.m_b) || 
                   other.InRange(m_a, epsilon) ||
                   other.InRange(m_b, epsilon);
        }

        public bool Contains(DoubleInterval other, double epsilon = MathHelper.Epsilon)
        {
            return InRange(other.m_a , epsilon) && InRange(other.m_b, epsilon);
        }
    }
}