using System;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public sealed class RandomShapeGenerator
    {
        private readonly double m_screenWidth;
        private readonly double m_screenHeight;
        private readonly Random m_random;
        public RandomShapeGenerator(Random random, double screenWidth, double screenHeight)
        {
            m_random = random;
            m_screenWidth = screenWidth;
            m_screenHeight = screenHeight;
        }

        public Shape Generate()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            int type = random.Next(0, 2);

            switch (type)
            {
                case 0:
                    return RandomLine();
                case 1:
                    return RandomCircle();
                default:
                    return RandomLine();
            }
        }

        private Shape RandomLine()
        {
            double x1 = Utility.RandomDouble(m_random, 0d, m_screenWidth);
            double x2 = Utility.RandomDouble(m_random, 0d, m_screenWidth);
            double y1 = Utility.RandomDouble(m_random, 0d, m_screenHeight);
            double y2 = Utility.RandomDouble(m_random, 0d, m_screenHeight);

            return new Line(RandomPen(m_random), new Point(x1, y1), new Point(x2, y2));
        }

        private Shape RandomCircle()
        {
            double x = Utility.RandomDouble(m_random, 0d, m_screenWidth);
            double y = Utility.RandomDouble(m_random, 0d, m_screenHeight);
            double radius = Utility.RandomDouble(m_random, 10d, 100d);

            return new Circle(RandomBoolean(m_random) ? Utility.RandomBrush(m_random) : null, RandomPen(m_random), new Point(x, y), radius);
        }

        private static Pen RandomPen(Random random)
        {
            return Utility.Freeze(new Pen(Utility.RandomBrush(random), 3d) { DashStyle = RandomDashStyle() });
        }

        private static bool RandomBoolean(Random random)
        {
            return random.Next(0, 1) != 0;
        }

        private static DashStyle RandomDashStyle()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            switch (random.Next(0, 4))
            {
                case 0:
                    return DashStyles.DashDot;
                case 1:
                    return DashStyles.Dash;
                case 2:
                    return DashStyles.DashDotDot;
                case 3:
                    return DashStyles.Dot;
                case 4:
                    return DashStyles.Solid;
                default:
                    return DashStyles.Solid;
            }
        }

    }
}